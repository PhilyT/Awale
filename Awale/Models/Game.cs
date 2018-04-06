using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awale.Models
{
    public class Game :ModelBase
    {

        private List<string> trous;
        private Player playeur1;
        private Player playeur2;
        private string winner;
        private string vicory;
        private string egualite;
        private Sauvegarde sauvegarde;
        private ObservableCollection<Player> joueurs;
        private readonly Client client;
        private readonly Serveur serveur;
        private readonly bool ia;

        public Game(Player player1, Player player2)
        {
            sauvegarde = new Sauvegarde();
            joueurs = sauvegarde.ReadXML();
            trous = new List<string>();
            trous.Add("Trou1");
            trous.Add("Trou2");
            trous.Add("Trou3");
            trous.Add("Trou4");
            trous.Add("Trou5");
            trous.Add("Trou6");
            trous.Add("Trou1Adverse");
            trous.Add("Trou2Adverse");
            trous.Add("Trou3Adverse");
            trous.Add("Trou4Adverse");
            trous.Add("Trou5Adverse");
            trous.Add("Trou6Adverse");
            Playeur1 = player1;
            Playeur2 = player2;
            joueurs.Where(joueur => joueur.Nom.Equals(playeur1.Nom)).First().NbPartie++;
            joueurs.Where(joueur => joueur.Nom.Equals(playeur2.Nom)).First().NbPartie++;
            sauvegarde.WriteXML(joueurs);
            vicory = "Hidden";
            egualite = "Hidden";
        }

        public Game(Player player1, Player player2, Serveur serveur)
        {
            sauvegarde = new Sauvegarde();
            joueurs = sauvegarde.ReadXML();
            trous = new List<string>();
            trous.Add("Trou1");
            trous.Add("Trou2");
            trous.Add("Trou3");
            trous.Add("Trou4");
            trous.Add("Trou5");
            trous.Add("Trou6");
            trous.Add("Trou1Adverse");
            trous.Add("Trou2Adverse");
            trous.Add("Trou3Adverse");
            trous.Add("Trou4Adverse");
            trous.Add("Trou5Adverse");
            trous.Add("Trou6Adverse");
            Playeur1 = player1;
            Playeur2 = player2;
            this.serveur = serveur;
            joueurs.Where(joueur => joueur.Nom.Equals(playeur1.Nom)).First().NbPartie++;
            sauvegarde.WriteXML(joueurs);
            vicory = "Hidden";
            egualite = "Hidden";
        }

        public Game(Player player1, Player player2, Client client)
        {
            sauvegarde = new Sauvegarde();
            joueurs = sauvegarde.ReadXML();
            trous = new List<string>();
            trous.Add("Trou1");
            trous.Add("Trou2");
            trous.Add("Trou3");
            trous.Add("Trou4");
            trous.Add("Trou5");
            trous.Add("Trou6");
            trous.Add("Trou1Adverse");
            trous.Add("Trou2Adverse");
            trous.Add("Trou3Adverse");
            trous.Add("Trou4Adverse");
            trous.Add("Trou5Adverse");
            trous.Add("Trou6Adverse");
            Playeur1 = player1;
            Playeur2 = player2;
            this.client = client;
            joueurs.Where(joueur => joueur.Nom.Equals(playeur1.Nom)).First().NbPartie++;
            sauvegarde.WriteXML(joueurs);
            vicory = "Hidden";
            egualite = "Hidden";
        }

        public Game(Player player1, bool ia)
        {
            sauvegarde = new Sauvegarde();
            joueurs = sauvegarde.ReadXML();
            trous = new List<string>();
            trous.Add("Trou1");
            trous.Add("Trou2");
            trous.Add("Trou3");
            trous.Add("Trou4");
            trous.Add("Trou5");
            trous.Add("Trou6");
            trous.Add("Trou1Adverse");
            trous.Add("Trou2Adverse");
            trous.Add("Trou3Adverse");
            trous.Add("Trou4Adverse");
            trous.Add("Trou5Adverse");
            trous.Add("Trou6Adverse");
            Playeur1 = player1;
            this.ia = ia;
            Playeur2 = new Player("IA");
            joueurs.Where(joueur => joueur.Nom.Equals(playeur1.Nom)).First().NbPartie++;
            sauvegarde.WriteXML(joueurs);
            vicory = "Hidden";
            egualite = "Hidden";
        }

        public Player Playeur1 { get => playeur1; set { playeur1 = value; RaisePropertyChanged("Playeur1"); } }
        public Player Playeur2 { get => playeur2; set { playeur2 = value; RaisePropertyChanged("Playeur2"); } }

        public string Next(string element)
        {
            int index = trous.FindIndex(item => element.Equals(item))+1;
            if(index == trous.Count)
            {
                return trous.ElementAt(0);
            }
            else
            {
                return trous.ElementAt(index);
            }
            
        }
        public string Previous(string element)
        {
            int index = trous.FindIndex(item => element.Equals(item)) - 1;
            if (index == -1)
            {
                return trous.ElementAt(trous.Count-1);
            }
            else
            {
                return trous.ElementAt(index);
            }
        }
        public string Winner
        {
            get => winner; set
            {
                winner = value;
                RaisePropertyChanged("Winner");
            }
        }
        public string Victory
        {
            get => vicory; set
            {
                vicory = value;
                RaisePropertyChanged("Victory");
            }
        }
        public string Egualite
        {
            get => egualite; set
            {
                egualite = value;
                RaisePropertyChanged("Egualite");
            }
        }

        public Serveur Serveur => serveur;

        public bool Ia => ia;

        public Client Client => client;
    }
}
