using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    class SeñalParabolica : Señal
    {
        
        public SeñalParabolica()
        {
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0.0;
        }
        override public double evaluar(double tiempo) //override sirve para sobrecargar con la declaracion de la clase
        {
            //Dar valor resultado para poder regresar
            double resultado;
            if(tiempo >= 0 )
            {
                resultado = (tiempo * tiempo) / 2.0;
            }
            else
            {
                resultado = 0.0;
            }
            return resultado;
        }
    }
}
