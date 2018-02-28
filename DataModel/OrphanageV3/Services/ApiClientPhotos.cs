using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.Services
{
    public partial interface IApiClient
    {
        Task<Image> GetImage(string url, Size size, int compress);
        Task<Image> GetImage(string url, Size size);
        Task<Image> GetImage(string url);
    }

    public partial class ApiClient : IApiClient
    {
        public async Task<Image> GetImage(string url, Size size , int compress )
        {
            if (url == null || url.Length ==0)
                throw new System.ArgumentNullException("url");

            if (size == null)
                throw new System.ArgumentNullException("size");

            if (compress <= 0)
                throw new System.ArgumentNullException("Compress");

            if (!url.StartsWith("/"))
                url = "/" + url;
            string imgSize = size.Height + "x" + size.Width;
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl).Append(url+"/{size}/{compress}");
            urlBuilder_.Replace("{compress}", System.Uri.EscapeDataString(System.Convert.ToString(compress, System.Globalization.CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{size}", System.Uri.EscapeDataString(System.Convert.ToString(imgSize, System.Globalization.CultureInfo.InvariantCulture)));

            var client_ = new System.Net.Http.HttpClient();
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        foreach (var item_ in response_.Content.Headers)
                            headers_[item_.Key] = item_.Value;

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var responseData_ = await response_.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                            var result_ = default(Image);
                            try
                            {
                                using (var mem = new System.IO.MemoryStream(responseData_))
                                {
                                    result_ = Image.FromStream(mem);
                                }
                                return result_;
                            }
                            catch (System.Exception exception)
                            {
                                throw new ApiClientException("Could not deserialize the response body.", status_, responseData_.ToString(), headers_, exception);
                            }
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiClientException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", status_, responseData_, headers_, null);
                        }

                        return default(Image);
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (client_ != null)
                    client_.Dispose();
            }
        }

        public async Task<Image> GetImage(string url, Size size)
        {
            if (url == null || url.Length == 0)
                throw new System.ArgumentNullException("url");

            if (size == null)
                throw new System.ArgumentNullException("size");

            if (!url.StartsWith("/"))
                url = "/" + url;

            string imgSize = size.Height + "x" + size.Width;
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl).Append(url + "/{size}");
            urlBuilder_.Replace("{size}", System.Uri.EscapeDataString(System.Convert.ToString(imgSize, System.Globalization.CultureInfo.InvariantCulture)));

            var client_ = new System.Net.Http.HttpClient();
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        foreach (var item_ in response_.Content.Headers)
                            headers_[item_.Key] = item_.Value;

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var responseData_ = await response_.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                            var result_ = default(Image);
                            try
                            {
                                using (var mem = new System.IO.MemoryStream(responseData_))
                                {
                                    result_ = Image.FromStream(mem);
                                }
                                return result_;
                            }
                            catch (System.Exception exception)
                            {
                                throw new ApiClientException("Could not deserialize the response body.", status_, responseData_.ToString(), headers_, exception);
                            }
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiClientException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", status_, responseData_, headers_, null);
                        }

                        return default(Image);
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (client_ != null)
                    client_.Dispose();
            }
        }

        public async Task<Image> GetImage(string url)
        {
            if (url == null || url.Length == 0)
                throw new System.ArgumentNullException("url");

            if (!url.StartsWith("/"))
                url = "/" + url;

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl).Append(url);
            var client_ = new System.Net.Http.HttpClient();
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        foreach (var item_ in response_.Content.Headers)
                            headers_[item_.Key] = item_.Value;

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var responseData_ = await response_.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                            var result_ = default(Image);
                            try
                            {
                                using (var mem = new System.IO.MemoryStream(responseData_))
                                {
                                    result_ = Image.FromStream(mem);
                                }
                                return result_;
                            }
                            catch (System.Exception exception)
                            {
                                throw new ApiClientException("Could not deserialize the response body.", status_, responseData_.ToString(), headers_, exception);
                            }
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiClientException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", status_, responseData_, headers_, null);
                        }

                        return default(Image);
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (client_ != null)
                    client_.Dispose();
            }
        }
    }
}
