using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Printing;
using System.Drawing;

namespace Gestion
{
    class clsFunciones
    {       

        public void borrarItem(clsConexion C, String consulta)
        {
            C.InsertOrUpdate(consulta);
        }

        public void limpiarCampos(Control frm)
        {
            foreach (Control ctrl in frm.Controls)
            {
                if ((ctrl is TextBox) || (ctrl is RichTextBox) || (ctrl is MaskedTextBox))
                {
                    ctrl.Text = "";
                }
                else
                {
                    if (ctrl is DataGridView)
                    {
                        ((DataGridView)ctrl).DataSource = null;
                        ((DataGridView)ctrl).Rows.Clear();
                    }
                    else
                    {
                        limpiarCampos(ctrl);
                    }
                }
            }
        }     

        public Boolean CuiltValido(String Cuilt)
        {

            int Control, XA, XB, XC, XD, XE, XF, XG, XH, XI, XJ, XK, X;

            if (Cuilt.Length == 13)
            {
                //Individualiza y multiplica los dígitos.
                XA = Int32.Parse(Cuilt.Substring(0, 1)) * 5;
                XB = Int32.Parse(Cuilt.Substring(1, 1)) * 4;
                XC = Int32.Parse(Cuilt.Substring(3, 1)) * 3;
                XD = Int32.Parse(Cuilt.Substring(4, 1)) * 2;
                XE = Int32.Parse(Cuilt.Substring(5, 1)) * 7;
                XF = Int32.Parse(Cuilt.Substring(6, 1)) * 6;
                XG = Int32.Parse(Cuilt.Substring(7, 1)) * 5;
                XH = Int32.Parse(Cuilt.Substring(8, 1)) * 4;
                XI = Int32.Parse(Cuilt.Substring(9, 1)) * 3;
                XJ = Int32.Parse(Cuilt.Substring(10, 1)) * 2;
                XK = Int32.Parse(Cuilt.Substring(12, 1)) * 1;

                //Suma los resultantes.
                X = XA + XB + XC + XD + XE + XF + XG + XH + XI + XJ + XK;

                //Calcula el dígito de control.
                Control = Mod(X, 11);

                //Verifica si el dígito de control ingresado difiere con el calculado.
                return (Control == 0);

            }
            return false;
        }

        public int Mod(int number, int m)
        {
            int aux;

            aux = number / m;
            return (number - (aux * m));
        }

        public void ValidarNumero(TextBox txtBox,String msg1,String msg2)
        {
            float aux;

            txtBox.Text = txtBox.Text.Replace(",", ".");
            if (!float.TryParse(txtBox.Text, out aux))
            {
                MessageBox.Show(msg1,msg2, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBox.Text = "0"; //como por defecto
            }
        }

        public void CargarDatos(ComboBox cmb, DataSet data)
        {
            cmb.Items.Clear();
            foreach (DataRow r in data.Tables[0].Rows)
            {
                cmb.Items.Add(r[0].ToString());
            }
            if (cmb.Items.Count != 0)
            {
                cmb.SelectedIndex = 0;
            }
        }

       
    }
}
