namespace MovieList.Interfaces
{
    public interface ILookupProtectorKeyRing
    {
        string CurrentKeyId { get; }
        string this[string keyId] { get; }
        IEnumerable<string> GetAllKeyIds();
    }
}
