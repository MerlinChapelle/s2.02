using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Metier.Tavernes;

namespace TavernManagerMetier.Metier.Algorithmes.Graphes
{
    /// <summary>
    /// 
    /// </summary>
    public class Graphe
    {
        /// <summary>
        /// Dictionnaire qui associe des clients à leurs sommets correspondants.
        /// </summary>
        private Dictionary<Client, Sommet> sommets;
        private int tailletalbes;

        /// <summary>
        /// Liste des sommets distincts dans le dictionnaire de sommets.
        /// </summary>
        public List<Sommet> Sommets => this.sommets.Values.Distinct().ToList<Sommet>();
        public Dictionary<Client, Sommet> dictSommets { get; }

        private Dictionary<Sommet, int> couleurs;
        private Dictionary<int, int> nbcouleurs =new Dictionary<int, int>();
        public Dictionary<Sommet, int> Couleurs
        {
            get { return couleurs; } 
            set { value = couleurs; } 
        }



        /// <summary>
        /// Ajoute une couleur au sommet spécifié dans le dictionnaire des couleurs.
        /// </summary>
        /// <param name="sommet">Le sommet auquel ajouter la couleur.</param>
        /// <param name="couleur">La couleur à attribuer au sommet.</param>
        public void AjouterCouleur(Sommet sommet, int couleur)
        {
            Couleurs.Add(sommet, couleur);
        }

        /// <summary>
        /// Ajoute un sommet associé au client spécifié à la collection de sommets, s'il n'est pas déjà présent.
        /// Incrémente également le compteur NbClients du sommet ajouté et récursivement ajoute le sommet à tous les amis du client.
        /// </summary>
        /// <param name="client">Le client associé au sommet à ajouter.</param>
        /// <param name="sommet">Le sommet à ajouter.</param>
        public void AjouterSommet(Client client, Sommet sommet)
        {
            if (!this.sommets.ContainsKey(client))
            {
                this.sommets[client] = sommet;
                sommet.NbClients++;
                foreach (Client ami in client.Amis){this.AjouterSommet(ami, sommet);}
            }
        }


        /// <summary>
        /// Ajoute une arête entre deux clients en ajoutant un voisin au premier sommet.
        /// </summary>
        /// <param name="client1">Le premier client de l'arête.</param>
        /// <param name="client2">Le deuxième client de l'arête.</param>
        private void AjouterArette(Client client1, Client client2)
        {
            sommets[client1].AjouterVoisin(sommets[client2]);
        }

        public List<Sommet> VoisinsColo(Sommet sommet)
        {
            List<Sommet> res = new List<Sommet>();
            foreach (Sommet voisin in sommet.Voisins)
            {
                if (couleurs[voisin]==0) { res.Add(voisin); }
            }
            return res;
        }

        /// <summary>
        /// Change la couleur du sommet spécifié avec la nouvelle couleur spécifiée.
        /// </summary>
        /// <param name="sommet">Le sommet dont la couleur doit être modifiée.</param>
        /// <param name="couleur">La nouvelle couleur à attribuer au sommet.</param>
        /// 
        public void ChangerCouleur(Sommet sommet, int couleur)
        {
            Couleurs[sommet] = couleur;
        }

        public void Colorier(Sommet sommet) 
        {
            List<int> couleur =new List<int>();
            int min = 1;
            foreach(Sommet voisin in sommet.Voisins)
            {
                couleur.Add(couleurs[voisin]);
            }
            while ((couleur.Contains(min))||(nbcouleurs[min]+1>tailletalbes)) { min++; }

            Couleurs[sommet] = min;
            if (!nbcouleurs.ContainsKey(min+1)) { nbcouleurs.Add(min+1, 0); }
            this.nbcouleurs[min] += 1;
        }

        /// <summary>
        /// Génération du graphe à partir de la taverne
        /// </summary>
        /// <param name="taverne">La taverne qui sera utiliser comme modèle pour la création du graphe</param>
        public Graphe(Taverne taverne)
        {
            sommets = new Dictionary<Client, Sommet>();
            couleurs = new Dictionary<Sommet, int>();
            foreach (Client client in taverne.Clients) { AjouterSommet(client, new Sommet()); }
            foreach(Client client2 in taverne.Clients) { foreach (Client ennemie in client2.Ennemis) {  AjouterArette(client2, ennemie);  } }
            foreach (Sommet sommet in Sommets) { AjouterCouleur(sommet, 0); }
            tailletalbes = taverne.CapactieTables;
            nbcouleurs.Clear();
            nbcouleurs.Add(1, 0);
        }
    }
}
