using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SpotifyApp.Models;
using Xamarin.Forms;

namespace SpotifyApp.ViewModels
{
    public class AlbumSongInfoViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        public AlbumSongInfoViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var submittedParameters = parameters.GetValue<AlbumsModel>("songInfo");

            Image = submittedParameters.Images;
            SongTitle = submittedParameters.Title;
            Artist = submittedParameters.Artist;
            MenuContainerMargin = new Thickness(Prism.PrismApplicationBase.Current.MainPage.Width * 0.2, 0, 0, 0);
        }



        #region Properties

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
        #endregion

        #region Methods
        #endregion
    }
}
