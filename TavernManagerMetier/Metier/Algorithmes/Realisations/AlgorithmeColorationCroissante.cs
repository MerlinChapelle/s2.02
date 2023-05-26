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
            if (taverne.NombreTables >= 1)
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
            int table = 0;
            for (int sommet = 0; sommet < graphe.Sommets.Count(); sommet++)
            {
                taverne.AjouterTable();
                graphe.ChangerCouleur(graphe.Sommets[sommet], sommet + 1);
                for (int client = 0; client < graphe.Sommets[sommet].NbClients; client++)
                {
                    taverne.AjouterClientTable(client, table);
                    break;
                }
                table++;
            }
            sw.Stop();
            this.tempsExecution = sw.ElapsedMilliseconds;
        }
    }
}
