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
    public class AlgorithmeLDO : IAlgorithme
    {
        public string Nom => "LDO";

        private long tempsExecution;
        public long TempsExecution => -1;

        public void Executer(Taverne taverne)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Graphe graphe = new Graphe(taverne);
            for (int i = 0; i < graphe.Sommets.Count(); i++)
            {
                Sommet sommet = graphe.Sommets[graphe.Sommets.Count() - i];
                int t = 0;
                int max = 0;
                int v = sommet.Voisins.Count;
                foreach (Sommet voisin in sommet.Voisins)
                {
                    if (graphe.Couleurs[voisin] == graphe.Couleurs[sommet])
                    {
                        graphe.AjouterCouleur(voisin, graphe.Couleurs[sommet] + 1);
                        graphe.ChangerCouleur(voisin, graphe.Couleurs[sommet] + 1);
                        if (t < taverne.CapactieTables)
                        {
                            taverne.AjouterClientTable(sommet.Voisins.Count - v, graphe.Couleurs[voisin]);
                            t++;
                        }
                        else
                        {
                            max++;
                            taverne.AjouterTable();
                            taverne.AjouterClientTable(sommet.Voisins.Count - v, graphe.Couleurs[voisin] + max);
                        }
                    }
                    v--;
                }
            }
            stopwatch.Stop();
            this.tempsExecution = stopwatch.ElapsedMilliseconds;
            Console.WriteLine(TempsExecution);
        }
    }
}
