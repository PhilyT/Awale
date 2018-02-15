using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Player : INotifyPropertyChanged
    {
        private string nom;
        private int id;
        private int nbVictoire;
        private int nbPartie;
        private string ip;

        public string Nom { get => nom; set { nom = value; RaisePropertyChanged("Nom"); } }
        public int Id { get => id; set { id = value; RaisePropertyChanged("Id"); } }
        public int NbVictoire { get => nbVictoire; set { nbVictoire = value; RaisePropertyChanged("NbVictoire"); } }
        public int NbPartie { get => nbPartie; set { nbPartie = value; RaisePropertyChanged("NbPartie"); } }
        public string Ip { get => ip; set => ip = value; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Player() { }

    }
}
