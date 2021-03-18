using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculadoraPrestamo.Models
{
    public class Cuotas
    {
        public Cuotas(int _NumeroCuota, double _Principal,double _Interes, double _Saldo,double _Formalizacion)
        {
            NumeroCuota = _NumeroCuota;
            Principal = _Principal;
            Interes = _Interes;
            Formalizacion = _Formalizacion;
            Saldo = _Saldo;
            //TotalCuota = _Principal + _Interes + _Formalizacion;
        }
        public int NumeroCuota { get; set; }

        public double Principal { get; set; }

        public double Interes { get; set; }
        public double Formalizacion { get; set; }
        public double TotalCuota
        { get { return Principal + Interes + Formalizacion; } }
                 

        public double Saldo { get; set; }

    }
}