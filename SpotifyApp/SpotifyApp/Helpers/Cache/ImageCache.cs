using System;
using System.Net.Http;
using MonkeyCache.FileStore;

namespace SpotifyApp.Helpers.Cache
{
    public class ImageCache : IImageCache
    {
        //private static HttpClient client = new HttpClient();

        public void AddCachedImageByteAray(string key, byte[] contents) => Barrel.Current.Add(key, contents, TimeSpan.FromDays(1));

        public byte[] GetCachedImageByteArray(string key)
        {
            if (!Barrel.Current.Exists(key) || Barrel.Current.IsExpired(key))
            {
                var byteArrayToCache = App.Client().DownloadData(key);
                Barrel.Current.Add(key, byteArrayToCache, TimeSpan.FromDays(1));
                return Barrel.Current.Get<byte[]>(key);
            }
            else
            {
                return Barrel.Current.Get<byte[]>(key);
            }
        }
    }
}