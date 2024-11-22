namespace BussinesLayer.Interfaces.Token;

public interface IPresistanceService
{
    T Set<T>(string key, T data, TimeSpan Expiry);
    T? Get<T>(string key);
    void Remove(string key);
    void Clear();


}