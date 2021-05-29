using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApp.Helpers.API
{
    public class ModifiedObservableCollection<T> : ObservableCollection<T>
    {
        public ModifiedObservableCollection() : base() { }

        public ModifiedObservableCollection(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            CopyFrom(collection);
        }

        public ModifiedObservableCollection(List<T> list) : base((list != null) ? new List<T>(list.Count) : list)
        {

            CopyFrom(list);
        }

        private void CopyFrom(IEnumerable<T> collection)
        {
            IList<T> items = Items;
            if (collection != null && items != null)
            {
                using (IEnumerator<T> enumerator = collection.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        items.Add(enumerator.Current);
                    }
                }
            }
        }


        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            Parallel.For(0, collection.Count(), i =>
            {
                Items.Add(collection.ElementAt(i));
            });

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
