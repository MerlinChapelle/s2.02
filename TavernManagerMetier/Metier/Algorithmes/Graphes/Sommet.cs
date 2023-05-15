using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TavernManagerMetier.Metier.Algorithmes.Graphes
{
    internal class Sommet
    { private List<Sommet> voisins;
      
        public List<Sommet> Voisins { get { return voisins; } }


        public Sommet() 
        {
            voisins = new List<Sommet>();
        }

        public void AjouterVoisin(Sommet sommet)
        {
            voisins.Add(sommet);
        }
    }
}
