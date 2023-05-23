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

        /// <summary>
        /// Execute l'algorithme LDO
        /// </summary>
        /// <param name="taverne"></param>
        public void Executer(Taverne taverne)
        {
            Graphe graphe = new Graphe(taverne);
            int max = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < graphe.Sommets.Count(); i++)
            {
                Client client = taverne.Clients[graphe.Sommets.Count() - (i + 1)];
                Sommet sommet = graphe.Sommets[graphe.Sommets.Count() - (i+1)];
                int t = 0;
                int v = sommet.Voisins.Count();
                if (!client.AsUneTable)
                    taverne.AjouterTable();
                if (sommet.Voisins != null)
                    taverne.AjouterTable();
                taverne.AjouterClientTable(graphe.Sommets.Count() - (i + 1),i + max);
                foreach (Sommet voisin in sommet.Voisins)
                {
                    if (graphe.Couleurs[voisin] == graphe.Couleurs[sommet])
                    {
                        graphe.ChangerCouleur(voisin, graphe.Couleurs[sommet] + 1);
                        if (t < taverne.CapactieTables)
                        {
                            taverne.AjouterClientTable(sommet.Voisins.Count - (v), graphe.Couleurs[voisin] + max);
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
