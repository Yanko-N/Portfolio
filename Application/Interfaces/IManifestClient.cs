namespace Application.Interfaces
{
    public interface IManifestClient
    {
        Task<IEnumerable<TKey>?> GetAsync<TKey>(CancellationToken cancellationToken = default);

    }
}
