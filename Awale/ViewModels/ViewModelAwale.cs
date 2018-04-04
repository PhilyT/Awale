using Awale.Utils;
using Awale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

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
        private Game game;

        private Sauvegarde sauvegarde;
        private ObservableCollection<Player> joueurs;


        public ViewModelAwale(Game game)
        {
            sauvegarde = new Sauvegarde();
            joueurs = sauvegarde.ReadXML();
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
            Game = game;

        }

        private void OnClickTrou(object o)
        {
            string nameSender = "";
            Label senderAsLabel = o as Label;
            if(senderAsLabel != null)
            {
                nameSender = senderAsLabel.Name.Split('_')[0];
            }
            Ellipse senderAsEllise = o as Ellipse;
            if (senderAsEllise != null)
            {
                nameSender = senderAsEllise.Name.Split('_')[0];
            }
            bool tourJoueur2 = (nameSender.Contains("Adverse") && Game.Playeur2.TourDeJeu);
            bool tourJoueur1 = (!nameSender.Contains("Adverse") && Game.Playeur1.TourDeJeu);
            if (tourJoueur2 || tourJoueur1)
            {
                List<PropertyInfo> propertiesInfos = new List<PropertyInfo>(GetType().GetProperties());
                PropertyInfo sender = propertiesInfos.Find(item => item.Name.Equals(nameSender));
                int nbGraines = (int)sender.GetValue(this);
                if(nbGraines > 0)
                {
                    sender.SetValue(this, 0);
                    string nameDest = Game.Next(nameSender);
                    PropertyInfo dest = null;
                    while (nbGraines > 0)
                    {
                        if (!nameSender.Equals(nameDest))
                        {
                            dest = propertiesInfos.Find(item => item.Name.Equals(nameDest));
                            int nbGrainesDest = (int)dest.GetValue(this);
                            dest.SetValue(this, nbGrainesDest + 1);
                            nbGraines--;
                        }
                        if(nbGraines > 0)
                        {
                            nameDest = Game.Next(nameDest);
                        }
                    }
                    int count = 5;
                    nbGraines = (int)dest.GetValue(this);
                    bool peuRecolterJoueur1 = tourJoueur1 && nameDest.Contains("Adverse") && (nbGraines == 2 || nbGraines == 3);
                    bool peuRecolterJoueur2 = tourJoueur2 && !nameDest.Contains("Adverse") && (nbGraines == 2 || nbGraines == 3);
                    while (peuRecolterJoueur1||peuRecolterJoueur2 && count>0)
                    {
                        dest.SetValue(this, 0);
                        if (tourJoueur1)
                        {
                            Game.Playeur1.Recolte += nbGraines;
                        }
                        else
                        {
                            Game.Playeur2.Recolte += nbGraines;
                        }
                        nameDest = Game.Previous(nameDest);
                        dest = propertiesInfos.Find(item => item.Name.Equals(nameDest));
                        count--;
                        nbGraines = (int)dest.GetValue(this);
                        peuRecolterJoueur1 = tourJoueur1 && nameDest.Contains("Adverse") && (nbGraines == 2 || nbGraines == 3);
                        peuRecolterJoueur2 = tourJoueur2 && !nameDest.Contains("Adverse") && (nbGraines == 2 || nbGraines == 3);
                    }
                    if(Game.Playeur1.Recolte > 24)
                    {
                        Game.Playeur1.TourDeJeu = false;
                        Game.Playeur2.TourDeJeu = false;
                        joueurs.Where(joueur => joueur.Nom.Equals(Game.Playeur1.Nom)).First().NbVictoire++;
                        sauvegarde.WriteXML(joueurs);
                        Game.Victory = "Visible";
                        Game.Winner = Game.Playeur1.Nom;
                    }
                    else if(Game.Playeur2.Recolte > 24)
                    {
                        Game.Playeur1.TourDeJeu = false;
                        Game.Playeur2.TourDeJeu = false;
                        joueurs.Where(joueur => joueur.Nom.Equals(Game.Playeur2.Nom)).First().NbVictoire++;
                        sauvegarde.WriteXML(joueurs);
                        Game.Victory = "Visible";
                        Game.Winner = Game.Playeur2.Nom;
                    }
                    else if(Game.Playeur1.Recolte ==24 && Game.Playeur2.Recolte == 24)
                    {
                        Game.Playeur1.TourDeJeu = false;
                        Game.Playeur2.TourDeJeu = false;
                        Game.Egualite = "Visible";
                    }
                    else
                    {
                        Game.Playeur1.TourDeJeu = !Game.Playeur1.TourDeJeu;
                        Game.Playeur2.TourDeJeu = !Game.Playeur2.TourDeJeu;
                    }
                }
            }            
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
        public Game Game { get => game; set
            {
                game = value;
                RaisePropertyChanged("Game");
            }
        }
        
    }
}
