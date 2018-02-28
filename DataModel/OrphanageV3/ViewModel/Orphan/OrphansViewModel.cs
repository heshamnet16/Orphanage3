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
        public delegate void OrphansChnagedDelegate();
        public delegate void OrphanChnagedDelegate(int Oid,Image newPhoto);
        public event OrphansChnagedDelegate OrphansChangedEvent;
        public event OrphanChnagedDelegate PhotoLoadedEvent;
        public IEnumerable<OrphanModel> Orphans { get; set; }

        private Size PhotoSize = new Size(75, 75);
        private int PhotoCompressRatio = 70;

        public OrphansViewModel(IApiClient apiClient,IMapperService mapperService)
        {
            _apiClient = apiClient;
            _mapperService = mapperService;
        }
        public void LoadData()
        {
            GetOrphans();
        }

        private async void GetOrphans()
        {
            var Ocounts = await _apiClient.OrphansController_GetOrphansCountAsync();
            var ReturnedOrphans = await _apiClient.OrphansController_GetAllAsync(Ocounts, 0);
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
                        orp.Photo = await _apiClient.GetImage(orp.FacePhotoURI, PhotoSize , PhotoCompressRatio);
                        PhotoLoadedEvent?.Invoke(orp.ID, orp.Photo);
                    }
                    catch  {    }
            }
        }

        public async Task<Image> GetOrphanFacePhoto(int id)
        {            
            try
            {
                string url = Orphans.FirstOrDefault(o => o.ID == id)?.FacePhotoURI;
                if (url != null)
                {
                    var img = await _apiClient.GetImage(url);
                    return img;
                }
                return null;
            }
            catch { return null; }
        }
    }
}
