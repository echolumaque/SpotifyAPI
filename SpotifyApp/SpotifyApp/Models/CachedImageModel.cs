using SQLite;

namespace SpotifyApp.Models
{
    public class CachedImageModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public string SourceURL { get; set; }
    }
}
