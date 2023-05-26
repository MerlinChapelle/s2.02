using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Markup.Localizer;
using TavernManagerMetier.Exceptions.Realisations;
using TavernManagerMetier.Exceptions.Realisations.GestionDesTables;
using TavernManagerMetier.Exceptions.Realisations.GestionTaverne;
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
            Graphe graphe = new Graphe(taverne);
            Array.Clear(taverne.Tables, 0, taverne.NombreTables);
            graphe.testTaverne();
            foreach (Sommet sommet in graphe.Sommets)
            {
                graphe.Colorier(sommet);
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
