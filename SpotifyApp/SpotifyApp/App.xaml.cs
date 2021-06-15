using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MonkeyCache.FileStore;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using SpotifyApp.Helpers.Cache;
using SpotifyApp.Helpers.SQLiteHelper;
using SpotifyApp.Models;
using SpotifyApp.ViewModels;
using SpotifyApp.Views;
using SQLite;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

[assembly: ExportFont("MaterialIcons-Regular.ttf", Alias = "matf")]
[assembly: ExportFont("MaterialIconsOutlined-Regular.otf", Alias = "mat")]
[assembly: ExportFont("Roboto-Bold.ttf", Alias = "Bold")]
[assembly: ExportFont("Roboto-Regular.ttf", Alias = "Regular")]

namespace SpotifyApp
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            Barrel.ApplicationId = "CachedBytes";
            await NavigationService.NavigateAsync("NavigationPage/MainTabbedPage?createTab=HomePage&createTab=HomePage"/*"ArtistPage"*/);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainTabbedPage, MainTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<AlbumSongsPage, AlbumSongsPageViewModel>();
            containerRegistry.RegisterForNavigation<SongPage, SongPageViewModel>();
            containerRegistry.RegisterForNavigation<AlbumSongInfoPopupPage, AlbumSongInfoPopupViewModel>();
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterForNavigation<PlaylistPage, PlaylistPageViewModel>();
            containerRegistry.RegisterForNavigation<NewPlaylistPagePopup, NewPlaylistPagePopupViewModel>();
            containerRegistry.RegisterForNavigation<ArtistPage, ArtistPageViewModel>();

            containerRegistry.Register<IImageCache, ImageCache>();
        }

        //statics
        public static WebClient Client()
        {
            using (var client = new WebClient())
            {
                return client;
            }
        }

        private static readonly Lazy<SQLiteAsyncConnection> dbConnection = new Lazy<SQLiteAsyncConnection>(() => new SQLiteAsyncConnection(SQLiteConstants.GetDatabasePath, SQLiteConstants.Flags));

        public static SQLiteAsyncConnection ConnectionString = dbConnection.Value;
        
        public static async Task<SQLiteAsyncConnection> CreateDatabaseTable<T>()
        {
            if (!ConnectionString.TableMappings.Any(x => x.MappedType == typeof(T)))
            {
                await ConnectionString.EnableWriteAheadLoggingAsync();
                await ConnectionString.CreateTablesAsync(CreateFlags.None, typeof(T));
            }
            return ConnectionString;
        }
    }
}
