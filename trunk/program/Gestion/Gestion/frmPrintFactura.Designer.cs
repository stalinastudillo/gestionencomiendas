namespace Gestion
{
    partial class frmPrintFactura
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCrearFactura = new System.Windows.Forms.Button();
            this.richTextDescripcion = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNumFact = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateFechaLiquidacion = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.printLiq = new System.Drawing.Printing.PrintDocument();
            this.printDialogLiq = new System.Windows.Forms.PrintDialog();
            this.printFacturaA = new System.Drawing.Printing.PrintDocument();
            this.printDialogFacturaA = new System.Windows.Forms.PrintDialog();
            this.printFacturaB = new System.Drawing.Printing.PrintDocument();
            this.printDialogFacturaB = new System.Windows.Forms.PrintDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCrearFactura
            // 
            this.btnCrearFactura.Location = new System.Drawing.Point(166, 291);
            this.btnCrearFactura.Name = "btnCrearFactura";
            this.btnCrearFactura.Size = new System.Drawing.Size(75, 35);
            this.btnCrearFactura.TabIndex = 2;
            this.btnCrearFactura.Text = "LIQUIDAR";
            this.btnCrearFactura.UseVisualStyleBackColor = true;
            this.btnCrearFactura.Click += new System.EventHandler(this.btnCrearFactura_Click);
            // 
            // richTextDescripcion
            // 
            this.richTextDescripcion.Location = new System.Drawing.Point(48, 21);
            this.richTextDescripcion.MaxLength = 210;
            this.richTextDescripcion.Name = "richTextDescripcion";
            this.richTextDescripcion.Size = new System.Drawing.Size(263, 96);
            this.richTextDescripcion.TabIndex = 4;
            this.richTextDescripcion.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tipo de Factura:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNumFact);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dateFechaLiquidacion);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbTipo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(20, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 130);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // txtNumFact
            // 
            this.txtNumFact.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumFact.Location = new System.Drawing.Point(108, 82);
            this.txtNumFact.Name = "txtNumFact";
            this.txtNumFact.Size = new System.Drawing.Size(143, 22);
            this.txtNumFact.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Nº Factura:";
            // 
            // dateFechaLiquidacion
            // 
            this.dateFechaLiquidacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateFechaLiquidacion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFechaLiquidacion.Location = new System.Drawing.Point(108, 53);
            this.dateFechaLiquidacion.Name = "dateFechaLiquidacion";
            this.dateFechaLiquidacion.Size = new System.Drawing.Size(143, 22);
            this.dateFechaLiquidacion.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Fecha:";
            // 
            // cmbTipo
            // 
            this.cmbTipo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "A",
            "B"});
            this.cmbTipo.Location = new System.Drawing.Point(108, 23);
            this.cmbTipo.Margin = new System.Windows.Forms.Padding(5, 0, 3, 10);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(50, 24);
            this.cmbTipo.TabIndex = 24;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.richTextDescripcion);
            this.groupBox2.Location = new System.Drawing.Point(20, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(366, 131);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Descripción";
            // 
            // printLiq
            // 
            this.printLiq.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printLiq_PrintPage);
            // 
            // printDialogLiq
            // 
            this.printDialogLiq.UseEXDialog = true;
            // 
            // printFacturaA
            // 
            this.printFacturaA.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printFacturaA_PrintPage);
            // 
            // printDialogFacturaA
            // 
            this.printDialogFacturaA.UseEXDialog = true;
            // 
            // printFacturaB
            // 
            this.printFacturaB.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printFacturaB_PrintPage);
            // 
            // printDialogFacturaB
            // 
            this.printDialogFacturaB.UseEXDialog = true;
            // 
            // frmPrintFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 338);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCrearFactura);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPrintFactura";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión - Imprimir Factura";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmPrintFactura_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPrintFactura_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCrearFactura;
        private System.Windows.Forms.RichTextBox richTextDescripcion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Drawing.Printing.PrintDocument printLiq;
        private System.Windows.Forms.PrintDialog printDialogLiq;
        private System.Drawing.Printing.PrintDocument printFacturaA;
        private System.Windows.Forms.PrintDialog printDialogFacturaA;
        private System.Drawing.Printing.PrintDocument printFacturaB;
        private System.Windows.Forms.PrintDialog printDialogFacturaB;
        private System.Windows.Forms.DateTimePicker dateFechaLiquidacion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNumFact;
        private System.Windows.Forms.Label label3;
    }
}