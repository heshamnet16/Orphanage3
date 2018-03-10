﻿using OrphanageV3.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Orphan
{
    public class OrphanViewModel
    {
        private readonly IApiClient _apiClient;
        private Size _ImageSize = new Size(153, 126);

        public Services.Orphan CurrentOrphan { get; private set; }

        public Size ImagesSize { get => _ImageSize; set { _ImageSize = value; } }

        public OrphanViewModel (IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<bool> Save(Services.Orphan orphan)
        {
            try
            {
                var ret = await _apiClient.OrphansController_PutAsync(orphan);
                return true;
            }
            catch(ApiClientException apiException)
            {
                if(apiException.StatusCode != "304")
                {
                    //TODO Status Message not changed
                    return false;
                }
                return true;
            }
        }

        public async Task<Services.Orphan> getOrphan (int Oid)
        {            
            var returnedOrphan =  await _apiClient.OrphansController_GetAsync(Oid);
            var facePhotoTask = _apiClient.GetImageData(returnedOrphan.FacePhotoURI,_ImageSize,50);
            var bodyPhotoTask = _apiClient.GetImageData(returnedOrphan.FullPhotoURI,_ImageSize,50);
            var birthCertificateTask = _apiClient.GetImageData(returnedOrphan.BirthCertificatePhotoURI, _ImageSize,50);
            var familiyCardPhotoTask = _apiClient.GetImageData(returnedOrphan.FamilyCardPagePhotoURI, _ImageSize,50);
            if(returnedOrphan.EducationId.HasValue)
            {
                try
                {
                    returnedOrphan.Education.CertificatePhotoFront = await _apiClient.GetImageData(returnedOrphan.Education.CertificateImageURI, _ImageSize, 50);
                }
                catch(ApiClientException apiException)
                {
                    if(apiException.StatusCode != "404")
                    {
                        //TODO show error message
                    }
                    returnedOrphan.Education.CertificatePhotoFront = null;
                }
                try
                {
                    returnedOrphan.Education.CertificatePhotoBack = await _apiClient.GetImageData(returnedOrphan.Education.CertificateImage2URI, _ImageSize, 50);
                }
                catch (ApiClientException apiException)
                {
                    if (apiException.StatusCode != "404")
                    {
                        //TODO show error message
                    }
                    returnedOrphan.Education.CertificatePhotoBack = null;
                }
            }
            if(returnedOrphan.HealthId.HasValue)
            {
                try
                {
                    returnedOrphan.HealthStatus.ReporteFileData = await _apiClient.GetImageData(returnedOrphan.HealthStatus.ReporteFileURI);
                }
                catch (ApiClientException apiException)
                {
                    if (apiException.StatusCode != "404")
                    {
                        //TODO show error message
                    }
                    returnedOrphan.HealthStatus.ReporteFileData = null;
                }
            }
            returnedOrphan.FullPhotoData = await bodyPhotoTask;
            returnedOrphan.FacePhotoData = await facePhotoTask;
            returnedOrphan.BirthCertificatePhotoData = await birthCertificateTask;
            returnedOrphan.FamilyCardPagePhotoData = await familiyCardPhotoTask;
            CurrentOrphan = returnedOrphan;
            return returnedOrphan;
        }
        public async Task<bool> SaveBodyImage (string url ,Image image )
        {
            var ret = await _apiClient.SetImage(url, image);
            return ret;
        }
        public async Task<bool> Save()
        {
           return  await Save(CurrentOrphan);
        }

        public async void UploadImage(string url,Image img)
        {
            await _apiClient.SetImage(url, img);
        }
    }
}
