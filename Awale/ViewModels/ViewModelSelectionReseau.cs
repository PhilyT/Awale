using Awale.Models;
using Awale.Utils;
using Awale.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;

namespace Awale.ViewModels
{
    public class ViewModelSelectionReseau : ViewModelBase
    {
        Frame frame;
        private DelegateCommand retour;
        private DelegateCommand ajouter;
        private DelegateCommand heberger;
        private DelegateCommand rejoindre;
        private string existe;
        private string port;
        private string iPAdverse;
        private string portAdverse;
        private string nouveauJoueur;
        private bool activeAjout;
        private ObservableCollection<Player> selection1;
        private Sauvegarde sauvegarde;
        private Player player1;
        private ObservableCollection<Player> joueurs;
        private Serveur server;
        private Client client;
        Task taskClient;
        Task taskServeur;
        Timer timer1;
        Timer timer2;
        public ViewModelSelectionReseau(Frame frame)
        {
            this.frame = frame;
            retour = new DelegateCommand(o => OnClickRetour(o));
            ajouter = new DelegateCommand(o => OnClickAjouter(o));
            heberger = new DelegateCommand(o => OnClickHeberger(o));
            rejoindre = new DelegateCommand(o => OnClickRejoindre(o));
            sauvegarde = new Sauvegarde();
            existe = "Hidden";
            activeAjout = false;
            joueurs = new ObservableCollection<Player>(sauvegarde.ReadXML().OrderBy(joueur => joueur.Nom));
            selection1 = new ObservableCollection<Player>(joueurs);
            if (selection1.Count > 0)
            {
                player1 = selection1.First();
            }
        }

        private void OnClickRejoindre(object o)
        {
            player1.TourDeJeu = true;
            taskClient = Client.StartClient(iPAdverse, portAdverse, player1.Nom);
            timer1 = new Timer(2000);
            timer1.Elapsed += new ElapsedEventHandler(TimerRejoindre);
            timer1.Start();
        }

        private void OnClickHeberger(object o)
        {
            player1.TourDeJeu = true;
            server = new Serveur();
            if (server.Start(port, player1.Nom)) {
                taskServeur = server.Accept();
                server.Reception += new EventHandler(ReceptionClient);
                timer2 = new Timer(120000);
                timer2.Elapsed += new ElapsedEventHandler(TimerHeberger);
                timer2.Start();
            }
        }

        private void ReceptionClient(object sender, EventArgs e)
        {

        }

        private void TimerRejoindre(object sender, ElapsedEventArgs e)
        {
            
            timer1.Stop();
        }

        private void TimerHeberger(object sender, ElapsedEventArgs e)
        {
            
            timer2.Stop();
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
            server.Stop();
            //client.Stop();
            MenuView menu = new MenuView(frame);
            frame.Navigate(menu);
        }



        public DelegateCommand Retour { get => retour; set => retour = value; }
        public DelegateCommand Heberger { get => heberger; set => heberger = value; }
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

        public string IPAdverse { get => iPAdverse;
            set
            {
                iPAdverse = value;
                RaisePropertyChanged("IPAdverse");
            }
        }
        public string PortAdverse { get => portAdverse;
            set
            {
                portAdverse = value;
                RaisePropertyChanged("PortAdverse");
            }
        }
        public string Port { get => port;
            set
            {
                port = value;
                RaisePropertyChanged("Port");
            }
        }

        public DelegateCommand Rejoindre { get => rejoindre; set => rejoindre = value; }
    }
}
