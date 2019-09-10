using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraficadorSeñales
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {
            //Convertidor de texto a int
            double tiempoInicial = double.Parse(txtTiempoInicial.Text);
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo = double.Parse(txtFrecuenciaMuestreo.Text);

            //SeñalSenoidal señal = new SeñalSenoidal(amplitud, fase, frecuencia);
            //SeñalParabolica señal = new SeñalParabolica();
            //FuncionSigno señal = new FuncionSigno();
            Señal señal; //polimorfismo: que clases actuen como otras clases

            switch(cbTipoSeñal.SelectedIndex)
            {
                case 0: //Parabólica
                    señal = new SeñalParabolica();
                    break;
                case 1: //Senoidal
                    double amplitud = double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion.Children[0])).txtAmplitud.Text);
                    double fase =  double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion.Children[0])).txtFase.Text);
                    double frecuencia = double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion.Children[0])).txtFrecuencia.Text);
                    señal = new SeñalSenoidal(amplitud, fase, frecuencia);
                    break;
                case 2:
                    double alpha = double.Parse(((ConfigurcionExponencial)(panelConfiguracion.Children[0])).txtAlpha.Text);
                    señal = new SeñalExponencial(alpha);
                    break;
                default:
                    señal = null;
                    break;
            }
            señal.TiempoInicial = tiempoInicial;
            señal.TiempoFinal = tiempoFinal;
            señal.FrecuenciaMuestreo = frecuenciaMuestreo;

            señal.construirSeñal();

            double amplitudMaxima = señal.AmplitudMaxima;

            plnGrafica.Points.Clear();

            //Ayuda a recorrer todas las estructuras de datos que hay
            foreach (Muestra muestra in señal.Muestras)
            {
                plnGrafica.Points.Add(adaptarCoordenadas(muestra.X, muestra.Y, tiempoInicial, amplitudMaxima));
            }


            lblLimiteSuperior.Text = amplitudMaxima.ToString();
            lblLimiteInferior.Text = "-" + amplitudMaxima.ToString();
            //Entre más muestras haya, más calidad en la gráfica hay
            plnEjeX.Points.Clear();
            plnEjeX.Points.Add(adaptarCoordenadas(tiempoInicial, 0.0, tiempoInicial, amplitudMaxima)); //Esto es para hacer la linea del eje de las X 
            plnEjeX.Points.Add(adaptarCoordenadas(tiempoFinal, 0.0, tiempoInicial, amplitudMaxima)); //Esto es para hacer la linea del eje de las X

            //Eje Y
            plnEjeY.Points.Clear();
            plnEjeY.Points.Add(adaptarCoordenadas(0.0, amplitudMaxima, tiempoInicial, amplitudMaxima));
            plnEjeY.Points.Add(adaptarCoordenadas(0.0, -amplitudMaxima, tiempoInicial, amplitudMaxima));
        }
        public Point adaptarCoordenadas(double x, double y, double tiempoInicial, double amplitudMaxima)
        {
            return new Point((x - tiempoInicial) * scrGrafica.Width, (-1 * (y * (((scrGrafica.Height / 2.0) - 25) / amplitudMaxima))) + (scrGrafica.Height / 2.0));
        }

        private void CbTipoSeñal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            panelConfiguracion.Children.Clear();
            //selectedIndex te dice cuál esta seleccionado
            switch(cbTipoSeñal.SelectedIndex)
            {
                case 0: //Parabolica
                    panelConfiguracion.Children.Add(new ConfiguracionSeñalSenoidal());
                    break;
                case 1:  //Senoidal
                    panelConfiguracion.Children.Add(new ConfiguracionSeñalSenoidal());
                    break;
                case 2:
                    panelConfiguracion.Children.Add(new ConfigurcionExponencial());
                    break;
                default:
                    break;
            }
        }
    }
}