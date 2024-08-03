using System;
using System.Collections.Generic;
using UnityEngine;
public class Graph
{
    private Dictionary<Transform, List<Transform>> adjacencyList;

    public Graph()
    {
        adjacencyList = new Dictionary<Transform, List<Transform>>();
    }

    public void AddVertex(Transform vertex)
    {
        if (!adjacencyList.ContainsKey(vertex))
        {
            adjacencyList[vertex] = new List<Transform>();
        }
    }

    public void AddEdge(Transform vertex1, Transform vertex2)
    {
        if (adjacencyList.ContainsKey(vertex1) && adjacencyList.ContainsKey(vertex2))
        {
            adjacencyList[vertex1].Add(vertex2);
            adjacencyList[vertex2].Add(vertex1); // For undirected graph
        }
    }

    public List<Transform> GetNeighbors(Transform vertex)
    {
        if (adjacencyList.ContainsKey(vertex))
        {
            return adjacencyList[vertex];
        }
        return new List<Transform>();
    }
}
