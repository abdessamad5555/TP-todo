using System.ComponentModel.DataAnnotations;

namespace Todooo.ViewModel
{
    public class Uservm
    {
        [Required(ErrorMessage ="le champ est oblig ")]
   
        public string email {  get; set; }
        [Required(ErrorMessage = "le champ est oblig ")]
        public string password { get; set; }

    }
}
