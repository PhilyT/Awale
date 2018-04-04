using Awale.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Awale.Views
{
    /// <summary>
    /// Logique d'interaction pour SelectionLocal.xaml
    /// </summary>
    public partial class SelectionLocal : Page
    {
        public SelectionLocal(Frame frame)
        {
            InitializeComponent();
            DataContext = new ViewModelSelectionLocal(frame);
        }
    }
}
