using System.Collections.ObjectModel;
using Prism.Events;
using Prism.Navigation;
using SpotifyApp.Helpers;
using SpotifyApp.Models;

namespace SpotifyApp.ViewModels
{
    public class ArtistPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        public ArtistPageViewModel(INavigationService navigationService, IEventAggregator ea) : base(navigationService)
        {
            this.navigationService = navigationService;
            ea.GetEvent<HeightEventAggregator>().Subscribe(Testing);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var artist = parameters.GetValue<string>("artist");

            var artistInfo = await QueryData().GetArtistInfo(1, artist);
            ArtistName = artistInfo.Artist1;
            Followers = artistInfo.Followers;
            Image = artistInfo.Image;
            Test = 400;
            TopSongs = new ModifiedObservableCollection<ArtistTopSongsModel>(await QueryData().GetArtistTopSongs(1, artist));
            ScreenWidth = Prism.PrismApplicationBase.Current.MainPage.Width;

            PanelHeight = 920;
        }
        void Testing(double test) => PanelHeight = test;


        private double panelHeight;
        public double PanelHeight
        {
            get { return panelHeight; }
            set { SetProperty(ref panelHeight, value); }
        }
        #region Properties

        private double test;
        public double Test
        {
            get { return test; }
            set { SetProperty(ref test, value); }
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
        #endregion
    }
}
