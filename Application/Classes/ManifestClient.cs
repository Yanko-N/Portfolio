using Application.Interfaces;
using Persistence.JsonHelper;
using Persistence.Models;
using System.Net.Http.Json;

namespace Application.Classes
{
    public class ManifestClient : IManifestClient
    {
        private SchemaManifest? _manifest;
        private readonly Dictionary<string, object?> _cache = new();
        private readonly HttpClient _httpClient;
        public ManifestClient(HttpClient client)
        {
            _httpClient = client;
        }

        /// <summary>
        /// Returns the table of the T Key object
        /// </summary>
        /// <typeparam name="TKey"> table to retreive</typeparam>
        /// <param name="ct">cancelation token</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"> The table doesnt exist</exception>
        public async Task<IEnumerable<TKey>?> GetAsync<TKey>(CancellationToken ct = default)
        {
            try
            {
                var key = JsonFilesToObjectsMapper.GetKey<TKey>() ?? string.Empty;

                if (_manifest == null)
                {
                    _manifest = await _httpClient.GetFromJsonAsync<SchemaManifest>("./data/schema.json", ct);
                }
            
                if (_cache.TryGetValue(key, out var existing))
                {
                    return (IEnumerable<TKey>?)existing;
                }

                if (!_manifest.Files.TryGetValue(key, out var file))
                {
                    throw new KeyNotFoundException($"Schema key '{key}' not found.");
                }

                var url = $"{file.Path}?v={_manifest.Version}";
                var data = await _httpClient.GetFromJsonAsync<IEnumerable<TKey>>(url, ct);

                _cache[key] = data;
                return data;
            }
            catch(Exception ex)
            {
                return default(IEnumerable<TKey>);
            }
           
        }
    }
}
