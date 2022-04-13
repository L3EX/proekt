using GrowingApp.Models;
using GrowingApp.Views;

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GrowingApp.Services;
using Xamarin.Forms;

namespace GrowingApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        
        public static ListController _listController;
        
        
        public Command SelectToDo { get; }
        public Command SelectCompleted { get; }
        public Command SelectFailed { get; }
        public Command SelectDelayed { get; }

        public Command AddItemCommand { get; }
        public ObservableCollection<ToDo> Completed => _listController.Completed;
        public ObservableCollection<ToDo> ToLLDo => _listController.ToLLDo;
        public ObservableCollection<ToDo> Delayed => _listController.Delayed;
        public ObservableCollection<ToDo> Failed => _listController.Failed;
        public ObservableCollection<ToDo> CurrentToDoList { get; set; }
        public ItemsViewModel()
        {
            _listController = new ListController();
            CurrentToDoList = ToLLDo;           
            Title = "Browse";
            //_listController.Add(new ToDo() { Id=Staff.GetId(), Title = "New Delayed", Description = "Test Description", Priority = Priority.ForFreeTime, Status = Status.Delayed });
            //_listController.Add(new ToDo() { Id = Staff.GetId(), Title = "New Failed", Description = "Test Description", Priority = Priority.Normal, Status = Status.Failed });
            _listController.AddRange(Repository.LoadAll().Select(x=>x.Convert()).ToList(), true);

            AddItemCommand = new Command(OnAddItem);

            SelectToDo = new Command(() =>
            {
                CurrentToDoList = ToLLDo;               
                OnPropertyChanged("CurrentToDoList");
            });
            SelectCompleted = new Command(() =>
            {
                CurrentToDoList = Completed;
                OnPropertyChanged("CurrentToDoList");
            });
            SelectDelayed = new Command(() =>
            {
                CurrentToDoList = Delayed;
                OnPropertyChanged("CurrentToDoList");
            });
            SelectFailed = new Command(() =>
            {
                CurrentToDoList = Failed;
                OnPropertyChanged("CurrentToDoList");
            });
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

    }
}