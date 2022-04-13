using GrowingApp.ViewModels;

using System;
using System.Collections.Generic;
using System.Text;

namespace GrowingApp.Models
{
    public class SCounter : BaseViewModel
    {
        private int counter = 0;

        public string Counter
        {
            get { return counter.ToString(); }
            set
            {                
                if (int.TryParse(value, out counter))
                {
                    OnPropertyChanged();
                }
            }
        }

        public void Add(int num)
        {
            Counter=(counter+num).ToString();
        }
    }
}
