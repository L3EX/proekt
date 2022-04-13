using GrowingApp.ViewModels;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using GrowingApp.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace GrowingApp.Models
{

    public class ToDo : BaseViewModel
    {
        public Action<ToDo, object> ToDoChanged { get; set; }
        

        public int Id { get; set; }

        private string title;
        public new string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (SetProperty(ref title, value))
                    ToDoChanged?.Invoke(this, value);
            }
        }
        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (SetProperty(ref description, value))
                    ToDoChanged?.Invoke(this, value);
            }
        }
        private DateTime deadline;
        public DateTime Deadline
        {
            get
            {
                return deadline;
            }
            set
            {
                if (SetProperty(ref deadline, value))
                    ToDoChanged?.Invoke(this, value);
            }
        }
        public Xamarin.Forms.Command<ToDo> CardTapped { get; } = new Xamarin.Forms.Command<ToDo>(OnCardTapped);

        private async static void OnCardTapped(ToDo toDo)
        {
            string res = await App.Current.MainPage.DisplayActionSheet("Менеджер карточки", "Отмена", "Завершить", "Удалить");
            switch(res)
            {
                case "Удалить":
                    ItemsViewModel._listController.Remove(toDo);
                    Repository.Remove(toDo.Convert());
                    break;
                case "Завершить":
                    toDo.Status = Status.Completed;
                    Repository.Update(toDo.Convert());
                    break;
            }
        }

        private Status status;
        public Status Status
        {
            get
            {
                return status;
            }
            set
            {
                if (SetProperty(ref status, value))
                    ToDoChanged?.Invoke(this, value);
            }
        }
        public Color PriorityColor => Staff.PColor[priority];
        public Color StatusColor => Staff.SColor[status];
        public Color NStatusColor => Staff.SColor[status].Distract();
        private Priority priority;
        public Priority Priority
        {
            get
            {
                return priority;
            }
            set
            {
                if (SetProperty(ref priority, value))
                    ToDoChanged?.Invoke(this, value);
            }
        }
    }

    
    public class ListController
    {
        
        public ObservableCollection<ToDo> Completed = new ObservableCollection<ToDo>();
        public ObservableCollection<ToDo> ToLLDo = new ObservableCollection<ToDo>();
        public ObservableCollection<ToDo> Delayed = new ObservableCollection<ToDo>();
        public ObservableCollection<ToDo> Failed = new ObservableCollection<ToDo>();

        public List<ToDo> All => Completed.Concat(ToLLDo).Concat(Delayed).Concat(Failed).ToList();

        public void Remove(ToDo toDo)
        {
            switch (toDo.Status)
            {
                case Status.ToDo:
                    ToLLDo.Remove(toDo);
                    break;
                case Status.Completed:
                    Completed.Remove(toDo);
                    break;
                case Status.Delayed:
                    Delayed.Remove(toDo);
                    break;
                case Status.Failed:
                    Failed.Remove(toDo);
                    break;
            }
        }

        public void Add(ToDo todo, bool rep = false)
        {
            if(!rep) Repository.Add(todo.Convert());
            switch (todo.Status)
            {
                case Status.ToDo:
                    AddToLLDo(todo);
                    break;
                case Status.Completed:
                    AddCompleted(todo);
                    break;
                case Status.Delayed:
                    AddDelayed(todo);
                    break;
                case Status.Failed:
                    AddFailed(todo);
                    break;
            }
        }
        
        public void AddRange(List<ToDo> todos, bool rep)
        {
            todos.ForEach(x=>Add(x,rep));
        }

        public void AddCompleted(ToDo todo)
        {
            Completed.Add(todo);
            todo.ToDoChanged += ToDoChanged;
        }
        public void AddToLLDo(ToDo todo)
        {
            ToLLDo.Add(todo);
            todo.ToDoChanged += ToDoChanged;
        }
        public void AddDelayed(ToDo todo)
        {
            Delayed.Add(todo);
            todo.ToDoChanged += ToDoChanged;
        }
        public void AddFailed(ToDo todo)
        {
            Failed.Add(todo);
            todo.ToDoChanged += ToDoChanged;
        }
        public void ToDoChanged(ToDo todo, object property)
        {
            if (property is string || property is DateTime || property is Priority)
            {
                return;
            }
            if (property is Status)
            {
                var status = (Status)property;
                switch (status)
                {
                    case Status.ToDo:
                        if (Completed.Any(x => x == todo)) Completed.Remove(todo);
                        else
                        if (Delayed.Any(x => x == todo)) Delayed.Remove(todo);
                        else
                        if (Failed.Any(x => x == todo)) Failed.Remove(todo); else throw new Exception("ToDo not found, or already in rigt list");
                        ToLLDo.Add(todo);
                        break;
                    case Status.Completed:
                        if (ToLLDo.Any(x => x == todo)) ToLLDo.Remove(todo);
                        else
                        if (Delayed.Any(x => x == todo)) Delayed.Remove(todo);
                        else
                        if (Failed.Any(x => x == todo)) Failed.Remove(todo); else throw new Exception("ToDo not found, or already in rigt list");
                        Completed.Add(todo);
                        break;
                    case Status.Delayed:
                        if (Completed.Any(x => x == todo)) Completed.Remove(todo);
                        else
                        if (ToLLDo.Any(x => x == todo)) ToLLDo.Remove(todo);
                        else
                        if (Failed.Any(x => x == todo)) Failed.Remove(todo); else throw new Exception("ToDo not found, or already in rigt list");
                        Delayed.Add(todo);
                        break;
                    case Status.Failed:
                        if (Completed.Any(x => x == todo)) Completed.Remove(todo);
                        else
                        if (Delayed.Any(x => x == todo)) Delayed.Remove(todo);
                        else
                        if (ToLLDo.Any(x => x == todo)) ToLLDo.Remove(todo); else throw new Exception("ToDo not found, or already in rigt list");
                        Failed.Add(todo);
                        break;
                }
            }

        }
    }
}

