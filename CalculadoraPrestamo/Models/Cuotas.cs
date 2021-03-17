using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculadoraPrestamo.Models
{
    public class Cuotas
    {
        public Cuotas(int _NumeroCuota, double _Principal,double _Interes, double _Saldo)
        {
            NumeroCuota = _NumeroCuota;
            Principal = _Principal;
            Interes = _Interes;
            Saldo = _Saldo;
            TotalCuota = _Principal + _Interes;
        }
        public int NumeroCuota { get; set; }

        public double Principal { get; set; }

        public double Interes { get; set; }
        public double TotalCuota { get; set; }

        public double Saldo { get; set; }

    }
}