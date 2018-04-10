using System.Drawing;
using System.Threading.Tasks;

namespace OrphanageV3.Services
{
    public partial interface IApiClient
    {
        Task<Image> GetImage(string url, Size size, int compress);

        Task<Image> GetImage(string url, Size size);

        Task<Image> GetImage(string url);

        Task<byte[]> GetImageData(string url, Size size, int compress);

        Task<byte[]> GetImageData(string url, Size size);

        Task<byte[]> GetImageData(string url);

        Task<bool> SetImage(string url, Image img);
    }

    public partial class ApiClient : IApiClient
    {
        public ApiClient(bool useSettingUrl)
        {
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(() =>
            {
                var settings = new Newtonsoft.Json.JsonSerializerSettings();
                UpdateJsonSerializerSettings(settings);
                return settings;
            });
            if (useSettingUrl)
            {
                var url = Properties.Settings.Default.OrphanageServiceURL;
                if (url.EndsWith("/"))
                    url = url.Substring(0, url.Length - 1);
                BaseUrl = url;
            }
        }

        public async Task<Image> GetImage(string url, Size size, int compress)
        {
            if (url == null || url.Length == 0)
                throw new System.ArgumentNullException("url");

            if (size == null)
                throw new System.ArgumentNullException("size");

            if (compress <= 0)
                throw new System.ArgumentNullException("Compress");

            if (!url.StartsWith("/"))
                url = "/" + url;
            string imgSize = size.Height + "x" + size.Width;
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl).Append(url + "/{size}/{compress}");
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

        public async Task<byte[]> GetImageData(string url, Size size, int compress)
        {
            if (url == null || url.Length == 0)
                throw new System.ArgumentNullException("url");

            if (size == null)
                throw new System.ArgumentNullException("size");

            if (compress <= 0)
                throw new System.ArgumentNullException("Compress");

            if (!url.StartsWith("/"))
                url = "/" + url;
            string imgSize = size.Height + "x" + size.Width;
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl).Append(url + "/{size}/{compress}");
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
                            var result_ = default(byte[]);
                            try
                            {
                                result_ = responseData_;

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

                        return default(byte[]);
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

        public async Task<byte[]> GetImageData(string url, Size size)
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
                            var result_ = default(byte[]);
                            try
                            {
                                result_ = responseData_;
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

                        return default(byte[]);
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

        public async Task<byte[]> GetImageData(string url)
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
                            var result_ = default(byte[]);
                            try
                            {
                                result_ = responseData_;

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

                        return default(byte[]);
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

        public async Task<bool> SetImage(string url, Image img)
        {
            if (url == null || url.Length == 0)
                throw new System.ArgumentNullException("Url");

            if (!url.StartsWith("/"))
                url = "/" + url;

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl).Append(url);

            var client_ = new System.Net.Http.HttpClient();
            try
            {
                using (var memoryStream = new System.IO.MemoryStream())
                {
                    if (img != null)
                        img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                    using (var request_ = new System.Net.Http.HttpRequestMessage())
                    {
                        System.Net.Http.HttpContent content = new System.Net.Http.ByteArrayContent(memoryStream.ToArray());

                        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

                        var content_ = new System.Net.Http.MultipartFormDataContent();
                        content_.Add(content);
                        request_.Content = content_;
                        request_.Method = new System.Net.Http.HttpMethod("PUT");
                        request_.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

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
                                return true;
                            }
                            else if (status_ == "304")
                                return true;
                            else
                            {
                                if (status_ != "200" && status_ != "204")
                                {
                                    return false;
                                }
                            }
                            return false;
                        }
                        finally
                        {
                            if (response_ != null)
                                response_.Dispose();
                        }
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