using Prism.Events;
using Xamarin.Forms;

namespace SpotifyApp.Helpers.CustomControls
{
    public class CustomCollectionView : CollectionView
    {
        public CustomCollectionView()
        {

        }

        private IEventAggregator eventAggregator;
        public CustomCollectionView(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        protected override void OnScrolled(ItemsViewScrolledEventArgs e)
        {
            base.OnScrolled(e);
            //eventAggregator.GetEvent<BlurEventAggregator>().Publish(0.01f + ((float)e.VerticalOffset * 0.1f));
        }
    }
}
