//#define STRASSEN
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
#if false
            Matrix<int> m1 = new Matrix<int>(3,2,2);
            Matrix<int> m2 = new Matrix<int>(2,2,2);
#else
            int[,] table1 = { { 1, 2, 0 ,5,6}, { 4, 3, -1 ,5,6} };
            int[,] table2 = { { 5, 1 }, { 2, 3 }, { 3, 4 }, { 3, 4 }, { 3, 4 } };
            Matrix<int> m1 = new Matrix<int>(table1);
            Matrix<int> m2 = new Matrix<int>(table2);
#endif
            (m1 * m2).print();
            Console.ReadLine();

        }
    }
    class Matrix<T>
    {
        public T[,] table;
#region Constructor
        public Matrix(int x = 4 , int y = 4) 
        {
            table = new T[x, y];
        }
        public Matrix(T[,] table)
        {
            this.table = table;
        }
        public Matrix(T nb , int x = 4, int y = 4):this(x,y)
        {
            for (int i = 0; i < table.GetLength(0); i++)
                for (int j = 0; j < table.GetLength(1); j++)
                    table[i, j] = nb;
        }
#endregion
        #region Operator
        public static Matrix<T> operator -(Matrix<T> m1, Matrix<T> m2)
        {
            if (m1.table.GetLength(1) != m2.table.GetLength(0))
                throw new Exception("Wrong dimension of matrix!");
            Matrix<T> result = new Matrix<T>();
            for (int i = 0; i < m1.table.GetLength(0); ++i)
                for (int j = 0; j < m1.table.GetLength(1); ++j)
                    result.table[i, j] = (dynamic)m1.table[i, j] - (dynamic)m2.table[i, j];
            return result;
        }
        public static Matrix<T> operator +(Matrix<T> m1, Matrix<T> m2)
        {
            if (m1.table.GetLength(1) != m2.table.GetLength(0))
                throw new Exception("Wrong dimension of matrix!");
            Matrix<T> result = new Matrix<T>();
            for (int i = 0; i < m1.table.GetLength(0); ++i)
                for (int j = 0; j < m1.table.GetLength(1); ++j)
                    result.table[i, j] = (dynamic)m1.table[i, j] + (dynamic)m2.table[i, j];
            return result;
        }
#if STRASSEN
        public void split(Matrix<T> P, int iB, int jB)
        {
            for (int i1 = 0, i2 = iB; i1 < table.GetLength(0); i1++, i2++)
                for (int j1 = 0, j2 = jB; j1 < table.GetLength(0); j1++, j2++)
                    table[i1,j1] = P.table[i2,j2];
        }
        public void join(Matrix<T> P, int iB, int jB)
        {
            for (int i1 = 0, i2 = iB; i1 < table.GetLength(0); i1++, i2++)
                for (int j1 = 0, j2 = jB; j1 < table.GetLength(0); j1++, j2++)
                    table[i2,j2] = table[i1,j1];
        }
        public static Matrix<T> operator *(Matrix<T> m1, Matrix<T> m2)
        {
            int msize = Math.Max(Math.Max(m1.table.GetLength(0), m1.table.GetLength(1)), Math.Max(m2.table.GetLength(0), m2.table.GetLength(1)));
            int n = m1.table.GetLength(0);
            Matrix<T> R = new Matrix<T>(n, n);
            if (msize < 32)
            {
                R = new Matrix<T>(m2.table.GetLength(1), m1.table.GetLength(0));
                for (int i = 0; i < m2.table.GetLength(1); i++)
                    for (int j = 0; j < m1.table.GetLength(0); j++)
                        for (int k = 0; k < m1.table.GetLength(1); k++)
                            R.table[i, j] += (dynamic)m1.table[j, k] * (dynamic)m2.table[k, i];
                return R;
            }
            Matrix<T> A11 = new Matrix<T>(n / 2, n / 2);
            Matrix<T> A12 = new Matrix<T>(n / 2, n / 2);
            Matrix<T> A21 = new Matrix<T>(n / 2, n / 2);
            Matrix<T> A22 = new Matrix<T>(n / 2, n / 2);
            Matrix<T> B11 = new Matrix<T>(n / 2, n / 2);
            Matrix<T> B12 = new Matrix<T>(n / 2, n / 2);
            Matrix<T> B21 = new Matrix<T>(n / 2, n / 2);
            Matrix<T> B22 = new Matrix<T>(n / 2, n / 2);
 

            A11.split(m1, 0 , 0);
            A12.split(m1, 0 , n/2);
            A21.split(m1, n/2, 0);
            A22.split(m1, n/2, n/2);

            B11.split( m2, 0 , 0);
            B12.split( m2, 0 , n/2);
            B21.split( m2, n/2, 0);
            B22.split( m2, n/2, n/2);


            Matrix<T> M1 = (A11 + A22) * (B11 + B22);
            Matrix<T> M2 = (A21 + A22) * B11;
            Matrix<T> M3 = A11 * (B12 - B22);
            Matrix<T> M4 = A22 * (B21 - B11);
            Matrix<T> M5 = (A11 + A12) * B22;
            Matrix<T> M6 = (A21 - A11) * (B11 + B12);
            Matrix<T> M7 = (A12 - A22) * (B21 + B22);


            Matrix<T> C11 = ((M1 + M4) - M5) + M7;
            Matrix<T> C12 = M3 + M5;
            Matrix<T> C21 = M2 + M4;
            Matrix<T> C22 = ((M1 + M3) - M2) + M6;
 

            R.join(C11, 0 , 0);
            R.join(C12, 0 , n/2);
            R.join(C21, n/2, 0);
            R.join(C22, n/2, n/2);
            return R;
        }
#else
        public static Matrix<T> operator *(Matrix<T> m1, Matrix<T> m2)
        {
            if (m1.table.GetLength(1) != m2.table.GetLength(0))
                throw new Exception("Wrong dimension of matrix!");
            Matrix<T> result = new Matrix<T>(m2.table.GetLength(1), m1.table.GetLength(0));
            for (int i = 0; i < m2.table.GetLength(1); i++)
                for (int j = 0; j < m1.table.GetLength(0); j++)
                    for (int k = 0; k < m1.table.GetLength(1); k++)
                        result.table[i, j] += (dynamic)m1.table[j, k] * (dynamic)m2.table[k, i];
            return result;
        }
#endif
        #endregion
        public void print()
        {
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                    Console.Write(table[j, i]+" ");
                Console.WriteLine('\n');
            }             
        }
    }
}
