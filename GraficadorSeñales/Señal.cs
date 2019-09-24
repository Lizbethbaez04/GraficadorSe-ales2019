using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    abstract class Señal //Guia de cómo se debe de comportar las clases
    {
        public List<Muestra> Muestras { get; set; }
        public double TiempoInicial { get; set; }
        public double TiempoFinal { get; set; }
        public double FrecuenciaMuestreo { get; set; }

        public double AmplitudMaxima { get; set; }

        public abstract double evaluar(double tiempo);//Se va a cumplir cada evaluar que haya, pero con las especificaciones que se necesiten

        public void construirSeñal()
        {
            double periodoMuestreo = 1 / FrecuenciaMuestreo;

            Muestras.Clear();

            for(double i = TiempoInicial; i <= TiempoFinal; i += periodoMuestreo)
            {
                double muestra = evaluar(i);

                Muestras.Add(new Muestra(i, muestra));

                if(Math.Abs(muestra) > AmplitudMaxima)
                {
                    AmplitudMaxima = Math.Abs(muestra);
                }
            }
        }

        public static Señal escalarAmplitud(Señal señalOriginal, double factorEscala)
        {
            SeñalResultante resultado = new SeñalResultante();
            resultado.TiempoInicial = señalOriginal.TiempoInicial;
            resultado.TiempoFinal = señalOriginal.TiempoFinal;
            resultado.FrecuenciaMuestreo = señalOriginal.FrecuenciaMuestreo;

            foreach(var muestra in señalOriginal.Muestras)
            {
                double nuevoValor = muestra.Y * factorEscala;
                resultado.Muestras.Add(new Muestra(muestra.X, muestra.Y * factorEscala)); //El valor se cambia con todos los datos ingresados

                if (Math.Abs(nuevoValor)>resultado.AmplitudMaxima)
                {
                    resultado.AmplitudMaxima = Math.Abs(nuevoValor);
                }
            }

            return resultado;
        }

        public static Señal desplazamientoAmplitud(Señal señalDesplazamiento, double cantidadDesplazamiento )
        {
            SeñalResultante resultado = new SeñalResultante();
            resultado.TiempoInicial = señalDesplazamiento.TiempoInicial;
            resultado.TiempoFinal = señalDesplazamiento.TiempoFinal;
            resultado.FrecuenciaMuestreo = señalDesplazamiento.FrecuenciaMuestreo;

            foreach (var muestra in señalDesplazamiento.Muestras)
            {
                double nuevoValor = muestra.Y + cantidadDesplazamiento;
                resultado.Muestras.Add(new Muestra(muestra.X, nuevoValor)); //El valor se cambia con todos los datos ingresados

                if (Math.Abs(nuevoValor) > resultado.AmplitudMaxima)
                {
                    resultado.AmplitudMaxima = Math.Abs(nuevoValor);
                }
            }
            return resultado;
        }
    }
}
