using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gestion
{
    public partial class frmBusquedaCliente : Form
    {
        clsConexion Conn;
        DataSet dsC;
        frmPrincipal principal;
        clsFunciones Funciones = new clsFunciones();
        String destino;

        String all_clientes = "select idcliente as NroCliente,cuil as Cuil,nombre as Nombre,apellido as Apellido,direccion as Dirección,ciudad as Ciudad,codigo_postal as CP,telefono as Teléfono,celular as Celular,email as Email from clientes";

        public frmBusquedaCliente(clsConexion C,frmPrincipal prc,String dest)
        {
            InitializeComponent();
            Conn = C;
            dsC = new DataSet();
            principal = prc;
            destino = dest;
        }
 
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "")
            {                                
                txtBuscar.Text = txtBuscar.Text.Replace("'","");                
                Conn.CargarDatos(dataGridClientes, dsC, "dsC", all_clientes + " where " + cmbBuscarpor.Items[cmbBuscarpor.SelectedIndex].ToString() + " LIKE '" + txtBuscar.Text + "%'");
            }
            else
            {                
                Conn.CargarDatos(dataGridClientes, dsC, "dsC", all_clientes);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            principal.Enabled = true;
            this.Hide();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if ((dataGridClientes.Rows.Count != 0) && (dataGridClientes.SelectedRows[0].Index != dataGridClientes.Rows.Count - 1))
            {
                if (destino == "remito")
                {
                    principal.CargarClienteRemito(dataGridClientes.SelectedRows[0].Cells[2].Value.ToString(), dataGridClientes.SelectedRows[0].Cells[3].Value.ToString(), dataGridClientes.SelectedRows[0].Cells[4].Value.ToString(), dataGridClientes.SelectedRows[0].Cells[5].Value.ToString(), dataGridClientes.SelectedRows[0].Cells[1].Value.ToString(), dataGridClientes.SelectedRows[0].Cells[0].Value.ToString());
                }
                else
                {
                    if(destino == "info remitos")
                        principal.CargarClienteInfoRemitos(dataGridClientes.SelectedRows[0].Cells[2].Value.ToString(), dataGridClientes.SelectedRows[0].Cells[3].Value.ToString(), dataGridClientes.SelectedRows[0].Cells[0].Value.ToString());
                    else
                        if (destino == "factura")
                        {
                            principal.CargarClienteFactura(dataGridClientes.SelectedRows[0].Cells[2].Value.ToString(), dataGridClientes.SelectedRows[0].Cells[3].Value.ToString(), dataGridClientes.SelectedRows[0].Cells[0].Value.ToString());
                        }
                        else
                        {
                            principal.CargarClienteLiquidacion(dataGridClientes.SelectedRows[0].Cells[2].Value.ToString(), dataGridClientes.SelectedRows[0].Cells[3].Value.ToString(), dataGridClientes.SelectedRows[0].Cells[0].Value.ToString()); 
                        }
                }
                
                principal.Enabled = true;
                this.Hide();
            }
        }

        private void frmBusquedaCliente_Load(object sender, EventArgs e)
        {
            principal.Enabled = false;
                      
            //Funciones.cargarDatosEnGrid(Conn, all_clientes, dataGridClientes, dsC, "dsC");
            Conn.CargarDatos(dataGridClientes, dsC, "dsC", all_clientes);
            
            cmbBuscarpor.SelectedIndex = 0;
            
        }

        private void frmBusquedaCliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            principal.Enabled = true;
        }

        public void ActualizarDestino(String dest)
        {
            destino = dest;
        }
    }
}