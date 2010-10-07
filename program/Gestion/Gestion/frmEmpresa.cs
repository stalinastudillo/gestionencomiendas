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
    public partial class frmEmpresa : Form
    {
        frmPrincipal principal;
        clsConexion C;
        int idEmp;
        DataSet dsEmpresa = new DataSet();

        public frmEmpresa(frmPrincipal prc,clsConexion C)
        {
            this.C = C;
            InitializeComponent();

            C.CargarDatos(dsEmpresa, "dsEmpresa", "select * from empresa");
            
            idEmp = (int.Parse(dsEmpresa.Tables[0].Rows[0][0].ToString()));
            txtNombre.Text=(dsEmpresa.Tables[0].Rows[0][1].ToString());
            txtDireccion.Text=(dsEmpresa.Tables[0].Rows[0][2].ToString());
            txtTelefono.Text=(dsEmpresa.Tables[0].Rows[0][3].ToString());
            txtCiudad.Text=(dsEmpresa.Tables[0].Rows[0][4].ToString());

            principal = prc;
            principal.Enabled = false;
               
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            principal.Enabled = true;
            this.Dispose();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            
            String stringUpdate = "update empresa set nombre=" + "'" + txtNombre.Text + "'" + "," +
                "direccion=" + "'" + txtDireccion.Text + "'" + "," +
                "telefono=" + "'" + txtTelefono.Text + "'" + "," +
                "ciudad=" + "'" + txtCiudad.Text + "'" +
                "where idempresa=" + idEmp.ToString();
                C.InsertOrUpdate(stringUpdate);

                frmEmpresa_FormClosing(null, null);

        }

        private void frmEmpresa_FormClosing(object sender, FormClosingEventArgs e)
        {
            principal.Enabled = true;
            this.Dispose();
        }
    }
}
