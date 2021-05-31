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
    public class PlaylistPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        public PlaylistPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var submittedParameter = parameters.GetValue<AlbumsModel>("playlist");
            var rawData = await QueryData().GetUserPlaylists(3);

            Playlists = new ObservableCollection<Playlist>(rawData);
        }

        #region Properties

        private IEnumerable<Playlist> playlist;
        public IEnumerable<Playlist> Playlists
        {
            get { return playlist; }
            set { SetProperty(ref playlist, value); }
        }
        #endregion

        #region Methods
        #endregion
    }
}
