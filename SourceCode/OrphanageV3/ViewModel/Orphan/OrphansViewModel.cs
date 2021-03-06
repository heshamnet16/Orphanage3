﻿using OrphanageV3.Extensions;
using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Orphan
{
    public class OrphansViewModel
    {
        private readonly IApiClient _apiClient;
        private readonly IMapperService _mapperService;
        private readonly ITranslateService _translateService;
        private readonly IDataFormatterService _dataFormatterService;
        private readonly Bail.BailsViewModel _bailsViewModel;
        private readonly IExceptionHandler _exceptionHandler;

        public IEnumerable<Bail.BailModel> Bails { get; set; }

        public delegate void OrphansChnagedDelegate();

        public delegate void OrphanChnagedDelegate(int Oid, Image newPhoto);

        public event OrphansChnagedDelegate DataLoaded;

        public ObservableCollection<OrphanModel> Orphans { get; set; }
        private IList<OrphanageDataModel.Persons.Orphan> _SourceOrphans;
        private Size PhotoSize = new Size(75, 75);
        private int PhotoCompressRatio = 40;

        public OrphansViewModel(IApiClient apiClient, IMapperService mapperService, ITranslateService translateService, IDataFormatterService dataFormatterService,
            Bail.BailsViewModel bailsViewModel, IExceptionHandler exceptionHandler)
        {
            _apiClient = apiClient;
            _mapperService = mapperService;
            _translateService = translateService;
            _dataFormatterService = dataFormatterService;
            _bailsViewModel = bailsViewModel;
            _exceptionHandler = exceptionHandler;
            _bailsViewModel.DataLoaded += bailsLoaded;
            _bailsViewModel.LoadBailsByIsFamily(false);
        }

        private void bailsLoaded(object sender, System.EventArgs e)
        {
            Bails = _bailsViewModel.Bails;
        }

        public void LoadData()
        {
            GetOrphans();
        }

        public async void LoadData(IEnumerable<int> orphansList)
        {
            var ReturnedOrphans = await _apiClient.Orphans_GetByIdsAsync(orphansList);
            //delete excluded orphans
            if (Properties.Settings.Default.ShowHiddenRows)
                _SourceOrphans = ReturnedOrphans;
            else
                _SourceOrphans = ReturnedOrphans.Where(o => o.IsExcluded == false || !o.IsExcluded.HasValue).ToList();
            Orphans = new ObservableCollection<OrphanModel>(_mapperService.MapToOrphanModel(_SourceOrphans));
            DataLoaded?.Invoke();
        }

        public void LoadData(IEnumerable<OrphanageDataModel.Persons.Orphan> orphansList)
        {
            _SourceOrphans = orphansList.ToList();
            Orphans = new ObservableCollection<OrphanModel>(_mapperService.MapToOrphanModel(_SourceOrphans));
            DataLoaded?.Invoke();
        }

        private async void GetOrphans()
        {
            var Ocounts = await _apiClient.Orphans_GetOrphansCountAsync();
            var ReturnedOrphans = await _apiClient.Orphans_GetAllAsync(Ocounts, 0);
            //delete excluded orphans
            if (Properties.Settings.Default.ShowHiddenRows)
                _SourceOrphans = ReturnedOrphans;
            else
                _SourceOrphans = ReturnedOrphans.Where(o => o.IsExcluded == false || !o.IsExcluded.HasValue).ToList();
            Orphans = new ObservableCollection<OrphanModel>(_mapperService.MapToOrphanModel(_SourceOrphans));
            DataLoaded?.Invoke();
        }

        public async Task LoadImages(IList<int> lst)
        {
            foreach (var id in lst)
            {
                try
                {
                    var orp = Orphans.FirstOrDefault(o => o.Id == id);
                    var img = await _apiClient.GetImage(orp.FacePhotoURI, PhotoSize, PhotoCompressRatio);
                    if (img == null)
                    {
                        img = _translateService.IsBoy(orp.Gender) ? new Bitmap(Properties.Resources.UnknownBoyPic, PhotoSize) : new Bitmap(Properties.Resources.UnknownGirlPic, PhotoSize);
                    }
                    UpdateOrphanPhoto(orp.Id, img);
                }
                catch { }
            }
        }

        public async Task<Image> GetOrphanFacePhoto(int id)
        {
            try
            {
                var orp = Orphans.FirstOrDefault(o => o.Id == id);
                if (orp == null) return null;
                string url = orp.FacePhotoURI;
                if (url != null)
                {
                    var img = await _apiClient.GetImage(url, new Size(300, 300), 80);
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
                await _apiClient.Orphans_DeleteAsync(Oid);
                return true;
            }
            catch (ApiClientException apiEx)
            {
                return _exceptionHandler.HandleApiSaveException(apiEx);
            }
        }

        public async Task<bool> DeleteOrphan(List<int> orphanIds)
        {
            try
            {
                foreach (var orphanId in orphanIds)
                {
                    await DeleteOrphan(orphanId);
                }
                return true;
            }
            catch (ApiClientException apiEx)
            {
                return _exceptionHandler.HandleApiSaveException(apiEx);
            }
        }

        public async Task<bool> ExcludeOrphan(int Oid)
        {
            try
            {
                var orphan = _SourceOrphans.FirstOrDefault(o => o.Id == Oid);
                var orphanModel = Orphans.FirstOrDefault(o => o.Id == Oid);
                await _apiClient.Orphans_SetOrphanExcludeAsync(orphan.Id, true);
                orphanModel.IsExcluded = true;
                orphan.IsExcluded = true;
                return true;
            }
            catch (ApiClientException apiEx)
            {
                return _exceptionHandler.HandleApiSaveException(apiEx);
            }
        }

        public async Task<bool> UnExcludeOrphan(int Oid)
        {
            try
            {
                var orphan = _SourceOrphans.FirstOrDefault(o => o.Id == Oid);
                var orphanModel = Orphans.FirstOrDefault(o => o.Id == Oid);
                await _apiClient.Orphans_SetOrphanExcludeAsync(orphan.Id, false);
                orphan.IsExcluded = false;
                orphanModel.IsExcluded = false;
                return true;
            }
            catch (ApiClientException apiEx)
            {
                return _exceptionHandler.HandleApiSaveException(apiEx);
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
                    orphan.ColorMark = -1;
                await _apiClient.Orphans_SetOrphanColorAsync(orphan.Id, (int)orphan.ColorMark.Value);
                return orphan.ColorMark;
            }
            catch (ApiClientException apiEx)
            {
                _exceptionHandler.HandleApiSaveException(apiEx);
                return returnedColor;
            }
        }

        public async Task<string> GetOrphanSummary(int Oid)
        {
            var orp = _SourceOrphans.FirstOrDefault(o => o.Id == Oid);
            if (orp == null)
                return string.Empty;
            var brothersTask = _apiClient.Orphans_GetBrothersAsync(Oid);
            Task<OrphanageDataModel.FinancialData.Bail> bailTask = null;
            Task<OrphanageDataModel.FinancialData.Bail> FamilyBailTask = null;
            if (orp.IsBailed)
                bailTask = _apiClient.Bails_GetAsync(orp.BailId.Value);
            if (orp.Family.IsBailed)
                FamilyBailTask = _apiClient.Bails_GetAsync(orp.Family.BailId.Value);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(Properties.Resources.FatherName + ": " + orp.Family.Father.Name.FullName());
            stringBuilder.AppendLine(Properties.Resources.FatherDeathDate + ": " + _dataFormatterService.GetFormattedDate(orp.Family.Father.DateOfDeath));
            stringBuilder.AppendLine(Properties.Resources.MotherFirstName + ": " + orp.Family.Mother.Name.FullName());
            stringBuilder.AppendLine(Properties.Resources.MotherIsDead + ": " + _translateService.BooleanToString(orp.Family.Mother.IsDead));
            if (orp.Family.Mother.IsDead && orp.Family.Mother.DateOfDeath.HasValue)
                stringBuilder.AppendLine(Properties.Resources.MotherDeathDate + ": " + _dataFormatterService.GetFormattedDate(orp.Family.Mother.DateOfDeath.Value));
            stringBuilder.AppendLine(Properties.Resources.MotherIsMarried + ": " + _translateService.BooleanToString(orp.Family.Mother.IsMarried));
            if (orp.Family.Mother.IsMarried && orp.Family.Mother.HusbandName != null && orp.Family.Mother.HusbandName.Length > 0)
                stringBuilder.AppendLine(Properties.Resources.MotherHusbandName + ": " + orp.Family.Mother.HusbandName);
            if (orp.Age.HasValue)
                stringBuilder.AppendLine(Properties.Resources.Age + ": " + _translateService.DateToString(orp.Birthday));
            var brothers = await brothersTask;
            if (brothers != null)
            {
                int boys = brothers.Count(o => _translateService.IsBoy(o.Gender));
                int girls = brothers.Count(o => !_translateService.IsBoy(o.Gender));
                stringBuilder.AppendLine(Properties.Resources.BrothersCountString + ": " + boys + " " + Properties.Resources.MalesString + ", " + girls + " " + Properties.Resources.FemalesString);
            }
            else
            {
                stringBuilder.AppendLine(Properties.Resources.BrothersCountString + ": 0 " + Properties.Resources.MalesString + ", 0 " + Properties.Resources.FemalesString);
            }
            if (bailTask != null || FamilyBailTask != null)
            {
                stringBuilder.AppendLine(Properties.Resources.IsBailed + ": " + Properties.Resources.BooleanTrue);
                OrphanageDataModel.FinancialData.Bail orpBail = null;
                if (bailTask != null)
                {
                    orpBail = await bailTask;
                }
                if (FamilyBailTask != null)
                {
                    orpBail = await FamilyBailTask;
                }
                if (orpBail != null)
                {
                    stringBuilder.AppendLine(Properties.Resources.GuarantorName + ": " + orpBail?.Guarantor?.Name.FullName());
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

            if (orp.EducationId.HasValue && orp.Education != null)
            {
                if (orp.Education.Stage.Contains(Properties.Resources.EducationNonStudyKeyword))
                {
                    stringBuilder.AppendLine(Properties.Resources.IsStudying + ": " + Properties.Resources.BooleanFalse);
                    if (orp.Education.Reasons != null && orp.Education.Reasons.Length > 0)
                    {
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
                if (orp.HealthStatus.SicknessName != null && orp.HealthStatus.SicknessName.Length > 0)
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

        public async void UpdateOrphan(int Oid)
        {
            var orp = await _apiClient.Orphans_GetAsync(Oid);
            int orpIndex = _SourceOrphans.IndexOf(_SourceOrphans.FirstOrDefault(o => o.Id == Oid));
            _SourceOrphans[orpIndex] = orp;
            int orpMIndex = Orphans.IndexOf(Orphans.FirstOrDefault(o => o.Id == Oid));
            Orphans[orpMIndex] = _mapperService.MapToOrphanModel(orp);
            await UpdateOrphanPhoto(Oid);
        }

        public void UpdateOrphanPhoto(int Oid, Image img)
        {
            int orpMIndex = Orphans.IndexOf(Orphans.FirstOrDefault(o => o.Id == Oid));
            Orphans[orpMIndex].Photo = img;
        }

        public async Task UpdateOrphanPhoto(int Oid)
        {
            var orp = Orphans.FirstOrDefault(o => o.Id == Oid);
            int orpMIndex = Orphans.IndexOf(Orphans.FirstOrDefault(o => o.Id == Oid));
            Image img = null;
            try
            {
                img = await _apiClient.GetImage(orp.FacePhotoURI, PhotoSize, PhotoCompressRatio);
            }
            catch
            {
                img = null;
            }
            if (img == null)
            {
                img = _translateService.IsBoy(orp.Gender) ? Properties.Resources.UnknownBoyPic : Properties.Resources.UnknownGirlPic;
            }
            Orphans[orpMIndex].Photo = img;
        }

        public async Task UpdateOrphanThumbnail(int Oid)
        {
            var orp = Orphans.FirstOrDefault(o => o.Id == Oid);
            var sourceOrp = _SourceOrphans.FirstOrDefault(o => o.Id == Oid);
            int orpMIndex = Orphans.IndexOf(Orphans.FirstOrDefault(o => o.Id == Oid));
            Image img = null;
            try
            {
                if (sourceOrp.FacePhoto != null && sourceOrp.FacePhoto.Size != Properties.Resources.loading.Size)
                    img = sourceOrp.FacePhoto;
                else
                {
                    byte[] imageData = await _apiClient.GetImageData(orp.FacePhotoURI, PhotoSize, PhotoCompressRatio);
                    sourceOrp.FacePhotoData = imageData;
                    using (var mem = new MemoryStream(imageData))
                    {
                        img = Image.FromStream(mem);
                    }
                }
            }
            catch
            {
                img = null;
            }
            if (img == null)
            {
                img = _translateService.IsBoy(orp.Gender) ? Properties.Resources.UnknownBoyPic : Properties.Resources.UnknownGirlPic;
            }
            Orphans[orpMIndex].Photo = img;
        }

        public async Task<IEnumerable<int>> GetBrothers(int Oid)
        {
            var orphans = await _apiClient.Orphans_GetBrothersAsync(Oid);
            if (orphans != null)
            {
                return orphans.Select(o => o.Id).ToArray();
            }
            else
                return null;
        }

        public int GetMother(int Oid)
        {
            var orphan = _SourceOrphans.FirstOrDefault(o => o.Id == Oid);
            if (orphan != null)
            {
                return orphan.Family.MotherId;
            }
            else
                return -1;
        }

        public IEnumerable<int> GetMothers(IEnumerable<int> Oids)
        {
            foreach (var Oid in Oids)
            {
                var orphan = _SourceOrphans.FirstOrDefault(o => o.Id == Oid);
                if (orphan != null)
                {
                    yield return orphan.Family.MotherId;
                }
                else
                    yield return -1;
            }
        }

        public IEnumerable<int> GetFathers(IEnumerable<int> Oids)
        {
            foreach (var Oid in Oids)
            {
                var orphan = _SourceOrphans.FirstOrDefault(o => o.Id == Oid);
                if (orphan != null)
                {
                    yield return orphan.Family.FatherId;
                }
                else
                    yield return -1;
            }
        }

        public IEnumerable<int> GetFamilies(IEnumerable<int> Oids)
        {
            foreach (var Oid in Oids)
            {
                var orphan = _SourceOrphans.FirstOrDefault(o => o.Id == Oid);
                if (orphan != null)
                {
                    yield return orphan.Family.Id;
                }
                else
                    yield return -1;
            }
        }

        public async void BailOrphans(int bailId, IEnumerable<int> orphansIds)
        {
            if (bailId <= 0) return;
            if (orphansIds == null || orphansIds.Count() == 0) return;
            try
            {
                var ret = await _apiClient.Orphans_SetBailAsync(bailId, orphansIds);
                if (ret)
                {
                    foreach (int orphanId in orphansIds)
                        UpdateOrphan(orphanId);
                }
            }
            catch (ApiClientException apiEx)
            {
                _exceptionHandler.HandleApiSaveException(apiEx);
            }
        }

        public async void UnBailOrphans(IEnumerable<int> orphansIds)
        {
            if (orphansIds == null || orphansIds.Count() == 0) return;
            try
            {
                var ret = await _apiClient.Orphans_SetBailAsync(-1, orphansIds);
                if (ret)
                {
                    foreach (int orphanId in orphansIds)
                        UpdateOrphan(orphanId);
                }
            }
            catch (ApiClientException apiEx)
            {
                _exceptionHandler.HandleApiSaveException(apiEx);
            }
        }
    }
}