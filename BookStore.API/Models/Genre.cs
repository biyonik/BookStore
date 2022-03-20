using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.API.Models
{
    public class Genre
    {
        public Genre()
        {
            Books = new List<Book>();
        }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name {get; set;}
        public bool IsActive {get; set;} = true;

        public virtual ICollection<Book> Books { get; set; }
    }
}