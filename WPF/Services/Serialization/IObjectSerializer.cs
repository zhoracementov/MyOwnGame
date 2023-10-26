using System.Threading.Tasks;

namespace WPF.Services.Serialization
{
    public interface IObjectSerializer
    {
        string FileFormat { get; }
        T Deserialize<T>(string fileName);
        Task<T> DeserializeAsync<T>(string fileName);
        abstract void Serialize<T>(T obj, string fileName);
        abstract Task SerializeAsync<T>(T obj, string fileName);
    }
}
