using OrphanageV3.Services;
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

        public delegate void OrphansChnagedDelegate();
        public delegate void OrphanChnagedDelegate(int Oid, Image newPhoto);
        public event OrphansChnagedDelegate OrphansChangedEvent;
        public event OrphanChnagedDelegate PhotoLoadedEvent;
        public IEnumerable<OrphanModel> Orphans { get; set; }
        private IEnumerable<OrphanageV3.Services.Orphan> _SourceOrphans;
        private Size PhotoSize = new Size(75, 75);
        private int PhotoCompressRatio = 70;

        public OrphansViewModel(IApiClient apiClient, IMapperService mapperService, ITranslateService translateService)
        {
            _apiClient = apiClient;
            _mapperService = mapperService;
            _translateService = translateService;
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
            catch(ApiClientException apiEx)
            {
                if (apiEx.StatusCode == "304")
                    return true;
                return false;
            }
        }

        public async Task<long?> SetColor(int Oid , long? colorValue)
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
    }
}
