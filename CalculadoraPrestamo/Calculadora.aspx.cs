using CalculadoraPrestamo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic;
using System.Data;

namespace CalculadoraPrestamo
{
    public partial class Calculadora : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMontoCredito.Text == "" || txtPlazo.Text == "" || txtInterés.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err", "alert('Debe ingresar todos los datos del formulario.');", true);
                    return;
                }
                GenerarPlan();


            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err", "alert('" + Ex.Message + "');", true);
            }

        }

        public void GenerarPlan()
        {
            try
            {
                double Monto = Convert.ToDouble(txtMontoCredito.Text.Trim());
                int Plazos = Convert.ToInt32(txtPlazo.Text.Trim());
                double taza = Convert.ToDouble(txtInterés.Text.Trim());
                double Cuota = Math.Round(CalcularCuota(Monto,Plazos,taza), 2);
                double TIR = Math.Round(CalcularTIR(Monto, Plazos, taza,Cuota), 8); 
                double Formalizacion = CalcularFormalizacion(Monto,Plazos);

                double Principal = 0, Interes = 0, Acumulado = 0, Saldo = Monto;
                int Cont = 1;
                if (Cuota != 0)
                {
                    List<Cuotas> Plan = new List<Cuotas>();

                    while (Saldo >= 0 && Cont <= Plazos)
                    {
                        Interes = Math.Round(Saldo * (TIR),2);
                        Principal = Cuota - Interes;
                        Acumulado = Acumulado + Principal;
                        Saldo = Math.Round(Saldo,2) - Math.Round(Principal,2);                        
                        Plan.Add(new Cuotas(Cont, Principal, Interes, Saldo,Formalizacion));
                        Cont = Cont + 1;
                    }
                    if (Saldo != 0)
                    {
                        Plan[Plan.Count-1].Principal = Plan[Plan.Count-1].Principal + Saldo;
                        Plan[Plan.Count-1].Interes = Math.Round(Cuota,2) - (Math.Round(Plan[Plan.Count-1].Principal,2) + Math.Round(Saldo,2));                        
                        Plan[Plan.Count-1].Saldo = 0;
                    }

                    if (Plan.Count != 0)
                    {
                        lblPrincipal.Text = Convert.ToString(Plan.Sum(m=>m.Principal));
                        lblIntereses.Text = Convert.ToString(Plan.Sum(m => m.Interes));
                        lblFormalizacion.Text = Convert.ToString(Plan.Sum(m => m.Formalizacion));
                        lblTotal.Text = Convert.ToString(Math.Round(Plan.Sum(m => m.TotalCuota),2));

                        grvPlan.DataSource = Plan;                        
                        grvPlan.DataBind();
                    }

                }
            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err", "alert('" + Ex.Message + "');", true);
            }

        }

        public double CalcularCuota(double Monto,int Plazos, double taza)
        {
            try
            {               
                
                double Cuota_F = (Monto * taza) / Plazos;                

                return Cuota_F;
            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err", "alert('" + Ex.Message + "');", true);
                return 0;
            }
        }

        public double CalcularTIR(double Monto, int Plazos, double taza,double CuotaF)
        {
            double TIR = 0;
            double[] flujo = new double[Plazos+1];
            flujo[0] = Monto * -1;
            int cont = 1;
            for(cont=1;cont<=Plazos;cont++)
            {
                flujo[cont] = CuotaF;
            }
            TIR = computeIRR(flujo, Plazos + 1);

            return TIR;
        }

        public double CalcularFormalizacion(double Monto, int Plazos)
        {
            try
            {
                double Formalizacion = 0;
                if (Monto <= 5000)
                    Formalizacion = 200 / Plazos;
                else if (Monto > 5000 && Monto <= 10000)
                    Formalizacion = Monto * 0.001;
                else if (Monto > 10000 && Monto <= 20000)
                    Formalizacion = Monto * 0.0005;
                else if (Monto > 20000 && Monto <= 175000)
                    Formalizacion = Monto * 0.00007;

                return Math.Round(Formalizacion,2);
            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err", "alert('" + Ex.Message + "');", true);
                return 0;
            }
        }

        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }


        static double LOW_RATE = 0.01;
        static double HIGH_RATE = 0.5;
        static double MAX_ITERATION = 1000;
        static double PRECISION_REQ = 0.00000001;
        double computeIRR(double[] cf, int numOfFlows)
        {
            int i = 0, j = 0;
            double m = 0.0;
            double old = 0.00;
            double _new = 0.00;
            double oldguessRate = LOW_RATE;
            double newguessRate = LOW_RATE;
            double guessRate = LOW_RATE;
            double lowGuessRate = LOW_RATE;
            double highGuessRate = HIGH_RATE;
            double npv = 0.0;
            double denom = 0.0;
            for (i = 0; i < MAX_ITERATION; i++)
            {
                npv = 0.00;
                for (j = 0; j < numOfFlows; j++)
                {
                    denom = Math.Pow((1 + guessRate), j);
                    npv = npv + (cf[j] / denom);
                }
                /* Stop checking once the required precision is achieved */
                if ((npv > 0) && (npv < PRECISION_REQ))
                    break;
                if (old == 0)
                    old = npv;
                else
                    old = _new;
                _new = npv;
                if (i > 0)
                {
                    if (old < _new)
                    {
                        if (old < 0 && _new < 0)
                            highGuessRate = newguessRate;
                        else
                            lowGuessRate = newguessRate;
                    }
                    else
                    {
                        if (old > 0 && _new > 0)
                            lowGuessRate = newguessRate;
                        else
                            highGuessRate = newguessRate;
                    }
                }
                oldguessRate = guessRate;
                guessRate = (lowGuessRate + highGuessRate) / 2;
                newguessRate = guessRate;
            }
            return guessRate;
        }

        protected void grvPlan_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch(Exception Ex)
            {

            }
        }

        protected void grvPlan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grvPlan.PageIndex = e.NewPageIndex;
                GenerarPlan();                
            }
            catch(Exception Ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err", "alert('" + Ex.Message + "');", true);
            }

        }

        protected void grvPlan_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                GenerarPlan();
                List<Cuotas> Plan = grvPlan.DataSource as List<Cuotas>;

                List<Cuotas> SortList = new List<Cuotas>();
                if (e.SortDirection == SortDirection.Ascending)
                    SortList = Plan.OrderBy(m => m.GetType().GetProperties().First(n => n.Name == e.SortExpression).GetValue(m, null)).ToList();
                else if (e.SortDirection ==SortDirection.Descending)
                    SortList = Plan.OrderByDescending(m => m.GetType().GetProperties().First(n => n.Name == e.SortExpression).GetValue(m, null)).ToList();

                if (SortList.Count!=0)
                {
                    grvPlan.DataSource = SortList;
                    grvPlan.DataBind();
                }
                
            }
            catch(Exception Ex)
            {

            }
        }
    }
}