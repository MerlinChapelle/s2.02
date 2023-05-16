using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TavernManagerMetier.Metier.Algorithmes.Graphes
{
    public class Sommet
    {
        private List<Sommet> voisins;
        private int nbClients;
        public int NbClients { get { return nbClients; } set { nbClients=value; } }

        public List<Sommet> Voisins { get { return voisins; } }

        /// <summary>
        /// Constructeur du sommet
        /// </summary>
       public Sommet() 
        {
            voisins = new List<Sommet>();
            nbClients = 0;
        }

        /// <summary>
        /// Ajoute un sommet voisin à la liste des voisins.
        /// </summary>
        /// <param name="sommet">Le sommet voisin à ajouter.</param>
        public void AjouterVoisin(Sommet sommet)
        {
            voisins.Add(sommet);
        }
    }
}
