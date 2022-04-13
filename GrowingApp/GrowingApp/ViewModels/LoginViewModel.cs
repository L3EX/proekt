using GrowingApp.Views;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GrowingApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command<Button>(OnLoginClicked);
        }

        private async void OnLoginClicked(Button button)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Task.WhenAll(
                button.RotateTo(1080, 400), 
                button.TranslateTo(800, 0, 400)
            );
        

            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            await button.ScaleTo(1, 1);
            await button.RotateTo(0, 1);
            await button.TranslateTo(0, 0, 1);
        }
    }
}
