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
    public partial class frmUpdateProducto : Form
    {
        clsConexion Conn;
        clsFunciones Funciones;
        clsProducto Producto;

        frmPrincipal principal;
        DataSet dsCliente;
        String codigo;
        List<String> datos;

        public frmUpdateProducto(clsConexion C, frmPrincipal prc, List<String> dat)
        {
            InitializeComponent();

            Conn = C;
            principal = prc;
            Funciones = new clsFunciones();
            Producto = new clsProducto();
            dsCliente = new DataSet();
            cargarDatos(dat);
            datos = new List<string>(5);
        }

        public void cargarDatos(List<String> dat)
        {
            DataSet dsClienteAux = new DataSet();

            codigo = dat[0];
            txtNombre.Text = dat[1];
            txtPrecio.Text = dat[2];
            textDescripcion.Text = dat[5];

            ActualizarDatos();
            Conn.CargarDatos(dsClienteAux, "dsClienteAux", "select idcliente from producto where codigo = " + codigo);

            if (dat[4].ToString() != "")
            {
                cmbCliente.SelectedItem = dsClienteAux.Tables[0].Rows[0][0].ToString() + " - " + dat[4] + "," + dat[3];
            }
            else
            {
                cmbCliente.SelectedItem = dsClienteAux.Tables[0].Rows[0][0].ToString() + " - " + dat[3];
            }
        }

        private void frmUpdateProducto_Load(object sender, EventArgs e)
        {
            principal.Enabled = false;
        }

        public void ActualizarDatos()
        {
            Conn.CargarDatos(cmbCliente, dsCliente, "dsCliente", "select nombre,apellido,idcliente from clientes order by apellido");
            cmbCliente.SelectedIndex = 0;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            DataRow r;
            int index_separador;

            if (ParametrosValidos())
            {
                //Rescato el idcliente segun el cliente elegido
                index_separador = cmbCliente.SelectedItem.ToString().IndexOf("-");
                r = dsCliente.Tables[0].Select("idcliente = " + cmbCliente.SelectedItem.ToString().Substring(0, index_separador - 1)).ElementAt(0);             
                /////
                
                datos.Add(codigo);
                datos.Add(txtNombre.Text);
                datos.Add(txtPrecio.Text.Replace(",","."));
                datos.Add(r[2].ToString());
                datos.Add(textDescripcion.Text);

                Producto.Actualizar(Conn, datos);
                principal.cargarProductos();
                Funciones.limpiarCampos(this);
                principal.Enabled = true;
                this.Hide();
            }
        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            Funciones.ValidarNumero(txtPrecio, "Por favor ingrese un número.", "Gestión - Actualizar Producto");
     
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Funciones.limpiarCampos(this);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            principal.Enabled = true;
            this.Hide();
        }

        private Boolean ParametrosValidos()
        {
            List<String> errores = new List<string>(5);
            int cont = 0;
            string msg = "";


            if (txtNombre.Text == "")
            {
                errores.Add("Nombre");
            }
            if (txtPrecio.Text == "")
            {
                errores.Add("Precio");
            }
            if (errores.Count == 0)
            {
                return true;
            }
            else
            {
                while (cont < errores.Count)
                {
                    if (cont + 1 == errores.Count)
                    {
                        msg = msg + errores[cont];
                    }
                    else msg = msg + errores[cont] + ", ";
                    cont++;
                }
                MessageBox.Show("Faltan completar los siguientes campos: " + msg, "Gestión - Agregar Producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void frmUpdateProducto_FormClosing(object sender, FormClosingEventArgs e)
        {
            principal.Enabled = true;
            this.Hide();

        }
    }

}
