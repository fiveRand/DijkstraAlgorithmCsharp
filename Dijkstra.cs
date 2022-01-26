using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dijkstra : MonoBehaviour
{
    private void Start()
    {
        DijkstraNode A = new DijkstraNode('a');
        DijkstraNode B = new DijkstraNode('b');
        DijkstraNode C = new DijkstraNode('c');
        DijkstraNode D = new DijkstraNode('d');
        DijkstraNode E = new DijkstraNode('e');
        DijkstraNode F = new DijkstraNode('f');

        A.LinkNode(B,3);
        A.LinkNode(C,100);
        B.LinkNode(C,2);
        B.LinkNode(D,4);
        C.LinkNode(E,5);
        C.LinkNode(D,6);
        D.LinkNode(E,1);
        D.LinkNode(F,10);
        E.LinkNode(F,3);
        E.LinkNode(D,1);

        List<DijkstraNode> temp = new List<DijkstraNode>(new DijkstraNode[] {A,B,C,D,E,F});

        DijkstraNode node = Algorithm(A, F, temp);
        List<DijkstraNode> result = new List<DijkstraNode>();
        while(node != null)
        {
            result.Add(node);
            node = node.parent;
        }
        result.Reverse();


        foreach(DijkstraNode resulter in result)
        {
            print(resulter.name);
        }
    }

    DijkstraNode Algorithm(DijkstraNode start,DijkstraNode end,List<DijkstraNode> nodelist)
    {
        List<DijkstraNode> unexplored = new List<DijkstraNode>();

        foreach(DijkstraNode node in  nodelist)
        {
            node.Reset();
            unexplored.Add(node);
        }

        start.cost = 0;

        while(unexplored.Count > 0)
        {
            DijkstraNode curNode = unexplored[0];

            for(int i = 1; i < unexplored.Count; i++)
            {
                if(curNode.cost >= unexplored[i].cost)
                {
                    curNode = unexplored[i];
                }
            
            }

            if(curNode == end)
            {
                break;
            }


            unexplored.Remove(curNode);

            foreach(var neighbor in curNode.linkedNodeList)
            {
                if(unexplored.Contains(neighbor.Item1))
                {
                    int newcost = curNode.cost + neighbor.Item2;

                    if(newcost < neighbor.Item1.cost)
                    {
                        neighbor.Item1.cost = newcost;
                        neighbor.Item1.parent = curNode;
                    }
                }
            }    
        }

        return end;
    }
}

public class DijkstraNode
{
    public char name;
    public DijkstraNode parent;
    public int cost;
    public List<Tuple<DijkstraNode, int>> linkedNodeList;

    public DijkstraNode(char name_)
    {
        name = name_;
        cost = int.MaxValue;
        linkedNodeList = new List<Tuple<DijkstraNode, int>>();
    }

    public void Reset()
    {
        cost = int.MaxValue;
        parent = null;
    }

    public void LinkNode(DijkstraNode othernode, int distance)
    {
        var other = Tuple.Create(othernode, distance);

        linkedNodeList.Add(other);
        othernode.linkedNodeList.Add(other);
    }
}