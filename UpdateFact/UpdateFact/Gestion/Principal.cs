using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Drawing.Printing;
using System.Collections;

namespace Gestion
{
    public class Principal 
    {

        String cadenaConexion;
        clsConexion C;

        
        public Principal(String cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
            C = new clsConexion();
            C.Conectar(cadenaConexion);
        }

        


        public bool existeFactura(String numFact)
        {
            bool res = false;
            DataSet dataSet = null;
            try
            {
                dataSet = new DataSet();
                C.CargarDatos(dataSet, "dataSet", "select idfactura from factura where idfactura=" + numFact);
                res = dataSet.Tables[0].Rows.Count != 0;

            }
            catch
            {
            }
            return res;

        }

        private bool existePago(String numFactura)
        {
            DataSet dataSet = new DataSet();
            bool res=false;
            String query;
            try
            {
                query = "select * from pago_factura where idfactura="+numFactura;

                C.CargarDatos(dataSet, "DataSet",query);
                res = dataSet.Tables[0].Rows.Count != 0;
            }
            catch
            {
               
            }
            return res;
        }

        public void updateFact() {
            String nonQuery = "";
            DataSet dataSet = new DataSet();

            try
            {
                C.CargarDatos(dataSet, "DataSet", "select idfactura,total from factura");
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    String numFact = dataSet.Tables[0].Rows[i][0].ToString();
                    String total = dataSet.Tables[0].Rows[i][1].ToString();
                    if (!existePago(numFact))
                    {
                        nonQuery = "insert into pago_factura(idfactura,pagado,ibb,ganancias,suss,saldo) values("+numFact+",false,null,null,null,"+total+")";

                        C.InsertOrUpdate(nonQuery);
                    }
                }
            }
            catch
            {

            }
        }

    }
}