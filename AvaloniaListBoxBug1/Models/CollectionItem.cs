namespace AvaloniaListBoxBug1.Models
{
    using DynamicData;
    using ReactiveUI;

    public class CollectionItem<TObject, TKey> : ReactiveObject
    {
        /// <summary>
        /// Unique Id of the item
        /// </summary>
        public TKey Id { get; set; }
        public SourceCache<TObject, TKey> Source { get; set; }
    }
}
