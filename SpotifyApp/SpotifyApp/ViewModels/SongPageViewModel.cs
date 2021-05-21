using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SpotifyApp.Models;

namespace SpotifyApp.ViewModels
{
    public class SongPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        public SongPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
            UpdateCurrentTime = new DelegateCommand(() => CurrentTime = TimeSpan.FromMilliseconds(SliderValue * timeMultiplier).ToString(@"mm\:ss"));
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var submittedParameter = parameters.GetValue<AlbumsModel>("song");
            AlbumName = submittedParameter.AlbumName;
            Image = submittedParameter.Images;
            SongTitle = submittedParameter.Title;
            Artist = submittedParameter.Artist;
            Duration = TimeSpan.FromMilliseconds(submittedParameter.Duration).ToString(@"mm\:ss");
            timeMultiplier = submittedParameter.Duration;
        }

        #region Properties
        public DelegateCommand UpdateCurrentTime { get; set; }
        private double timeMultiplier;

        private string currentTime;
        public string CurrentTime
        {
            get { return currentTime; }
            set { SetProperty(ref currentTime, value); }
        }

        private double sliderValue;
        public double SliderValue
        {
            get { return sliderValue; }
            set { SetProperty(ref sliderValue, value); }
        }

        private string albumName;
        public string AlbumName
        {
            get { return albumName; }
            set { SetProperty(ref albumName, value); }
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

        private string duration;
        public string Duration
        {
            get { return duration; }
            set { SetProperty(ref duration, value); }
        }
        #endregion

        #region Methods
        #endregion
    }
}
