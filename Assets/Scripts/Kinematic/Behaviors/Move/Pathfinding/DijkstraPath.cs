using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Behaviors.Move.Pathfinding
{

    public class Dijkstra : FollowPath
    {
        private List<Node> nodes = new();
        
        public Dijkstra(BaseKinematic character) : base(character)
        {
        }

        public Node[] getNodes()
        {
            return nodes.ToArray();
        }

        public Node getCurrentNode()
        {
            Node closest = null;
            foreach (var node in nodes)
            {
                if (closest == null || Vector3.Distance(character.transform.position, node.position) < Vector3.Distance(character.transform.position, closest.position))
                {
                    closest = node;
                }
            }
            if (closest == null) throw new System.Exception("Node not found");
            return closest;
        }

        public Node addNode(Vector3 position, List<Vector3> neighbors)
        {
            List<Node> neighborsList = new();
            foreach (Vector3 neighbor in neighbors)
            {
                bool flag = false;
                foreach (var node in nodes)
                {
                    if (node.position == neighbor)
                    {
                        flag = true;
                        neighborsList.Add(node);
                    }
                }
                if (!flag)
                    neighborsList.Add(addNode(neighbor, new List<Vector3>()));
            }

            Node self = new Node(position, neighborsList);
            foreach (var node in neighborsList)
            {
                node.neighbors.Add(self);
            }
            nodes.Add(self);
            Debug.Log("Added node " + self.position);
            return self;
        }

        public void PathTo(Vector3 pos)
        {
            Node nPos = null;
            foreach (var node in nodes)
            {
                if (node.position == pos)
                {
                    nPos = node;
                    break;
                }
            }
            if (nPos == null) throw new System.Exception("Node not found");

            
            List<NodeRecord> open = new();
            var startNode = getCurrentNode();
            open.Add(new NodeRecord(startNode, null, 0));
            var closed = new List<NodeRecord>();
            NodeRecord current = null;
            List<Node> connections;
            while (open.Count > 0)
            {
                // find smallest cost
                var min = open.Min(e => e.costSoFar);
                current = open.First(e => e.costSoFar == min);
                if (current == null)
                {
                    throw new System.Exception("No current node");
                }
                if (current.node == nPos)
                {
                    break;
                }
                connections = current.node.neighbors;
                NodeRecord endNodeRecord;
                foreach (var endNode in connections)
                {
                    if (closed.Any((a) => a.node == endNode))
                    {
                        continue;
                    }

                    var endNodeCost = current.costSoFar + getConnectionCost(current.node, endNode);
                    
                    NodeRecord first = open.FirstOrDefault((a) => a.node == endNode);
                    if (first != null)
                    {
                        endNodeRecord = first;
                        if (endNodeRecord.costSoFar <= endNodeCost)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        endNodeRecord = new NodeRecord(endNode, current.node, endNodeCost);
                    }
                    endNodeRecord.costSoFar = endNodeCost;
                    endNodeRecord.connection = current.node;

                    if (!open.Contains(endNodeRecord))
                    {
                        open.Add(endNodeRecord);
                    }
                }

                open.Remove(current);
                closed.Add(current);
            }

            if (current == null || current.node != nPos)
            {
                throw new System.Exception("No path found");
            }
            
            List<Node> path = new();
            while (current.node != startNode)
            {
                path.Insert(0, current.node);
                current = closed.First(a => a.node == current.connection);
            }
            
            path.Insert(0, startNode);
            // write to path
            this.path = new Vector3[path.Count];
            for (int i = 0; i < path.Count; i++)
            {
                this.path[i] = path[i].position;
            }
        }

        public float getConnectionCost(Node start, Node end)
        {
            return Vector3.Distance(start.position, end.position);
        }


        public class Node
        {
            public readonly Vector3 position;
            public readonly List<Node> neighbors;

            public Node(Vector3 position, List<Node> neighbors)
            {
                this.position = position;
                this.neighbors = neighbors;
            }
        }

        public class NodeRecord
        {
            public readonly Node node;
            public Node connection;
            public float costSoFar;
            
            public NodeRecord(Node node, Node connection, float costSoFar)
            {
                this.node = node;
                this.connection = connection;
                this.costSoFar = costSoFar;
            }
        }

    }
}
