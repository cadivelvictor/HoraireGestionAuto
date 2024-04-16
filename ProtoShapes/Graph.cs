/*
 * Crée par SharpDevelop.
 * Utilisateur: vcadivel
 * Date: 04/05/2024
 * Heure: 08:51
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Generic;

namespace ProtoShapes
{
	/// <summary>
	/// Description of Graph.
	/// </summary>
	public class Graph
	{
		private Dictionary<string, List<Tuple<string, int>>> adjacencyList = new Dictionary<string, List<Tuple<string, int>>>();
		    
		    public void AddNode(string node)
		    {
		        if (!adjacencyList.ContainsKey(node))
		        {
		            adjacencyList[node] = new List<Tuple<string, int>>();
		        }
		    }
		    
		    public void AddEdge(string start, string end, int weight)
		    {
		        adjacencyList[start].Add(new Tuple<string, int>(end, weight));
		        // Pour un graphe non-orienté, ajoutez également l'arête dans l'autre sens :
		        // adjacencyList[end].Add(new Tuple<string, int>(start, weight));
		    }
		    
		    public List<string> Dijkstra(string start, string end)
		    {
		        Dictionary<string, int> distances = new Dictionary<string, int>();
		        Dictionary<string, string> previousNodes = new Dictionary<string, string>();
		        List<string> nodes = new List<string>();
		        
		        foreach(var vertex in adjacencyList)
		        {
		            if (vertex.Key == start)
		                distances[vertex.Key] = 0;
		            else
		                distances[vertex.Key] = int.MaxValue;
		            
		            nodes.Add(vertex.Key);
		        }
		        
		        while (nodes.Count != 0)
		        {
		            nodes.Sort((x, y) => distances[x].CompareTo(distances[y]));
		            var smallest = nodes[0];
		            nodes.Remove(smallest);
		            
		            if (smallest == end)
		            {
		                var path = new List<string>();
		                while (previousNodes.ContainsKey(smallest))
		                {
		                    path.Add(smallest);
		                    smallest = previousNodes[smallest];
		                }
		                path.Add(start);
		                path.Reverse();
		                return path;
		            }
		            
		            if (distances[smallest] == int.MaxValue)
		                break;
		            
		            foreach(var neighbor in adjacencyList[smallest])
		            {
		                var alt = distances[smallest] + neighbor.Item2;
		                if (alt < distances[neighbor.Item1])
		                {
		                    distances[neighbor.Item1] = alt;
		                    previousNodes[neighbor.Item1] = smallest;
		                }
		            }
		        }
		        
		        return null;
		    }
	}
}
