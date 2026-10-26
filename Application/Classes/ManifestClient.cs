using Application.Interfaces;
using Persistence.JsonHelper;
using Persistence.Models;
using System.Collections.Concurrent;
using System.Net.Http.Json;

namespace Application.Classes
{
    public class ManifestClient : IManifestClient
    {
        private readonly object _manifestLock = new();
        private Task<SchemaManifest>? _manifestTask;
        private readonly ConcurrentDictionary<string, Lazy<Task<object>>> _cache = new();
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
            var key = JsonFilesToObjectsMapper.GetKey<TKey>() ?? string.Empty;
            var manifestTask = GetManifestAsync();
            SchemaManifest manifest;
            try
            {
                manifest = await manifestTask.WaitAsync(ct);
            }
            catch
            {
                if (manifestTask.IsFaulted || manifestTask.IsCanceled)
                {
                    lock (_manifestLock)
                    {
                        if (ReferenceEquals(_manifestTask, manifestTask))
                        {
                            _manifestTask = null;
                        }
                    }
                }

                throw;
            }

            if (!manifest.Files.TryGetValue(key, out var file))
            {
                throw new KeyNotFoundException($"Schema key '{key}' not found.");
            }

            var url = $"{file.Path}?v={manifest.Version}";
            var cachedTask = _cache.GetOrAdd(
                key,
                _ => new Lazy<Task<object>>(
                    () => LoadDataAsync<TKey>(url),
                    LazyThreadSafetyMode.ExecutionAndPublication));

            try
            {
                return (IEnumerable<TKey>)await cachedTask.Value.WaitAsync(ct);
            }
            catch
            {
                if ((cachedTask.Value.IsFaulted || cachedTask.Value.IsCanceled)
                    && _cache.TryGetValue(key, out var currentTask)
                    && ReferenceEquals(currentTask, cachedTask))
                {
                    _cache.TryRemove(key, out _);
                }

                throw;
            }
        }

        private Task<SchemaManifest> GetManifestAsync()
        {
            lock (_manifestLock)
            {
                return _manifestTask ??= LoadManifestAsync();
            }
        }

        private async Task<SchemaManifest> LoadManifestAsync() =>
            await _httpClient.GetFromJsonAsync<SchemaManifest>("./data/schema.json")
            ?? throw new InvalidOperationException("The schema manifest response was empty.");

        private async Task<object> LoadDataAsync<TKey>(string url) =>
            await _httpClient.GetFromJsonAsync<IEnumerable<TKey>>(url)
            ?? throw new InvalidOperationException($"The data response for '{url}' was empty.");
    }
}
