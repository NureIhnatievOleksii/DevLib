using DevLib.Domain.BookAggregate;
using DevLib.Domain.UserAggregate;

namespace DevLib.Domain.NotesAggregate
{
    public class Note
    {
        public Guid NoteId { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public string Text { get; set; }

        public User User { get; set; }
        public Book Book { get; set; }
    }
}
