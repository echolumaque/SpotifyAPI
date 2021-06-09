using Prism.Events;
using SpotifyApp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

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

        private double ReMap(double oldValue, double oldMin, double oldMax, double newMin, double newMax)
        {
            return (((oldValue - oldMin) / (oldMax - oldMin)) * (newMax - newMin)) + newMin;
        }

       
        private void CollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            eventAggregator.GetEvent<HeightEventAggregator>().Publish(920 - (e.VerticalOffset * 2));
            panel.Opacity = ReMap(e.VerticalOffset, 0, 190, 1, 0) * 2;
        }

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            eventAggregator.GetEvent<HeightEventAggregator>().Publish(920 - e.ScrollY);
        }
    }
}
