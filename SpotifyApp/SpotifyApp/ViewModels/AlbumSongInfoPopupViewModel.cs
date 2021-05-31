using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using SpotifyApp.Helpers.Dependencies;
using SpotifyApp.Models;
using Xamarin.Forms;

namespace SpotifyApp.ViewModels
{
    public class AlbumSongInfoPopupViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IToast snackbar;
        public AlbumSongInfoPopupViewModel(INavigationService navigationService, IToast snackbar) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.snackbar = snackbar;

            LikeASongCommand = new DelegateCommand(async () => await LikeASong());
            HideASongCommand = new DelegateCommand(async () => await HideASong());
            GotoPlaylistPageCommand = new DelegateCommand(async () => await GotoPlaylistPage());
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var submittedParameters = parameters.GetValue<AlbumsModel>("songInfo");
            songId = submittedParameters.SongId;
            Image = submittedParameters.Images;
            SongTitle = submittedParameters.Title;
            Artist = submittedParameters.Artist;
            albumName = submittedParameters.AlbumName;
            duration = submittedParameters.Duration;
            MenuContainerMargin = new Thickness(Prism.PrismApplicationBase.Current.MainPage.Width * 0.2, 0, 0, 0);

            await CheckIfUserLikedTheSong();
            await CheckIfUserHideTheSong();
        }



        #region Properties

        private int duration;

        private string albumName;

        private string hiddenText;
        public string HiddenText
        {
            get { return hiddenText; }
            set { SetProperty(ref hiddenText, value); }
        }

        private Color hiddenColor;
        public Color HiddenColor
        {
            get { return hiddenColor; }
            set { SetProperty(ref hiddenColor, value); }
        }

        private string likedOrNot;
        public string LikedOrNot
        {
            get { return likedOrNot; }
            set { SetProperty(ref likedOrNot, value); }
        }

        private string likeFontFamily;
        public string LikeFontFamily
        {
            get { return likeFontFamily; }
            set { SetProperty(ref likeFontFamily, value); }
        }

        private string likeText;
        public string LikeText
        {
            get { return likeText; }
            set { SetProperty(ref likeText, value); }
        }

        private Color likeColor;
        public Color LikeColor
        {
            get { return likeColor; }
            set { SetProperty(ref likeColor, value); }
        }

        private int songId;

        private Thickness menuContainerMargin;
        public Thickness MenuContainerMargin
        {
            get { return menuContainerMargin; }
            set { SetProperty(ref menuContainerMargin, value); }
        }

        private string image;
        public string Image
        {
            get { return image; }
            set { SetProperty(ref image, value); }
        }

        private string songTitle;
        public string SongTitle
        {
            get { return songTitle; }
            set { SetProperty(ref songTitle, value); }
        }

        private string artist;
        public string Artist
        {
            get { return artist; }
            set { SetProperty(ref artist, value); }
        }

        public DelegateCommand LikeASongCommand { get; set; }

        public DelegateCommand HideASongCommand { get; set; }

        public DelegateCommand GotoPlaylistPageCommand { get; set; }

        #endregion

        #region Methods

        private async Task CheckIfUserLikedTheSong()
        {
            var listOfUserLikedSongs = await QueryData().GetUserLikedSongs(1);

            if (listOfUserLikedSongs.Any(x => x.SongId == songId))
            {
                LikeText = Helpers.Fonts.MaterialFilled.Favorite;
                LikeFontFamily = "mat";
                LikeColor = Color.FromHex("#1DB954");
                LikedOrNot = "Liked";
            }
            else
            {
                LikeText = Helpers.Fonts.Material.Favorite_border;
                LikeFontFamily = "matf";
                LikeColor = Color.FromHex("#7E7E7E");
                LikedOrNot = "Like";
            }
        }

        private async Task LikeASong()
        {
            var song = new UserLikeSongsModel
            {
                SongID = songId,
                UserID = 1
            };

            await QueryData().LikeASong(song);
            await navigationService.ClearPopupStackAsync();
            snackbar.ShowToast("Added to Liked songs.");
        }

        private async Task CheckIfUserHideTheSong()
        {
            var listOfUserHiddenSongs = await QueryData().GetUsersHiddenSongs(1);

            if (listOfUserHiddenSongs.Any(x => x == songId))
            {
                HiddenText = "Hidden";
                HiddenColor = Color.FromHex("#C61929");
            }
            else
            {
                HiddenText = "Hide this Song";
                HiddenColor = Color.FromHex("#7E7E7E");
            }
        }

        private async Task HideASong()
        {
            var song = new UserLikeSongsModel
            {
                SongID = songId,
                UserID = 1
            };

            await QueryData().HideASong(song);

            await navigationService.ClearPopupStackAsync();
            snackbar.ShowToast($"Hidden in {albumName}.");
        }

        private async Task GotoPlaylistPage()
        {
            var parameters = new NavigationParameters
            {
                {
                    "playlist",
                    new AlbumsModel
                    {
                        Title = SongTitle,
                        Images = Image,
                        Duration = duration,
                        Artist = Artist
                    }
                }
            };
            await navigationService.NavigateAsync("PlaylistPage", parameters);
        }

        #endregion
    }
}
