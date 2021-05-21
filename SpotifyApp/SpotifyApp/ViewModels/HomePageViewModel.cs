using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SpotifyApp.Helpers;
using SpotifyApp.Models;
using Xamarin.Forms;

namespace SpotifyApp.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        public HomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            var data = await QueryData().GetAlbums();
            Tailored = new ObservableCollection<AlbumsModel>(data.Take(4));
            Life = new ObservableCollection<AlbumsModel>(data.Skip(4).Take(4));
            Home = new ObservableCollection<AlbumsModel>(data.Skip(8).Take(4));
            Mood = new ObservableCollection<AlbumsModel>(data.Skip(12).Take(4));

            Parallel.For(0, Tailored.Count, i =>
            {
                Tailored[i].GotoAlbumSongsCommand = new DelegateCommand<AlbumsModel>(async (albumsModel) => await GotoSongsPage(albumsModel));
                Life[i].GotoAlbumSongsCommand = new DelegateCommand<AlbumsModel>(async (albumsModel) => await GotoSongsPage(albumsModel));
                Home[i].GotoAlbumSongsCommand = new DelegateCommand<AlbumsModel>(async (albumsModel) => await GotoSongsPage(albumsModel));
                Mood[i].GotoAlbumSongsCommand = new DelegateCommand<AlbumsModel>(async (albumsModel) => await GotoSongsPage(albumsModel));
            });
        }

        #region Properties

        private ObservableCollection<AlbumsModel> tailored;
        public ObservableCollection<AlbumsModel> Tailored
        {
            get { return tailored; }
            set { SetProperty(ref tailored, value); }
        }

        private ObservableCollection<AlbumsModel> life;
        public ObservableCollection<AlbumsModel> Life
        {
            get { return life; }
            set { SetProperty(ref life, value); }
        }

        private ObservableCollection<AlbumsModel> home;
        public ObservableCollection<AlbumsModel> Home
        {
            get { return home; }
            set { SetProperty(ref home, value); }
        }

        private ObservableCollection<AlbumsModel> mood;
        public ObservableCollection<AlbumsModel> Mood
        {
            get { return mood; }
            set { SetProperty(ref mood, value); }
        }
        #endregion

        #region Methods
        private async Task GotoSongsPage(AlbumsModel albumsModel)
        {
            var parameters = new NavigationParameters
            {
                {
                    "album",
                    new AlbumsModel
                    { 
                        Images = albumsModel.Images,
                        AlbumName = albumsModel.AlbumName,
                        Artist = albumsModel.Artist
                    }
                }
            };

            await navigationService.NavigateAsync("AlbumSongsPage", parameters);
        }
        #endregion
    }
}
