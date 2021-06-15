using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using SpotifyApp.Helpers;
using SpotifyApp.Helpers.Cache;
using SpotifyApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SpotifyApp.ViewModels
{
    public class ArtistPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IEventAggregator eventAggregator;
        private IImageCache imageCache;

        public ArtistPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IImageCache imageCache) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.eventAggregator = eventAggregator;
            this.imageCache = imageCache;

            eventAggregator.GetEvent<HeightEventAggregator>().Subscribe(ChangeHeight);
            eventAggregator.GetEvent<BlurEventAggregator>().Subscribe(ChangeSigmas);
            DetectScrollCommand = new DelegateCommand<ItemsViewScrolledEventArgs>((e) => DetectScroll(e));
        }
        
        public override async void Initialize(INavigationParameters parameters)
        {
            var artist = parameters.GetValue<string>("artist");

            var artistInfo = await QueryData().GetArtistInfo(1, artist);
            ArtistName = artistInfo.Artist1;
            Followers = artistInfo.Followers;
            TopSongs = new ModifiedObservableCollection<ArtistTopSongsModel>(await QueryData().GetArtistTopSongs(1, artist));
            ScreenWidth = Prism.PrismApplicationBase.Current.MainPage.Width;
            PanelHeight = 900;

            Parallel.For(0, TopSongs.Count, i =>
            {
                TopSongs[i].GotoAlbumSongInfoPopupPageCommand = new DelegateCommand<ArtistTopSongsModel>(async (artistTopSongsModel) => await GotoAlbumSongInfoPopupPage(artistTopSongsModel));
            });

            Image = artistInfo.Image;
            Preferences.Set("source", artistInfo.Image);
        }

        #region Properties

        public DelegateCommand<ItemsViewScrolledEventArgs> DetectScrollCommand { get; set; }

        private float sigmax;
        public float SigmaX
        {
            get { return sigmax; }
            set { SetProperty(ref sigmax, value); }
        }

        private float sigmay;
        public float SigmaY
        {
            get { return sigmay; }
            set { SetProperty(ref sigmay, value); }
        }

        private double panelHeight;
        public double PanelHeight
        {
            get { return panelHeight; }
            set { SetProperty(ref panelHeight, value); }
        }

        private double screenWidth;
        public double ScreenWidth
        {
            get { return screenWidth; }
            set { SetProperty(ref screenWidth, value); }
        }

        private string artistName;
        public string ArtistName
        {
            get { return artistName; }
            set { SetProperty(ref artistName, value); }
        }

        private int followers;
        public int Followers
        {
            get { return followers; }
            set { SetProperty(ref followers, value); }
        }

        private string image;
        public string Image
        {
            get { return image; }
            set { SetProperty(ref image, value); }
        }

        private ModifiedObservableCollection<ArtistTopSongsModel> topSongs;
        public ModifiedObservableCollection<ArtistTopSongsModel> TopSongs
        {
            get { return topSongs; }
            set { SetProperty(ref topSongs, value); }
        }
        #endregion

        #region Methods

        private void ChangeHeight(double height) => PanelHeight = height;
        private void ChangeSigmas(float sigmas)
        {
            SigmaX = sigmas;
            SigmaY = sigmas;
        }

        private void DetectScroll(ItemsViewScrolledEventArgs e)
        {
            PanelHeight = 900 - e.VerticalOffset;
            SigmaX = (float)e.VerticalOffset * 0.1f;
            SigmaY = (float)e.VerticalOffset * 0.1f;
        }

        private async Task GotoAlbumSongInfoPopupPage(ArtistTopSongsModel artistTopSongsModel)
        {
            var parameters = new NavigationParameters
            {
                {
                    "songInfo",
                    new AlbumsModel
                    {
                        Images = artistTopSongsModel.Image,
                        Title = artistTopSongsModel.Title,
                        Artist = artistTopSongsModel.Artist,
                        Duration = artistTopSongsModel.Duration,
                        SongId = 1,
                    }
                }
            };

            await navigationService.NavigateAsync("AlbumSongInfoPopupPage", parameters);
        }
        #endregion
    }
}
