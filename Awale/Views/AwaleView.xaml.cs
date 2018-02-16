using Awale.Models;
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
    /// Logique d'interaction pour Awale.xaml
    /// </summary>
    public partial class AwaleView : UserControl
    {
        public static readonly DependencyProperty GameProperty =
             DependencyProperty.Register("Game", typeof(Game),
            typeof(AwaleView),new PropertyMetadata(propertychanged));
        ViewModelAwale vm;


        public AwaleView()
        {
            InitializeComponent();
            vm = new ViewModelAwale(Game);
            DataContext = vm;
        }

        public Game Game
        {
            get { return (Game)GetValue(GameProperty); }
            set { SetValue(GameProperty, value); }
        }

        public static void propertychanged(object sender, DependencyPropertyChangedEventArgs evt)
        {
            AwaleView view = (AwaleView)sender;
            view.vm.Game = (Game)evt.NewValue;
        }

    }
}
