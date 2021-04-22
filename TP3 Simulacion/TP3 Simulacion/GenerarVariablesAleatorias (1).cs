using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaTP3SIM
{
    class GenerarVariablesAleatorias
    {

        public GenerarVariablesAleatorias()
        {

        }
        

        private double DisUniforme(double pseudo, int min, int max)
        {
            double res = 0;
            res = min + (pseudo * (max - min));
            return res;
        }


        private double ExpNeg(double pseudo, double media)
        {
            double frec = (1 / media);
            double res = 0;
            res = (-1 / frec) * Math.Log(1 - pseudo);
            return res;
        }

        public List<double> SerieDisPoisson(double lambda)
        {
            double p = 1;
            int x = -1;
            double a = Math.Exp(-lambda);
            List<double> lista = new List<double>();
            var Random = new Random();
            while (p >= a)
            {
                var u = Random.NextDouble();
                p = p * u;
                x = x + 1;
                lista.Add(x);
            }
            return lista;
            
        }


        public List<double> SerieDisNormal(int n)
        {
            bool ban = false;
            var Random = new Random();
            List<double> lista = new List<double>();
            if (n % 2 == 1)
            {
                n += 1;
                ban = true;
            }
            for (int i = 1; i <= (n / 2); i++)
            {
                double ps1def = Random.NextDouble();
                double ps2def = Random.NextDouble();
                double res1 = (Math.Sqrt(-2 * Math.Log(ps1def)) * Math.Cos(2 * Math.PI * ps2def));
                double res2 = (Math.Sqrt(-2 * Math.Log(ps1def)) * Math.Sin(2 * Math.PI * ps2def));
                if (i == (n/2) && ban)
                {
                    lista.Add(res1);
                }
                else
                {
                    lista.Add(res1);
                    lista.Add(res2);
                }
            }
            return lista;
        }

        
        public List<double> SerieDisUniforme(int n, int min, int max)
        {
            List<double> lista = new List<double>();
            double xi = 0;
            var Random = new Random();
            for (int i = 0; i < n; i++)
            {
                double psedef = Random.NextDouble();
                double num = DisUniforme(psedef, min, max);
                lista.Add(num);
            }
            return lista;
        }


        public List<double> SerieExpNeg(int n, double media)
        {
            List<double> lista = new List<double>();
            var Random = new Random();
            for (int i = 0; i < n; i++)
            {
                double psedef = Random.NextDouble();
                double num = ExpNeg(psedef, media);
                lista.Add(num);

            }
            return lista;
        }



      
    }
}
