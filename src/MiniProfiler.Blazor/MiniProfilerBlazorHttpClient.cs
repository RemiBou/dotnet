using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.IO;

namespace StackExchange.Profiling
{
    public class MiniProfilerBlazorHttpClient : HttpClient
    {
        private HttpClient _httpClient;

        public MiniProfilerBlazorHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public new async Task<HttpResponseMessage> DeleteAsync(Uri requestUri, CancellationToken cancellationToken)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "DELETE", true))
            {
                return await _httpClient.DeleteAsync(requestUri, cancellationToken);
            }
        }
        public new async Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "DELETE", true))
            {
                return await _httpClient.DeleteAsync(requestUri, cancellationToken);
            }
        }
        public new async Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "DELETE", true))
            {
                return await _httpClient.DeleteAsync(requestUri);
            }
        }
        public new async Task<HttpResponseMessage> DeleteAsync(Uri requestUri)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "DELETE", true))
            {
                return await _httpClient.DeleteAsync(requestUri);
            }
        }
        public new async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "GET", true))
            {
                return await _httpClient.GetAsync(requestUri);
            }
        }
        public new async Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "GET", true))
            {
                return await _httpClient.GetAsync(requestUri, completionOption);
            }
        }
        public new async Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "GET", true))
            {
                return await _httpClient.GetAsync(requestUri, completionOption, cancellationToken);
            }
        }
        public new async Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "GET", true))
            {
                return await _httpClient.GetAsync(requestUri, cancellationToken);
            }
        }
        public new async Task<HttpResponseMessage> GetAsync(Uri requestUri)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "GET", true))
            {
                return await _httpClient.GetAsync(requestUri);
            }
        }
        public new async Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "GET", true))
            {
                return await _httpClient.GetAsync(requestUri, completionOption);
            }
        }
        public new async Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "GET", true))
            {
                return await _httpClient.GetAsync(requestUri, completionOption, cancellationToken);
            }
        }
        public new async Task<HttpResponseMessage> GetAsync(Uri requestUri, CancellationToken cancellationToken)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "GET", true))
            {
                return await _httpClient.GetAsync(requestUri, cancellationToken);
            }
        }
        public new async Task<byte[]> GetByteArrayAsync(string requestUri)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "GET", true))
            {

                return await _httpClient.GetByteArrayAsync(requestUri);
            }
        }
        public new async Task<byte[]> GetByteArrayAsync(Uri requestUri)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "GET", true))
            {

                return await _httpClient.GetByteArrayAsync(requestUri);
            }
        }
        public new async Task<Stream> GetStreamAsync(string requestUri)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "GET", true))
            {

                return await _httpClient.GetStreamAsync(requestUri);
            }
        }
        public new async Task<Stream> GetStreamAsync(Uri requestUri)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "GET", true))
            {

                return await _httpClient.GetStreamAsync(requestUri);
            }
        }
        public new async Task<string> GetStringAsync(string requestUri)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "GET", true))
            {

                return await _httpClient.GetStringAsync(requestUri);
            }
        }
        public new async Task<string> GetStringAsync(Uri requestUri)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "GET", true))
            {

                return await _httpClient.GetStringAsync(requestUri);
            }
        }
        public new async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "POST", true))
            {
                return await _httpClient.PostAsync(requestUri, content);
            }
        }
        public new async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "POST", true))
            {
                return await _httpClient.PostAsync(requestUri, content, cancellationToken);
            }
        }
        public new async Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "POST", true))
            {
                return await _httpClient.PostAsync(requestUri, content);
            }
        }
        public new async Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "POST", true))
            {
                return await _httpClient.PostAsync(requestUri, content, cancellationToken);
            }
        }
        public new async Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "PUT", true))
            {
                return await _httpClient.PutAsync(requestUri, content);
            }
        }
        public new async Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "PUT", true))
            {
                return await _httpClient.PutAsync(requestUri, content, cancellationToken);
            }
        }
        public new async Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "PUT", true))
            {
                return await _httpClient.PutAsync(requestUri, content);
            }
        }
        public new async Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", requestUri.ToString(), "PUT", true))
            {
                return await _httpClient.PutAsync(requestUri, content, cancellationToken);
            }
        }
        public new async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", request.RequestUri.ToString(), request.Method.ToString(), true))
            {
                return await _httpClient.SendAsync(request);
            }
        }
        public new async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", request.RequestUri.ToString(), request.Method.ToString(), true))
            {
                return await _httpClient.SendAsync(request, completionOption);
            }
        }
        public new async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", request.RequestUri.ToString(), request.Method.ToString(), true))
            {
                return await _httpClient.SendAsync(request, completionOption, cancellationToken);
            }
        }
        public override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (MiniProfiler.Current.CustomTiming("http-blazor", request.RequestUri.ToString(), request.Method.ToString(), true))
            {
                return await _httpClient.SendAsync(request, cancellationToken);
            }
        }
    }


}
