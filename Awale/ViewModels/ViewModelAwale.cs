using Awale.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Awale.ViewModels
{
    public class ViewModelAwale : ViewModelBase
    {
        private int trou1Adverse;
        private int trou2Adverse;
        private int trou3Adverse;
        private int trou4Adverse;
        private int trou5Adverse;
        private int trou6Adverse;
        private int trou1;
        private int trou2;
        private int trou3;
        private int trou4;
        private int trou5;
        private int trou6;
        private DelegateCommand delegateCommand;

        public ViewModelAwale()
        {
            trou1 = 4;
            trou2 = 4;
            trou3 = 4;
            trou4 = 4;
            trou5 = 4;
            trou6 = 4;
            trou1Adverse = 4;
            trou2Adverse = 4;
            trou3Adverse = 4;
            trou4Adverse = 4;
            trou5Adverse = 4;
            trou6Adverse = 4;
            DelegateCommand = new DelegateCommand(o => OnClickTrou(o));
        }

        private void OnClickTrou(object o)
        {
            MessageBox.Show("Commande Fontionnelle.", "Test", MessageBoxButton.OK);
        }

        public int Trou1Adverse
        {
            get => trou1Adverse;
            set
            {
                trou1Adverse = value;
                RaisePropertyChanged("Trou1Adverse");
            }
        }
        public int Trou2Adverse
        {
            get => trou2Adverse;
            set
            {
                trou2Adverse = value;
                RaisePropertyChanged("Trou2Adverse");
            }
        }
        public int Trou3Adverse
        {
            get => trou3Adverse;
            set
            {
                trou3Adverse = value;
                RaisePropertyChanged("Trou3Adverse");
            }
        }
        public int Trou4Adverse
        {
            get => trou4Adverse;
            set
            {
                trou4Adverse = value;
                RaisePropertyChanged("Trou4Adverse");
            }
        }
        public int Trou5Adverse
        {
            get => trou5Adverse;
            set
            {
                trou5Adverse = value;
                RaisePropertyChanged("Trou5Adverse");
            }
        }
        public int Trou6Adverse
        {
            get => trou6Adverse;
            set
            {
                trou6Adverse = value;
                RaisePropertyChanged("Trou6Adverse");
            }
        }
        public int Trou1
        {
            get => trou1;
            set
            {
                trou1 = value;
                RaisePropertyChanged("Trou1");
            }
        }
        public int Trou2
        {
            get => trou2;
            set
            {
                trou2 = value;
                RaisePropertyChanged("Trou2");
            }
        }
        public int Trou3
        {
            get => trou3;
            set
            {
                trou3 = value;
                RaisePropertyChanged("Trou3");
            }
        }
        public int Trou4
        {
            get => trou4;
            set
            {
                trou4 = value;
                RaisePropertyChanged("Trou4");
            }
        }
        public int Trou5
        {
            get => trou5;
            set
            {
                trou5 = value;
                RaisePropertyChanged("Trou5");
            }
        }
        public int Trou6
        {
            get => trou6;
            set
            {
                trou6 = value;
                RaisePropertyChanged("Trou6");
            }
        }
        public DelegateCommand DelegateCommand { get => delegateCommand; set => delegateCommand = value; }
    }
}
