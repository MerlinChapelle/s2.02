using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Metier.Tavernes;

namespace TavernManagerMetier.Metier.Algorithmes.Graphes
{
    public class Graphe
    { private Dictionary<Client, Sommet> sommets;
        private Dictionary<Sommet,int> couleurs; 
        public List<Sommet> Sommets => this.sommets.Values.Distinct().ToList<Sommet>();

        

        private void AjouterCouleur(Sommet sommet,int couleur) 
        {
            couleurs.Add(sommet, couleur);
        }

        private void changerCouleur(Sommet sommet, int couleur)
        {
            couleurs[sommet] = couleur ;
        }


        private void AjouterSommet(Client client,Sommet sommet)
        {
           if(!this.sommets.ContainsKey(client)) 
            {
                this.sommets[client] = sommet;
                sommet.NbClients ++ ;
                foreach(Client ami in client.Amis) this.AjouterSommet(ami, sommet);
            }
           
        }

        private void AjouterArette(Client client1, Client client2)
        {
            sommets[client1].AjouterVoisin(sommets[client2]);
        }

        public Graphe(Taverne taverne)
        {
            sommets = new Dictionary<Client, Sommet>();
            foreach(Client client in taverne.Clients) { AjouterSommet(client, new Sommet()); }
            foreach(Client client2 in taverne.Clients) { foreach (Client ennemie in client2.Ennemis) { sommets[client2].AjouterVoisin(sommets[ennemie]);  } }
            foreach (Sommet sommet in Sommets) { AjouterCouleur(sommet, 0); }
        }
    }
}
