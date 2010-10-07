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
    public partial class frmCiudad : Form
    {
        frmPrincipal principal;
        clsConexion Conn;
        DataSet dsCiudad;

        public frmCiudad(frmPrincipal prc, clsConexion C)
        {
            InitializeComponent();
            principal = prc;
            Conn = C;
            dsCiudad = new DataSet();
            principal.Enabled = false;
        }

        private void frmCiudad_Load(object sender, EventArgs e)
        {
            cargarCiudad();            
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtCiudad.Text != "")
            {
                if (!YaExiste(txtCiudad.Text))
                {
                    Conn.InsertOrUpdate("insert into city (ciudad) values ('" + txtCiudad.Text.ToUpper() + "')");
                    cargarCiudad();
                    txtCiudad.Text = "";
                }
                else
                {
                    MessageBox.Show("La ciudad ya esta cargada en el sistema.", "Gestión - Ciudades", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if ((lstCiudades.Items.Count != 0) && (lstCiudades.SelectedItem != null))
            {
                DialogResult result = MessageBox.Show("¿Esta seguro que desea eliminar la ciudad?", "Gestión - Ciudad", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Conn.InsertOrUpdate("delete from city where ciudad = '" + lstCiudades.SelectedItem.ToString() + "'");
                    cargarCiudad();
                }
            }

        }

        private Boolean YaExiste(String ciudad)
        {
            DataSet dsAux = new DataSet();

            Conn.CargarDatos(dsAux, "dsAux", "select ciudad from city where ciudad = '" + ciudad + "'");
            return (dsAux.Tables[0].Rows.Count == 1);
        }
        
        
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Cerrar();
        }

        private void frmCiudad_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cerrar();
        }

        private void Cerrar()
        {
            principal.cargarCiudades();
            principal.Enabled = true;
            this.Dispose();
        }

        private void cargarCiudad()
        {
            Conn.CargarDatos(lstCiudades, dsCiudad, "dsCiudad", "select ciudad from city order by ciudad");
        }
    }
}
