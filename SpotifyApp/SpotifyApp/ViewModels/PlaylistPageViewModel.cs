using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SpotifyApp.Helpers.Dependencies;
using SpotifyApp.Models;

namespace SpotifyApp.ViewModels
{
    public class PlaylistPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IToast toast;
        public PlaylistPageViewModel(INavigationService navigationService, IToast toast) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.toast = toast;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var submittedParameter = parameters.GetValue<AlbumsModel>("playlist");
            songID = submittedParameter.SongId;

            Playlists = new ObservableCollection<Playlist>(await QueryData().GetUserPlaylists(1));

            Parallel.For(0, Playlists.Count(), i =>
            {
                Playlists.ElementAt(i).AddSongToPlaylistCommand = new DelegateCommand<Playlist>(async (song) => await AddSongToPlaylist(song));
            });
        }

        #region Properties

        private int songID;

        private IEnumerable<Playlist> playlist;
        public IEnumerable<Playlist> Playlists
        {
            get { return playlist; }
            set { SetProperty(ref playlist, value); }
        }
        #endregion

        #region Methods

        private async Task AddSongToPlaylist(Playlist playlist)
        {
            var songToAdd = new AddPlaylistSongModel
            {
                PlaylistID = playlist.PlaylistID,
                SongID = songID,
                UserID = 1
            };

            await QueryData().AddSongToPlaylist(songToAdd);
            toast.ShowToast($"Added to {playlist.PlaylistName}");
        }
        #endregion
    }
}
