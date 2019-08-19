﻿using System;
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

            SeñalSenoidal señal = new SeñalSenoidal(amplitud, fase, frecuencia);
            double periodoMuestreo = 1.0 / frecuenciaMuestreo;

            plnGrafica.Points.Clear();
            for (double i = tiempoInicial; i <= tiempoFinal; i += periodoMuestreo)
            {
                //Tiempo actual es i
                //En la clase señal senoidal, pide el tiempo para la estructura 'evaluar', entonces se le manda la i
                //La primera i se multiplica para ver qué tan ancho es
                //La segunda i se multiplica para ver qué tan alto es
                //La segunda i se multiplica por -1 para observar bien las curvas de la señal y multiplicar por la mitad
                plnGrafica.Points.Add(new Point(i * scrGrafica.Width, -1 * (señal.evaluar(i) * scrGrafica.Height / 2.0)));
            }
        }
    }
}
