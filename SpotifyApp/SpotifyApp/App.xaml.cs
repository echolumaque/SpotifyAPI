using System.Net;
using System.Net.Http;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using SpotifyApp.ViewModels;
using SpotifyApp.Views;
using Syncfusion.Licensing;
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
            SyncfusionLicenseProvider.RegisterLicense("NDU2NzQ4QDMxMzkyZTMxMmUzMFlMZE54SzRHSDhHTUhEZWt5OUZIY3pvT2wwVC90bTI0cm5ERDFCb24zVUk9");
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
        }

        //statics
        public static WebClient Client()
        {
            using (var client = new WebClient())
            {
                return client;
            }
        }
    }
}
