using System.Threading.Tasks;

namespace SpotifyApp.Helpers.Cache
{
    public interface IImageCache
    {
        void AddCachedImageByteAray(string key, byte[] contents);
        byte[] GetCachedImageByteArray(string key);
    }
}
