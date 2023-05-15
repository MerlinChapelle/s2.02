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
    public class AlgorithmeWelshPowell: IAlgorithme
    {
        public string Nom => "Chacun sa table";

        private long tempsExecution;
        public long TempsExecution => -1;

        public void Executer(Taverne taverne)
        {  Stopwatch stopwatch = Stopwatch.StartNew();
           Graphe graphe = new Graphe(taverne);
            for (int i=0;i<taverne.Clients.Count();i++)
            {
                
                taverne.AjouterTable();
                taverne.AjouterClientTable(i, i) ;
            }
            stopwatch.Stop();
            this.tempsExecution = stopwatch.ElapsedMilliseconds;
            Console.WriteLine(TempsExecution);
        }
    }
}
