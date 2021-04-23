using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3_Simulacion.Clases
{
    class Intervalo
    {
        public Intervalo() { }
        public Intervalo(decimal limiteInferior, decimal limiteSuperior, double frecuenciaObtenida, double frecuenciaEsperada)
        {
            LimiteInferior = limiteInferior;
            LimiteSuperior = limiteSuperior;
            FrecuenciaObtenida = frecuenciaObtenida;
            FrecuenciaEsperada = frecuenciaEsperada;
            Diferencia = frecuenciaEsperada - frecuenciaObtenida < 0 ? frecuenciaObtenida - frecuenciaEsperada : frecuenciaEsperada - frecuenciaObtenida;
        }


        public double ProbabilidadObservada { get; set; }

        public double ProbabilidadEsperada { get; set; }
        public decimal LimiteInferior { get; set; }

        public decimal LimiteSuperior { get; set; }

        public double FrecuenciaObtenida { get; set; }

        public double FrecuenciaEsperada { get; set; }

        public double Diferencia { get; set; }

        // Marca de clase del intervalo
        public double Media => (double)Math.Round((LimiteInferior + LimiteSuperior) / 2, 2);

    }
}
