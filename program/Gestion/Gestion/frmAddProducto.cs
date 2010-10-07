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
    public partial class frmAddProducto : Form
    {
        clsConexion Conn;
        clsFunciones Funciones;
        clsProducto Producto;

        frmPrincipal principal;
        DataSet dsCliente = new DataSet();
        List<String> datos = new List<string>(4);

        public frmAddProducto(clsConexion C, frmPrincipal prc)
        {
            InitializeComponent();
            Conn = C;
            principal = prc;
            Funciones = new clsFunciones();
            Producto = new clsProducto();
        }

        private void frmAddProducto_Load(object sender, EventArgs e)
        {
            ActualizarDatos();
            principal.Enabled = false;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {    
            DataRow r;
            int index_separador;

            if (ParametrosValidos())
            {
                //Rescato el idcliente segun el cliente elegido
                index_separador = cmbCliente.SelectedItem.ToString().IndexOf("-");
                r = dsCliente.Tables[0].Select("idcliente = " + cmbCliente.SelectedItem.ToString().Substring(0,index_separador - 1)).ElementAt(0);             
                /////

                datos.Clear();
                datos.Add(txtNombre.Text);
                datos.Add(txtPrecio.Text);
                datos.Add(r[2].ToString());
                datos.Add(textDescripcion.Text);

                Producto.Insertar(Conn, datos);
                principal.cargarProductos();
                Funciones.limpiarCampos(this);             
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            principal.Enabled = true;
            this.Hide();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Funciones.limpiarCampos(this);
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

        private void frmAddProducto_FormClosing(object sender, FormClosingEventArgs e)
        {
            principal.Enabled = true;
        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            Funciones.ValidarNumero(txtPrecio, "Por favor ingrese un número.", "Gestión - Agregar Producto");
        }

        public void ActualizarDatos()
        {                        
            Conn.CargarDatos(cmbCliente, dsCliente,"dsCliente", "select nombre,apellido,idcliente from clientes order by idcliente");
            cmbCliente.SelectedIndex = 0;
        }

    }
}
