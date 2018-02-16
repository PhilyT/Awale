using Awale.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Awale.ViewModels
{
    public class ViewModelMain : ViewModelBase
    {
        private Page page;
        public ViewModelMain(Frame frame)
        {
            page = new MenuView(frame);
           
        }

        public Page Page
        {
            get => page;
            set
            {
                if (Equals(page, value))
                {
                    return;
                }

                this.page = value;
                RaisePropertyChanged("Page");
            }
        }

    }
}
