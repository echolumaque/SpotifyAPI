using Prism.Navigation;

namespace SpotifyApp.ViewModels
{
    public class MainTabbedPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        public MainTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
        }
    }
}
