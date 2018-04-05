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
    public class ViewModelSelectionIA : ViewModelBase
    {
        Frame frame;
        private DelegateCommand retour;
        private DelegateCommand ajouter;
        private DelegateCommand commencer;
        private string existe;
        private string nouveauJoueur;
        private bool activeAjout;
        private ObservableCollection<Player> selection1;
        private Sauvegarde sauvegarde;
        private Player player1;
        private ObservableCollection<Player> joueurs;
        public ViewModelSelectionIA(Frame frame)
        {
            this.frame = frame;
            retour = new DelegateCommand(o => OnClickRetour(o));
            ajouter = new DelegateCommand(o => OnClickAjouter(o));
            commencer = new DelegateCommand(o => OnClickCommencer(o));
            sauvegarde = new Sauvegarde();
            existe = "Hidden";
            activeAjout = false;
            joueurs = new ObservableCollection<Player>(sauvegarde.ReadXML().OrderBy(joueur=>joueur.Nom));
            selection1 = new ObservableCollection<Player>(joueurs);
            if (selection1.Count > 0)
            {
                player1 = selection1.First();
            }
        }

        private void OnClickCommencer(object o)
        {
            //player1.TourDeJeu = true;
            //GameView game = new GameView(frame, player1, player2);
            //frame.Navigate(game);
        }

        private void OnClickAjouter(object o)
        {
            joueurs.Add(new Player(nouveauJoueur));
            joueurs = new ObservableCollection<Player>(joueurs.OrderBy(joueur => joueur.Nom));
            NouveauJoueur = "";
            sauvegarde.WriteXML(joueurs);
            Selection1 = new ObservableCollection<Player>(joueurs);
            if (player1 == null && selection1.Count > 0)
            {
                player1 = selection1.First();
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
        public string Existe
        {
            get => existe;
            set
            {
                existe = value;
                RaisePropertyChanged("Existe");
                ActiveAjout = !String.IsNullOrEmpty(nouveauJoueur) && existe.Equals("Hidden");
            }
        }
        public string NouveauJoueur
        {
            get => nouveauJoueur;
            set
            {
                nouveauJoueur = value;
                RaisePropertyChanged("NouveauJoueur");
                if (joueurs.Where(joueur => joueur.Nom == value).ToList().Count > 0)
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

        public ObservableCollection<Player> Selection1
        {
            get => selection1;
            set
            {
                selection1 = value;
                RaisePropertyChanged("Selection1");
            }
        }
        

        public Player Player1
        {
            get => player1;
            set
            {
                
                player1 = value;
                RaisePropertyChanged("Player1");
            }
        }
        
    }
}
