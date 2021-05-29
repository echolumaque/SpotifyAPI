using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace SpotifyApp.ViewModels
{
    public class NewPlaylistPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        public NewPlaylistPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
        }
    }
}
