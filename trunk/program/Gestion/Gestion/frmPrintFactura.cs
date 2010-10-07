using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Odbc;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Gestion
{
    public partial class frmPrintFactura : Form
    {
         clsConexion C;
         clsFunciones Funciones;
    
        frmPrincipal principal;
        clsRemito Remito;
        Boolean facturaA = true;
        Boolean liquidarRemitos;
        DataGridView dataGridRemitos;
        DataSet dsEmpresa = new DataSet();
        DataSet dsRemito = new DataSet();
        DataSet dsFactura = new DataSet();
        String iduser;
        String facturaActual;
        String fechaDesde;
        String fechaHasta;
        List<String> datos;
        clsImpresion Impresion = new clsImpresion();

        int indexProd = 0;


        public frmPrintFactura(clsConexion C, frmPrincipal prc, List<String> datos, DataGridView dataGridRemitos, clsRemito Remito)
        {

            InitializeComponent();
            this.C=C;
            this.principal=prc;
            Funciones = new clsFunciones();
            this.datos=datos;
            this.dataGridRemitos = dataGridRemitos;
            this.Remito = Remito;
            fechaDesde=datos[0];
            fechaHasta = datos[1];
          
            iduser = prc.getIdUser();
        }

        private string obtenerUltimoNumFact()
        {

            //DataSet aux = new DataSet();
            //C.CargarDatos(aux, "aux", "select max(idfactura) from factura");
            
            //if (aux.Tables[0].Rows.Count == 0)
            //    return (null);
            //else
            //    return (aux.Tables[0].Rows[0][0].ToString());
            return facturaActual;
          
        }

        private void crearFacturaCuentaCorriente(String numFact,String tipoFact,String fechaLiquidacion)
        {

            Boolean insercionValida;
            DataSet aux = new DataSet();
            insercionValida = Remito.liquidarCuentaCorriente(C, fechaDesde, fechaHasta,fechaLiquidacion, dataGridRemitos, richTextDescripcion.Text, tipoFact, iduser, numFact);
            if (insercionValida)
                MessageBox.Show("Factura creada exitosamente", "Gestion - Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }



        private void btnCrearFactura_Click(object sender, EventArgs e)
        {
            String numFact=txtNumFact.Text.ToString();
            String fechaLiquidacion;
            if(!principal.facturaValida(numFact))
            {
                MessageBox.Show("El número de factura ya existe o es incorrecto");
            }
            else
            {

               fechaLiquidacion=dateFechaLiquidacion.Value.ToString("yyyy-MM-dd");

                if (cmbTipo.SelectedItem.ToString() == "B")
                    facturaA = false;
                else
                    facturaA = true;

                crearFacturaCuentaCorriente(numFact, cmbTipo.SelectedItem.ToString(),fechaLiquidacion);
                principal.cargarClientes();

                DialogResult resultDialog = MessageBox.Show("¿Desea imprimir la factura?", "Gestión - Remito", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                facturaActual = numFact;

                if (resultDialog == DialogResult.Yes)
                {
                    //imprimimos factura A o B dependiendo de lo elegido
                    if (facturaA)
                    {
                        if (printDialogFacturaA.ShowDialog() == DialogResult.OK)
                        {
                            printFacturaA.PrinterSettings = printDialogFacturaA.PrinterSettings;
                            indexProd = 0;
                            printFacturaA.Print();
                        }
                    }
                    else
                    {
                        if (printDialogFacturaB.ShowDialog() == DialogResult.OK)
                        {
                            printFacturaB.PrinterSettings = printDialogFacturaB.PrinterSettings;
                            indexProd = 0;
                            printFacturaB.Print();
                        }
                    }


                    //e imprimimos ahora la lista de remitos con sus descripciones
                    if (printDialogLiq.ShowDialog() == DialogResult.OK)
                    {
                        printLiq.PrinterSettings = printDialogLiq.PrinterSettings;
                        indexProd = 0;
                        printLiq.DefaultPageSettings.Landscape = true;
                        printLiq.Print();
                    }

                }
             
            }
       
            principal.limpiarRemitos();// agregado por pedido de fer
            principal.Enabled = true;
            this.Hide();
   
        }

       
        private void printLiq_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int nuevoindex;
            nuevoindex = Impresion.DetalleRemitos(C, dataGridRemitos, indexProd, facturaActual, e);
            indexProd = nuevoindex;

            //String dateFechaDesde = principal.fechaInicialLiquidacion();
            //String dateFechaHasta = principal.fechaFinalLiquidacion();
            //dsFactura.Clear();
            //C.CargarDatos(dsFactura, "dsFactura", "select max(idfactura) from factura");

            //String idFact = ((dsFactura.Tables[0].Rows[0][0]).ToString());
            //DataSet aux = new DataSet();
            //C.CargarDatos(aux, "aux", "select total from factura where idfactura=" + idFact);
          
            //String subtotal = (aux.Tables[0].Rows[0][0].ToString());

            //int largo, maxLargo, cantProd, i;
       
            //Font font = new Font("Tahoma", 10, FontStyle.Bold);

            //cantProd = dataGridRemitos.Rows.Count - 1;
            //maxLargo = 1700;

            //dsEmpresa.Clear();
            //C.CargarDatos(dsEmpresa, "dsEmpresa", "select * from empresa");

            //int idEmp = (int.Parse(dsEmpresa.Tables[0].Rows[0][0].ToString()));
            //String nombreEmpresa = (dsEmpresa.Tables[0].Rows[0][1].ToString());
            //String direccionEmpresa = (dsEmpresa.Tables[0].Rows[0][2].ToString());
            //String telefonoEmpresa = (dsEmpresa.Tables[0].Rows[0][3].ToString());
            //String ciudadEmpresa = (dsEmpresa.Tables[0].Rows[0][4].ToString());

            //DataSet auxNumRemito = new DataSet();
            //C.CargarDatos(auxNumRemito, "auxNumRemito", "select idremito from remito where idfactura="+ idFact);
            //String idRemito = auxNumRemito.Tables[0].Rows[0][0].ToString();

            //DataSet auxClienteRemito = new DataSet();
            //C.CargarDatos(auxClienteRemito, "auxClienteRemito", "select idcliente from remito where idremito=" + idRemito);
            //String idCliente = auxClienteRemito.Tables[0].Rows[0][0].ToString();

            //DataSet auxCliente = new DataSet();
            //C.CargarDatos(auxCliente, "auxCliente", "select nombre,apellido from clientes where idcliente=" + idCliente);
            
            //String nombre = auxCliente.Tables[0].Rows[0][0].ToString();
            //String apellido = auxCliente.Tables[0].Rows[0][1].ToString();

            //DataSet auxCredito = new DataSet();
           
            //C.CargarDatos(auxCredito, "auxCredito", "select credito from factura where idfactura=" + idFact);
            //String creditoCliente = auxCredito.Tables[0].Rows[0][0].ToString();
            

            //String infoCLiente = idCliente + "-" + nombre + " " + apellido;
            //String total = (Utils.parseAndRound(subtotal) - Utils.parseAndRound(creditoCliente)).ToString();
            ////Imprime el nombre de la empresa
            //e.Graphics.DrawString(nombreEmpresa, font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            ////Imprime la direccion y el telefono de la empresa
            //e.Graphics.DrawString(direccionEmpresa + "-" + telefonoEmpresa, font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            ////Imprime la ciudad de la empresa
            //e.Graphics.DrawString(ciudadEmpresa, font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(300, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            ////Imprime el resumen de cuenta
            //e.Graphics.DrawString("RESUMEN DE CUENTA CORRIENTE", font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(350, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            ////Imprime el numero y nombre de cliente
            //e.Graphics.DrawString(infoCLiente, font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(400, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //e.Graphics.DrawString(("D E S D E:  " + dateFechaDesde).Remove(23), font, Brushes.Black, PrinterUnitConvert.Convert(1450, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(400, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //e.Graphics.DrawString(("H A S T A:  " + dateFechaHasta).Remove(23), font, Brushes.Black, PrinterUnitConvert.Convert(1950, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(400, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            ////imprime una linea divisoria
            //for (i = 255; i < 2500; i++)
            //{
            //    e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(500, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //}
            ////imprime el header de la tabla
            //e.Graphics.DrawString("|     C O M P R O B A N T E     |   E  M  I  S  I  O  N   |        D   E   S   C   R   I   P   C   I   O   N        |    D   E   B   E    |    H  A  B  E  R    |", font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(550, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //for (i = 255; i < 2500; i++)
            //{
            //    e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(560, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //}
            //largo = 610;

            //while ((indexProd <= cantProd) && (largo < maxLargo))
            //{
            //    Console.WriteLine(dataGridRemitos.Rows[indexProd].Cells[1].ToString());

            //    if (dataGridRemitos.Rows[indexProd].Cells[0].Value.ToString() == "True")
            //    {//imprimo una linea unicamente si esta tildado el casillero de LIQUIDAR
            //        e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(250, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //        e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2520, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            //        //Cada uno de los valores a continuacion debera ser reemplazado por un valor del datagrid de remitos
            //        //Por ejemplo, para "Descripcion" usamos dataGridRemitos.Rows[indexProd].Cells[3].ToString()
            //        //Por algun motivo no se imprime bien la string del datagrid, VER!!! Ya esta todo alineado en su lugar
            //        e.Graphics.DrawString(dataGridRemitos.Rows[indexProd].Cells[1].Value.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(280, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //        e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(765, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //        e.Graphics.DrawString(dataGridRemitos.Rows[indexProd].Cells[2].Value.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(795, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //        e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //        e.Graphics.DrawString(dataGridRemitos.Rows[indexProd].Cells[3].Value.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(1170, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //        e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(1905, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //        e.Graphics.DrawString(dataGridRemitos.Rows[indexProd].Cells[4].Value.ToString(), font, Brushes.Black, PrinterUnitConvert.Convert(1935, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //        e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //       // e.Graphics.DrawString(creditoCliente, font, Brushes.Black, PrinterUnitConvert.Convert(2230, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //        largo = largo + 50;
            //    }
            //    indexProd++;

            //}

            //if (indexProd >= cantProd)
            //{
            //    //imprime una linea divisoria
            //    for (i = 255; i < 2500; i++)
            //    {
            //        e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo - 20, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //    }//imprime la fila de SALDO, con DEBE y HABER totales.
            //    e.Graphics.DrawString("|         S     U     B     T     O     T     A     L         |", font, Brushes.Black, PrinterUnitConvert.Convert(1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //    e.Graphics.DrawString(subtotal, font, Brushes.Black, PrinterUnitConvert.Convert(1965, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //    e.Graphics.DrawString(creditoCliente, font, Brushes.Black, PrinterUnitConvert.Convert(2230, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2520, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //    for (i = 1140; i < 2500; i++)
            //    {
            //        e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 50, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //    }
            //    largo = largo + 70;
            //    e.Graphics.DrawString("|                   T      O      T      A      L                  |", font, Brushes.Black, PrinterUnitConvert.Convert(1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //    e.Graphics.DrawString(total, font, Brushes.Black, PrinterUnitConvert.Convert(2100, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //    e.Graphics.DrawString("|", font, Brushes.Black, PrinterUnitConvert.Convert(2520, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 30, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //    for (i = 1140; i < 2500; i++)
            //    {
            //        e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo + 50, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //    }
            //    e.HasMorePages = false;
            //    principal.limpiarRemitos();
            //}
            //else
            //{
            //    for (i = 255; i < 2500; i++)
            //    {
            //        e.Graphics.DrawString("_", font, Brushes.Black, PrinterUnitConvert.Convert(i, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(largo - 20, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //    }
            //    e.HasMorePages = true;
            //}
        }

       
         //private List<String> dividirDescripcion(String detalleFact){
         //    int i,start;
         //    List<String> listAux= new List<string>(7);
         //    String lineaDescripcion = richTextDescripcion.Text.ToString();
             

         //    start = lineaDescripcion.Length;
         //    for(i=start;i<211;i++)
         //    {
         //       lineaDescripcion = lineaDescripcion + " ";
         //    }
         //    listAux.Add(lineaDescripcion.Substring(0,30));                 
         //    listAux.Add(lineaDescripcion.Substring(30, 30));
         //    listAux.Add(lineaDescripcion.Substring(60, 30));
         //    listAux.Add(lineaDescripcion.Substring(90, 30));
         //    listAux.Add(lineaDescripcion.Substring(120, 30));
         //    listAux.Add(lineaDescripcion.Substring(150, 30));
         //    listAux.Add(lineaDescripcion.Substring(180, 30));           

         //    return (listAux);
         //}

         private void printFacturaA_PrintPage(object sender, PrintPageEventArgs e)
         {
             String idfactura = facturaActual;

             //DataSet dsIdFactura = new DataSet();
             //C.CargarDatos(dsIdFactura, "dsIdFactura", "select max(idfactura) from factura");
             //idfactura = dsIdFactura.Tables[0].Rows[0][0].ToString();

             Impresion.FacturaACuentaCorriente(C, Remito,e,idfactura);
         }

        private void printFacturaB_PrintPage(object sender, PrintPageEventArgs e)
        {
            String idfactura = facturaActual;

            //DataSet dsIdFactura = new DataSet();
            //C.CargarDatos(dsIdFactura, "dsIdFactura", "select max(idfactura) from factura");
            //idfactura = dsIdFactura.Tables[0].Rows[0][0].ToString();

            Impresion.FacturaBCuentaCorriente(C, Remito, e,idfactura);
        }


        private void frmPrintFactura_Load(object sender, EventArgs e)
        {            
            principal.Enabled = false;
            cmbTipo.SelectedIndex = 0;
        }

        private void frmPrintFactura_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            principal.Enabled = true;
        }

    }//end class PrintFactura

}//end namespace