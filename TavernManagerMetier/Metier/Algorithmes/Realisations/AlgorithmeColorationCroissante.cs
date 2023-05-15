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
    public class AlgorithmeColorationCroissante: IAlgorithme
    {
        private long tempsExecution = -1;
        public string Nom => "Coloration Croissante";

        public long TempsExecution { get; }
        public Graphe graphe;
        private Stopwatch sw;

        public void Executer(Taverne taverne)
        {
            sw = new Stopwatch();
            sw.Start();
            this.graphe = new Graphe(taverne);
            taverne.AjouterTable();
            taverne.AjouterClientTable(0, 0);
            for (int i = 1; i < taverne.Clients.Count(); i++)
            {
                for (int j = 0; j < taverne.NombreTables; j++)
                {
                    for (int k = 1; k < taverne.Tables[j].Clients.Count(); k++)
                    {

                        if (taverne.Clients[i].EstEnnemisAvec(taverne.Clients[k]))
                        {
                            taverne.AjouterClientTable(i, j);
                        }
                        else
                        {
                            taverne.AjouterTable();
                            taverne.AjouterClientTable(i, taverne.NombreTables);
                        }
                    }

                }
            }
            sw.Stop();
            this.tempsExecution = sw.ElapsedMilliseconds;
        }
    }
}
