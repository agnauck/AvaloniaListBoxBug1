namespace AvaloniaListBoxBug1.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using DynamicData;
    using ReactiveUI;

    public abstract class Collection<TObject, TKey> : ReactiveObject
    {
        private readonly SourceCache<TObject, TKey> _source;
        private readonly ReadOnlyObservableCollection<TObject> _items;

        protected Collection(Func<TObject, TKey> keySelector)
        {
            _source = new SourceCache<TObject, TKey>(keySelector);
            _source
                .Connect()
                .Bind(out _items)
                .Subscribe();
        }

        public ReadOnlyObservableCollection<TObject> Items => _items;

        public void Clear()
        {
            _source.Clear();
        }

        public void Add(TObject item)
        {
            if (item is CollectionItem<TObject, TKey> i)
            {
                i.Source = _source;
            }
            _source.AddOrUpdate(item);
        }

        public void Remove(TKey key)
        {
            _source.Remove(key);
        }

        public bool Contains(TKey key)
        {
            return _source.Keys.Contains(key);
        }

        public TObject Get(TKey key)
        {
            return _source.Lookup(key).Value;
        }

        public IObservable<IChangeSet<TObject, TKey>> Connect(Func<TObject, bool> predicate = null) => _source.Connect();
    }
}