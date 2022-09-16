using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Utils.Extensions
{
    public static class HttpMessageExtensions
    {
        public static HttpRequestMessage Clone(this HttpRequestMessage original)
        {
            var clone = new HttpRequestMessage(original.Method, original.RequestUri)
            {
                Version = original.Version,
                Content = original.Content.Clone()
            };
            CloneHeaders(original.Headers, clone.Headers);

            original.Properties
                ?.ForEach(p => clone.Properties.Add(p));

            return clone;
        }

        public static async Task<HttpRequestMessage> CloneAsync(this HttpRequestMessage original)
        {
            var clone = new HttpRequestMessage(original.Method, original.RequestUri)
            {
                Version = original.Version,
                Content = await original.Content.CloneAsync()
            };
            CloneHeaders(original.Headers, clone.Headers);

            original.Properties
                ?.ForEach(p => clone.Properties.Add(p));

            return clone;
        }

        public static HttpResponseMessage Clone(this HttpResponseMessage original)
        {
            var clone = new HttpResponseMessage(original.StatusCode)
            {
                Version = original.Version,
                Content = original.Content.Clone(),
                ReasonPhrase = original.ReasonPhrase,
                RequestMessage = original.RequestMessage.Clone()
            };
            CloneHeaders(original.Headers, clone.Headers);

            return clone;
        }

        public static async Task<HttpResponseMessage> CloneAsync(this HttpResponseMessage original)
        {
            var clone = new HttpResponseMessage(original.StatusCode)
            {
                Version = original.Version,
                Content = await original.Content.CloneAsync(),
                ReasonPhrase = original.ReasonPhrase,
                RequestMessage = await original.RequestMessage.CloneAsync()
            };
            CloneHeaders(original.Headers, clone.Headers);

            return clone;
        }

        public static HttpContent Clone(this HttpContent original)
        {
            if (original == null) return null;

            StreamContent clone;
            using (var ms = new MemoryStream())
            {
                original.CopyToAsync(ms).Wait();
                ms.Position = 0;
                clone = new StreamContent(ms);
            }
            CloneHeaders(original.Headers, clone.Headers);

            return clone;
        }

        public static async Task<HttpContent> CloneAsync(this HttpContent original)
        {
            if (original == null) return null;

            StreamContent clone;
            using (var ms = new MemoryStream())
            {
                await original.CopyToAsync(ms).ConfigureAwait(false);
                ms.Position = 0;
                clone = new StreamContent(ms);
            }
            CloneHeaders(original.Headers, clone.Headers);

            return clone;
        }

        private static void CloneHeaders(HttpHeaders original, HttpHeaders clone)
        {
            original?.ForEach(h => clone.Add(h.Key, h.Value));
        }
    }

}
