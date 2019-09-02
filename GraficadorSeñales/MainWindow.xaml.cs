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

            /*plnGrafica.Points.Add(new Point(0, 10));
            plnGrafica.Points.Add(new Point(20, 15));
            plnGrafica.Points.Add(new Point(100, 50));
            plnGrafica.Points.Add(new Point(200, 1));
            plnGrafica.Points.Add(new Point(300, 70));
            plnGrafica.Points.Add(new Point(1000, 70));*/
        }

        private void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {
            //Convertidor de texto a int
            double amplitud = double.Parse(txtAmplitud.Text);
            double fase = double.Parse(txtFase.Text);
            double frecuencia = double.Parse(txtFrecuencia.Text);
            double tiempoInicial = double.Parse(txtTiempoInicial.Text);
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo = double.Parse(txtFrecuenciaMuestreo.Text);

            //SeñalSenoidal señal = new SeñalSenoidal(amplitud, fase, frecuencia);
            double periodoMuestreo = 1.0 / frecuenciaMuestreo;
            //SeñalParabolica señal = new SeñalParabolica();
            FuncionSigno señal = new FuncionSigno();

            double amplitudMaxima = 0.0;

            plnGrafica.Points.Clear();

            for(double i = tiempoInicial; i <= tiempoFinal; i += periodoMuestreo)
            {
                double valorMuestra = señal.evaluar(i);
                //Comparamos los valores para verificar si el valor muestra es mayor que amplitud maxima
                //Se le saca el absoluto para sacar la magnitud sin signo
                if(Math.Abs(valorMuestra) > amplitudMaxima)
                {
                    amplitudMaxima = Math.Abs(valorMuestra);
                }
                Muestra muestra = new Muestra(i, valorMuestra);
                señal.Muestras.Add(muestra);

            }

            //Ayuda a recorrer todas las estructuras de datos que hay
            foreach(Muestra muestra in señal.Muestras)
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
            plnEjeY.Points.Add(adaptarCoordenadas(0.0, - amplitudMaxima,tiempoInicial,amplitudMaxima));
        }
            public Point adaptarCoordenadas(double x, double y, double tiempoInicial, double amplitudMaxima)
            {
                return new Point((x - tiempoInicial) * scrGrafica.Width, (-1 * (y * (( (scrGrafica.Height / 2.0)  - 25) / amplitudMaxima) )) + (scrGrafica.Height / 2.0));
            }
    }
}
