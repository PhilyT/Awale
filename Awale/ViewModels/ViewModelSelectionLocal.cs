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
            joueurs = new ObservableCollection<Player>(sauvegarde.ReadXML().OrderBy(joueur => joueur.Nom));
            Selection1 = new ObservableCollection<Player>(joueurs);
            if(selection1.Count > 0)
            {
                Player1 = selection1.First();
                Selection2 = new ObservableCollection<Player>(joueurs.Where(joueur => !joueur.Nom.Equals(player1.Nom)));
                if (selection2.Count > 0)
                {
                    Player2 = selection2.First();
                }
            }
            else
            {
                selection2 = new ObservableCollection<Player>();
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
            joueurs = new ObservableCollection<Player>(joueurs.OrderBy(joueur => joueur.Nom));
            NouveauJoueur = "";
            sauvegarde.WriteXML(joueurs);
            string nomjoueur1 = "";
            string nomjoueur2 = "";
            if(player1 != null)
            {
                nomjoueur1 = player1.Nom;
            }
            if(player2 != null)
            {
                nomjoueur2 = player2.Nom;
            }
            Selection1 = new ObservableCollection<Player>(joueurs.Where(joueur=>!joueur.Nom.Equals(nomjoueur2)));
            if (!String.IsNullOrEmpty(nomjoueur1))
            {
                Player1 = Selection1.First(joueur => joueur.Nom.Equals(nomjoueur1));
            }
            else
            {
                Player1 = Selection1.First();
            }
            Selection2 = new ObservableCollection<Player>(joueurs.Where(joueur => !joueur.Nom.Equals(Player1.Nom)));
            if (!String.IsNullOrEmpty(nomjoueur2))
            {
                Player2 = Selection2.First(joueur => joueur.Nom.Equals(nomjoueur2));
            }
            else if(Selection2.Count>0)
            {
                Player2 = Selection2.First();
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
                player1 = value;
                RaisePropertyChanged("Player1");
                if (player2 != null && value != null)
                {
                    string nomjoueur2 = player2.Nom;
                    Selection2 = new ObservableCollection<Player>(joueurs.Where(joueur => !joueur.Nom.Equals(value.Nom)));
                    player2 = Selection2.First(joueur => joueur.Nom.Equals(nomjoueur2));
                }
            }
        }
        public Player Player2 { get => player2;
            set
            {
                player2 = value;
                RaisePropertyChanged("Player2");
                if (player1 != null && value != null)
                {
                    string nomjoueur1 = player1.Nom;
                    Selection1 = new ObservableCollection<Player>(joueurs.Where(joueur => !joueur.Nom.Equals(value.Nom)));
                    player1 = Selection1.First(joueur => joueur.Nom.Equals(nomjoueur1));
                }
            }
        }
    }
}
