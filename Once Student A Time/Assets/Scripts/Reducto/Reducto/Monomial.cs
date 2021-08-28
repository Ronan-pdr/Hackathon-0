using System;

namespace Reducto
{
    public class Monomial
    {
        private readonly int _coef;
        private readonly int _degree;
        public bool IsZero => _coef == 0;
        public int Coef => _coef;
        public int Degree => _degree;

        // Constructor for Monomial
        // - A monomial of coefficient 0 is of degree -1
        // - A monomial of degree negative (and of coefficient different than 0) is an ArgumentException
        public Monomial(int coef, int degree = 0)
        {
            _coef = coef;
            
            if (coef == 0)
            {
                _degree = -1;
            }
            else
            {
                if (degree < 0)
                {
                    throw new ArgumentException();
                }
                
                _degree = degree;
            }
        }
        
        // Operator
        public static bool HasSameDegree(Monomial m1, Monomial m2)
        {
            return m1.Degree == m2.Degree;
        }

        public static bool operator ==(Monomial m1, Monomial m2)
        {
            return m1.Coef == m2.Coef &&
                   HasSameDegree(m1, m2);
        }

        public static bool operator !=(Monomial m1, Monomial m2)
        {
            return !(m1 == m2);
        }

        public static Monomial operator -(Monomial m)
        {
            return new Monomial(-m.Coef, m.Degree);
        }
            
        public static Monomial operator +(Monomial m1, Monomial m2)
        {
            if (HasSameDegree(m1, m2) || m2.IsZero)
            {
                return new Monomial(m1.Coef + m2.Coef, m1.Degree);
            }

            if (m1.IsZero)
            {
                return new Monomial(m2.Coef, m2.Degree);
            }
            
            throw new ArithmeticException();
        }

        public static Monomial operator -(Monomial m1, Monomial m2)
        {
            return m1 + (-m2);
        }
        
        public static Monomial operator *(Monomial m1, Monomial m2)
        {
            return new Monomial(m1.Coef * m2.Coef, m1.Degree + m2.Degree);
        }
        public static Monomial operator /(Monomial m1, Monomial m2)
        {
            if (m2.IsZero)
            {
                throw new ArithmeticException();
            }
            
            return new Monomial(m1.Coef / m2.Coef, m1.Degree - m2.Degree);
        }
        
        

        public override string ToString()
        {
            if (Degree == 0)
            {
                return $"{Coef}";
            }

            if (Degree == 1)
            {
                return $"{Coef}x";
            }


            return $"{Coef}x^{Degree}";
        }
    }
}