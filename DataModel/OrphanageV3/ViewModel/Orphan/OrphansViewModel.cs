﻿using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Orphan
{
    public class OrphansViewModel
    {
        private readonly IApiClient _apiClient;
        private readonly IMapperService _mapperService;
        private readonly ITranslateService _translateService;
        private readonly IDataFormatterService _dataFormatterService;

        public delegate void OrphansChnagedDelegate();
        public delegate void OrphanChnagedDelegate(int Oid, Image newPhoto);
        public event OrphansChnagedDelegate OrphansChangedEvent;
        public event OrphanChnagedDelegate PhotoLoadedEvent;
        public IEnumerable<OrphanModel> Orphans { get; set; }
        private IEnumerable<OrphanageV3.Services.Orphan> _SourceOrphans;
        private Size PhotoSize = new Size(75, 75);
        private int PhotoCompressRatio = 70;

        public OrphansViewModel(IApiClient apiClient, IMapperService mapperService, ITranslateService translateService, IDataFormatterService dataFormatterService)
        {
            _apiClient = apiClient;
            _mapperService = mapperService;
            _translateService = translateService;
            _dataFormatterService = dataFormatterService;
        }
        public void LoadData()
        {
            GetOrphans();
        }

        private async void GetOrphans()
        {
            var Ocounts = await _apiClient.OrphansController_GetOrphansCountAsync();
            var ReturnedOrphans = await _apiClient.OrphansController_GetAllAsync(Ocounts, 0);
            //delete excluded orphans
            if (Properties.Settings.Default.ShowHiddenRows)
                _SourceOrphans = ReturnedOrphans;
            else
                _SourceOrphans = ReturnedOrphans.Where(o => o.IsExcluded == false || !o.IsExcluded.HasValue).ToList();
            Orphans = _mapperService.MapToOrphanModel(_SourceOrphans);
            OrphansChangedEvent?.Invoke();
            //get first page orphan ids
            var ids = Orphans.Take(30).Select(op => op.ID).ToList();
            //Start Image thread after data loading
            await LoadImages(ids);
        }

        public async Task LoadImages(IList<int> lst)
        {
            foreach (var id in lst)
            {
                try
                {
                    var orp = Orphans.FirstOrDefault(o => o.ID == id);
                    var img = await _apiClient.GetImage(orp.FacePhotoURI, PhotoSize, PhotoCompressRatio);
                    if (img == null)
                    {
                        img = _translateService.IsBoy(orp.Gender) ? new Bitmap(Properties.Resources.UnknownBoyPic, PhotoSize) : new Bitmap(Properties.Resources.UnknownGirlPic, PhotoSize);
                    }
                    PhotoLoadedEvent?.Invoke(orp.ID, img);
                }
                catch { }
            }
        }

        public async Task<Image> GetOrphanFacePhoto(int id)
        {
            try
            {
                var orp = Orphans.FirstOrDefault(o => o.ID == id);
                if (orp == null) return null;
                string url = orp.FacePhotoURI;
                if (url != null)
                {
                    var img = await _apiClient.GetImage(url);
                    if (img == null)
                    {
                        img = _translateService.IsBoy(orp.Gender) ? Properties.Resources.UnknownBoyPic : Properties.Resources.UnknownGirlPic;
                    }
                    return img;
                }
                return null;
            }
            catch { return null; }
        }

        public async Task<bool> DeleteOrphan(int Oid)
        {
            try
            {
                await _apiClient.OrphansController_DeleteAsync(Oid);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ExcludeOrphan(int Oid)
        {
            try
            {
                var orphan = _SourceOrphans.FirstOrDefault(o => o.Id == Oid);
                orphan.IsExcluded = true;
                await _apiClient.OrphansController_PutAsync(orphan);
                return true;
            }
            catch (ApiClientException apiEx)
            {
                if (apiEx.StatusCode == "304")
                    return true;
                return false;
            }
        }

        public async Task<long?> SetColor(int Oid, long? colorValue)
        {
            long? returnedColor = null;
            try
            {
                var orphan = _SourceOrphans.FirstOrDefault(o => o.Id == Oid);
                returnedColor = orphan.ColorMark;
                if (colorValue != Color.White.ToArgb() && colorValue != Color.Black.ToArgb())
                    orphan.ColorMark = colorValue;
                else
                    orphan.ColorMark = null;
                await _apiClient.OrphansController_PutAsync(orphan);
                return orphan.ColorMark;
            }
            catch (ApiClientException apiEx)
            {
                if (apiEx.StatusCode == "304")
                    return returnedColor;
                return null;
            }
        }

        public async Task<string> GetOrphanSummary(int Oid)
        {
            var orp = _SourceOrphans.FirstOrDefault(o => o.Id == Oid);
            if (orp == null)
                return string.Empty;
            var brothersTask = _apiClient.OrphansController_GetBrothersAsync(Oid);
            Task<Bail> bailTask = null;
            Task<Bail> FamilyBailTask = null;
            if (orp.IsBailed )
                bailTask = _apiClient.BailsController_GetAsync(orp.BailId.Value);
            if (orp.Family.IsBailed)
                FamilyBailTask = _apiClient.BailsController_GetAsync(orp.Family.BailId.Value);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(Properties.Resources.FatherName + ": " + _dataFormatterService.GetFullNameString(orp.Family.Father.Name));
            stringBuilder.AppendLine(Properties.Resources.FatherDeathDate + ": " + _dataFormatterService.GetFormattedDate(orp.Family.Father.DateOfDeath));
            stringBuilder.AppendLine(Properties.Resources.MotherFirstName + ": " + _dataFormatterService.GetFullNameString(orp.Family.Mother.Name));
            stringBuilder.AppendLine(Properties.Resources.MotherIsDead + ": " + _translateService.BooleanToString(orp.Family.Mother.IsDead));
            if (orp.Family.Mother.IsDead && orp.Family.Mother.DateOfDeath.HasValue)
                stringBuilder.AppendLine(Properties.Resources.MotherDeathDate + ": " + _dataFormatterService.GetFormattedDate(orp.Family.Mother.DateOfDeath.Value));
            stringBuilder.AppendLine(Properties.Resources.MotherIsMarried + ": " + _translateService.BooleanToString(orp.Family.Mother.IsMarried));
            if (orp.Family.Mother.IsMarried && orp.Family.Mother.HusbandName != null && orp.Family.Mother.HusbandName.Length > 0)
                stringBuilder.AppendLine(Properties.Resources.MotherHusbandName + ": " + orp.Family.Mother.HusbandName);
            if (orp.Age.HasValue)
                stringBuilder.AppendLine(Properties.Resources.Age + ": " + _translateService.DateToString(orp.Birthday));
            var brothers = await brothersTask;
            int boys = brothers.Count(o => _translateService.IsBoy(o.Gender));
            int girls = brothers.Count(o => !_translateService.IsBoy(o.Gender));
            stringBuilder.AppendLine(Properties.Resources.BrothersCountString + ": " + boys + " " + Properties.Resources.MalesString + ", " + girls + " " + Properties.Resources.FemalesString);

            if (bailTask != null || FamilyBailTask !=null)
            {
                stringBuilder.AppendLine(Properties.Resources.IsBailed + ": " + Properties.Resources.BooleanTrue);
                Bail orpBail = null;
                if (bailTask != null)
                {
                    orpBail = await bailTask;
                }
                if(FamilyBailTask != null)
                {
                    orpBail = await FamilyBailTask;
                }
                if (orpBail != null)
                {
                    stringBuilder.AppendLine(Properties.Resources.GuarantorName + ": " + _dataFormatterService.GetFullNameString(orpBail?.Guarantor?.Name));
                    stringBuilder.AppendLine(Properties.Resources.BailIsFamily + ": " + _translateService.BooleanToString(orpBail.IsFamilyBail));
                    stringBuilder.AppendLine(Properties.Resources.BailAmount + ": " + orpBail.Amount.ToString() + " " + orpBail?.Account.CurrencyShortcut);
                    stringBuilder.AppendLine(Properties.Resources.BailIsMonthly + ": " + _translateService.BooleanToString(orpBail.IsMonthlyBail));
                    stringBuilder.AppendLine(Properties.Resources.BailIsEnded + ": " + _translateService.BooleanToString(orpBail.IsExpired));
                }
            }
            else
            {
                stringBuilder.AppendLine(Properties.Resources.IsBailed + ": " + Properties.Resources.BooleanFalse);
            }

            if(orp.EducationId.HasValue && orp.Education != null)
            {
                if (orp.Education.Stage.Contains(Properties.Resources.EducationNonStudyKeyword))
                {
                    if (orp.Education.Reasons != null && orp.Education.Reasons.Length > 0)
                    {
                        stringBuilder.AppendLine(Properties.Resources.IsStudying + ": " + Properties.Resources.BooleanFalse);
                        stringBuilder.AppendLine(Properties.Resources.EducationNonStudyingReasons + ": " + orp.Education.Reasons);
                    }
                }
                else
                {
                    stringBuilder.AppendLine(Properties.Resources.IsStudying + ": " + Properties.Resources.BooleanTrue);
                    if (orp.Education.Stage != null && orp.Education.Stage.Length > 0)
                    {
                        stringBuilder.AppendLine(Properties.Resources.EducationStage + ": " + orp.Education.Stage);
                    }
                    if (orp.Education.DegreesRate.HasValue)
                    {
                        stringBuilder.AppendLine(Properties.Resources.EducationAvaregeGrade + ": " + orp.Education.DegreesRate.Value + "%");
                    }
                    if (orp.Education.School != null && orp.Education.School.Length > 0)
                    {
                        stringBuilder.AppendLine(Properties.Resources.EducationSchoolName + ": " + orp.Education.School);
                    }
                }                
            }
            else
            {
                stringBuilder.AppendLine(Properties.Resources.IsStudying + ": " + Properties.Resources.BooleanFalse);
            }
            if (orp.HealthId.HasValue && orp.HealthStatus != null)
            {
                stringBuilder.AppendLine(Properties.Resources.IsSick + ": " + Properties.Resources.BooleanTrue);
                if(orp.HealthStatus.SicknessName != null && orp.HealthStatus.SicknessName.Length >0)
                {
                    stringBuilder.AppendLine(Properties.Resources.HealthSicknessName + ": " + orp.HealthStatus.SicknessName);
                }
                if (orp.HealthStatus.Medicine != null && orp.HealthStatus.Medicine.Length > 0)
                {
                    stringBuilder.AppendLine(Properties.Resources.HealthMedicen + ": " + orp.HealthStatus.Medicine);
                }
                if (orp.HealthStatus.Cost.HasValue)
                {
                    stringBuilder.AppendLine(Properties.Resources.Cost + ": " + orp.HealthStatus.Cost.Value);
                }
            }
            else
            {
                stringBuilder.AppendLine(Properties.Resources.IsSick + ": " + Properties.Resources.BooleanFalse);
            }
            return stringBuilder.ToString();
        }
    }
}
