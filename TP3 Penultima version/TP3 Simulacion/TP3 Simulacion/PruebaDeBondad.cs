using BibliotecaTP3SIM;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TP3_Simulacion
{
    public partial class PruebaDeBondad : Form
    {
        private int N = 0;
        private Series serieGR = new Series();

        public PruebaDeBondad()
        {
            InitializeComponent();
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void rbUniforme_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUniforme.Checked)
            {
                grpUniforme.Enabled = true;
            }
            else
            {
                grpUniforme.Enabled = false;
            }
        }

        private void rdNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNormal.Checked)
            {
                txtMedia.Enabled = true;
                txtDesviacion.Enabled = true;
                lblDesviacion.Enabled = true;
                lblMedia.Enabled = true;
            }
            else
            {
                lblDesviacion.Enabled = false;
                lblMedia.Enabled = false;
                txtMedia.Enabled = false;
                txtDesviacion.Enabled = false;
            }
        }

        private void rdPoisson_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPoisson.Checked)
            {
                txtLambda.Enabled = true;
                lblLambda.Enabled = true;
                txtN.Enabled = false;
                Muestra.Enabled = false;
            }
            else
            {
                txtN.Enabled = true;
                Muestra.Enabled = true;
                txtLambda.Enabled = false;
                lblLambda.Enabled = false;
            }
        }

        private void rdExponencialNegativa_CheckedChanged(object sender, EventArgs e)
        {
            if (rdExponencialNegativa.Checked)
            {
                txtMedia.Enabled = true;
                lblMedia.Enabled = true;
            }
            else
            {
                txtMedia.Enabled = false;
                lblMedia.Enabled = false;
            }
        }

        

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            limparCampos();
            double Cacum = 0;
            GenerarVariablesAleatorias Metodos = new GenerarVariablesAleatorias();
            List<double> list = new List<double>();
            
       
            bool validarTamañoMuestra = int.TryParse(txtN.Text,out N);

            double funcionDensidad = 0;

            // Distribucion Uniforme
            if (rbUniforme.Checked)
            {
                int min = Convert.ToInt32(txtMin.Text);
                int max = Convert.ToInt32(txtMax.Text);
                list = Metodos.SerieDisUniforme(N, min, max);               
            }

            // Distribucion Exponencial Negativa
            if (rdExponencialNegativa.Checked)
            {
                double Media = Convert.ToDouble(txtMedia.Text);
                list = Metodos.SerieExpNeg(N, Media);
            }

            // Distribucion Normal
            if (rdNormal.Checked)
            {
                double Media = Convert.ToDouble(txtMedia.Text);
                double desviacion = Convert.ToDouble(txtDesviacion.Text);
                list = Metodos.SerieDisNormal(N,Media,desviacion);
            }

            // Distribucion Poisson
            if (rdPoisson.Checked)
            {
                double lambda = Convert.ToDouble(txtLambda.Text);
                list = Metodos.SerieDisPoisson(lambda);
            }

            // Agregar valores a la lista
            foreach (var item in list)
            {
                lstNumeros.Items.Add(Math.Round(item,4));
            }

            // Prueba de bondad
            if (txtCantidadIntervalos.Text.Equals(""))
            {
                MessageBox.Show("debe completar todos los campos");
            }
            else
            {
                double[] extremos = CalcularExtremos(list);
                double valorMin = Math.Round(extremos[0], 4);
                double valorMax = Math.Round(extremos[1], 4);
                Console.WriteLine(valorMax.ToString(), valorMin);
                double Prueba = 0;
                double ancho = Math.Round((valorMax - valorMin) / Convert.ToDouble(txtCantidadIntervalos.Text), 4);

                int FreqEs = 0;

                for (int i = 0; i < Convert.ToInt32(txtCantidadIntervalos.Text); i++)
                {

                    int contador = 0;

                    DataGridViewRow fila = new DataGridViewRow();


                    // celda intervalo
                    DataGridViewTextBoxCell celdaIntervalo = new DataGridViewTextBoxCell();


                    if (i == 0)
                    {
                        valorMax = valorMin + ancho;

                        foreach (double item in lstNumeros.Items)
                        {
                            if (item >= valorMin && item < valorMax)
                            {
                                contador = contador + 1;
                            }

                        }
                        celdaIntervalo.Value = valorMin + " - " + valorMax;
                        fila.Cells.Add(celdaIntervalo);

                    }
                    else
                    {
                        valorMin = valorMin + ancho;
                        valorMax = valorMax + ancho;
                        if (i == Convert.ToInt32(txtCantidadIntervalos.Text) - 1)
                        {
                            foreach (double item in lstNumeros.Items)
                            {
                                if (item > valorMin)
                                {
                                    contador = contador + 1;
                                }
                            }
                        }
                        else
                        {
                            foreach (double item in lstNumeros.Items)
                            {
                                if (item >= valorMin && item < valorMax)
                                {
                                    contador = contador + 1;
                                }
                            }
                        }

                        celdaIntervalo.Value = valorMin + " - " + valorMax;
                        fila.Cells.Add(celdaIntervalo);
                    }


                    //// Celda Fo
                    DataGridViewTextBoxCell celdaFo = new DataGridViewTextBoxCell();
                    Convert.ToString(contador);
                    celdaFo.Value = contador;
                    fila.Cells.Add(celdaFo);

                    //// celda Fe
                    DataGridViewTextBoxCell celdaFe = new DataGridViewTextBoxCell();
                    double CeldaAux = 0;
                    if (rbUniforme.Checked)
                    {
                        CeldaAux = (FrecuenciaAcumuladaUniforme(Convert.ToDouble(txtMax.Text), Convert.ToDouble(txtMin.Text), valorMax) - FrecuenciaAcumuladaUniforme(Convert.ToDouble(txtMax.Text), Convert.ToDouble(txtMin.Text), valorMin)) * N;

                    }

                    if (rdExponencialNegativa.Checked)
                    {

                        double lambda = 1 / Convert.ToDouble(txtMedia.Text);
                        CeldaAux = (FrecuenciaAcumuladaExponencial(lambda, valorMax) - FrecuenciaAcumuladaExponencial(lambda, valorMin)) * N;
                    }

                    if (rdNormal.Checked)
                    {
                        double Media = Convert.ToDouble(txtMedia.Text);
                        double desviacion = Convert.ToDouble(txtDesviacion.Text);
                        CeldaAux = (FrecuenciaAcumuladaNormal(valorMax, Media, desviacion) - FrecuenciaAcumuladaNormal(valorMin, Media, desviacion)) * N;
                    }
                    if (rdPoisson.Checked)
                    {
                        N = list.Count();
                        double lambda = Convert.ToDouble(txtLambda.Text);
                        CeldaAux = (FrecuenciaAcumuladaPoisson(valorMax, lambda) - FrecuenciaAcumuladaPoisson(valorMin, lambda)) * N;
                        //MessageBox.Show("Con valor max: "+ Convert.ToString(FrecuenciaAcumuladaPoisson(valorMax, lambda) + "Con valor min: "+ Convert.ToString(FrecuenciaAcumuladaPoisson(valorMin, lambda) + " = "+ CeldaAux )));
                        //MessageBox.Show("Con valor min: "+Convert.ToString(FrecuenciaAcumuladaPoisson(valorMin, lambda)));
                        //MessageBox.Show("Resta: " + CeldaAux);
                    }

                    celdaFe.Value = CeldaAux;
                    fila.Cells.Add(celdaFe);

                    // Celda C
                    DataGridViewTextBoxCell celdaC = new DataGridViewTextBoxCell();
                    double Fe = CeldaAux;
                    double Fo = Convert.ToDouble(contador);
                    double resta = Fo - Fe;

                    decimal c = Convert.ToDecimal(Math.Pow(resta, 2)) / (decimal)Fe;
                    celdaC.Value = c;
                    fila.Cells.Add(celdaC);

                    //Celda Cacum
                    DataGridViewTextBoxCell celdaCacum = new DataGridViewTextBoxCell();

                    if (i == 0)
                    {
                        Cacum = (double)c;
                    }
                    else
                    {
                        Cacum += (double)c;
                    }
                    celdaCacum.Value = Cacum;
                    fila.Cells.Add(celdaCacum);

                    //AgregarFila
                    grillaDatos.Rows.Add(fila);

                    //// Grafico
                    string intervalo = Convert.ToString(valorMin) + "-" + Convert.ToString(valorMax);
                    Series serieGR = Grafico.Series.Add(intervalo);
                    serieGR.Points.AddXY("intervalo", contador);
                }

                if (rbUniforme.Checked)
                {
                    txtChi.Text = Convert.ToString(getChiMaximo(Convert.ToInt32(txtCantidadIntervalos.Text), 0.05));
                }
                else
                {
                    txtChi.Text = Convert.ToString(getChiMaximo(Convert.ToInt32(txtCantidadIntervalos.Text) - 1, 0.05));

                }

                if (Cacum < Convert.ToDouble(txtChi.Text))
                {
                    lblConclusion.Text = "No se rechaza la hipotesis planteada.";
                    lblConclusion.BackColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblConclusion.Text = "Se rechaza la hipotesis planteada.";
                    lblConclusion.BackColor = System.Drawing.Color.Red;
                }
            }
            
        }

        public double[] CalcularExtremos(List<double> lista)
        {
            double ValorMin = 0;
            double ValorMax = 0;
            int contador = 0;

            foreach (var item in lista)
            {
                contador++;
                if (contador == 1)
                {
                    ValorMax = item;
                    ValorMin = item;
                }
                else
                {
                    if (item > ValorMax)
                    {
                        ValorMax = item;
                    }
                    if (item < ValorMin)
                    {
                        ValorMin = item;
                    }
                }
            }

            double[] listaNumeros = new double[2];
            listaNumeros[0] = ValorMin;
            listaNumeros[1] = ValorMax;

            return listaNumeros;
        }

        private void btnLimpiarCampos_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        public double FrecuenciaAcumuladaUniforme(double Max,double Min,double Lim)
        {
            double x = (Lim - Min) / (Max - Min);
            return x;
        }

        public double FrecuenciaAcumuladaExponencial(double x, double lambda)
        {
            MathNet.Numerics.Distributions.Exponential resultado = new MathNet.Numerics.Distributions.Exponential(lambda);
            return resultado.CumulativeDistribution(x);
        }

        public double FrecuenciaAcumuladaNormal(double x, double mu, double sigma)
        {
            MathNet.Numerics.Distributions.Normal resultado = new MathNet.Numerics.Distributions.Normal(mu, sigma);
            return resultado.CumulativeDistribution(x);
        }

        public double FrecuenciaAcumuladaPoisson(double x, double lambda)
        {
            //MathNet.Numerics.Distributions.Poisson resultado = new MathNet.Numerics.Distributions.Poisson(lambda);
            //return resultado.CumulativeDistribution(x);
            //MessageBox.Show(Convert.ToString(MathNet.Numerics.Distributions.Poisson.CDF(lambda, x)));
            return (MathNet.Numerics.Distributions.Poisson.CDF(lambda, x));
        }

        public void limparCampos()
        {
            lstNumeros.Items.Clear();
            grillaDatos.Rows.Clear();
            Grafico.Series.Clear();
            lblConclusion.Text = string.Empty;
            txtChi.Text = string.Empty;
        }

        public double getChiMaximo(int cantidadIntervalos, double alfa)
        {
            return (ChiSquared.InvCDF(cantidadIntervalos - 1, 1 - alfa));
        }
    }
}
