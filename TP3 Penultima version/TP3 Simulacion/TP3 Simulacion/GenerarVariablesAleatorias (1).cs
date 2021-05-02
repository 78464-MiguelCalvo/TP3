using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace BibliotecaTP3SIM
{
    class GenerarVariablesAleatorias
    {

        public GenerarVariablesAleatorias()
        {

        }
        
        private List<Double> intervalosPoisson = new List<double>();
        private List<double> varAleatorias = new List<double>();
        private Random rnd = new Random();

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



        public List<double> calcularVariablesPoisson(int cantidad, double lambda)
        {
            /* Hice que el X "máximo" sea 4 veces lambda, de esta forma la probabilidad
             * de que un valor 4 veces mayor a lambda aparezca suele ser inferior al 1% y es rarísimo que se rompa. */
            double maxX = 4 * lambda;

            for (int j = 0; j < maxX; j++)
            {
                double limiteSuperior = MathNet.Numerics.Distributions.Poisson.CDF(lambda, j);
                intervalosPoisson.Add(limiteSuperior);
            }


            for (int i = 0; i < cantidad; i++)
            {
                varAleatorias.Add(calcularSiguienteVariable());
            }

            return varAleatorias;
        }

        public double calcularSiguienteVariable()
        {
            double aleatorioDecimal = rnd.NextDouble();

            for (int variablePoisson = 0; variablePoisson < intervalosPoisson.Count; variablePoisson++)
            {

                if (aleatorioDecimal <= intervalosPoisson[variablePoisson])
                {
                    return (double)variablePoisson;
                }
            }

            /* Esto hace que todos los números aleatorios correspondientes a la cola
                    de la distribución, sean metidos en la última categoría. */
            return intervalosPoisson.Count - 1;
        }




        public List<double> SerieDisNormal(int n,double media, double desviacion)
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
                double res1 = (((Math.Sqrt(-2 * Math.Log(ps1def)) * Math.Cos(2 * Math.PI * ps2def)))*desviacion) + media;
                double res2 = (((Math.Sqrt(-2 * Math.Log(ps1def)) * Math.Sin(2 * Math.PI * ps2def))) * desviacion) + media;
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
