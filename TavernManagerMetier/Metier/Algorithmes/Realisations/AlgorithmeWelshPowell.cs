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
        public string Nom => "WelshPowell";

        private long tempsExecution;
        public long TempsExecution => -1;

        public void Executer(Taverne taverne)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            taverne.Clients.OrderByDescending(s => s.Ennemis.Count);
            Graphe graphe = new Graphe(taverne);
            int c = 1; 
            int j = 0;
            int t = 0;
            bool voisincolorie = false;
            while (graphe.Couleurs.Values.Contains(0) ) 
            {
                taverne.AjouterTable();

                 
                for (int i = 0; i < graphe.Sommets.Count(); i++)
                {
                    Sommet x = graphe.Sommets[i];
                    j = 0;
                    voisincolorie = false;
                    
                   /* while ( (voisincolorie == false) && ( j < x.Voisins.Count() ) )
                    {
                        if (graphe.Couleurs[x.Voisins[j]] == c)
                        {
                            voisincolorie = true;
                        }
                        j++;
                    }
                    */
                    if (voisincolorie == false)
                    {
                        graphe.ChangerCouleur(x, c);
                        t++;
                        if ( t < taverne.CapactieTables)
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
            stopwatch.Stop();
            this.tempsExecution = stopwatch.ElapsedMilliseconds;
            Console.WriteLine(TempsExecution);
        }
    }
}
