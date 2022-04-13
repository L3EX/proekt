using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GrowDBContext;

namespace GrowingApp.Services
{
    public static class Repository
    {
        public static void Remove(ToDo toDo)
        {
            using (var growContext = new GrowContext())
            {

                growContext.ToDos.Remove(toDo);
                growContext.SaveChanges();
            }
        }

        public static void Add(ToDo toDo)
        {
            using (var growContext = new GrowContext())
            {
                growContext.ToDos.Add(toDo);
                growContext.SaveChanges();
            }

        }

        public static void Update(ToDo toDo)
        {
            using (var growContext = new GrowContext())
            {
                growContext.ToDos.Update(toDo);
                growContext.SaveChanges();
            }
        }

        public static List<ToDo> LoadAll()
        {
            using (var growContext = new GrowContext())
            {
                growContext.Database.EnsureCreated();
                return growContext.ToDos.ToList();
            }
        }
    }
}
