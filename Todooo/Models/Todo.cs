using System.ComponentModel.DataAnnotations;
using Todooo.Enum;

namespace Todooo.Models
{
    public class Todo
    {
        public string libele { get; set; }
        
        public string discription { get; set; }
  
        public DateTime datelimite { get; set; }
  
        public State statut { get; set; }
    }
}
