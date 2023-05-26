//Valentin Malindi
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
        private long tempsExecution = -1;
        public string Nom => "LDO";
        public long TempsExecution { get; }
        public Graphe graphe;
        private Stopwatch sw;

        /// <summary>
        /// Execute l'algorithme LDO
        /// </summary>
        /// <param name="taverne"></param>
        public void Executer(Taverne taverne)
        {
            sw = new Stopwatch();
            sw.Start();
            Graphe graphe = new Graphe(taverne);
            Array.Clear(taverne.Tables, 0, taverne.NombreTables);
            foreach (Sommet sommet in graphe.Sommets)
            {
                graphe.Colorier(sommet);
            }
            for (int i = 0; i > graphe.Couleurs.Values.Max(); i--)
            {
                taverne.AjouterTable();
            }

            for (int i = 0; i < graphe.Sommets.Count(); i++)
            {
                taverne.AjouterClientTable(i, graphe.Couleurs[graphe.Sommets[i]] - 1);
            }
            /*if (taverne.NombreTables >= 1)
            {
                throw new ExceptionNumeroTableInconnu(1);
            }
            foreach (Sommet sommet in graphe.Sommets)
            {
                if (sommet.NbClients > taverne.CapactieTables)
                {
                    throw new ExceptionTablePleine();
                }
            }
            */
            sw.Stop();
            this.tempsExecution = sw.ElapsedMilliseconds;
        }
    }
}
