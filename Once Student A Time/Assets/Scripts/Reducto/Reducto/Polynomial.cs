using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Reducto
{
    public class Polynomial
    {
        // List of monomials
        private List<Monomial> _monomials;
        public ReadOnlyCollection<Monomial> Monomials => _monomials.AsReadOnly(); // Don't delete this line
        public bool IsZero => _monomials.Count == 0;

        // Constructor
        public Polynomial()
        {
            _monomials = new List<Monomial>();
        }

        public Polynomial(Monomial m)
        {
            _monomials = new List<Monomial>();

            if (!m.IsZero)
            {
                _monomials.Add(m);
            }
        }

        private Polynomial(Polynomial p)
        {
            _monomials = new List<Monomial>();
            int len = p._monomials.Count;
            for (int i = 0; i < len; i++)
            {
                _monomials.Add(p._monomials[i]);
            }
        }

        // ===== Operator overload =====
        
        // Unary -
        public static Polynomial operator -(Polynomial p)
        {
            Polynomial res = new Polynomial();
            
            int len = p._monomials.Count;
            for (int i = 0; i < len; i++)
            {
                res._monomials.Add(-p._monomials[i]);
            }

            return res;
        }
        
        // Binary +
        public static Polynomial operator +(Polynomial p1, Polynomial p2)
        {
            Polynomial res = new Polynomial(p1);

            int l = res._monomials.Count;
            foreach (Monomial m in p2._monomials)
            {
                int i;
                for (i = 0; i < l && !Monomial.HasSameDegree(m, res._monomials[i]); i++)
                {}

                if (i < l) // Un monome de degré égale existe déjà
                {
                    res._monomials[i] += m;
                    if (res._monomials[i].IsZero) 
                    {
                        res._monomials.RemoveAt(i);
                        l -= 1;
                    }
                }
                else // nouveau degrée -> nouveau monome
                {
                    res._monomials.Add(m);
                    l += 1;
                }
            }

            return res;
        }
        // Binary -
        public static Polynomial operator -(Polynomial p1, Polynomial p2)
        {
            return p1 + (-p2);
        }

        // Binary *
        public static Polynomial operator *(Polynomial p1, Polynomial p2)
        {
            Polynomial res = new Polynomial();

            foreach (Monomial m1 in p1._monomials)
            {
                Polynomial s = new Polynomial();
                foreach (Monomial m2 in p2._monomials)
                {
                    s._monomials.Add(m1 * m2);
                }

                res += s;
            }

            return res;
        }
        
        // ===== Ajout =====
        private static Monomial GetBestMonom(Polynomial p)
        {
            Monomial best = new Monomial(0);
            int l = p._monomials.Count;
            for (int i = 0; i < l; i++)
            {
                if (p._monomials[i].Degree > best.Degree)
                {
                    best = p._monomials[i];
                }
            }

            return best;
        }
        
        // Binary /
        public static Polynomial operator /(Polynomial p1, Polynomial p2)
        {
            if (p2.IsZero)
            {
                throw new ArithmeticException();
            }
            
            Polynomial r = new Polynomial(p1);
            Polynomial quot = new Polynomial();

            Monomial bestr;
            Monomial best2 = GetBestMonom(p2);
            while ((bestr = GetBestMonom(r)).Degree >= best2.Degree)
            {
                Monomial div = bestr / best2;
                
                r -= new Polynomial(div) * p2;

                if (div.IsZero)
                {
                    r._monomials.Remove(bestr);
                }
                else
                {
                    quot._monomials.Add(div);
                }
            }
            
            return quot;
        }
        
        // Binary ^
        public static Polynomial operator ^(Polynomial p1, Polynomial p2)
        {
            if (p2.IsZero)
            {
                return new Polynomial(new Monomial(1));
            }

            if (p2._monomials.Count > 1 || p2._monomials[0].Degree != 0 || p2._monomials[0].Coef < 0)
            {
                throw new ArithmeticException();
            }
            
            Polynomial res = new Polynomial(new Monomial(1));

            for (int i = 0; i < p2._monomials[0].Coef; i++)
            {
                res *= p1;
            }

            return res;
        }

        public override string ToString()
        {
            string res = "(";

            foreach (Monomial m in _monomials)
            {
                res += m;
            }

            return res + ")";
        }
    }
}