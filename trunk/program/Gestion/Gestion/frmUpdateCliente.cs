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
    public partial class frmUpdateCliente : Form
    {
        clsConexion Conn;
        clsConexion Conn_City;
        clsFunciones Funciones;
        clsCliente Cliente;
        
        frmPrincipal principal;
        String idcliente;
        List<String> datos = new List<string>(13); //para crearlo una sola vez y no cada vez que se hace una insercion
        DataSet dsCiudad = new DataSet();
        String ciudad;

        public frmUpdateCliente(clsConexion C,clsConexion C2, frmPrincipal prc, List<String> datos)
        {
            InitializeComponent();
            Conn = C;
            Conn_City = C2;
            principal = prc;
            Funciones = new clsFunciones();
            Cliente = new clsCliente();
            cargarDatos(datos);
           
        }

        public void cargarDatos(List<String> datos)
        {
            idcliente = datos[0];
            txtCUIL.Text = datos[1];
            txtNombre.Text = datos[2];
            txtApellido.Text = datos[3];
            txtDireccion.Text = datos[4];
            //txtCiudad.Text = datos[5];
            cmbCiudad.SelectedItem = datos[5];
            txtCP.Text = datos[6];
            txtTelefono.Text = datos[7];
            txtCelular.Text = datos[8];
            txtEmail.Text = datos[9];
            txtVD.Text = datos[10];
            txtCR.Text = datos[11];
            txtCredito.Text = datos[12];

            ciudad = datos[5];
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ParametrosValidos())
            {
                datos.Add(txtCUIL.Text);
                datos.Add(txtNombre.Text);
                datos.Add(txtApellido.Text);
                datos.Add(txtDireccion.Text);
                //datos.Add(txtCiudad.Text);
                datos.Add(cmbCiudad.SelectedItem.ToString());
                datos.Add(txtCP.Text);
                datos.Add(txtTelefono.Text);
                datos.Add(txtCelular.Text);
                datos.Add(txtEmail.Text);
                datos.Add(txtVD.Text.Replace(",","."));
                datos.Add(txtCR.Text.Replace(",","."));
                datos.Add(txtCredito.Text.Replace(",","."));
                datos.Add(idcliente);
               

                Cliente.Actualizar(Conn,datos);    
                principal.cargarClientes();
                Funciones.limpiarCampos(this);
                principal.Enabled = true;
                this.Hide();
            }
        }

        public Boolean ParametrosValidos()
        {
            List<String> errores = new List<string>(6);
            int cont = 0;
            string msg = "";

            if (txtNombre.Text == "")
            {
                errores.Add("Empresa");
            }
            if (txtVD.Text == "")
            {
                txtVD.Text = "0";
            }
            if (txtCR.Text == "")
            {
                txtCR.Text = "0";
            }
            if (txtCredito.Text == "")
            {
                txtCredito.Text = "0";
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
                MessageBox.Show("Falta completar los siguientes campos: " + msg + ".", "Gestión - Agregar Clientes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
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

        private void txtCUIL_Leave(object sender, EventArgs e)
        {
            if ((txtCUIL.Text != "  -        -") && (!Funciones.CuiltValido(txtCUIL.Text)))
            {
                MessageBox.Show("El Cuil ingresado no es valido.", "Gestión - Modificar Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCUIL.Clear();
            }
        }

        private void frmUpdateCliente_Load(object sender, EventArgs e)
        {
            cargarCiudades();
            principal.Enabled = false;
        }

        private void frmUpdateCliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            principal.Enabled = true;
        }

        private void txtVD_Leave(object sender, EventArgs e)
        {
            Funciones.ValidarNumero(txtVD, "Por favor ingrese un número.", "Gestión - Actualizar Cliente");
        }

        private void txtCR_Leave(object sender, EventArgs e)
        {
            Funciones.ValidarNumero(txtCR, "Por favor ingrese un número.", "Gestión - Actualizar Cliente");
        }

        private void txtCredito_Leave(object sender, EventArgs e)
        {
            Funciones.ValidarNumero(txtCredito, "Por favor ingrese un número.", "Gestión - Actualizar Cliente");
        }

        private void cargarCiudades()
        {
            Conn_City.CargarDatos(dsCiudad, "dsCiudad", "select ciudad from city order by ciudad");
            Funciones.CargarDatos(cmbCiudad, dsCiudad);
            cmbCiudad.SelectedItem = ciudad;
        }
    }
}
