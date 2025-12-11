using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using Todooo.Mappers;
using Todooo.Models;
using Todooo.Services;
using Todooo.ViewModel;

namespace Todooo.Controllers
{
    public class TodoController : Controller
    {
        private readonly ISessionManagerService _session;

        public TodoController(ISessionManagerService session)
        {
            _session = session;
        }

        
        public IActionResult Index()
        {
            string todoJson = HttpContext.Session.GetString("todo");
            List<Todo> list = (todoJson == null)
                ? new List<Todo>()
                : JsonSerializer.Deserialize<List<Todo>>(todoJson);

            return View(list);
        }

        
        [HttpGet]
        public IActionResult Add()
        {
            if (HttpContext.Session.GetString("user") == null)
                return RedirectToAction("Login", "User");

            return View(new TodoAddvm());
        }

        [HttpPost]
        public IActionResult Add(TodoAddvm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            string todoJson = HttpContext.Session.GetString("todo");
            List<Todo> list = (todoJson == null)
                ? new List<Todo>()
                : JsonSerializer.Deserialize<List<Todo>>(todoJson);

            Todo todo = Todomapper.GetTodoFromAddvm(vm);
            list.Add(todo);

            _session.add("todo", list, HttpContext);

            return RedirectToAction("Index"); 
        }

        
        [HttpPost]
        public IActionResult Delete(int index)
        {
            string todoJson = HttpContext.Session.GetString("todo");
            if (todoJson != null)
            {
                var list = JsonSerializer.Deserialize<List<Todo>>(todoJson);
                if (index >= 0 && index < list.Count)
                {
                    list.RemoveAt(index);
                    _session.add("todo", list, HttpContext);
                }
            }
            return RedirectToAction("Index");
        }

        
        [HttpGet]
        public IActionResult Edit(int index)
        {
            string todoJson = HttpContext.Session.GetString("todo");
            if (todoJson != null)
            {
                var list = JsonSerializer.Deserialize<List<Todo>>(todoJson);
                if (index >= 0 && index < list.Count)
                {
                    var todo = list[index];
                    var vm = new TodoAddvm
                    {
                        libele = todo.libele,
                        discription = todo.discription,
                        datelimite = todo.datelimite,
                        statut = todo.statut
                    };
                    ViewBag.Index = index; // Pour identifier quel Todo modifier
                    return View(vm);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(int index, TodoAddvm vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Index = index;
                return View(vm);
            }

            string todoJson = HttpContext.Session.GetString("todo");
            if (todoJson != null)
            {
                var list = JsonSerializer.Deserialize<List<Todo>>(todoJson);
                if (index >= 0 && index < list.Count)
                {
                    list[index].libele = vm.libele;
                    list[index].discription = vm.discription;
                    list[index].datelimite = vm.datelimite;
                    list[index].statut = vm.statut;

                    _session.add("todo", list, HttpContext);
                }
            }

            return RedirectToAction("Index"); 
        }
    }
}
