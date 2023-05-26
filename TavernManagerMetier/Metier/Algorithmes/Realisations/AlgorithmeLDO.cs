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
            AlgorithmeColorationCroissante original = new AlgorithmeColorationCroissante();
            taverne.Clients.OrderByDescending(s => s.Ennemis.Count).ToList();
            original.Executer(taverne);
        }
    }
}
