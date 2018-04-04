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
    public class ViewModelSelectionLocal : ViewModelBase
    {
        Frame frame;
        private DelegateCommand retour;
        private DelegateCommand ajouter;
        private DelegateCommand commencer;
        private string existe;
        private string nouveauJoueur;
        private bool activeAjout;
        private ObservableCollection<Player> selection1;
        private ObservableCollection<Player> selection2;
        private Sauvegarde sauvegarde;
        private Player player1;
        private Player player2;
        private ObservableCollection<Player> joueurs;
        public ViewModelSelectionLocal(Frame frame)
        {
            this.frame = frame;
            retour = new DelegateCommand(o => OnClickRetour(o));
            ajouter = new DelegateCommand(o => OnClickAjouter(o));
            commencer = new DelegateCommand(o => OnClickCommencer(o));
            sauvegarde = new Sauvegarde();
            existe = "Hidden";
            activeAjout = false;
            joueurs = sauvegarde.ReadXML();
            selection1 = joueurs;
            if(selection1.Count > 0)
            {
                player1 = selection1.First();
            }
            IEnumerable<Player> numQuery =
                                        from joueur in joueurs
                                        where joueur.Nom != Player1.Nom
                                        select joueur;
            selection2 = new ObservableCollection<Player>();
            foreach (Player joueur in numQuery)
            {
                selection2.Add(joueur);
            }
            if (selection2.Count > 0)
            {
                Player2 = selection2.First();
            }
        }

        private void OnClickCommencer(object o)
        {
            player1.TourDeJeu = true;
            GameView game = new GameView(frame, player1, player2);
            frame.Navigate(game);
        }

        private void OnClickAjouter(object o)
        {
            joueurs.Add(new Player(nouveauJoueur));
            NouveauJoueur = "";
            sauvegarde.WriteXML(joueurs);
            Selection1 = joueurs;
            if (player1 == null  && selection1.Count > 0)
            {
                player1 = selection1.First();
            }
            IEnumerable<Player> numQuery =
                                        from joueur in joueurs
                                        where joueur.Nom != Player1.Nom
                                        select joueur;
            Selection2.Clear();
            foreach (Player joueur in numQuery)
            {
                Selection2.Add(joueur);
            }
            if (player2 == null && selection2.Count > 0)
            {
                Player2 = selection2.First();
            }
        }

        private void OnClickRetour(object o)
        {
            LocalView menu = new LocalView(frame);
            frame.Navigate(menu);
        }



        public DelegateCommand Retour { get => retour; set => retour = value; }
        public DelegateCommand Commencer { get => commencer; set => commencer = value; }
        public DelegateCommand Ajouter { get => ajouter; set => ajouter = value; }
        public string Existe { get => existe;
            set
            {
                existe = value;
                RaisePropertyChanged("Existe");
                ActiveAjout = !String.IsNullOrEmpty(nouveauJoueur) && existe.Equals("Hidden");
            }
        }
        public string NouveauJoueur { get => nouveauJoueur;
            set
            {
                nouveauJoueur = value;
                RaisePropertyChanged("NouveauJoueur");
                if(joueurs.Where(joueur=>joueur.Nom==value).ToList().Count > 0)
                {
                    Existe = "Visible";
                }
                else
                {
                    Existe = "Hidden";
                }
            }
        }
        public bool ActiveAjout
        {
            get
            {
                return activeAjout;
            }
            set
            {
                activeAjout = value;
                RaisePropertyChanged("ActiveAjout");
            }
        }

        public ObservableCollection<Player> Selection1 { get => selection1;
            set
            {
                selection1 = value;
                RaisePropertyChanged("Selection1");
            }
        }
        public ObservableCollection<Player> Selection2 { get => selection2;
            set
            {
                selection2 = value;
                RaisePropertyChanged("Selection2");
            }
        }

        public Player Player1 { get => player1;
            set
            {
                if (player1 != null)
                {
                    Selection2.Add(player1);
                }
                player1 = value;
                Selection2.Remove(value);
                RaisePropertyChanged("Player1");
            }
        }
        public Player Player2 { get => player2;
            set
            {                
                if(player2 != null)
                {
                    Selection1.Add(player2);
                }
                player2 = value;
                Selection1.Remove(value);
                RaisePropertyChanged("Player2");
            }
        }
    }
}
