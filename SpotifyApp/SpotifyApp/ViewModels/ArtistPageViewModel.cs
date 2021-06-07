using System.Collections.ObjectModel;
using Prism.Navigation;
using SpotifyApp.Models;

namespace SpotifyApp.ViewModels
{
    public class ArtistPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        public ArtistPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var artist = parameters.GetValue<string>("artist");

            var artistInfo = await QueryData().GetArtistInfo(1, artist);
            ArtistName = artistInfo.Artist1;
            Followers = artistInfo.Followers;
            Image = artistInfo.Image;

            TopSongs = new ObservableCollection<ArtistTopSongsModel>(await QueryData().GetArtistTopSongs(1, artist));
        }

        #region Properties
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

        private ObservableCollection<ArtistTopSongsModel> topSongs;
        public ObservableCollection<ArtistTopSongsModel> TopSongs
        {
            get { return topSongs; }
            set { SetProperty(ref topSongs, value); }
        }
        #endregion

        #region Methods
        #endregion
    }
}
