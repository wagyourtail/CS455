using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;


namespace Behaviors.Move.Pathfinding
{

    public class Dijkstra : FollowPath
    {
        private List<Node> nodes = new();
        
        public bool useWeights = false;
        
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

        public Node addNode([CanBeNull] GameObject gameObject, Vector3 position, List<Vector3> neighbors)
        {
            List<Node> neighborsList = new();
            foreach (Vector3 neighbor in neighbors)
            {
                bool flag = false;
                foreach (var node in nodes)
                {
                    if (Vector3.Distance(node.position, neighbor) < 0.2f)
                    {
                        flag = true;
                        neighborsList.Add(node);
                    }
                }
                if (!flag)
                    neighborsList.Add(addNode(null, neighbor, new List<Vector3>()));
            }
            // find if self exists
            Node self = new Node(gameObject, position, neighborsList);
            foreach (var node in nodes)
            {
                if (Vector3.Distance(node.position, position) < 0.2f)
                {
                    node.gameObject = gameObject;
                    self = node;
                    break;
                }
            }
            foreach (var node in neighborsList)
            {
                node.neighbors.Add(self);
            }
            nodes.Add(self);
            Debug.Log("Added node " + self.position);
            return self;
        }
        
        public Node addNode(GameObject gameObject, List<Vector3> neighbors)
        {
            return addNode(gameObject, gameObject.transform.position, neighbors);
        }

        public void addNodes(Dictionary<GameObject, List<GameObject>> nodes)
        {
            Dictionary<GameObject, Node> nodeMap = new();
            foreach (var key in nodes.Keys)
            {
                Node n = new(key, key.transform.position, new List<Node>());
                nodeMap.Add(key, n);
            }

            foreach (var key in nodes.Keys)
            {
                Node k = nodeMap[key];
                foreach (var neighbor in nodes[key])
                {
                    k.neighbors.Add(nodeMap[neighbor]);
                }
            }
            Debug.Log("Added " + nodeMap.Count + " nodes");
            this.nodes.AddRange(nodeMap.Values);
        }

        public void PathTo(Vector3 pos)
        {
            Node nPos = null;
            foreach (var node in nodes)
            {
                if (Vector3.Distance(node.position, pos) < 0.2f)
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
            float weight = 1.0f;
            if (useWeights)
            {
                if (start.gameObject != null)
                {
                    weight *= start.gameObject.GetComponent<PathNode>().weight;
                }
                if (end.gameObject != null)
                {
                    weight *= end.gameObject.GetComponent<PathNode>().weight;
                }
            }
            return Vector3.Distance(start.position, end.position) * weight;
        }


        public class Node
        {
            [CanBeNull] public GameObject gameObject;
            public readonly Vector3 position;
            public readonly List<Node> neighbors;

            public Node([CanBeNull] GameObject gameObject, Vector3 position, List<Node> neighbors)
            {
                this.gameObject = gameObject;
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
