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
        //private RelayCommand relayCommand;
        private Page page;
        //public RelayCommand RelayCommand { get => relayCommand; set => relayCommand = value; }
        public ViewModelMain(Frame frame)
        {
            page = new MenuView(frame);

            //this.RelayCommand = new RelayCommand(o => OpenGameClick("MainButton"));
        }

        //private void OpenGameClick(object v)
        //{
        //    throw new NotImplementedException();
        //}
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
