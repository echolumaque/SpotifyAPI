using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Refit;
using SpotifyApp.Models;
using Xamarin.Forms;

namespace SpotifyApp.ViewModels
{
    public class AlbumSongInfoPopupViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        public AlbumSongInfoPopupViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
            LikeASongCommand = new DelegateCommand(async () => await LikeASong());
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var submittedParameters = parameters.GetValue<AlbumsModel>("songInfo");
            songId = submittedParameters.SongId;
            Image = submittedParameters.Images;
            SongTitle = submittedParameters.Title;
            Artist = submittedParameters.Artist;
            MenuContainerMargin = new Thickness(Prism.PrismApplicationBase.Current.MainPage.Width * 0.2, 0, 0, 0);

            await CheckIfUserLikedTheSong();
        }



        #region Properties

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
        #endregion

        #region Methods
        private async Task LikeASong()
        {
            var song = new UserLikeSongsModel
            {
                SongID = songId,
                UserID = 1
            };

            await QueryData().LikeASong(song);
        }

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
        #endregion
    }
}
