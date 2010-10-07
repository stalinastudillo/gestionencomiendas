namespace Gestion
{
    partial class frmRemitoContado
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
            this.btnFactura = new System.Windows.Forms.Button();
            this.btnNoImprimir = new System.Windows.Forms.Button();
            this.printFacturaA = new System.Drawing.Printing.PrintDocument();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.printFacturaB = new System.Drawing.Printing.PrintDocument();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNumFact = new System.Windows.Forms.TextBox();
            this.rbtnFactA = new System.Windows.Forms.RadioButton();
            this.rbtnFactB = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFactura
            // 
            this.btnFactura.Location = new System.Drawing.Point(46, 120);
            this.btnFactura.Name = "btnFactura";
            this.btnFactura.Size = new System.Drawing.Size(72, 28);
            this.btnFactura.TabIndex = 2;
            this.btnFactura.Text = "Imprimir";
            this.btnFactura.UseVisualStyleBackColor = true;
            this.btnFactura.Click += new System.EventHandler(this.btnFacturaA_Click);
            // 
            // btnNoImprimir
            // 
            this.btnNoImprimir.Location = new System.Drawing.Point(147, 120);
            this.btnNoImprimir.Name = "btnNoImprimir";
            this.btnNoImprimir.Size = new System.Drawing.Size(72, 28);
            this.btnNoImprimir.TabIndex = 4;
            this.btnNoImprimir.Text = "&No Imprimir";
            this.btnNoImprimir.UseVisualStyleBackColor = true;
            this.btnNoImprimir.Click += new System.EventHandler(this.btnNoImprimir_Click);
            // 
            // printFacturaA
            // 
            this.printFacturaA.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printFacturaA_PrintPage);
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // printFacturaB
            // 
            this.printFacturaB.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printFacturaB_PrintPage);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtNumFact);
            this.groupBox1.Controls.Add(this.rbtnFactA);
            this.groupBox1.Controls.Add(this.rbtnFactB);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 91);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "";
            this.groupBox1.Text = "Factura";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Numero:";
            // 
            // txtNumFact
            // 
            this.txtNumFact.Location = new System.Drawing.Point(113, 49);
            this.txtNumFact.Name = "txtNumFact";
            this.txtNumFact.Size = new System.Drawing.Size(94, 20);
            this.txtNumFact.TabIndex = 11;
            // 
            // rbtnFactA
            // 
            this.rbtnFactA.AutoSize = true;
            this.rbtnFactA.Location = new System.Drawing.Point(32, 19);
            this.rbtnFactA.Name = "rbtnFactA";
            this.rbtnFactA.Size = new System.Drawing.Size(71, 17);
            this.rbtnFactA.TabIndex = 10;
            this.rbtnFactA.TabStop = true;
            this.rbtnFactA.Text = "Factura A";
            this.rbtnFactA.UseVisualStyleBackColor = true;
            // 
            // rbtnFactB
            // 
            this.rbtnFactB.AutoSize = true;
            this.rbtnFactB.Location = new System.Drawing.Point(119, 19);
            this.rbtnFactB.Name = "rbtnFactB";
            this.rbtnFactB.Size = new System.Drawing.Size(71, 17);
            this.rbtnFactB.TabIndex = 9;
            this.rbtnFactB.TabStop = true;
            this.rbtnFactB.Text = "Factura B";
            this.rbtnFactB.UseVisualStyleBackColor = true;
            // 
            // frmRemitoContado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 169);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnNoImprimir);
            this.Controls.Add(this.btnFactura);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmRemitoContado";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión - Remito";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRemitoContado_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFactura;
        private System.Windows.Forms.Button btnNoImprimir;
        private System.Drawing.Printing.PrintDocument printFacturaA;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Drawing.Printing.PrintDocument printFacturaB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNumFact;
        private System.Windows.Forms.RadioButton rbtnFactA;
        private System.Windows.Forms.RadioButton rbtnFactB;

    }
}