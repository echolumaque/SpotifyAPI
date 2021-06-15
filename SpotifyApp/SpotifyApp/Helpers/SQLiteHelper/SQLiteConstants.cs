using System.IO;

namespace SpotifyApp.Helpers.SQLiteHelper
{
    public static class SQLiteConstants
    {
        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.SharedCache |
            SQLite.SQLiteOpenFlags.Create;

        public static string GetDatabasePath
        {
            get => Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "CachedByteArrays.db3");
        }
    }
}