namespace AvaloniaListBoxBug1.Models
{
    using ReactiveUI;

    public class Contact : ReactiveObject
    {
        public string Jid { get; set; }

        private string _name;

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
    }

}
