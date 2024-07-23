using System.ComponentModel.DataAnnotations;

namespace RengoMediaTask.Data.DomianModels
{
    public class Reminder
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}
