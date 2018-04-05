using Awale.Models;
using Awale.Utils;
using Awale.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Awale.ViewModels
{
    public class ViewModelScore : ViewModelBase
    {
        Frame frame;
        private DelegateCommand retour;
        private ObservableCollection<Player> joueurs;
        public ViewModelScore(Frame frame)
        {
            this.frame = frame;
            retour = new DelegateCommand(o => OnClickRetour(o));
            joueurs = new Sauvegarde().ReadXML();
            joueurs = new ObservableCollection<Player>(joueurs.OrderByDescending(joueur => joueur.NbVictoire));
        }
        private void OnClickRetour(object o)
        {
            MenuView menu = new MenuView(frame);
            frame.Navigate(menu);
        }
        public DelegateCommand Retour { get => retour; set => retour = value; }
        public ObservableCollection<Player> Joueurs { get => joueurs;
            set
            {
                joueurs = value;
                RaisePropertyChanged("Joueurs");
            }
        }
    }
}
