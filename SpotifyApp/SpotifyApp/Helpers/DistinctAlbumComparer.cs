using System.Collections.Generic;
using SpotifyApp.Models;

namespace SpotifyApp.Helpers
{
    public class DistinctAlbumComparer : IEqualityComparer<AlbumsModel>
    {
        public bool Equals(AlbumsModel x, AlbumsModel y) => x.AlbumName == y.AlbumName;

        public int GetHashCode(AlbumsModel obj) => obj.AlbumName.GetHashCode();
    }
}
