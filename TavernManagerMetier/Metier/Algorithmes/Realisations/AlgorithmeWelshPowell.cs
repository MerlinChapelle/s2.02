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
            int c = 1;
            int j = 0;
            int t = 0;
            bool voisincolorie = false;
            while (graphe.Couleurs.Values.Contains(0))
            {
                taverne.AjouterTable();

                for (int i = 0; i < graphe.Sommets.Count(); i++)
                {
                    Sommet x = graphe.Sommets[i];
                    j = 0;
                    voisincolorie = false;

                    foreach (Sommet v in x.Voisins)
                    {
                        if (graphe.Couleurs[v] == c)
                        {
                            voisincolorie = true;
                        }
                    }
                    if (voisincolorie == false)
                    {
                        graphe.ChangerCouleur(x, c);
                        t++;
                        if (t < taverne.CapactieTables)
                            taverne.AjouterClientTable(i, c - 1);
                        else
                        {
                            t = 0;
                            taverne.AjouterTable();
                            c++;
                            taverne.AjouterClientTable(i, c - 1);
                        }
                    }

                }
                c++;
            }
            sw.Stop();
            this.tempsExecution = sw.ElapsedMilliseconds;
            Console.WriteLine(TempsExecution);
        }
    }
}