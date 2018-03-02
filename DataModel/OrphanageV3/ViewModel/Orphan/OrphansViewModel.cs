using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            _SourceOrphans = ReturnedOrphans;
            Orphans = _mapperService.MapToOrphanModel(ReturnedOrphans);
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
                        img = _translateService.IsBoy(orp.Gender) ? new Bitmap(Properties.Resources.UnknownBoy, PhotoSize) : new Bitmap(Properties.Resources.UnknownGirl, PhotoSize);
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
                        img = _translateService.IsBoy(orp.Gender) ? Properties.Resources.UnknownBoy : Properties.Resources.UnknownGirl;
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
            catch
            {
                return false;
            }
        }
    }
}
