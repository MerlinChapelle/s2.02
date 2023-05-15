﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Metier.Tavernes;

namespace TavernManagerMetier.Metier.Algorithmes.Graphes
{
    internal class Graphe
    { private Dictionary<Client, Sommet> sommets;
        public List<Sommet> Sommets => this.sommets.Values.Distinct().ToList<Sommet>();
        private void AjouterSommet(Client client)
        {
            Sommet sommet = new Sommet();
            sommets.Add(client, sommet);
        }

        private void AjouterArette(Client client1, Client client2)
        {
            sommets[client1].AjouterVoisin(sommets[client2]);
        }

        public Graphe(Taverne taverne)
        {
            sommets = new Dictionary<Client, Sommet>();
            foreach(Client client in taverne.Clients) { sommets[client] = new Sommet(); }
            foreach (Client client2 in taverne.Clients) { foreach (Client ennemie in client2.Ennemis){ sommets[client2].AjouterVoisin(sommets[ennemie]);  } }
        }
    }
}
