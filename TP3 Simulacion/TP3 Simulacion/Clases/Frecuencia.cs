using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3_Simulacion.Clases
{
    class Frecuencia
    {
        public static List<Intervalo> FrecuenciasEnIntervalo(List<double> listaNumeros, int cantidadIntervalos)
        {
            var frecuencias = new List<Intervalo>();
            var maximoValor = (decimal)listaNumeros.Max();
            var minimoValor = (decimal)listaNumeros.Min();
            decimal ancho = (decimal)(maximoValor - minimoValor) / cantidadIntervalos;

            int intervaloActual = 0;

            for (int i = 0; i < cantidadIntervalos; i++)
            {
                var intervalo = new Intervalo();
                intervalo.LimiteInferior = minimoValor + (i * ancho);
                intervalo.LimiteSuperior = intervalo.LimiteInferior + ancho;
                frecuencias.Add(intervalo);
            }

            for (int j = 0; j < listaNumeros.Count; j++)
            {
                decimal numero = (decimal)listaNumeros[j];

                for (int k = 0; k < cantidadIntervalos; k++)
                {
                    if (numero <= frecuencias[k].LimiteSuperior)
                    {
                        intervaloActual = j;
                        break;
                    }
                }

                frecuencias[intervaloActual].FrecuenciaObtenida++;
            }

            return frecuencias;

        }

        public double FrecuenciaAcumuladaNormal (double x, double mu, double sigma)
        {
            MathNet.Numerics.Distributions.Normal resultado = new MathNet.Numerics.Distributions.Normal(mu, sigma);
            return resultado.CumulativeDistribution(x);
        }

        public double FrecuenciaAcumuladaPoisson(double x, double lambda)
        {
            // MathNet.Numerics.Distributions.Poisson resultado = new MathNet.Numerics.Distributions.Poisson(lambda);
            // return resultado.CumulativeDistribution(x);
            return MathNet.Numerics.Distributions.Poisson.CDF(lambda, x);
        }

        public double FrecuenciaAcumuladaExponencial(double x, double lambda)
        {
            MathNet.Numerics.Distributions.Exponential resultado = new MathNet.Numerics.Distributions.Exponential(lambda);
            return resultado.CumulativeDistribution(x);
        }

        public enum TipoDistribucion
        {
            NORMAL = 0, POISSON = 1, EXPONENCIAL = 2
        }
    }
}
