﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Metier.Algorithmes.Graphes;
using TavernManagerMetier.Metier.Tavernes;

namespace TavernManagerMetier.Metier.Algorithmes.Realisations
{
    public class AlgorithmeDSATUR : IAlgorithme
    {
        public string Nom => "DSATUR";

        private long tempsExecution;
        public long TempsExecution => -1;

        private Graphe graphe;


        public void Executer(Taverne taverne)
        {  Stopwatch stopwatch = Stopwatch.StartNew();
            graphe = new Graphe(taverne);
            List<Sommet> sommets = graphe.Sommets;
            Sommet sommetencours ;
            while (graphe.Couleurs.ContainsValue(0))
            {
                Sommet sommetChoisit = sommets[0];

                List<Sommet> maxvoisinscolo =new List<Sommet>();

                foreach(Sommet sommet in sommets) 
                {   
                    if (graphe.VoisinsColo(sommetChoisit).Count()< graphe.VoisinsColo(sommet).Count())
                    {
                        sommetChoisit = sommet;
                    }
                }
                maxvoisinscolo.Add(sommetChoisit);
                foreach (Sommet sommet in sommets)
                {
                    if (graphe.VoisinsColo(sommetChoisit).Count() == graphe.VoisinsColo(sommet).Count())
                    {
                        maxvoisinscolo.Add(sommet);
                    }
                }
                List<Sommet> maxvoisins = new List<Sommet>();
                foreach (Sommet sommet in maxvoisinscolo)
                {
                    if (sommetChoisit.Voisins.Count()<sommet.Voisins.Count())
                    {
                        sommetChoisit=sommet;
                    }
                }
                maxvoisins.Add(sommetChoisit);
                foreach (Sommet sommet in sommets)
                {
                    if (sommetChoisit.Voisins.Count() < sommet.Voisins.Count())
                    {
                        maxvoisins.Add(sommet);
                    }
                }
                Random rand = new Random();

                sommetChoisit = maxvoisins[rand.Next(maxvoisins.Count)];

                graphe.Colorier(sommetChoisit);
                sommets.Remove(sommetChoisit);
            }
            
            for (int i=0 ;  i< graphe.Couleurs.Values.Max() ; i++)
            {
                taverne.AjouterTable();
            }

            for (int i = 0; i < graphe.Sommets.Count(); i++)
            {
                taverne.AjouterClientTable(i, graphe.Couleurs[graphe.Sommets[i]]-1);
            }

                stopwatch.Stop();
            this.tempsExecution = stopwatch.ElapsedMilliseconds;
            Console.WriteLine(TempsExecution);
        }

    }
}
