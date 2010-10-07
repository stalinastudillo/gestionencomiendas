namespace Gestion
{
    partial class frmActualizarFactura
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
            this.btnActualizarFacturaAceptar = new System.Windows.Forms.Button();
            this.textBoxNumFactura = new System.Windows.Forms.TextBox();
            this.groupBoxActualizarFactura = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxActualizarFactura.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnActualizarFacturaAceptar
            // 
            this.btnActualizarFacturaAceptar.Location = new System.Drawing.Point(49, 119);
            this.btnActualizarFacturaAceptar.Name = "btnActualizarFacturaAceptar";
            this.btnActualizarFacturaAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnActualizarFacturaAceptar.TabIndex = 0;
            this.btnActualizarFacturaAceptar.Text = "Aceptar";
            this.btnActualizarFacturaAceptar.UseVisualStyleBackColor = true;
            this.btnActualizarFacturaAceptar.Click += new System.EventHandler(this.btnActualizarFacturaAceptar_Click);
            // 
            // textBoxNumFactura
            // 
            this.textBoxNumFactura.Location = new System.Drawing.Point(59, 41);
            this.textBoxNumFactura.Name = "textBoxNumFactura";
            this.textBoxNumFactura.Size = new System.Drawing.Size(75, 20);
            this.textBoxNumFactura.TabIndex = 1;
            // 
            // groupBoxActualizarFactura
            // 
            this.groupBoxActualizarFactura.Controls.Add(this.label1);
            this.groupBoxActualizarFactura.Controls.Add(this.textBoxNumFactura);
            this.groupBoxActualizarFactura.Location = new System.Drawing.Point(12, 12);
            this.groupBoxActualizarFactura.Name = "groupBoxActualizarFactura";
            this.groupBoxActualizarFactura.Size = new System.Drawing.Size(156, 87);
            this.groupBoxActualizarFactura.TabIndex = 2;
            this.groupBoxActualizarFactura.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Número:";
            // 
            // frmActualizarFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(181, 150);
            this.Controls.Add(this.groupBoxActualizarFactura);
            this.Controls.Add(this.btnActualizarFacturaAceptar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmActualizarFactura";
            this.Text = "Gestión - Número de Factura";
            this.Load += new System.EventHandler(this.frmActualizarFactura_Load);
            this.groupBoxActualizarFactura.ResumeLayout(false);
            this.groupBoxActualizarFactura.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnActualizarFacturaAceptar;
        private System.Windows.Forms.TextBox textBoxNumFactura;
        private System.Windows.Forms.GroupBox groupBoxActualizarFactura;
        private System.Windows.Forms.Label label1;
    }
}