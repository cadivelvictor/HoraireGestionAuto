/*
 * Crée par SharpDevelop.
 * Utilisateur: vcadivel
 * Date: 04/05/2024
 * Heure: 08:50
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Generic;

namespace ProtoShapes
{
	class Program
	{
		public static void Main(string[] args)
		{
			
	        Graph graph = new Graph();
	        
	        // Ajout des arrêts
	        graph.AddNode("STADE DE L’EST");
	        graph.AddNode("Parc des Expositions");
	        graph.AddNode("ZEC du Chaudron");
	        graph.AddNode("Manès");
	        graph.AddNode("Pierre Aubert");
	        graph.AddNode("Roger Payet");
	        graph.AddNode("Lacroix");
	        graph.AddNode("Sainte-Clotilde Centre");
	        graph.AddNode("Banian");
	        graph.AddNode("Deux Canons");
	        graph.AddNode("Camp Jacquot");
	        graph.AddNode("Saint-Jacques");
	        graph.AddNode("Petit Marché");
	        graph.AddNode("École Centrale");
	        graph.AddNode("Station Chaudron");
	        graph.AddNode("Mail du Chaudron");
	        graph.AddNode("Parc Aquatique");
	        graph.AddNode("Butor");
	        graph.AddNode("Hôtel des Impôts");
	        graph.AddNode("HÔTEL DE VILLE DE SAINT-DENIS");
	        
	        // Ajout des liaisons entre les arrêts
	        graph.AddEdge("STADE DE L’EST", "Parc des Expositions", 1);
	        graph.AddEdge("Parc des Expositions", "ZEC du Chaudron", 2);
	        graph.AddEdge("ZEC du Chaudron", "Manès", 3);
	        graph.AddEdge("Manès", "Pierre Aubert", 1);
	        graph.AddEdge("Pierre Aubert", "Roger Payet", 2);
	        graph.AddEdge("Roger Payet", "Lacroix", 3);
	        graph.AddEdge("Lacroix", "Sainte-Clotilde Centre", 1);
	        graph.AddEdge("Sainte-Clotilde Centre", "Banian", 2);
	        graph.AddEdge("Banian", "Deux Canons", 3);
	        graph.AddEdge("Deux Canons", "Camp Jacquot", 1);
	        graph.AddEdge("Camp Jacquot", "Saint-Jacques", 2);
	        graph.AddEdge("Saint-Jacques", "Petit Marché", 3);
	        graph.AddEdge("Petit Marché", "École Centrale", 1);
	        graph.AddEdge("École Centrale", "Station Chaudron", 2);
	        graph.AddEdge("Station Chaudron", "Mail du Chaudron", 3);
	        graph.AddEdge("Mail du Chaudron", "Parc Aquatique", 1);
	        graph.AddEdge("Parc Aquatique", "Butor", 2);
	        graph.AddEdge("Butor", "Hôtel des Impôts", 3);
	        graph.AddEdge("Hôtel des Impôts", "HÔTEL DE VILLE DE SAINT-DENIS", 1);
	        
	        // Trouver le chemin le plus court entre le STADE DE L’EST et HÔTEL DE VILLE DE SAINT-DENIS
	        List<string> shortestPath = graph.Dijkstra("STADE DE L’EST", "HÔTEL DE VILLE DE SAINT-DENIS");
	        
	        // Afficher le chemin le plus court
	        Console.WriteLine("Chemin le plus court :");
	        foreach(var stop in shortestPath)
	        {
	            Console.WriteLine(stop);
	        }
	        
			Console.WriteLine("Hello World!");
			
			// TODO: Implement Functionality Here
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}