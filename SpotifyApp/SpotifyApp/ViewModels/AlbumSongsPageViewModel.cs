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

            Image = submitedParameter.Images;
            AlbumName = submitedParameter.AlbumName;
            Artist = submitedParameter.Artist;
            Songs = new ObservableCollection<AlbumsModel>(await QueryData().GetAlbumSongs(submitedParameter.AlbumName));

            Parallel.For(0, Songs.Count, i =>
            {
                Songs[i].GotoSongCommand = new DelegateCommand<AlbumsModel>(async (song) => await GotoSongPage(song));
            });
        }

        #region Properties

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

        private ObservableCollection<AlbumsModel> songs;
        public ObservableCollection<AlbumsModel> Songs
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
        #endregion
    }
}
