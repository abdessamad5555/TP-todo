using Todooo.Models;
using Todooo.ViewModel;

namespace Todooo.Mappers
{
    public class Todomapper
    {
        public static Todo GetTodoFromAddvm(TodoAddvm vm)
        {
            /* Todo todo = new Todo();
             todo.libele = vm.libele;
             todo.discription = vm.discription;
             todo.datelimite = vm.datelimite;
             todo.statut = vm.statut;
             return todo;
 */
            //la methode plus simplifier ------>La méthode linéaire
            return new Todo
            {
                libele = vm.libele,
                discription = vm.discription,
                datelimite = vm.datelimite,
                statut = vm.statut,
            };
        }
    }
}
