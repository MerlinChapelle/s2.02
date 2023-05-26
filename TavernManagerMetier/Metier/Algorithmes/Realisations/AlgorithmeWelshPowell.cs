using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Metier.Algorithmes.Graphes;
using TavernManagerMetier.Metier.Tavernes;

namespace TavernManagerMetier.Metier.Algorithmes.Realisations
{
    public class AlgorithmeWelshPowell : IAlgorithme
    {
        public string Nom => "WelshPowell";

        private long tempsExecution;
        public long TempsExecution => -1;
        private Stopwatch sw;

        public void Executer(Taverne taverne)
        {

            sw = new Stopwatch();
            sw.Start();
            taverne.Clients.OrderByDescending(s => s.Ennemis.Count);
            Graphe graphe = new Graphe(taverne);
            List<Sommet> sommets = graphe.Sommets.OrderBy(sommet => sommet.Voisins.Count).ToList();
            bool voisincolorie = false;
            int couleurEnCour = 1;
            graphe.testTaverne();
            while (graphe.Couleurs.Values.Contains(0))
            {
                List<Sommet> sommetsaretirer = new List<Sommet>();
                foreach(Sommet sommet in sommets)
                {
                    voisincolorie = false;
                    int nbcouleur= 0;
                    foreach (Sommet voisin in sommet.Voisins)
                    {
                        if (graphe.Couleurs[voisin] == couleurEnCour)
                        {
                            voisincolorie = true;
                        }
                    }
                    if (!voisincolorie && nbcouleur<taverne.CapactieTables)
                    {
                        graphe.ChangerCouleur(sommet, couleurEnCour);
                        sommetsaretirer.Add(sommet);
                        nbcouleur++;
                    }
                }
                foreach(Sommet sommet in sommetsaretirer)
                {
                    sommets.Remove(sommet);
                }
                couleurEnCour++;
            }

            for (int i = 0; i < graphe.Couleurs.Values.Max(); i++)
            {
                taverne.AjouterTable();
            }
            for (int i = 0; i < graphe.Sommets.Count(); i++)
            {
                foreach (Client client in graphe.dictSommets.Keys)
                {
                    if (graphe.dictSommets[client] == graphe.Sommets[i])
                    {
                        taverne.AjouterClientTable(client.Numero, graphe.Couleurs[graphe.Sommets[i]] - 1);
                    }
                }

            }
            sw.Stop();
            this.tempsExecution = sw.ElapsedMilliseconds;
            Console.WriteLine(TempsExecution);
        }
    }
}