namespace AvaloniaListBoxBug1
{
    using System;    
    using System.Collections.ObjectModel;
    using System.Linq;
    using ReactiveUI;
    using AvaloniaListBoxBug1.Models;
    using DynamicData.Binding;    
    using System.Reactive.Linq;    
    using DynamicData;

    public class MainWindowViewModel : ReactiveObject
    {        
        readonly ReadOnlyObservableCollection<Contact> _filteredContactsObservable;
        readonly Contacts _contacts = new Contacts();

        public MainWindowViewModel()
        {
            BuildDummyContacts();

            Func<Contact, bool> searchFunc(string text)
            {
                if (string.IsNullOrEmpty(text))
                    return _ => true;

                return c => c.Name != null && c.Name.ToUpper().Contains(text.ToUpper())
                            || c.Jid.ToUpper().Contains(text.ToUpper());
            }

            var contactSearchTextFilter = this.WhenValueChanged(@this => @this.SearchFilter)
                .Throttle(TimeSpan.FromMilliseconds(500))
                .Select(searchFunc);

            _contacts
                .Connect()                
                .Filter(contactSearchTextFilter)
                .Sort(
                    SortExpressionComparer<Contact>
                        .Ascending(c => (c.Name + c.Jid).ToUpper())
                )
                .Bind(out _filteredContactsObservable)
                .Subscribe();
        }

        public ReadOnlyObservableCollection<Contact> FilteredContacts => _filteredContactsObservable;

        private string _searchFilter;

        public string SearchFilter
        {
            get => _searchFilter;
            set => this.RaiseAndSetIfChanged(ref _searchFilter, value);
        }


        private void BuildDummyContacts()
        {
            _contacts.Add(new Contact() { Name = "Romeo", Jid = "romeo@jabber.org" });          
            _contacts.Add(new Contact() { Name = "Alex", Jid = "alex@jabber.org" });
            _contacts.Add(new Contact() { Name = "Alexander Artie", Jid = "alexander_artie@jabber.org" });
            _contacts.Add(new Contact() { Name = "Fabian Loren", Jid = "fabian_loren@jabber.org" });
            _contacts.Add(new Contact() { Name = "Kayly Declan", Jid = "kayly_declan@jabber.org" });
            _contacts.Add(new Contact() { Name = "Liam Kerry", Jid = "liam_kerry@jabber.org" });
            _contacts.Add(new Contact() { Name = "Katheryn Samantha", Jid = "katheryn_samantha@jabber.org" });
            _contacts.Add(new Contact() { Name = "Dom Laverne", Jid = "dom_laverne@jabber.org" });
            _contacts.Add(new Contact() { Name = "Ritchie Ciera", Jid = "ritchie_ciera@jabber.org" });
            _contacts.Add(new Contact() { Name = "Harleigh Breanne", Jid = "harleigh_breanne@jabber.org" });
            _contacts.Add(new Contact() { Name = "Earl Tristram", Jid = "earl_tristram@jabber.org" });
            _contacts.Add(new Contact() { Name = "Ron Christiana", Jid = "ron_christiana@jabber.org" });
        }

    }
}
