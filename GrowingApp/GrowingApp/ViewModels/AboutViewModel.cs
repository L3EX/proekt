using GrowingApp.Models;

using System;
using System.Windows.Input;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace GrowingApp.ViewModels
{

    public class AboutViewModel : BaseViewModel
    {
        public SCounter Counter {get;set;}
        public AboutViewModel()
        {
            Counter = new SCounter();
            Title = "About";
            Plus = new Command(()=>
            {
                Counter.Add(1);
            });
            Minus = new Command(() =>
            {
                Counter.Add(-7);
            });
            ZXC = new Command(() =>
            {
                Counter.Add(1000);
            });
        }      
        


        
        public ICommand Plus { get; }
        public ICommand Minus { get; }
        public ICommand ZXC { get; }
        
    }
}