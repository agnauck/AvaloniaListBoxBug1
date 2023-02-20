namespace AvaloniaListBoxBug1.Models
{
    public class Contacts : Collection<Contact, string>
    {
        public Contacts() : base(c => c.Jid)
        {
        }
    }
}
