using System;

namespace Gestion
{
    public class clsImpresion
    {

        public void FacturaA(PrintPageEventArgs e, int idFact)
         {
            DataSet dsRemitoA = new DataSet();
            DataSet dsFacturaA = new DataSet();
            DataSet dsEmpresa = new DataSet();
            DataSet dsCliente = new DataSet();
            DataSet dsFacturaCliente = new DataSet();

            String detalleFact;
            String idcliente;
           
            C.CargarDatos(dsFacturaA, "dsFacturaA", "select max(idfactura) from factura");

            String idFact = ((dsFacturaA.Tables[0].Rows[0][0]).ToString());

            dsFacturaA.Clear();
            C.CargarDatos(dsFacturaA, "dsFacturaA", "select flete,seguro,total from factura where idfactura=" + idFact);

            C.CargarDatos(dsRemitoA, "dsRemitoA", "select * from remito where idfactura=" + idFact);
            Console.WriteLine("select * from remito where idfactura=" + idFact);
            detalleFact = Remito.getInfoRemito(C,dsRemitoA.Tables[0].Rows[0][0].ToString());

            String fecha = DateTime.Now.ToShortDateString();

            C.CargarDatos(dsEmpresa, "dsEmpresa", "select * from empresa");

          
            String remitente = (dsEmpresa.Tables[0].Rows[0][1].ToString());
            String dirRemitente = (dsEmpresa.Tables[0].Rows[0][2].ToString());
            String telefonoEmpresa = (dsEmpresa.Tables[0].Rows[0][3].ToString());
            String ciudadRemitente = (dsEmpresa.Tables[0].Rows[0][4].ToString());
            String cuilRemitente = "";



           
            C.CargarDatos(dsFacturaCliente, "dsFacturaCliente", "select idcliente from factura where idfactura=" + idFact);

            idcliente = dsFacturaCliente.Tables[0].Rows[0][0].ToString();
           
            C.CargarDatos(dsCliente,"dsCliente","select nombre,apellido,direccion,CUIL,ciudad from clientes where idcliente=" + idcliente);
           
           

            String destinatario = (dsCliente.Tables[0].Rows[0][0].ToString()+ " "+dsCliente.Tables[0].Rows[0][1].ToString());
            String dirDestinatario = (dsCliente.Tables[0].Rows[0][2].ToString());
            String ciudadDestinatario = (dsCliente.Tables[0].Rows[0][4].ToString());
            String cuilDestinatario = (dsCliente.Tables[0].Rows[0][3].ToString());

            String IVA = (dsRemitoA.Tables[0].Rows[0][11].ToString());

            String VD = (dsRemitoA.Tables[0].Rows[0][12].ToString());
            String CR = (dsRemitoA.Tables[0].Rows[0][13].ToString());

            String condVenta = (dsRemitoA.Tables[0].Rows[0][10].ToString());

            DataSet aux = new DataSet();
            C.CargarDatos(aux, "aux", "select flete,seguro,total,neto,ivari from factura where idfactura=" + idFact);

            String flete = (aux.Tables[0].Rows[0][0].ToString()); ;
            String seguro = (aux.Tables[0].Rows[0][1].ToString()); ;
            String total = (aux.Tables[0].Rows[0][2].ToString()); ;
            String neto = (aux.Tables[0].Rows[0][3].ToString());
            String ivari = (aux.Tables[0].Rows[0][4].ToString()); 

            Font font = new Font("Tahoma", 10, FontStyle.Bold);

            //Imprime la fecha actual
            //Fecha 1100 / 1170 / 1250 - 270
            e.Graphics.DrawString(fecha, font, Brushes.Black, PrinterUnitConvert.Convert(1050, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            
      
            //Remitente 400 - 550
            e.Graphics.DrawString(remitente, font, Brushes.Black, PrinterUnitConvert.Convert(390, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(480, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Destinatario 1200 - 550 
            e.Graphics.DrawString(destinatario, font, Brushes.Black, PrinterUnitConvert.Convert(1190, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(480, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Domicilio 380 - 610
            e.Graphics.DrawString(dirRemitente, font, Brushes.Black, PrinterUnitConvert.Convert(370, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(540, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Domicilio 1160 - 610
            e.Graphics.DrawString(dirDestinatario, font, Brushes.Black, PrinterUnitConvert.Convert(1140, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(540, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Localidad 400 - 670
            e.Graphics.DrawString(ciudadRemitente, font, Brushes.Black, PrinterUnitConvert.Convert(390, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(590, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Localidad 1170 - 670
            e.Graphics.DrawString(ciudadDestinatario, font, Brushes.Black, PrinterUnitConvert.Convert(1190, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(590, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //C.U.I.T. 340 - 730
            e.Graphics.DrawString(cuilRemitente, font, Brushes.Black, PrinterUnitConvert.Convert(340, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(660, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //C.U.I.T. 1120 - 730
            e.Graphics.DrawString(cuilDestinatario, font, Brushes.Black, PrinterUnitConvert.Convert(1110, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(660, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
        
               
            //Cuenta Corriente 910 - 800
            e.Graphics.DrawString("x", font, Brushes.Black, PrinterUnitConvert.Convert(820, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(735, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //Flete
            //A cobrar 1180 - 1420
            e.Graphics.DrawString(flete, font, Brushes.Black, PrinterUnitConvert.Convert(1160, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1350, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //seguro
            //A cobrar 1180 - 1480
            e.Graphics.DrawString(seguro, font, Brushes.Black, PrinterUnitConvert.Convert(1160, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1410, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //total
            //A cobrar 1180 - 1720
            e.Graphics.DrawString(total, font, Brushes.Black, PrinterUnitConvert.Convert(1160, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1670, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));


            //Neto
            e.Graphics.DrawString(neto, font, Brushes.Black, PrinterUnitConvert.Convert(1150, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1480, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            //IVA R.I.
            e.Graphics.DrawString(ivari, font, Brushes.Black, PrinterUnitConvert.Convert(1150, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1550, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));

            List<String> detalleDividido = new List<String>(7);
            detalleFact=richTextDescripcion.Text.ToString();
            detalleDividido=dividirDescripcion(detalleFact);

            e.Graphics.DrawString(detalleDividido.ElementAt(0), font, Brushes.Black, PrinterUnitConvert.Convert(320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(900, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            e.Graphics.DrawString(detalleDividido.ElementAt(1), font, Brushes.Black, PrinterUnitConvert.Convert(320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(950, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            e.Graphics.DrawString(detalleDividido.ElementAt(2), font, Brushes.Black, PrinterUnitConvert.Convert(320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1000, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            e.Graphics.DrawString(detalleDividido.ElementAt(3), font, Brushes.Black, PrinterUnitConvert.Convert(320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1050, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            e.Graphics.DrawString(detalleDividido.ElementAt(4), font, Brushes.Black, PrinterUnitConvert.Convert(320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1100, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            e.Graphics.DrawString(detalleDividido.ElementAt(5), font, Brushes.Black, PrinterUnitConvert.Convert(320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1150, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
            e.Graphics.DrawString(detalleDividido.ElementAt(6), font, Brushes.Black, PrinterUnitConvert.Convert(320, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(1200, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
        
        
            //Resp. Insc. 1220 - 800
            e.Graphics.DrawString("x", font, Brushes.Black, PrinterUnitConvert.Convert(1160, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(745, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));            
        }//end printFacturaA


    }
}