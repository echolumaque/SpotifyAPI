using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using SpotifyApp.Helpers;
using SpotifyApp.Models;
using Xamarin.Forms;

namespace SpotifyApp.ViewModels
{
    public class AlbumSongsPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        public AlbumSongsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            var submitedParameter = parameters.GetValue<AlbumsModel>("album");
            var albumSongs = await QueryData().GetAlbumSongs(submitedParameter.AlbumName);

            Image = submitedParameter.Images;
            AlbumName = submitedParameter.AlbumName;
            Artist = submitedParameter.Artist;
            Year = submitedParameter.Year;

            var listOfUsersHiddenSongs = await QueryData().GetUsersHiddenSongs(1);

            Songs = new ModifiedObservableCollection<AlbumsModel>();

            notHiddenSongs = albumSongs.Where(x => !listOfUsersHiddenSongs.Contains(x.SongId));
            Parallel.For(0, albumSongs.Where(x => !listOfUsersHiddenSongs.Contains(x.SongId)).Count(), i => notHiddenSongs.ElementAt(i).SongOpacity = 1.0);
            Songs.AddRange(notHiddenSongs);

            hiddenSongs = albumSongs.Where(x => listOfUsersHiddenSongs.Contains(x.SongId));
            Parallel.For(0, albumSongs.Where(x => listOfUsersHiddenSongs.Contains(x.SongId)).Count(), i => hiddenSongs.ElementAt(i).SongOpacity = 0.5);
            Songs.AddRange(hiddenSongs);

            Parallel.For(0, Songs.Count, i =>
            {
                Songs[i].GotoSongCommand = new DelegateCommand<AlbumsModel>(async (song) => await GotoSongPage(song));
                Songs[i].GotoSongInfoCommand = new DelegateCommand<AlbumsModel>(async (songInfo) => await GotoAlbumSongInfoPage(songInfo));
            });

            BottomMargin = new Thickness(0, 0, 0, Prism.PrismApplicationBase.Current.MainPage.Height * 0.05);
        }

        #region Properties

        private Thickness bottomMargin;
        public Thickness BottomMargin
        {
            get { return bottomMargin; }
            set { SetProperty(ref bottomMargin, value); }
        }

        private IEnumerable<AlbumsModel> notHiddenSongs;
        private IEnumerable<AlbumsModel> hiddenSongs;

        private string year;
        public string Year
        {
            get { return year; }
            set { SetProperty(ref year, value); }
        }

        private string image;
        public string Image
        {
            get { return image; }
            set { SetProperty(ref image, value); }
        }

        private string albumName;
        public string AlbumName
        {
            get { return albumName; }
            set { SetProperty(ref albumName, value); }
        }

        private string artist;
        public string Artist
        {
            get { return artist; }
            set { SetProperty(ref artist, value); }
        }

        private ModifiedObservableCollection<AlbumsModel> songs;
        public ModifiedObservableCollection<AlbumsModel> Songs
        {
            get { return songs; }
            set { SetProperty(ref songs, value); }
        }
        #endregion

        #region Methods
        private async Task GotoSongPage(AlbumsModel albumsModel)
        {
            var parameters = new NavigationParameters
            {
                {
                    "song",
                    new AlbumsModel
                    {
                        Images = albumsModel.Images,
                        Title = albumsModel.Title,
                        Artist = albumsModel.Artist,
                        Duration = albumsModel.Duration,
                        AlbumName = AlbumName
                    }
                }
            };
            await navigationService.NavigateAsync("SongPage", parameters);
        }

        private async Task GotoAlbumSongInfoPage(AlbumsModel albumsModel)
        {
            var parameters = new NavigationParameters
            {
                {
                    "songInfo",
                    new AlbumsModel
                    {
                        Images =  albumsModel.Images,
                        Title = albumsModel.Title,
                        Artist = albumsModel.Artist,
                        SongId = albumsModel.SongId,
                        AlbumName = albumsModel.AlbumName,
                        Duration = albumsModel.Duration
                    }
                }
            };

            await navigationService.NavigateAsync("AlbumSongInfoPopupPage", parameters);
        }
        #endregion
    }
}
