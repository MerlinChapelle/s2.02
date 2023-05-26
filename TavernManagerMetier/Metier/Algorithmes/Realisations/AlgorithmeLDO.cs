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
    ///<summary>
    ///Algorithme LDO
    ///</summary>
    public class AlgorithmeLDO : IAlgorithme
    {
        private long tempsExecution = -1;
        public string Nom => "LDO";
        public long TempsExecution { get { return tempsExecution; } set { tempsExecution = value; } }
        public Graphe graphe;
        private Stopwatch sw;

        /// <summary>
        /// Execute l'algorithme LDO
        /// </summary>
        /// <param name="taverne">La taverne dans laquelle l'algorithme va s'executer</param>
        public void Executer(Taverne taverne)
        {
            sw = new Stopwatch();
            sw.Start();
            Graphe graphe = new Graphe(taverne);
            Array.Clear(taverne.Tables, 0, taverne.NombreTables);
            List<Sommet> sommets = graphe.Sommets.OrderBy(sommet=>sommet.Voisins.Count).ToList();   //On attribut à sommets la liste des sommets du graphe trier de manière décroissante

            foreach (Sommet sommet in sommets)                                                      //Pour chaque sommet de la liste sommets
            {
                graphe.Colorier(sommet);                                                            //On colorie les sommets du graphe
            }
            for (int i = 0; i < graphe.Couleurs.Values.Max(); i++)                                  //Pour i allant de 0 au nombre de couleurs dans le graphe
            {
                taverne.AjouterTable();                                                             //On ajoute une table
            }
            for (int i = 0; i < graphe.Sommets.Count(); i++)                                        //Pour i allant de 0 au nombre de sommets dans le graphe
            {
                taverne.AjouterClientTable(i, graphe.Couleurs[graphe.Sommets[i]] - 1);              //On ajoute le client i sur la table
            }
            sw.Stop();
            TempsExecution = sw.ElapsedMilliseconds;
        }
    }
}
