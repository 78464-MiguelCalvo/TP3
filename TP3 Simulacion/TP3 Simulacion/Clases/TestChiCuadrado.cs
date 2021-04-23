using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;


namespace TP3_Simulacion.Clases
{
    class TestChiCuadrado
    {
        public static double valorChiObtenido, valorChiMaximo;

        private List<Intervalo> listaIntervalos;
        private double media, varianza;

        public List<Intervalo> Frecuencias
        {
            get => listaIntervalos;
            set => listaIntervalos = value;
        }

        public double Media
        {
            get => media;
            set => media = value;
        }

        public double Varianza
        {
            get => varianza;
            set => varianza = value;
        }

        public static double getValorChi(List<Intervalo> listaIntervalos)
        {

            var valorChi = 0.0; // Aqui almacenaremos el valor de Chi que obtenemos

            for (int i = 0; i < listaIntervalos.Count; i++)
            {
                var frecuenciaEsperada = listaIntervalos[i].FrecuenciaEsperada;
                var restaFrecuencias = (double)listaIntervalos[i].FrecuenciaObtenida - frecuenciaEsperada;

                var numerador = Math.Pow(restaFrecuencias, 2);
                valorChi = valorChi + (numerador / frecuenciaEsperada);

            }

            return valorChi;
        }


        // Compara valores de Chi, si obtenido es menor o igual que el máximo retorna false;
        public static bool compararValoresChi(double valorObtenido, double valorMaximo)
        {
            return (valorObtenido <= valorMaximo);
        }


        public static bool getTestDeChi(List<Intervalo> listaIntervalos, double alfa)
        {
            double valorObtenido = getValorChi(listaIntervalos);
            double maximoValor = getChiMaximo(listaIntervalos.Count(), alfa);


            return compararValoresChi(valorObtenido, maximoValor);
        } 


        private static double getChiMaximo(int cantidadIntervalos, double alfa)
        {
            return (ChiSquared.InvCDF(cantidadIntervalos - 1, 1 - alfa));
        }
    }
}
