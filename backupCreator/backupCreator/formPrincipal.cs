using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class formPrincipal : Form
    {
        String dataBase;
        String connectAntares;
        String connectDelivery;
        conexionBackup C = new conexionBackup();
        public formPrincipal()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;

            String pathCommand = System.IO.Path.GetDirectoryName(Application.ExecutablePath).ToString();

            //Se obtiene del archivo conf.dat la ip de la maquina host
            //---------------------------------------------------------
            StreamReader objReader = new StreamReader(pathCommand + @"\" + "conf.dat");
            string sLine = objReader.ReadLine();
            //Obtengo la ip que aparece despues de la cadena "ipHost= "
            string dirIp = sLine.Substring(8);
            //---------------------------------------------------------

            connectAntares = "Driver={MySQL ODBC 5.1 Driver};Server="+dirIp +";Database=gestionAntares; User=postal;Password=postal;Option=3";
            connectDelivery = "Driver={MySQL ODBC 5.1 Driver};Server=" + dirIp + ";Database=gestionDelivery; User=postal;Password=postal;Option=3";
            //C.Conectar("Driver={MySQL ODBC 5.1 Driver};Server=" + dirIp + ";Database=gestionE; User=userRed;Password=clave;Option=3");

            
          
        }

        private void ActualizarFechaBackup(String newDate)
        {
            String hoy = DateTime.Now.ToString("dd-MM-yyyy");
            C.InsertOrUpdate("truncate ultimaFecha");
            C.InsertOrUpdate("insert into ultimaFecha(fecha) values" + "('" + hoy + "')");
        }

        private void createBackup(String pathFile)
        {
            System.Console.WriteLine(System.IO.Path.GetDirectoryName(Application.ExecutablePath).ToString());
            String pathCommand = System.IO.Path.GetDirectoryName(Application.ExecutablePath).ToString();

            if(dataBase=="Antares")
                            pathCommand = pathCommand + @"\createBackupAntares.bat";
            else
                            pathCommand = pathCommand + @"\createBackupDelivery.bat";
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();


                p.StartInfo.FileName = pathCommand;
                p.StartInfo.Arguments = "\"" + pathFile + "\"";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.WaitForExit();

                System.Console.WriteLine(p.StandardOutput.ReadToEnd());
                System.Console.WriteLine(p.StandardError.ReadToEnd());
                p.Close();

               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

    

        private void loadBackup(String pathFile)
        {
            try

            {
                String pathCommand = System.IO.Path.GetDirectoryName(Application.ExecutablePath).ToString();
                //String pathBackup = pathCommand + @"\save.sql";
                //createBackup(pathBackup);



                //C.InsertOrUpdate("DROP TABLE IF EXISTS remitosaLiquidar");
                //C.InsertOrUpdate(" DROP TABLE IF EXISTS empresa");
                //C.InsertOrUpdate("DROP TABLE IF EXISTS factura");
                //C.InsertOrUpdate("DROP TABLE IF EXISTS remito");
                //C.InsertOrUpdate("DROP TABLE IF EXISTS clientes");
                //C.InsertOrUpdate("DROP TABLE IF EXISTS producto");
                //C.InsertOrUpdate("DROP TABLE IF EXISTS productos_vendidos");
                //C.InsertOrUpdate("DROP TABLE IF EXISTS productos_vender");
                //C.InsertOrUpdate("DROP PROCEDURE IF EXISTS crear_remito");
                //C.InsertOrUpdate("DROP PROCEDURE IF EXISTS aumento_producto");
                //C.InsertOrUpdate("DROP PROCEDURE IF EXISTS crear_factura");


                if (dataBase == "Antares")
                    pathCommand = pathCommand + @"\loadBackupAntares.bat";
                else
                    pathCommand = pathCommand + @"\loadBackupDelivery.bat";
                System.Diagnostics.Process p = new System.Diagnostics.Process();

                p.StartInfo.FileName = pathCommand;
                p.StartInfo.Arguments = "\"" + pathFile + "\"";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.WaitForExit();
                p.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
   

        private void cargarBackup_Click(object sender, EventArgs e)
        {
            C.CerrarConexion();
            if (comboBox1.Items[comboBox1.SelectedIndex].ToString() == "Antares S.A.")
            {
              dataBase="Antares";
              C.Conectar(connectAntares);
            }
            else
            {
                dataBase = "PostalDelivery";
                C.Conectar(connectDelivery);
            }
                openFile.ShowDialog();
            
            
        }

        private void crearBackup_Click(object sender, EventArgs e)
        {
            C.CerrarConexion();
            if (comboBox1.Items[comboBox1.SelectedIndex].ToString() == "Antares S.A.")
            {
                dataBase = "Antares";
             
            }
            else
            {
                dataBase = "PostalDelivery";
                
            }
                saveFile.ShowDialog();
            
                
         
        }

        private void openFile_FileOk(object sender, CancelEventArgs e)
        {
            if (openFile.CheckFileExists)
                loadBackup(openFile.FileName.ToString());
        }

        private void saveFile_FileOk(object sender, CancelEventArgs e)
        {
          
                createBackup(saveFile.FileName.ToString());
        }
    }
}
