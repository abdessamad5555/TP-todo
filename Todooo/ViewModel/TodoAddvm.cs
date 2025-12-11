using System.ComponentModel.DataAnnotations;
using Todooo.Enum;

namespace Todooo.ViewModel
{
    public class TodoAddvm
    {
        [Required(ErrorMessage ="oblig")]
        public string libele { get; set; }
        [Required(ErrorMessage ="oblig")]
        public string discription { get; set; }
        [Required(ErrorMessage = "oblig")]
        [DataType(DataType.Date)]
        public DateTime datelimite { get; set; }
        [Required]
        public State statut {  get; set; }

    }
}
