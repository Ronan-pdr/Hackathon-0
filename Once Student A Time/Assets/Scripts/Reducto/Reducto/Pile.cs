using System;

namespace Reducto
{
    public class Pile<T>
    {
        private class Node
        {
            private Node _after;
            private T _key;

            public Node After
            {
                get => _after;
                set
                {
                    if (value == this)
                    {
                        throw new Exception("Impossible de lier un node avec lui-même");
                    }

                    _after = value;
                }
            }
            public T Key => _key;

            public Node(T key)
            {
                _after = null;
                _key = key;
            }
        }
        
        // attribut
        private Node _tete;

        // constructeur
        public Pile()
        {
            _tete = null;
        }
        
        // méthodes
        public bool IsEmpty() => _tete is null;

        public void Empiler(T key)
        {
            if (IsEmpty())
            {
                _tete = new Node(key);
            }
            else
            {
                Node node = new Node(key);
                node.After = _tete;
                _tete = node;
            }
        }

        public T Depiler()
        {
            if (IsEmpty())
            {
                throw new Exception("Cannot depiler an empty pile");
            }
            
            T res = _tete.Key;
            _tete = _tete.After;
            
            return res;
        }

        public T Premier()
        {
            if (IsEmpty())
            {
                throw new Exception("Cannot take premier in an empty pile");
            }
            
            return _tete.Key;
        }

        public override string ToString()
        {
            Node node = _tete;
            string res = "";
            
            while (!(node is null))
            {
                res = node.Key + " " + res;
                node = node.After;
            }

            return res;
        }
    }
}