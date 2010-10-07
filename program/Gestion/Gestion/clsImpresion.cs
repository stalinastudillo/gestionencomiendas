using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Gestion
{
    class clsImpresion
    {
        Font font = new Font("Tahoma", 8, FontStyle.Bold);
        int paginaAjusteY = 20;
        clsRemito claseRemito = new clsRemito();

        float totalPago;
        float totalImpago;

        private List<String> dividirDescripcion(String lineaDescripcion)
        {
            int i, start, cantLineas;
            List<String> listAux = new List<string>(7);
           
            //List<String> listaConEspacios = new List<string>(7);
            //String lineaDescripcion = richTextDescripcion.Text.ToString();
            if (lineaDescripcion.Contains("\n"))
            {
                char[] separador = { '\n' };

                string[] arr = lineaDescripcion.Split(separador);

                for (i = 0; i < arr.Length; i++)
                {
                    while (arr[i].Length > 30)
                    {
                        listAux.Add(arr[i].Remove(30));
                        arr[i] = arr[i].Substring(30);
                    }
                    listAux.Add(arr[i]);
                }
                return listAux;
            }
            else
            {
                start = lineaDescripcion.Length;
                for (i = start; i < 211; i++)
                {
                    lineaDescripcion = lineaDescripcion + " ";
                }
                listAux.Add(lineaDescripcion.Substring(0, 30));
                listAux.Add(lineaDescripcion.Substring(30, 30));
                listAux.Add(lineaDescripcion.Substring(60, 30));
                listAux.Add(lineaDescripcion.Substring(90, 30));
                listAux.Add(lineaDescripcion.Substring(120, 30));
                listAux.Add(lineaDescripcion.Substring(150, 30));
                listAux.Add(lineaDescripcion.Substring(180, 30));
                return (listAux);
            }

        }

        //este procedimiento se llama con un dataset que contiene una tabla de dos columnas: idcliente y idfactura
        public int ResumenGeneralFacturas(clsConexion C, String filtro, String fechaInicial, String fechaFinal, PrintPageEventArgs e, int startindex)
        {
            DataSet dsFacturas = new DataSet();
            DataSet dsDatosFactura = new DataSet();
            DataSet dsCliente = new DataSet();
            int largo = 150;
            int maxlargo = 2450;
            int x,i;
            String filtroPago;

            if (startindex == 0)
            {
                totalPago = 0;
                totalImpago = 0;
            }

            if (filtro == "Todas")
                filtroPago = "";
            else
            {
                if (filtro == "Pagas")
                    filtroPago = " where pagado = 1";
                else filtroPago = " where pagado = 0"; 
            }
            C.CargarDatos(dsFacturas, "dsFacturas", "select * from pago_factura" + filtroPago);
            for (x = 155; x < 2058; x++)
            {
                e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(x, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo-30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            }
            e.Graphics.DrawString("|                 N      O      M      B      R      E                 |   NRO. FACTURA   |        E  M  I  S  I  O  N        |    M   O   N   T   O    |     P  A  G  A   ?     |", font, Brushes.Black, PrinterUnitConvert.Convert(150, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            i = startindex;
            while ((i < dsFacturas.Tables[0].Rows.Count) && (largo < maxlargo))
            {
                for (x = 150; x < 2058; x++)
                {
                    e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(x, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo+10, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }
                dsDatosFactura.Clear();
                dsCliente.Clear();

                C.CargarDatos(dsDatosFactura, "dsDatosFactura", "select idcliente,fechacreacion,total from factura where fechacreacion >= '" + fechaInicial + "' and fechacreacion <= '" + fechaFinal + "' and idfactura = " + dsFacturas.Tables[0].Rows[i][0].ToString());

                if (dsDatosFactura.Tables[0].Rows.Count != 0)
                {
                    largo = largo + 50;
                    C.CargarDatos(dsCliente, "dsCliente", "select nombre,apellido from clientes where idcliente = " + dsDatosFactura.Tables[0].Rows[0][0].ToString());

                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(150, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(840, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1118, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1505, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1800, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2072, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString(dsCliente.Tables[0].Rows[0][0].ToString() + " " + dsCliente.Tables[0].Rows[0][1].ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString(dsFacturas.Tables[0].Rows[i][0].ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(900, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString(dsDatosFactura.Tables[0].Rows[0][1].ToString().Remove(10), font, Brushes.Black, PrinterUnitConvert.Convert(1200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString(dsDatosFactura.Tables[0].Rows[0][2].ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(1600, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    if (dsFacturas.Tables[0].Rows[i][1].ToString() == "1")
                    {
                        e.Graphics.DrawString("SI", font, Brushes.Black, PrinterUnitConvert.Convert(1900, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                        totalPago = totalPago + (float)dsDatosFactura.Tables[0].Rows[0][2];
                    }
                    else
                    {
                        e.Graphics.DrawString("NO", font, Brushes.Black, PrinterUnitConvert.Convert(1900, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                        totalImpago = totalImpago + (float)dsDatosFactura.Tables[0].Rows[0][2];
                    }
                }
                i++;
            }
            for (x = 150; x < 2058; x++)
            {
                e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(x, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo+10, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            }
            if (i < dsFacturas.Tables[0].Rows.Count)
            {
                Boolean encontre = false;
                while ((!encontre) && (i < dsFacturas.Tables[0].Rows.Count))
                {
                    dsDatosFactura.Clear();
                    dsCliente.Clear();

                    C.CargarDatos(dsDatosFactura, "dsDatosFactura", "select idcliente,fechacreacion,total from factura where fechacreacion >= '" + fechaInicial + "' and fechacreacion <= '" + fechaFinal + "' and idfactura = " + dsFacturas.Tables[0].Rows[i][0].ToString());

                    if (dsDatosFactura.Tables[0].Rows.Count != 0)
                        encontre = true;

                    i++;
                }

                e.HasMorePages = encontre;
            }
            else
                e.HasMorePages = false;

            if (!e.HasMorePages)
            {
                if ((filtro == "Pagas") || (filtro == "Todas"))
                {
                    largo = largo + 50;
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1505, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1800, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2072, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("Total Pago", font, Brushes.Black, PrinterUnitConvert.Convert(1530, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    for (x = 1510; x < 2058; x++)
                    {
                        e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(x, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo +10, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    }
                    e.Graphics.DrawString("" + totalPago, font, Brushes.Black, PrinterUnitConvert.Convert(1830, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }
                if ((filtro == "Impagas") || (filtro == "Todas"))
                {
                    largo = largo + 50;
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1505, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1800, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2072, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("Total Impago", font, Brushes.Black, PrinterUnitConvert.Convert(1530, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    for (x = 1510; x < 2058; x++)
                    {
                        e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(x, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo +10, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    }
                    e.Graphics.DrawString("" + totalImpago, font, Brushes.Black, PrinterUnitConvert.Convert(1830, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }
                if (filtro == "Todas")
                {
                    largo = largo + 50;
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1505, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1800, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2072, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    for (x = 1510; x < 2058; x++)
                    {
                        e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(x, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo +10, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    }
                    e.Graphics.DrawString("Total", font, Brushes.Black, PrinterUnitConvert.Convert(1530, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    totalPago = totalPago + totalImpago;
                    e.Graphics.DrawString("" + totalPago, font, Brushes.Black, PrinterUnitConvert.Convert(1830, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }
            }

            return i;
        }

        public int FacturasDeUnCliente(clsConexion C, String idcliente, String filtro, String fechaInicial, String fechaFinal, PrintPageEventArgs e, int startindex)
        {
            DataSet dsFacturas = new DataSet();
            DataSet dsDatosFactura = new DataSet();
            DataSet dsCliente = new DataSet();
            int largo = 200;
            int maxlargo = 2500;
            int i,x;
            String filtroFecha = "";

            if (startindex == 0)
            {
                totalPago = 0;
                totalImpago = 0;
            }
            i = startindex;

            C.CargarDatos(dsCliente, "dsCliente", "select nombre,apellido from clientes where idcliente = " + idcliente);

            if (filtro == "Todas")
                C.CargarDatos(dsFacturas, "dsFacturas", "select * from pago_factura");
            else
            {
                if (filtro == "Pagas")
                    C.CargarDatos(dsFacturas, "dsFacturas", "select * from pago_factura where pagado = 1");
                else C.CargarDatos(dsFacturas, "dsFacturas", "select * from pago_factura where pagado = 0");
            }
            for (x = 155; x < 2058; x++)
            {
                e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(x, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo - 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            }
            e.Graphics.DrawString("|      N  R  O.    F  A  C  T  U  R  A      |         E    M    I    S    I    O    N         |           M      O      N      T      O           |         P    A    G    A     ?         |", font, Brushes.Black, PrinterUnitConvert.Convert(150, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            if (fechaInicial != "")
            {
                e.Graphics.DrawString("Resumen de facturas de " + dsCliente.Tables[0].Rows[0][0].ToString() + dsCliente.Tables[0].Rows[0][1].ToString() + "; Periodo: " + fechaInicial + " al " + fechaFinal, font, Brushes.Black, PrinterUnitConvert.Convert(150, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo - 100, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                filtroFecha = " and fechacreacion >= '" + fechaInicial + "' and fechacreacion <= '" + fechaFinal + "'";
            }
            else
            {
                e.Graphics.DrawString("Resumen de facturas de " + dsCliente.Tables[0].Rows[0][0].ToString() + dsCliente.Tables[0].Rows[0][1].ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(150, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo - 100, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            }

            while ((i < dsFacturas.Tables[0].Rows.Count) && (largo < maxlargo))
            {
                dsDatosFactura.Clear();
                dsCliente.Clear();

                C.CargarDatos(dsDatosFactura, "dsDatosFactura", "select idcliente,fechacreacion,total from factura where idfactura = " + dsFacturas.Tables[0].Rows[i][0].ToString() + filtroFecha);

                if (dsDatosFactura.Tables[0].Rows.Count != 0)
                {
                    for (x = 150; x < 2058; x++)
                    {
                        e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(x, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 10, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    }

                    if (dsDatosFactura.Tables[0].Rows[0][0].ToString() == idcliente)
                    {
                        largo = largo + 50;

                        e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(150, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                        e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(647, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                        e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1145, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                        e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1660, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                        e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2070, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                        e.Graphics.DrawString(dsFacturas.Tables[0].Rows[i][0].ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                        e.Graphics.DrawString(dsDatosFactura.Tables[0].Rows[0][1].ToString().Remove(10), font, Brushes.Black, PrinterUnitConvert.Convert(800, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                        e.Graphics.DrawString(dsDatosFactura.Tables[0].Rows[0][2].ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(1250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                        if (dsFacturas.Tables[0].Rows[i][1].ToString() == "1")
                        {
                            e.Graphics.DrawString("SI", font, Brushes.Black, PrinterUnitConvert.Convert(1800, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                            totalPago = totalPago + (float)dsDatosFactura.Tables[0].Rows[0][2];
                        }
                        else
                        {
                            e.Graphics.DrawString("NO", font, Brushes.Black, PrinterUnitConvert.Convert(1800, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                            totalImpago = totalImpago + (float)dsDatosFactura.Tables[0].Rows[0][2];
                        }
                    }
                }
                i++;
            }

            for (x = 150; x < 2058; x++)
            {
                e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(x, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 10, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            }

            if (i < dsFacturas.Tables[0].Rows.Count)
            {
                Boolean encontre = false;
                while ((i < dsFacturas.Tables[0].Rows.Count) && (!encontre))
                {
                    dsDatosFactura.Clear();
                    dsCliente.Clear();
                    C.CargarDatos(dsDatosFactura, "dsDatosFactura", "select idcliente,fechacreacion,total from factura where idfactura = " + dsFacturas.Tables[0].Rows[i][0].ToString() + " and fechacreacion >= '" + fechaInicial + "' and fechacreacion <= '" + fechaFinal + "'");
                    if (dsDatosFactura.Tables[0].Rows.Count != 0)
                        encontre = true;

                    i++;
                }
                e.HasMorePages = encontre;
            }
            else e.HasMorePages = false;

            if (!e.HasMorePages)
            {
                if ((filtro == "Pagas") || (filtro == "Todas"))
                {
                    largo = largo + 50;
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1145, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1660, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2072, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("Total Pago", font, Brushes.Black, PrinterUnitConvert.Convert(1200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    for (x = 1145; x < 2058; x++)
                    {
                        e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(x, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 10, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    }
                    e.Graphics.DrawString("" + totalPago, font, Brushes.Black, PrinterUnitConvert.Convert(1700, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }
                if ((filtro == "Impagas") || (filtro == "Todas"))
                {
                    largo = largo + 50;
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1145, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1660, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2072, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("Total Impago", font, Brushes.Black, PrinterUnitConvert.Convert(1200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    for (x = 1145; x < 2058; x++)
                    {
                        e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(x, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 10, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    }
                    e.Graphics.DrawString("" + totalImpago, font, Brushes.Black, PrinterUnitConvert.Convert(1700, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }
                if (filtro == "Todas")
                {
                    largo = largo + 50;
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1145, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1660, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2072, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    for (x = 1145; x < 2058; x++)
                    {
                        e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(x, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 10, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    }
                    e.Graphics.DrawString("Total", font, Brushes.Black, PrinterUnitConvert.Convert(1200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    totalPago = totalPago + totalImpago;
                    e.Graphics.DrawString("" + totalPago, font, Brushes.Black, PrinterUnitConvert.Convert(1700, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }
            }

            return i;


        }

        public void FacturaAContado(clsConexion C, clsRemito Remito, PrintPageEventArgs e,String idfactura)
        {
            DataSet dsRemitoA = new DataSet();
            DataSet dsFacturaA = new DataSet();
            DataSet dsEmpresa = new DataSet();
            DataSet dsCliente = new DataSet();
            DataSet dsFacturaCliente = new DataSet();

            DataSet dsFecha = new DataSet();
            String detalleFact;    

            String idFact = idfactura;

            dsFacturaA.Clear();
            C.CargarDatos(dsFacturaA, "dsFacturaA", "select flete,seguro,total from factura where idfactura=" + idFact);

            C.CargarDatos(dsRemitoA, "dsRemitoA", "select * from remito where idfactura=" + idFact);
            Console.WriteLine("select * from remito where idfactura=" + idFact);
            detalleFact = Remito.getInfoRemito(C, dsRemitoA.Tables[0].Rows[0][0].ToString());

            dsFecha.Clear();
            C.CargarDatos(dsFecha, "dsFecha", "select fechacreacion from factura where idfactura=" + idFact);

            String fecha = dsFecha.Tables[0].Rows[0][0].ToString().Remove(10);
            String remitente = (dsRemitoA.Tables[0].Rows[0][2].ToString());
            String dirRemitente = (dsRemitoA.Tables[0].Rows[0][3].ToString());
            String ciudadRemitente = (dsRemitoA.Tables[0].Rows[0][4].ToString());
            String cuilRemitente = (dsRemitoA.Tables[0].Rows[0][5].ToString());

            String destinatario = (dsRemitoA.Tables[0].Rows[0][6].ToString());
            String dirDestinatario = (dsRemitoA.Tables[0].Rows[0][7].ToString());
            String ciudadDestinatario = (dsRemitoA.Tables[0].Rows[0][8].ToString());
            String cuilDestinatario = (dsRemitoA.Tables[0].Rows[0][9].ToString());

            String IVA = (dsRemitoA.Tables[0].Rows[0][11].ToString());

            String VD = (dsRemitoA.Tables[0].Rows[0][12].ToString());
            String CR = (dsRemitoA.Tables[0].Rows[0][13].ToString());

            String condVenta = (dsRemitoA.Tables[0].Rows[0][10].ToString());

            DataSet aux = new DataSet();
            C.CargarDatos(aux, "aux", "select flete,seguro,total,neto,ivari from factura where idfactura=" + idFact);

            String flete = (aux.Tables[0].Rows[0][0].ToString()); ;
            String seguro = (aux.Tables[0].Rows[0][1].ToString()); ;
            String total = (aux.Tables[0].Rows[0][2].ToString()); ;
            String neto = (aux.Tables[0].Rows[0][3].ToString());
            String ivari = (aux.Tables[0].Rows[0][4].ToString());

            int offset = -120; //esto es porque el idiota de Mauro cambió las facturas, así que hubo que correr todo para la izquierda

            //Imprime la fecha actual
            //Fecha 1100 / 1170 / 1250 - 270
            e.Graphics.DrawString(fecha, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1050, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(200 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));


            //Remitente 400 - 550
            e.Graphics.DrawString(remitente, font, Brushes.Black, PrinterUnitConvert.Convert(offset+390, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(480, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Destinatario 1200 - 550 
            e.Graphics.DrawString(destinatario, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1190, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(480 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Domicilio 380 - 610
            e.Graphics.DrawString(dirRemitente, font, Brushes.Black, PrinterUnitConvert.Convert(offset+370, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(540, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Domicilio 1160 - 610
            e.Graphics.DrawString(dirDestinatario, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(540 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Localidad 400 - 670
            e.Graphics.DrawString(ciudadRemitente, font, Brushes.Black, PrinterUnitConvert.Convert(offset+390, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(590, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Localidad 1170 - 670
            e.Graphics.DrawString(ciudadDestinatario, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1190, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(590 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //C.U.I.T. 340 - 730
            e.Graphics.DrawString(cuilRemitente, font, Brushes.Black, PrinterUnitConvert.Convert(offset+340, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(660, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //C.U.I.T. 1120 - 730
            e.Graphics.DrawString(cuilDestinatario, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1110, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(660 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            //Contado 525 - 800
            e.Graphics.DrawString("x", font, Brushes.Black, PrinterUnitConvert.Convert(offset+460, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(760 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //flete	
            //Contado 1410 - 1420
            e.Graphics.DrawString(flete, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1460, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1410 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Seguro 
            //Contado 1410 - 1480
            e.Graphics.DrawString(seguro, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1460, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1475 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            //neto 1410 - 1540
            e.Graphics.DrawString(neto, font, Brushes.Black, PrinterUnitConvert.Convert(offset + 1460, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1545 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //iva ri 1410 - 1600
            e.Graphics.DrawString(ivari, font, Brushes.Black, PrinterUnitConvert.Convert(offset + 1460, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1605 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            //Total
            //Contado 1410 - 1720
            e.Graphics.DrawString(total, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1460, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1670 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));


            e.Graphics.DrawString(detalleFact, font, Brushes.Black, PrinterUnitConvert.Convert(offset+320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(950 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            e.Graphics.DrawString(VD, font, Brushes.Black, PrinterUnitConvert.Convert(offset+350, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1350, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            e.Graphics.DrawString(CR, font, Brushes.Black, PrinterUnitConvert.Convert(offset+350, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1400, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));


            //Resp. Insc. 1220 - 800
            e.Graphics.DrawString("x", font, Brushes.Black, PrinterUnitConvert.Convert(offset+1155, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(745 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));


        }

        public void FacturaACuentaCorriente(clsConexion C, clsRemito Remito, PrintPageEventArgs e,String idfactura)
        {
            DataSet dsRemitoA = new DataSet();
            DataSet dsFacturaA = new DataSet();
            DataSet dsEmpresa = new DataSet();
            DataSet dsDetalleFact = new DataSet();
            DataSet dsCliente = new DataSet();
            DataSet dsFacturaCliente = new DataSet();
            int offset = -120; //esto es porque el idiota de Mauro cambió las facturas, así que hubo que correr todo para la izquierda
            DataSet dsFecha = new DataSet();

            String idcliente;
            String detalleFact;

            //C.CargarDatos(dsFacturaA, "dsFacturaA", "select max(idfactura) from factura");

            String idFact = idfactura;

            dsFacturaA.Clear();
            C.CargarDatos(dsFacturaA, "dsFacturaA", "select flete,seguro,total from factura where idfactura=" + idFact);
            C.CargarDatos(dsDetalleFact, "dsDetalleFact", "select descripcion from factura where idfactura=" + idFact);
            detalleFact = dsDetalleFact.Tables[0].Rows[0][0].ToString();

            C.CargarDatos(dsRemitoA, "dsRemitoA", "select * from remito where idfactura=" + idFact);
            Console.WriteLine("select * from remito where idfactura=" + idFact);

            dsFecha.Clear();
            C.CargarDatos(dsFecha, "dsFecha", "select fechacreacion from factura where idfactura=" + idFact);

            String fecha = dsFecha.Tables[0].Rows[0][0].ToString().Remove(10);

            C.CargarDatos(dsEmpresa, "dsEmpresa", "select * from empresa");


            String remitente = (dsEmpresa.Tables[0].Rows[0][1].ToString());
            String dirRemitente = (dsEmpresa.Tables[0].Rows[0][2].ToString());
            String telefonoEmpresa = (dsEmpresa.Tables[0].Rows[0][3].ToString());
            String ciudadRemitente = (dsEmpresa.Tables[0].Rows[0][4].ToString());
            String cuilRemitente = "";




            C.CargarDatos(dsFacturaCliente, "dsFacturaCliente", "select idcliente from factura where idfactura=" + idFact);

            idcliente = dsFacturaCliente.Tables[0].Rows[0][0].ToString();

            C.CargarDatos(dsCliente, "dsCliente", "select nombre,apellido,direccion,CUIL,ciudad from clientes where idcliente=" + idcliente);



            String destinatario = (dsCliente.Tables[0].Rows[0][0].ToString() + " " + dsCliente.Tables[0].Rows[0][1].ToString());
            String dirDestinatario = (dsCliente.Tables[0].Rows[0][2].ToString());
            String ciudadDestinatario = (dsCliente.Tables[0].Rows[0][4].ToString());
            String cuilDestinatario = (dsCliente.Tables[0].Rows[0][3].ToString());

            String IVA = (dsRemitoA.Tables[0].Rows[0][11].ToString());

            String VD = (dsRemitoA.Tables[0].Rows[0][12].ToString());
            String CR = (dsRemitoA.Tables[0].Rows[0][13].ToString());

            String condVenta = (dsRemitoA.Tables[0].Rows[0][10].ToString());

            DataSet aux = new DataSet();
            C.CargarDatos(aux, "aux", "select flete,seguro,total,neto,ivari from factura where idfactura=" + idFact);

            String flete = (aux.Tables[0].Rows[0][0].ToString()); ;
            String seguro = (aux.Tables[0].Rows[0][1].ToString()); ;
            String total = (aux.Tables[0].Rows[0][2].ToString()); ;
            String neto = (aux.Tables[0].Rows[0][3].ToString());
            String ivari = (aux.Tables[0].Rows[0][4].ToString());

            //Font font = new Font("Tahoma", 10, FontStyle.Bold);

            //Imprime la fecha actual
            //Fecha 1100 / 1170 / 1250 - 270
            e.Graphics.DrawString(fecha, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1050, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            //Remitente 400 - 550
            e.Graphics.DrawString(remitente, font, Brushes.Black, PrinterUnitConvert.Convert(offset+390, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(480, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Destinatario 1200 - 550 
            e.Graphics.DrawString(destinatario, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1190, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(480 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Domicilio 380 - 610
            e.Graphics.DrawString(dirRemitente, font, Brushes.Black, PrinterUnitConvert.Convert(offset+370, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(540, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Domicilio 1160 - 610
            e.Graphics.DrawString(dirDestinatario, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(540 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Localidad 400 - 670
            e.Graphics.DrawString(ciudadRemitente, font, Brushes.Black, PrinterUnitConvert.Convert(offset+390, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(590, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Localidad 1170 - 670
            e.Graphics.DrawString(ciudadDestinatario, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1190, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(590 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //C.U.I.T. 340 - 730
            e.Graphics.DrawString(cuilRemitente, font, Brushes.Black, PrinterUnitConvert.Convert(offset+340, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(660, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //C.U.I.T. 1120 - 730
            e.Graphics.DrawString(cuilDestinatario, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1110, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(660 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));


            //Cuenta Corriente 910 - 800
            e.Graphics.DrawString("x", font, Brushes.Black, PrinterUnitConvert.Convert(offset+820, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(735, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Flete
            //A cobrar 1180 - 1420
            e.Graphics.DrawString(flete, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1460, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1410 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //seguro
            //A cobrar 1180 - 1480
            e.Graphics.DrawString(seguro, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1460, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1475 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            //Neto
            e.Graphics.DrawString(neto, font, Brushes.Black, PrinterUnitConvert.Convert(offset + 1460, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1545 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //IVA R.I.
            e.Graphics.DrawString(ivari, font, Brushes.Black, PrinterUnitConvert.Convert(offset + 1460, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1605 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            //total
            //A cobrar 1180 - 1720
            e.Graphics.DrawString(total, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1460, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1670 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));


            List<String> detalleDividido = new List<String>(7);
            //detalleFact = richTextDescripcion.Text.ToString();
            detalleDividido = dividirDescripcion(detalleFact);

            try
            {
                e.Graphics.DrawString(detalleDividido.ElementAt(0), font, Brushes.Black, PrinterUnitConvert.Convert(offset+320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(900, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(detalleDividido.ElementAt(1), font, Brushes.Black, PrinterUnitConvert.Convert(offset+320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(950, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(detalleDividido.ElementAt(2), font, Brushes.Black, PrinterUnitConvert.Convert(offset+320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1000, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(detalleDividido.ElementAt(3), font, Brushes.Black, PrinterUnitConvert.Convert(offset+320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1050, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(detalleDividido.ElementAt(4), font, Brushes.Black, PrinterUnitConvert.Convert(offset+320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1100, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(detalleDividido.ElementAt(5), font, Brushes.Black, PrinterUnitConvert.Convert(offset+320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1150, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(detalleDividido.ElementAt(6), font, Brushes.Black, PrinterUnitConvert.Convert(offset+320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            }
            catch { }

            //Resp. Insc. 1220 - 800
            e.Graphics.DrawString("x", font, Brushes.Black, PrinterUnitConvert.Convert(offset+1155, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(745 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
        }

        public int DetalleRemitos(clsConexion C, DataGridView dataGridRemitos, int indexProd, String idFact, PrintPageEventArgs e)
        {
            DataSet dsFactura = new DataSet();
            DataSet dsEmpresa = new DataSet();
            //String dateFechaDesde = principal.fechaInicialLiquidacion();
            //String dateFechaHasta = principal.fechaFinalLiquidacion();
            //C.CargarDatos(dsFactura, "dsFactura", "select max(idfactura) from factura");

            //String idFact = ((dsFactura.Tables[0].Rows[0][0]).ToString());
            DataSet aux = new DataSet();
            C.CargarDatos(aux, "aux", "select neto,total from factura where idfactura=" + idFact);

            String subtotal = (aux.Tables[0].Rows[0][0].ToString());
            String total = (aux.Tables[0].Rows[0][1].ToString());

            double credito, preciofinal;

            int largo, maxLargo, cantProd, i;

            Font font = new Font("Tahoma", 10, FontStyle.Bold);

            cantProd = dataGridRemitos.Rows.Count - 1;
            maxLargo = 1700;

            dsEmpresa.Clear();
            C.CargarDatos(dsEmpresa, "dsEmpresa", "select * from empresa");

            int idEmp = (int.Parse(dsEmpresa.Tables[0].Rows[0][0].ToString()));
            String nombreEmpresa = (dsEmpresa.Tables[0].Rows[0][1].ToString());
            String direccionEmpresa = (dsEmpresa.Tables[0].Rows[0][2].ToString());
            String telefonoEmpresa = (dsEmpresa.Tables[0].Rows[0][3].ToString());
            String ciudadEmpresa = (dsEmpresa.Tables[0].Rows[0][4].ToString());

            DataSet auxNumRemito = new DataSet();
            C.CargarDatos(auxNumRemito, "auxNumRemito", "select idremito from remito where idfactura=" + idFact);
            String idRemito = auxNumRemito.Tables[0].Rows[0][0].ToString();

            DataSet auxClienteRemito = new DataSet();
            C.CargarDatos(auxClienteRemito, "auxClienteRemito", "select idcliente from remito where idremito=" + idRemito);
            String idCliente = auxClienteRemito.Tables[0].Rows[0][0].ToString();

            DataSet auxCliente = new DataSet();
            C.CargarDatos(auxCliente, "auxCliente", "select nombre,apellido from clientes where idcliente=" + idCliente);

            String nombre = auxCliente.Tables[0].Rows[0][0].ToString();
            String apellido = auxCliente.Tables[0].Rows[0][1].ToString();

            DataSet auxCredito = new DataSet();

            C.CargarDatos(auxCredito, "auxCredito", "select credito from factura where idfactura=" + idFact);
            String creditoCliente = auxCredito.Tables[0].Rows[0][0].ToString();


            String infoCLiente = idCliente + "-" + nombre + " " + apellido;

            //Imprime el nombre de la empresa
            e.Graphics.DrawString(nombreEmpresa, font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime la direccion y el telefono de la empresa
            e.Graphics.DrawString(direccionEmpresa + "-" + telefonoEmpresa, font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime la ciudad de la empresa
            e.Graphics.DrawString(ciudadEmpresa, font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(300, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime el resumen de cuenta
            e.Graphics.DrawString("RESUMEN DE CUENTA CORRIENTE", font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(350, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            //Imprime el numero y nombre de cliente

            e.Graphics.DrawString(infoCLiente, font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(400, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            DataSet auxFechasFactura = new DataSet();

            C.CargarDatos(auxFechasFactura, "auxFechasFactura", "select FechaFirst,FechaLast from factura where idfactura=" + idFact);

            String dateFechaDesde = auxFechasFactura.Tables[0].Rows[0][0].ToString();
            String dateFechaHasta = auxFechasFactura.Tables[0].Rows[0][1].ToString();

            dateFechaDesde = ("D E S D E:  " + dateFechaDesde).Remove(23);
            dateFechaHasta = ("H A S T A:  " + dateFechaHasta).Remove(23);

            e.Graphics.DrawString(dateFechaDesde, font, Brushes.Black, PrinterUnitConvert.Convert(1450, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(400, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            e.Graphics.DrawString(dateFechaHasta, font, Brushes.Black, PrinterUnitConvert.Convert(1950, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(400, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            //imprime una linea divisoria
            for (i = 255; i < 2500; i++)
            {
                e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(500, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            }
            //imprime el header de la tabla
            e.Graphics.DrawString("|     C O M P R O B A N T E     |   E  M  I  S  I  O  N   |        D   E   S   C   R   I   P   C   I   O   N        |    D   E   B   E    |    H  A  B  E  R    |", font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(550, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            for (i = 255; i < 2500; i++)
            {
                e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(560, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            }
            largo = 610;

            while ((indexProd <= cantProd) && (largo < maxLargo))
            {
                Console.WriteLine(dataGridRemitos.Rows[indexProd].Cells[1].ToString());

                if (dataGridRemitos.Rows[indexProd].Cells[0].Value.ToString() == "True")
                {//imprimo una linea unicamente si esta tildado el casillero de LIQUIDAR
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2520, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

                    //Cada uno de los valores a continuacion debera ser reemplazado por un valor del datagrid de remitos
                    //Por ejemplo, para "Descripcion" usamos dataGridRemitos.Rows[indexProd].Cells[3].ToString()
                    //Por algun motivo no se imprime bien la string del datagrid, VER!!! Ya esta todo alineado en su lugar
                    e.Graphics.DrawString(dataGridRemitos.Rows[indexProd].Cells[1].Value.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(280, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(765, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString(dataGridRemitos.Rows[indexProd].Cells[2].Value.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(795, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString(dataGridRemitos.Rows[indexProd].Cells[3].Value.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(1170, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1905, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString(dataGridRemitos.Rows[indexProd].Cells[4].Value.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(1935, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    //e.Graphics.DrawString(creditoCliente, font, Brushes.Black, PrinterUnitConvert.Convert(2230, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    largo = largo + 50;
                }
                indexProd++;

            }

            if (indexProd >= cantProd)
            {
                //imprime una linea divisoria
                for (i = 255; i < 2500; i++)
                {
                    e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo - 20, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }//imprime la fila de SALDO, con DEBE y HABER totales.
                e.Graphics.DrawString("|         S     U     B     T     O     T     A     L         |", font, Brushes.Black, PrinterUnitConvert.Convert(1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(subtotal, font, Brushes.Black, PrinterUnitConvert.Convert(1965, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(creditoCliente, font, Brushes.Black, PrinterUnitConvert.Convert(2230, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2520, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                for (i = 1140; i < 2500; i++)
                {
                    e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 50, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }
                largo = largo + 70;
                e.Graphics.DrawString("|                   T      O      T      A      L                  |", font, Brushes.Black, PrinterUnitConvert.Convert(1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(total, font, Brushes.Black, PrinterUnitConvert.Convert(2100, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2520, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                for (i = 1140; i < 2500; i++)
                {
                    e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 50, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }
                if (creditoCliente != "0")
                {
                    credito = double.Parse(creditoCliente);
                    if (double.Parse(total) <= credito)
                    {
                        preciofinal = 0;
                    }
                    else
                    {
                        preciofinal = Math.Round(double.Parse(total) - credito, 2);
                    }
                    largo = largo + 70;
                    e.Graphics.DrawString("|    T   O   T   A   L   -   C   R   E   D   I   T   O    |", font, Brushes.Black, PrinterUnitConvert.Convert(1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString(preciofinal.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(2100, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2520, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    for (i = 1140; i < 2500; i++)
                    {
                        e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 50, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    }
                }
                e.HasMorePages = false;
                //principal.limpiarRemitos();
            }
            else
            {
                for (i = 255; i < 2500; i++)
                {
                    e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo - 20, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }
                e.HasMorePages = true;
            }
            return indexProd;
        }

        public void FacturaBCuentaCorriente(clsConexion C, clsRemito Remito, PrintPageEventArgs e,String idfactura)
        {
            String idcliente;
            String detalleFact;
            int offset = 0; //esto es porque el idiota de Mauro cambió las facturas, así que hubo que correr todo para la izquierda
            DataSet dsRemitoB = new DataSet();
            //DataSet dsFacturaB = new DataSet();
            DataSet dsEmpresa = new DataSet();
            DataSet dsCliente = new DataSet();
            DataSet dsDetalleFact = new DataSet();
            DataSet dsFacturaCliente = new DataSet();

            DataSet dsFecha = new DataSet();


            //C.CargarDatos(dsFacturaB, "dsFacturaB", "select max(idfactura) from factura");
            String idFact = idfactura;


            C.CargarDatos(dsDetalleFact, "dsDetalleFact", "select descripcion from factura where idfactura=" + idFact);
            detalleFact = dsDetalleFact.Tables[0].Rows[0][0].ToString();
            //dsFacturaB.Clear();
            //C.CargarDatos(dsFacturaB, "dsFactura", "select flete,seguro,neto from factura where idfactura=" + idFact);

            C.CargarDatos(dsRemitoB, "dsRemito", "select * from remito where idfactura=" + idFact);

            dsFecha.Clear();
            C.CargarDatos(dsFecha, "dsFecha", "select fechacreacion from factura where idfactura=" + idFact);

            String fecha = dsFecha.Tables[0].Rows[0][0].ToString().Remove(10);

            C.CargarDatos(dsEmpresa, "dsEmpresa", "select * from empresa");

            String remitente = (dsEmpresa.Tables[0].Rows[0][1].ToString());
            String dirRemitente = (dsEmpresa.Tables[0].Rows[0][2].ToString());
            String telefonoEmpresa = (dsEmpresa.Tables[0].Rows[0][3].ToString());
            String ciudadRemitente = (dsEmpresa.Tables[0].Rows[0][4].ToString());
            String cuilRemitente = "";

            C.CargarDatos(dsFacturaCliente, "dsFacturaCliente", "select idcliente from factura where idfactura=" + idFact);
            idcliente = dsFacturaCliente.Tables[0].Rows[0][0].ToString();
            C.CargarDatos(dsCliente, "dsCliente", "select nombre,apellido,direccion,CUIL,ciudad from clientes where idcliente=" + idcliente);

            String destinatario = (dsCliente.Tables[0].Rows[0][0].ToString() + " " + dsCliente.Tables[0].Rows[0][1].ToString());
            String dirDestinatario = (dsCliente.Tables[0].Rows[0][2].ToString());
            String ciudadDestinatario = (dsCliente.Tables[0].Rows[0][4].ToString());
            String cuilDestinatario = (dsCliente.Tables[0].Rows[0][3].ToString());

            String VD = (dsRemitoB.Tables[0].Rows[0][12].ToString());
            String CR = (dsRemitoB.Tables[0].Rows[0][13].ToString());

            String IVA = (dsRemitoB.Tables[0].Rows[0][11].ToString());

            String condVenta = (dsRemitoB.Tables[0].Rows[0][10].ToString());

            DataSet aux = new DataSet();
            C.CargarDatos(aux, "aux", "select flete,seguro,total,neto,ivari from factura where idfactura=" + idFact);

            String flete = (aux.Tables[0].Rows[0][0].ToString());
            String seguro = (aux.Tables[0].Rows[0][1].ToString());
            //Mauro dijo que la factura B va SIN EL IVA, así que ahora imprimimos la columna NETO de aux
            String total = (aux.Tables[0].Rows[0][3].ToString());

            //Font font = new Font("Tahoma", 10, FontStyle.Bold);

            //Imprime la fecha actual
            e.Graphics.DrawString(fecha, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1050, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(200 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));


            //Datos del remitente--------------
            //Imprime el remitente
            e.Graphics.DrawString(remitente, font, Brushes.Black, PrinterUnitConvert.Convert(offset+390, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(480, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime el domicilio
            e.Graphics.DrawString(dirRemitente, font, Brushes.Black, PrinterUnitConvert.Convert(offset+370, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(540, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime la Localidad               
            e.Graphics.DrawString(ciudadRemitente, font, Brushes.Black, PrinterUnitConvert.Convert(offset+390, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(590, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime el cuil              
            e.Graphics.DrawString(cuilRemitente, font, Brushes.Black, PrinterUnitConvert.Convert(offset+340, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(660, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //-----------------------------------

            //Datos del destinatario--------------
            //Imprime el destinatario
            e.Graphics.DrawString(destinatario, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1190, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(480 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime el domicilio
            e.Graphics.DrawString(dirDestinatario, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(540 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime la Localidad            
            e.Graphics.DrawString(ciudadDestinatario, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1190, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(590 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            //Imprimeel cuil   
            e.Graphics.DrawString(cuilDestinatario, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1110, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(660 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //---------------------------------

            //Si es cuenta corriente
            e.Graphics.DrawString("x", font, Brushes.Black, PrinterUnitConvert.Convert(offset+750, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(740 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Importe
            //Imprime el Flete
            e.Graphics.DrawString(flete, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1460, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1480 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime el Seguro
            e.Graphics.DrawString(seguro, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1460, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1550 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime TOTAL               
            e.Graphics.DrawString(total, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1460, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1620 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            List<String> detalleDividido = new List<String>(7);
            //detalleFact = richTextDescripcion.Text.ToString();
            detalleDividido = dividirDescripcion(detalleFact);
            try
            {
                e.Graphics.DrawString(detalleDividido.ElementAt(0), font, Brushes.Black, PrinterUnitConvert.Convert(offset+320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(900 + paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(detalleDividido.ElementAt(1), font, Brushes.Black, PrinterUnitConvert.Convert(offset+320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(950 + paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(detalleDividido.ElementAt(2), font, Brushes.Black, PrinterUnitConvert.Convert(offset+320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1000 + paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(detalleDividido.ElementAt(3), font, Brushes.Black, PrinterUnitConvert.Convert(offset+320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1050 + paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(detalleDividido.ElementAt(4), font, Brushes.Black, PrinterUnitConvert.Convert(offset+320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1100 + paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(detalleDividido.ElementAt(5), font, Brushes.Black, PrinterUnitConvert.Convert(offset+320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1150 + paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(detalleDividido.ElementAt(6), font, Brushes.Black, PrinterUnitConvert.Convert(offset+320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1200 + paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            }
            catch { }
            

            //---------------------------------
            //IVA

            if (IVA == "No Responsable")
                //Si es No Resp.
                e.Graphics.DrawString("x", font, Brushes.Black, PrinterUnitConvert.Convert(offset+1090, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(720-paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            if (IVA == "Monotributista")
                //Si es Resp. Monotributo
                e.Graphics.DrawString("x", font, Brushes.Black, PrinterUnitConvert.Convert(offset+1510, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(720 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            if (IVA == "Exento")
                //Si es Exento               
                e.Graphics.DrawString("x", font, Brushes.Black, PrinterUnitConvert.Convert(offset+1060, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(770 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            if (IVA == "Consumidor Final")
                //Si es  Cons. Final            
                e.Graphics.DrawString("x", font, Brushes.Black, PrinterUnitConvert.Convert(offset+1510, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(770 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));


        }//end printFactB

        public void FacturaBContado(clsConexion C, clsRemito Remito, PrintPageEventArgs e,String idfactura)
        {
            DataSet dsRemitoB = new DataSet();
            //DataSet dsFacturaB = new DataSet();
            DataSet dsEmpresa = new DataSet();
            DataSet dsCliente = new DataSet();
            DataSet dsFacturaCliente = new DataSet();
            DataSet dsFecha = new DataSet();
            String detalleFact;
            int offset = 0; //esto es porque el idiota de Mauro cambió las facturas, así que hubo que correr todo para la izquierda
            //C.CargarDatos(dsFacturaB, "dsFacturaB", "select max(idfactura) from factura");
            String idFact = idfactura;

            //dsFacturaB.Clear();
            //C.CargarDatos(dsFacturaB, "dsFactura", "select flete,seguro,total from factura where idfactura=" + idFact);

            dsFecha.Clear();
            C.CargarDatos(dsFecha, "dsFecha", "select fechacreacion from factura where idfactura=" + idFact);

            C.CargarDatos(dsRemitoB, "dsRemito", "select * from remito where idfactura=" + idFact);
            detalleFact = Remito.getInfoRemito(C, dsRemitoB.Tables[0].Rows[0][0].ToString());

            String fecha = dsFecha.Tables[0].Rows[0][0].ToString().Remove(10);
            String remitente = (dsRemitoB.Tables[0].Rows[0][2].ToString());
            String dirRemitente = (dsRemitoB.Tables[0].Rows[0][3].ToString());
            String ciudadRemitente = (dsRemitoB.Tables[0].Rows[0][4].ToString());
            String cuilRemitente = (dsRemitoB.Tables[0].Rows[0][5].ToString());

            String destinatario = (dsRemitoB.Tables[0].Rows[0][6].ToString());
            String dirDestinatario = (dsRemitoB.Tables[0].Rows[0][7].ToString());
            String ciudadDestinatario = (dsRemitoB.Tables[0].Rows[0][8].ToString());
            String cuilDestinatario = (dsRemitoB.Tables[0].Rows[0][9].ToString());
            String VD = (dsRemitoB.Tables[0].Rows[0][12].ToString());
            String CR = (dsRemitoB.Tables[0].Rows[0][13].ToString());

            String IVA = (dsRemitoB.Tables[0].Rows[0][11].ToString());

            String condVenta = (dsRemitoB.Tables[0].Rows[0][10].ToString());

            DataSet aux = new DataSet();
            C.CargarDatos(aux, "aux", "select flete,seguro,total,neto,ivari from factura where idfactura=" + idFact);

            String flete = (aux.Tables[0].Rows[0][0].ToString());
            String seguro = (aux.Tables[0].Rows[0][1].ToString());
            //Mauro dijo que la factura B va sin IVA, así que ahora se imprime la columna NETO de aux
            String total = (aux.Tables[0].Rows[0][3].ToString());

            //Font font = new Font("Tahoma", 10, FontStyle.Bold);

            //Imprime la fecha actual
            e.Graphics.DrawString(fecha, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1050, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(200 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));


            //Datos del remitente--------------
            //Imprime el remitente
            e.Graphics.DrawString(remitente, font, Brushes.Black, PrinterUnitConvert.Convert(offset+390, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(480, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime el domicilio
            e.Graphics.DrawString(dirRemitente, font, Brushes.Black, PrinterUnitConvert.Convert(offset+370, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(540, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime la Localidad               
            e.Graphics.DrawString(ciudadRemitente, font, Brushes.Black, PrinterUnitConvert.Convert(offset+390, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(590, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime el cuil              
            e.Graphics.DrawString(cuilRemitente, font, Brushes.Black, PrinterUnitConvert.Convert(offset+340, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(660, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //-----------------------------------

            //Datos del destinatario--------------
            //Imprime el destinatario
            e.Graphics.DrawString(destinatario, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1190, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(480 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime el domicilio
            e.Graphics.DrawString(dirDestinatario, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(540 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime la Localidad            
            e.Graphics.DrawString(ciudadDestinatario, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1190, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(590 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            //Imprimeel cuil   
            e.Graphics.DrawString(cuilDestinatario, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1110, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(660 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //---------------------------------



            //Si Contado
            e.Graphics.DrawString("x", font, Brushes.Black, PrinterUnitConvert.Convert(offset+440, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(745, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Si es contado...

            //Imprime el Flete
            e.Graphics.DrawString(flete, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1460, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1480-paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime el Seguro
            e.Graphics.DrawString(seguro, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1460, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1550 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime TOTAL               
            e.Graphics.DrawString(total, font, Brushes.Black, PrinterUnitConvert.Convert(offset+1460, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1620 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            e.Graphics.DrawString(detalleFact, font, Brushes.Black, PrinterUnitConvert.Convert(offset+310, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(910 + paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            e.Graphics.DrawString(VD, font, Brushes.Black, PrinterUnitConvert.Convert(offset+350, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1350 + paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            e.Graphics.DrawString(CR, font, Brushes.Black, PrinterUnitConvert.Convert(offset+350, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1400 + paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));



            //---------------------------------
            //IVA

            if (IVA == "No Responsable")
                //Si es No Resp.
                e.Graphics.DrawString("x", font, Brushes.Black, PrinterUnitConvert.Convert(offset+1090, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(720-paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            if (IVA == "Monotributista")
                //Si es Resp. Monotributo
                e.Graphics.DrawString("x", font, Brushes.Black, PrinterUnitConvert.Convert(offset+1510, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(720 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
          if (IVA == "Exento")
                //Si es Exento               
                e.Graphics.DrawString("x", font, Brushes.Black, PrinterUnitConvert.Convert(offset+1060, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(770 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            if (IVA == "Consumidor Final")
                //Si es  Cons. Final            
                e.Graphics.DrawString("x", font, Brushes.Black, PrinterUnitConvert.Convert(offset+1510, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(770 - paginaAjusteY, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));



        }//end printFactB

        public int ResumenClientes(clsConexion C, int index_cliente, PrintPageEventArgs e)
        {
            int maxlargo = 2500;
            DataSet dsAllClientes = new DataSet();
            DataSet dsProdXClientes = new DataSet();
            Font font = new Font("Tahoma", 8, FontStyle.Bold);
            int cantProdImpresos = 0;

            int index_producto, largo, i;

            String productos_precio;
            String linea;

            C.CargarDatos(dsAllClientes, "dsAllClientes", "select idcliente,nombre,apellido from clientes order by idcliente");

            for (i = 100; i < 2000; i++)
            {
                e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(100, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            }
            e.Graphics.DrawString("|   N R O.   |           E    M    P    R    E    S    A           |                   P    R    O    D    U    C    T    O    S        -        P    R    E    C    I    O                   |", font, Brushes.Black, PrinterUnitConvert.Convert(100, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(150, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            for (i = 100; i < 2000; i++)
            {
                e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(160, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            }
            largo = 200;

            // bucle que imprime las rows
            while ((index_cliente < dsAllClientes.Tables[0].Rows.Count) && (largo <= maxlargo))
            {
                C.CargarDatos(dsProdXClientes, "dsProdXClientes", "select nombre,precio from producto where idcliente = " + dsAllClientes.Tables[0].Rows[index_cliente][0].ToString());
                e.Graphics.DrawString("| " + dsAllClientes.Tables[0].Rows[index_cliente][0].ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(100, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(dsAllClientes.Tables[0].Rows[index_cliente][1].ToString() + " " + dsAllClientes.Tables[0].Rows[index_cliente][2].ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(300, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2015, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(255, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(800, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                
                if (dsProdXClientes.Tables[0].Rows.Count == 0)
                {
                    index_cliente++;
                    for (i = 100; i < 2000; i++)
                    {
                        e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo +10, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    }
                    largo = largo + 50;
                }
                else
                {
                    index_producto = 0;
                    productos_precio = "";
                    linea = "";
                    while (index_producto < dsProdXClientes.Tables[0].Rows.Count)
                    {
                        while ((index_producto < dsProdXClientes.Tables[0].Rows.Count) && (linea.Length < 80))
                        {
                            productos_precio = linea;
                            if (productos_precio == "")
                            {
                                linea = dsProdXClientes.Tables[0].Rows[index_producto][0].ToString() + " - $" + dsProdXClientes.Tables[0].Rows[index_producto][1].ToString();
                            }
                            else
                            {
                                linea = productos_precio + ", " + dsProdXClientes.Tables[0].Rows[index_producto][0].ToString() + " - $" + dsProdXClientes.Tables[0].Rows[index_producto][1].ToString();
                            }
                            index_producto++;
                        }

                        if (linea.Length >= 80)
                        {
                            linea = linea.Substring(productos_precio.Length + 2);

                            e.Graphics.DrawString(productos_precio, font, Brushes.Black, PrinterUnitConvert.Convert(830, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                            e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(255, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                            e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(800, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                            e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2015, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                            e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(100, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                            largo = largo + 50;

                            if (index_producto == dsProdXClientes.Tables[0].Rows.Count)
                            {
                                e.Graphics.DrawString(linea, font, Brushes.Black, PrinterUnitConvert.Convert(830, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                                e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(255, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                                e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(800, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                                e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2015, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                                e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(100, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                                largo = largo + 50;
                            }
                        }
                        else
                        {
                            productos_precio = linea;
                            e.Graphics.DrawString(productos_precio, font, Brushes.Black, PrinterUnitConvert.Convert(830, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                            e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(255, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                            e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(800, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                            e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2015, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                            e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(100, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                            largo = largo + 50;
                        }

                        productos_precio = "";
                    }
                    for (i = 100; i < 2000; i++)
                    {
                        e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo - 40, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    }
                    index_cliente++;
                }
            }
            for (i = 255; i < 2000; i++)
            {
                e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo - 40, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            }
            if (largo > maxlargo)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
            return index_cliente;
        }

        //este método es el que imprime con debe y haber, es el que se usa en el tab factura
        public int InfoRemitos(clsConexion C, DataGridView dataGridInfoRemitos, int indexInfoRemitos, String idCliente, String nombreyapellido, String idFact, String fechaDesde, String fechaHasta, PrintPageEventArgs e)
        {
            DataSet dsEmpresa = new DataSet();
            DataSet dsRemito = new DataSet();
            DataSet dsFactura = new DataSet();
            int largo, maxLargo, cantProd, i;

            double credito, precioProdActual, preciofinal, totalneto = 0;

            Font font = new Font("Tahoma", 10, FontStyle.Bold);

            cantProd = dataGridInfoRemitos.Rows.Count - 1;
            maxLargo = 1700;

            dsEmpresa.Clear();
            C.CargarDatos(dsEmpresa, "dsEmpresa", "select * from empresa");

            int idEmp = (int.Parse(dsEmpresa.Tables[0].Rows[0][0].ToString()));
            String nombreEmpresa = (dsEmpresa.Tables[0].Rows[0][1].ToString());
            String direccionEmpresa = (dsEmpresa.Tables[0].Rows[0][2].ToString());
            String telefonoEmpresa = (dsEmpresa.Tables[0].Rows[0][3].ToString());
            String ciudadEmpresa = (dsEmpresa.Tables[0].Rows[0][4].ToString());

            DataSet auxCredito = new DataSet();

            C.CargarDatos(auxCredito, "auxCredito", "select credito from factura where idfactura=" + idFact);
            String creditoCliente = auxCredito.Tables[0].Rows[0][0].ToString();

            String infoCLiente = idCliente + " - " + nombreyapellido;
            //String total = (Utils.parseAndRound(subtotal) - Utils.parseAndRound(creditoCliente)).ToString();
            //Imprime el nombre de la empresa
            e.Graphics.DrawString(nombreEmpresa, font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime la direccion y el telefono de la empresa
            e.Graphics.DrawString(direccionEmpresa + "-" + telefonoEmpresa, font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime la ciudad de la empresa
            e.Graphics.DrawString(ciudadEmpresa, font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(300, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime el resumen de cuenta
            e.Graphics.DrawString("RESUMEN DE CUENTA CORRIENTE", font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(350, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            //Imprime el numero y nombre de cliente
            e.Graphics.DrawString(infoCLiente, font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(400, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            
            e.Graphics.DrawString(("D E S D E:  " + fechaDesde), font, Brushes.Black, PrinterUnitConvert.Convert(1450, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(400, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            e.Graphics.DrawString(("H A S T A:  " + fechaHasta), font, Brushes.Black, PrinterUnitConvert.Convert(1950, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(400, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            //imprime una linea divisoria
            for (i = 255; i < 2500; i++)
            {
                e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(500, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            }
            //imprime el header de la tabla
            e.Graphics.DrawString("|     C O M P R O B A N T E     |   E  M  I  S  I  O  N   |        D   E   S   C   R   I   P   C   I   O   N        |    D   E   B   E    |    H  A  B  E  R    |", font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(550, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            for (i = 255; i < 2500; i++)
            {
                e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(560, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            }
            largo = 610;

            while ((indexInfoRemitos <= cantProd) && (largo < maxLargo))
            {
                Console.WriteLine(dataGridInfoRemitos.Rows[indexInfoRemitos].Cells[1].ToString());

                if (dataGridInfoRemitos.Rows[indexInfoRemitos].Cells[0].Value.ToString() == "True")
                {//imprimo una linea unicamente si esta tildado el casillero de LIQUIDAR
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2520, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

                    //Cada uno de los valores a continuacion debera ser reemplazado por un valor del datagrid de remitos
                    e.Graphics.DrawString(dataGridInfoRemitos.Rows[indexInfoRemitos].Cells[1].Value.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(280, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(765, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString(dataGridInfoRemitos.Rows[indexInfoRemitos].Cells[2].Value.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(795, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString(dataGridInfoRemitos.Rows[indexInfoRemitos].Cells[3].Value.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(1170, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1905, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    precioProdActual = Math.Round(Utils.parseAndRound(dataGridInfoRemitos.Rows[indexInfoRemitos].Cells[4].Value.ToString()), 2);
                    //totalneto = totalneto + precioProdActual;
                    e.Graphics.DrawString(precioProdActual.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(1935, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    largo = largo + 50;
                }
                indexInfoRemitos++;

            }

            if (indexInfoRemitos >= cantProd)
            {
                //imprime una linea divisoria
                for (i = 255; i < 2500; i++)
                {
                    e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo - 20, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }//imprime la fila de SALDO, con DEBE y HABER totales.
                e.Graphics.DrawString("|        T    O    T    A    L         N    E    T    O       |", font, Brushes.Black, PrinterUnitConvert.Convert(1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                totalneto = claseRemito.calcTotalRemitos(dataGridInfoRemitos);
                e.Graphics.DrawString(totalneto.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(1965, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(creditoCliente, font, Brushes.Black, PrinterUnitConvert.Convert(2230, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2520, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                for (i = 1140; i < 2500; i++)
                {
                    e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 50, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }
                largo = largo + 70;
                totalneto = Math.Round(totalneto * 1.21,2);
                e.Graphics.DrawString("|          T    O    T    A    L    +    I    V    A          |", font, Brushes.Black, PrinterUnitConvert.Convert(1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(totalneto.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(2100, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2520, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                for (i = 1140; i < 2500; i++)
                {
                    e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 50, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }
                if (creditoCliente != "0")
                {
                    credito = double.Parse(creditoCliente);
                    if (totalneto <= credito)
                    {
                        preciofinal = 0;
                    }
                    else
                    {
                        preciofinal = Math.Round(totalneto - credito, 2);
                    }
                    largo = largo + 70;
                    e.Graphics.DrawString("|    T   O   T   A   L   -   C   R   E   D   I   T   O    |", font, Brushes.Black, PrinterUnitConvert.Convert(1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString(preciofinal.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(2100, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2520, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    for (i = 1140; i < 2500; i++)
                    {
                        e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 50, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    }
                }
                e.HasMorePages = false;
            }
            else
            {
                for (i = 255; i < 2500; i++)
                {
                    e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo - 20, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }
                e.HasMorePages = true;
            }
            return indexInfoRemitos;
        }

        //este método imprime toda la info pero sin restar el haber del cliente, es el que se usa en el tab info remitos
        public int InfoRemitos(clsConexion C, DataGridView dataGridInfoRemitos, int indexInfoRemitos, String idCliente, String nombreyapellido, String fechaDesde, String fechaHasta, PrintPageEventArgs e)
        {
            DataSet dsEmpresa = new DataSet();
            DataSet dsRemito = new DataSet();
            DataSet dsFactura = new DataSet();
            int largo, maxLargo, cantProd, i;

            double precioProdActual;//, totalneto = 0;

            Font font = new Font("Tahoma", 10, FontStyle.Bold);

            cantProd = dataGridInfoRemitos.Rows.Count - 1;
            maxLargo = 1700;

            dsEmpresa.Clear();
            C.CargarDatos(dsEmpresa, "dsEmpresa", "select * from empresa");

            int idEmp = (int.Parse(dsEmpresa.Tables[0].Rows[0][0].ToString()));
            String nombreEmpresa = (dsEmpresa.Tables[0].Rows[0][1].ToString());
            String direccionEmpresa = (dsEmpresa.Tables[0].Rows[0][2].ToString());
            String telefonoEmpresa = (dsEmpresa.Tables[0].Rows[0][3].ToString());
            String ciudadEmpresa = (dsEmpresa.Tables[0].Rows[0][4].ToString());

            DataSet auxCredito = new DataSet();

            //C.CargarDatos(auxCredito, "auxCredito", "select credito from factura where idfactura=" + idFact);
            //String creditoCliente = auxCredito.Tables[0].Rows[0][0].ToString();

            String infoCLiente = idCliente + " - " + nombreyapellido;
            //String total = (Utils.parseAndRound(subtotal) - Utils.parseAndRound(creditoCliente)).ToString();
            //Imprime el nombre de la empresa
            e.Graphics.DrawString(nombreEmpresa, font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime la direccion y el telefono de la empresa
            e.Graphics.DrawString(direccionEmpresa + "-" + telefonoEmpresa, font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime la ciudad de la empresa
            e.Graphics.DrawString(ciudadEmpresa, font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(300, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime el resumen de cuenta
            e.Graphics.DrawString("RESUMEN DE CUENTA CORRIENTE", font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(350, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            //Imprime el numero y nombre de cliente
            e.Graphics.DrawString(infoCLiente, font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(400, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            e.Graphics.DrawString(("D E S D E:  " + fechaDesde), font, Brushes.Black, PrinterUnitConvert.Convert(1450, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(400, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            e.Graphics.DrawString(("H A S T A:  " + fechaHasta), font, Brushes.Black, PrinterUnitConvert.Convert(1950, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(400, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            //imprime una linea divisoria
            for (i = 255; i < 2500; i++)
            {
                e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(500, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            }
            //imprime el header de la tabla
            e.Graphics.DrawString("|     C O M P R O B A N T E     |   E  M  I  S  I  O  N   |        D   E   S   C   R   I   P   C   I   O   N        |    D   E   B   E    |    H  A  B  E  R    |", font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(550, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            for (i = 255; i < 2500; i++)
            {
                e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(560, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            }
            largo = 610;

            while ((indexInfoRemitos <= cantProd) && (largo < maxLargo))
            {
                Console.WriteLine(dataGridInfoRemitos.Rows[indexInfoRemitos].Cells[1].ToString());

                if (dataGridInfoRemitos.Rows[indexInfoRemitos].Cells[0].Value.ToString() == "True")
                {//imprimo una linea unicamente si esta tildado el casillero de LIQUIDAR
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2520, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

                    //Cada uno de los valores a continuacion debera ser reemplazado por un valor del datagrid de remitos
                    e.Graphics.DrawString(dataGridInfoRemitos.Rows[indexInfoRemitos].Cells[1].Value.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(280, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(765, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString(dataGridInfoRemitos.Rows[indexInfoRemitos].Cells[2].Value.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(795, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString(dataGridInfoRemitos.Rows[indexInfoRemitos].Cells[3].Value.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(1170, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1905, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    precioProdActual = Math.Round(Utils.parseAndRound(dataGridInfoRemitos.Rows[indexInfoRemitos].Cells[4].Value.ToString()), 2);
                    //totalneto = totalneto + precioProdActual;
                    e.Graphics.DrawString(precioProdActual.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(1935, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    largo = largo + 50;
                }
                indexInfoRemitos++;

            }

            if (indexInfoRemitos >= cantProd)
            {
                //imprime una linea divisoria
                for (i = 255; i < 2500; i++)
                {
                    e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo - 20, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }//imprime la fila de SALDO, con DEBE y HABER totales.
                e.Graphics.DrawString("|        T    O    T    A    L         N    E    T    O       |", font, Brushes.Black, PrinterUnitConvert.Convert(1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                double totalneto = claseRemito.calcTotalRemitos(dataGridInfoRemitos);
                e.Graphics.DrawString(totalneto.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(1965, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                //e.Graphics.DrawString(creditoCliente, font, Brushes.Black, PrinterUnitConvert.Convert(2230, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2520, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                for (i = 1140; i < 2500; i++)
                {
                    e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 50, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }
                largo = largo + 70;
                totalneto = Math.Round(totalneto * 1.21,2);
                e.Graphics.DrawString("|          T    O    T    A    L    +    I    V    A          |", font, Brushes.Black, PrinterUnitConvert.Convert(1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(totalneto.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(2100, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2520, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                for (i = 1140; i < 2500; i++)
                {
                    e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 50, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }
                e.HasMorePages = false;
            }
            else
            {
                for (i = 255; i < 2500; i++)
                {
                    e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo - 20, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }
                e.HasMorePages = true;
            }
            return indexInfoRemitos;
        }

        //este método no se usa más porque no imprime los períodos; puede estar desactualizado, CUIDADO AL UTILIZAR!
        public int InfoRemitos(clsConexion C, DataGridView dataGridInfoRemitos, int indexInfoRemitos, String idCliente, String nombreyapellido, PrintPageEventArgs e)
        {
            DataSet dsEmpresa = new DataSet();
            DataSet dsRemito = new DataSet();
            DataSet dsFactura = new DataSet();
            int largo, maxLargo, cantProd, i;
            double precioProdActual, totalneto = 0;

            Font font = new Font("Tahoma", 10, FontStyle.Bold);

            cantProd = dataGridInfoRemitos.Rows.Count - 1;
            maxLargo = 1700;

            dsEmpresa.Clear();
            C.CargarDatos(dsEmpresa, "dsEmpresa", "select * from empresa");

            int idEmp = (int.Parse(dsEmpresa.Tables[0].Rows[0][0].ToString()));
            String nombreEmpresa = (dsEmpresa.Tables[0].Rows[0][1].ToString());
            String direccionEmpresa = (dsEmpresa.Tables[0].Rows[0][2].ToString());
            String telefonoEmpresa = (dsEmpresa.Tables[0].Rows[0][3].ToString());
            String ciudadEmpresa = (dsEmpresa.Tables[0].Rows[0][4].ToString());

            String infoCLiente = idCliente + " - " + nombreyapellido;
            //String total = (Utils.parseAndRound(subtotal) - Utils.parseAndRound(creditoCliente)).ToString();
            //Imprime el nombre de la empresa
            e.Graphics.DrawString(nombreEmpresa, font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime la direccion y el telefono de la empresa
            e.Graphics.DrawString(direccionEmpresa + "-" + telefonoEmpresa, font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime la ciudad de la empresa
            e.Graphics.DrawString(ciudadEmpresa, font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(300, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Imprime el resumen de cuenta
            e.Graphics.DrawString("RESUMEN DE CUENTA CORRIENTE", font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(350, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            //Imprime el numero y nombre de cliente
            e.Graphics.DrawString(infoCLiente, font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(400, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            //imprime una linea divisoria
            for (i = 255; i < 2500; i++)
            {
                e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(500, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            }
            //imprime el header de la tabla
            e.Graphics.DrawString("|     C O M P R O B A N T E     |   E  M  I  S  I  O  N   |        D   E   S   C   R   I   P   C   I   O   N        |    D   E   B   E    |    H  A  B  E  R    |", font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(550, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            for (i = 255; i < 2500; i++)
            {
                e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(560, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            }
            largo = 610;

            while ((indexInfoRemitos <= cantProd) && (largo < maxLargo))
            {
                Console.WriteLine(dataGridInfoRemitos.Rows[indexInfoRemitos].Cells[1].ToString());

                if (dataGridInfoRemitos.Rows[indexInfoRemitos].Cells[0].Value.ToString() == "True")
                {//imprimo una linea unicamente si esta tildado el casillero de LIQUIDAR
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2520, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

                    //Cada uno de los valores a continuacion debera ser reemplazado por un valor del datagrid de remitos
                    e.Graphics.DrawString(dataGridInfoRemitos.Rows[indexInfoRemitos].Cells[1].Value.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(280, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(765, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString(dataGridInfoRemitos.Rows[indexInfoRemitos].Cells[2].Value.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(795, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString(dataGridInfoRemitos.Rows[indexInfoRemitos].Cells[3].Value.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(1170, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1905, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    precioProdActual = Math.Round(Utils.parseAndRound(dataGridInfoRemitos.Rows[indexInfoRemitos].Cells[4].Value.ToString()), 2);
                    totalneto = totalneto + precioProdActual;
                    e.Graphics.DrawString(precioProdActual.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(1935, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    // e.Graphics.DrawString(creditoCliente, font, Brushes.Black, PrinterUnitConvert.Convert(2230, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                    largo = largo + 50;
                }
                indexInfoRemitos++;

            }

            if (indexInfoRemitos >= cantProd)
            {
                //imprime una linea divisoria
                for (i = 255; i < 2500; i++)
                {
                    e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo - 20, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }//imprime la fila de SALDO, con DEBE y HABER totales.
                e.Graphics.DrawString("|        T    O    T    A    L         N    E    T    O       |", font, Brushes.Black, PrinterUnitConvert.Convert(1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(totalneto.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(1965, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                //e.Graphics.DrawString(creditoCliente, font, Brushes.Black, PrinterUnitConvert.Convert(2230, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2520, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                for (i = 1140; i < 2500; i++)
                {
                    e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 50, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }
                largo = largo + 70;
                totalneto = totalneto * 1.21;
                e.Graphics.DrawString("|          T    O    T    A    L    +    I    V    A          |", font, Brushes.Black, PrinterUnitConvert.Convert(1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString(totalneto.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(2100, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2520, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                for (i = 1140; i < 2500; i++)
                {
                    e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 50, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }
                e.HasMorePages = false;
            }
            else
            {
                for (i = 255; i < 2500; i++)
                {
                    e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo - 20, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
                }
                e.HasMorePages = true;
            }
            return indexInfoRemitos;
        }

    }
}
