using Prism.Events;
using Xamarin.Forms;

namespace SpotifyApp.Views
{
    public partial class ArtistPage : ContentPage
    {
        private readonly IEventAggregator eventAggregator;
        public ArtistPage(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            this.eventAggregator = eventAggregator;
        }
        //private double ReMap(double oldValue, double oldMin, double oldMax, double newMin, double newMax)
        //{
        //    return (((oldValue - oldMin) / (oldMax - oldMin)) * (newMax - newMin)) + newMin;//starts in 1
        //    //ReMap(e.VerticalOffset, 0, 190, 1, 0)
        //}
    }
}
