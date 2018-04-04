using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awale.Models
{
    public class Sauvegarde
    {
        public void WriteXML(ObservableCollection<Player> uneListe)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(ObservableCollection<Player>));
            FileStream file = File.Create("./Joueurs.xml");
            writer.Serialize(file, uneListe);
            file.Close();
        }

        public ObservableCollection<Player> ReadXML()
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(ObservableCollection<Player>));
            if (File.Exists("./Joueurs.xml"))
            {
                StreamReader file = new StreamReader("./Joueurs.xml");
                ObservableCollection<Player> res = (ObservableCollection<Player>)reader.Deserialize(file);
                file.Close();
                return res;
            }
            return new ObservableCollection<Player>();
        }
    }
}
