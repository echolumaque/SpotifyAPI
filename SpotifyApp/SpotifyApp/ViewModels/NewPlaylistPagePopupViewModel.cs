using System;
using System.Collections;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using SpotifyApp.Helpers.Dependencies;
using SpotifyApp.Models;

namespace SpotifyApp.ViewModels
{
    public class NewPlaylistPagePopupViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IToast toast;

        public NewPlaylistPagePopupViewModel(INavigationService navigationService, IToast toast) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.toast = toast;
            AddPlaylistCommand = new DelegateCommand(async () => await AddNewSongToPlaylist());
            PopStackCommand = new DelegateCommand(async () => await this.navigationService.ClearPopupStackAsync());
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var submittedParameters = parameters.GetValue<AlbumsModel>("songAndPlaylist");
            songId = submittedParameters.SongId;
            playlistImage = submittedParameters.Images;
        }

        #region Properties

        private string playlistName;
        public string PlaylistName
        {
            get { return playlistName; }
            set { SetProperty(ref playlistName, value); }
        }

        private string playlistImage;
        private int songId;

        public DelegateCommand AddPlaylistCommand { get; set; }
        public DelegateCommand PopStackCommand { get; set; }
        #endregion

        #region Methods

        private async Task AddNewSongToPlaylist() //adds new playlist then adds the song frm the enwly created playlist.
        {
            try
            {
                var playlistToAdd = new AddNewPlaylistModel
                {
                    PlaylistName = PlaylistName,
                    PlaylistImage = playlistImage,
                    UserID = 1
                };

                var songToAddToNewCreatedPlaylist = new AddNewPlaylistSongModel
                {
                    SongID = songId,
                    UserID = 1,
                };

                var arrayList = new ArrayList
                {
                    playlistToAdd,
                    songToAddToNewCreatedPlaylist
                };

                await QueryData().AddNewPlaylist(arrayList);
                toast.ShowToast($"Added to {playlistToAdd.PlaylistName}");

                await navigationService.ClearPopupStackAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}