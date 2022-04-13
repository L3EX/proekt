using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using GrowDBContext;


namespace GrowingApp.Services
{
    public enum Status
    {
        ToDo,
        Completed,
        Delayed,
        Failed
    }
    public enum Priority
    {
        DoOrDead,
        Important,
        Normal,
        ForFreeTime
    }
    
    public static class Staff
    {
        public static int GetId()
        {
            var res = new Random(DateTime.Now.Millisecond * 42487).Next(140000, 9999999) + (int)DateTime.Now.Millisecond * 2451;
            return res;
        }

        public static ToDo Convert(this Models.ToDo toDo)
        {
            ToDo nToDo = new ToDo()
            {
                Id = toDo.Id,
                Title = toDo.Title,
                Description = toDo.Description,
                Status = (int)toDo.Status,
                Priority = (int)toDo.Priority
            };
            return nToDo;
        }
        public static Models.ToDo Convert(this ToDo toDo)
        {
            Models.ToDo nToDo = new Models.ToDo()
            {
                Id = toDo.Id,
                Title = toDo.Title,
                Description = toDo.Description,
                Status = (Status)toDo.Status,
                Priority = (Priority)toDo.Priority
            };
            return nToDo;
        }
        public static Color Distract(this Color color2)
        {
            Color color = Color.FromArgb(255 - color2.R, 255 - color2.G, 255 - color2.B);
            return color;
        }

        public static readonly Dictionary<Priority, Color> PColor = new Dictionary<Priority, Color>()
        {
            { Priority.DoOrDead, Color.FromArgb(86,10,10) },
            { Priority.Important, Color.FromArgb(94, 139, 15) },
            { Priority.Normal, Color.FromArgb(113,113,12) },
            { Priority.ForFreeTime, Color.FromArgb(12,113,12) }
        };

        public static readonly Dictionary<Status, Color> SColor = new Dictionary<Status, Color>()
        {
            { Status.Failed, Color.FromArgb(86, 10, 10) },
            { Status.ToDo, Color.FromArgb(94,139,15) },
            { Status.Delayed, Color.FromArgb(113, 113, 12) },
            { Status.Completed, Color.FromArgb(12, 113, 12) }
        };
    }
}
