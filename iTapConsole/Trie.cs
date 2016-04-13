using System.Collections.Generic;
using System.Linq;

namespace iTapConsole
{
    class Trie
    {
        readonly Node _root = new Node();
        const int LastWords = 10;
        internal class Node
        {
            public Dictionary<char, Node> Next;
            public class Value
            {
                public string Word { get; set; }
                public int Popular { get; set; }
            }
            public List<Value> NodeValues;


            public Node()
            {
                NodeValues = new List<Value>();
                Next = new Dictionary<char, Node>();
            }

            private List<Value> AddSort(Value newWord, List<Value> nodeValues)
            {
                for (int i = 0; i < nodeValues.Count; i++)
                {
                    if (nodeValues[i].Popular == newWord.Popular)
                    {
                        var compare = string.CompareOrdinal(nodeValues[i].Word, newWord.Word);
                        if (compare < 0)
                            continue;
                        nodeValues.Insert(i, newWord);
                        return nodeValues;
                    }
                    if (nodeValues[i].Popular >= newWord.Popular)
                        continue;
                    nodeValues.Insert(i, newWord);
                    return nodeValues;
                }
                nodeValues.Insert(nodeValues.Count, newWord);
                return nodeValues;
            }

            public void AddValue(Value newWord, int depth)
            {
                if (depth > 0)
                {
                    if (NodeValues.Count == 0)
                        NodeValues.Add(newWord);
                    else
                        NodeValues = AddSort(newWord, NodeValues);
                    if (NodeValues.Count > LastWords)
                        NodeValues.RemoveAt(LastWords);
                }

                if (depth >= newWord.Word.Length) return;
                Node subNode;
                if (!Next.TryGetValue(newWord.Word[depth], out subNode))
                {
                    subNode = new Node();
                    Next[newWord.Word[depth]] = subNode;
                }
                subNode.AddValue(newWord, depth + 1);
            }

            public Node GetNext(char c)
            {
                Node node;
                return Next.TryGetValue(c, out node) ? node : null;
            }

        }

        

        public void AddValue(string line)
        {
            var str = line.Split(' ');
            Node.Value newWord = new Node.Value { Word = str[0], Popular = int.Parse(str[1]) };
            _root.AddValue(newWord, 0);
        }

        public string[] FindValues(string prefix)
        {
            Node next = _root;
            int index = 0;
            while (index < prefix.Length && next != null)
            {
                next = next.GetNext(prefix[index++]);
                if (next == null)
                    return null;
            }
            return next?.NodeValues.Select(x => x.Word).ToArray();
        }
    }
}
