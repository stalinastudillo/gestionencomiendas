using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Gestion
{
    public partial class frmRemitoContado : Form
    {
        frmPrincipal principal;

        clsRemito Remito = new clsRemito();
        clsConexion Conn;
        String iduser;
        String fechaDesde;
        String fechaHasta;
        String facturaActual;
        bool valida = true;

        clsImpresion Impresion = new clsImpresion();

        public frmRemitoContado(clsConexion C, frmPrincipal prc, List<String> datos)
        {
            InitializeComponent();
            Conn = C;
            principal = prc;
            principal.Enabled = false;
            iduser = prc.getIdUser();
            fechaDesde = datos[0];
            fechaHasta = datos[1];
            rbtnFactA.Select();
        }

        private void crearFacturaContado(String tipoFact,String numFact){

            String fechaFirst = DateTime.Now.ToString("yyyy-MM-dd");
            String fechaLast = DateTime.Now.ToString("yyyy-MM-dd");
            Boolean insercionValida;
            DataSet aux = new DataSet();
            Conn.CargarDatos(aux, "aux", "select max(idremito) from remito");
            String maxRemito = aux.Tables[0].Rows[0][0].ToString();
            insercionValida = Remito.liquidarContado(Conn, fechaFirst,fechaLast, maxRemito, "", tipoFact, iduser,numFact);
            if(insercionValida)
                MessageBox.Show("Factura creada exitosamente", "Gestion - Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);
         }
        

        private void btnNoImprimir_Click(object sender, EventArgs e)
        {
            String numFact = txtNumFact.Text.ToString();
            try
            {
                valida = principal.facturaValida(numFact);
                if (valida)
                {
                    if (rbtnFactA.Checked)
                    {
                        crearFacturaContado("A", numFact);
                    }
                    else
                    {
                        crearFacturaContado("B", numFact);
                    }
                     Salir();
                }
                else
                {
                    MessageBox.Show("El numero de factura ingresado ya existe o es incorrecto");
                }
            }
            catch
            {
            }
               

        }

        private void btnFacturaA_Click(object sender, EventArgs e)
        {
            String numFact = txtNumFact.Text.ToString();
            valida = principal.facturaValida(numFact);
            if (valida)
            {
                facturaActual = numFact;
                if (rbtnFactA.Checked)
                {
                    crearFacturaContado("A", numFact);
                    imprimir(printFacturaA);
                }
                else
                {
                    crearFacturaContado("B", numFact);
                    imprimir(printFacturaB);
                }
            }
            else
            {
                MessageBox.Show("El numero de factura ingresado ya existe o es incorrecto");
            }
                        
        }


        private void imprimir(PrintDocument printFactura)
        {
            principal.cargarClientes();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printFactura.PrinterSettings = printDialog.PrinterSettings;
                printFactura.Print();
            }        
            Salir();
        }

        private void printFacturaA_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            //DataSet dsFacturaA = new DataSet();
            clsRemito Remito = new clsRemito();
            //Conn.CargarDatos(dsFacturaA, "dsFacturaA", "select max(idfactura) from factura");

            //String idFact = ((dsFacturaA.Tables[0].Rows[0][0]).ToString());

            Impresion.FacturaAContado(Conn, Remito, e, facturaActual);
        }

        private void Salir()
        {
            principal.Enabled = true;
            this.Dispose();
        }

      

        private void printFacturaB_PrintPage(object sender, PrintPageEventArgs e)
        {

            //DataSet dsFacturaB = new DataSet();
            clsRemito Remito = new clsRemito();
            //Conn.CargarDatos(dsFacturaB, "dsFacturaB", "select max(idfactura) from factura");

            //String idFact = ((dsFacturaB.Tables[0].Rows[0][0]).ToString());

            Impresion.FacturaBContado(Conn, Remito, e, facturaActual);
        }

        private void frmRemitoContado_FormClosed(object sender, FormClosedEventArgs e)
        {
            String numFact = txtNumFact.Text.ToString();
            try
            {
                valida = principal.facturaValida(numFact);
                if (valida)
                {
                    if (rbtnFactA.Checked)
                    {
                        crearFacturaContado("A", numFact);
                        imprimir(printFacturaA);
                    }
                    else
                    {
                        crearFacturaContado("B", numFact);
                        imprimir(printFacturaB);
                    }
                }
                else
                {
                    MessageBox.Show("El numero de factura ingresado ya existe o es incorrecto");
                }
            }
            catch
            {
            }
            if (valida)
                Salir();

        }

    }
}
