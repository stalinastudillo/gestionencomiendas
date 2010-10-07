namespace Gestion
{
    partial class frmResumenFacturas
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
            this.rbtnTodasLasFacturas = new System.Windows.Forms.RadioButton();
            this.rbtnFactDeUnCliente = new System.Windows.Forms.RadioButton();
            this.btnOKResumenFacturas = new System.Windows.Forms.Button();
            this.cmbClientesFacturas = new System.Windows.Forms.ComboBox();
            this.printDialogResumenFacturas = new System.Windows.Forms.PrintDialog();
            this.printResumenFacturas = new System.Drawing.Printing.PrintDocument();
            this.printDialogFacturasUnCliente = new System.Windows.Forms.PrintDialog();
            this.printFacturasUnCliente = new System.Drawing.Printing.PrintDocument();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbtnImpagas = new System.Windows.Forms.RadioButton();
            this.rbtnPagas = new System.Windows.Forms.RadioButton();
            this.rbtnTodas = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dateInicioResumenFacturas = new System.Windows.Forms.DateTimePicker();
            this.dateFinResumenFacturas = new System.Windows.Forms.DateTimePicker();
            this.checkBoxPeriodo = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbtnTodasLasFacturas
            // 
            this.rbtnTodasLasFacturas.AutoSize = true;
            this.rbtnTodasLasFacturas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnTodasLasFacturas.Location = new System.Drawing.Point(24, 19);
            this.rbtnTodasLasFacturas.Name = "rbtnTodasLasFacturas";
            this.rbtnTodasLasFacturas.Size = new System.Drawing.Size(211, 20);
            this.rbtnTodasLasFacturas.TabIndex = 0;
            this.rbtnTodasLasFacturas.TabStop = true;
            this.rbtnTodasLasFacturas.Text = "Resúmen de todas las facturas";
            this.rbtnTodasLasFacturas.UseVisualStyleBackColor = true;
            // 
            // rbtnFactDeUnCliente
            // 
            this.rbtnFactDeUnCliente.AutoSize = true;
            this.rbtnFactDeUnCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnFactDeUnCliente.Location = new System.Drawing.Point(24, 42);
            this.rbtnFactDeUnCliente.Name = "rbtnFactDeUnCliente";
            this.rbtnFactDeUnCliente.Size = new System.Drawing.Size(328, 20);
            this.rbtnFactDeUnCliente.TabIndex = 1;
            this.rbtnFactDeUnCliente.TabStop = true;
            this.rbtnFactDeUnCliente.Text = "Resúmen de facturas de la empresa seleccionada";
            this.rbtnFactDeUnCliente.UseVisualStyleBackColor = true;
            this.rbtnFactDeUnCliente.CheckedChanged += new System.EventHandler(this.rbtnFactDeUnCliente_CheckedChanged);
            // 
            // btnOKResumenFacturas
            // 
            this.btnOKResumenFacturas.Location = new System.Drawing.Point(156, 223);
            this.btnOKResumenFacturas.Name = "btnOKResumenFacturas";
            this.btnOKResumenFacturas.Size = new System.Drawing.Size(85, 30);
            this.btnOKResumenFacturas.TabIndex = 2;
            this.btnOKResumenFacturas.Text = "OK";
            this.btnOKResumenFacturas.UseVisualStyleBackColor = true;
            this.btnOKResumenFacturas.Click += new System.EventHandler(this.btnOKResumenFacturas_Click);
            // 
            // cmbClientesFacturas
            // 
            this.cmbClientesFacturas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbClientesFacturas.FormattingEnabled = true;
            this.cmbClientesFacturas.Location = new System.Drawing.Point(42, 63);
            this.cmbClientesFacturas.Name = "cmbClientesFacturas";
            this.cmbClientesFacturas.Size = new System.Drawing.Size(229, 24);
            this.cmbClientesFacturas.TabIndex = 3;
            this.cmbClientesFacturas.SelectedIndexChanged += new System.EventHandler(this.cmbClientesFacturas_SelectedIndexChanged);
            // 
            // printDialogResumenFacturas
            // 
            this.printDialogResumenFacturas.UseEXDialog = true;
            // 
            // printResumenFacturas
            // 
            this.printResumenFacturas.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printResumenFacturas_PrintPage);
            // 
            // printDialogFacturasUnCliente
            // 
            this.printDialogFacturasUnCliente.UseEXDialog = true;
            // 
            // printFacturasUnCliente
            // 
            this.printFacturasUnCliente.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printFacturasUnCliente_PrintPage);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbClientesFacturas);
            this.groupBox1.Controls.Add(this.rbtnFactDeUnCliente);
            this.groupBox1.Controls.Add(this.rbtnTodasLasFacturas);
            this.groupBox1.Location = new System.Drawing.Point(10, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 93);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seleccione lo que desea imprimir";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtnImpagas);
            this.groupBox2.Controls.Add(this.rbtnPagas);
            this.groupBox2.Controls.Add(this.rbtnTodas);
            this.groupBox2.Location = new System.Drawing.Point(10, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(381, 48);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Seleccione el filtro deseado";
            // 
            // rbtnImpagas
            // 
            this.rbtnImpagas.AutoSize = true;
            this.rbtnImpagas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnImpagas.Location = new System.Drawing.Point(237, 19);
            this.rbtnImpagas.Name = "rbtnImpagas";
            this.rbtnImpagas.Size = new System.Drawing.Size(110, 20);
            this.rbtnImpagas.TabIndex = 2;
            this.rbtnImpagas.TabStop = true;
            this.rbtnImpagas.Text = "Sólo impagas";
            this.rbtnImpagas.UseVisualStyleBackColor = true;
            this.rbtnImpagas.CheckedChanged += new System.EventHandler(this.rbtnImpagas_CheckedChanged);
            // 
            // rbtnPagas
            // 
            this.rbtnPagas.AutoSize = true;
            this.rbtnPagas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnPagas.Location = new System.Drawing.Point(123, 19);
            this.rbtnPagas.Name = "rbtnPagas";
            this.rbtnPagas.Size = new System.Drawing.Size(96, 20);
            this.rbtnPagas.TabIndex = 1;
            this.rbtnPagas.TabStop = true;
            this.rbtnPagas.Text = "Sólo pagas";
            this.rbtnPagas.UseVisualStyleBackColor = true;
            this.rbtnPagas.CheckedChanged += new System.EventHandler(this.rbtnPagas_CheckedChanged);
            // 
            // rbtnTodas
            // 
            this.rbtnTodas.AutoSize = true;
            this.rbtnTodas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnTodas.Location = new System.Drawing.Point(27, 19);
            this.rbtnTodas.Name = "rbtnTodas";
            this.rbtnTodas.Size = new System.Drawing.Size(66, 20);
            this.rbtnTodas.TabIndex = 0;
            this.rbtnTodas.TabStop = true;
            this.rbtnTodas.Text = "Todas";
            this.rbtnTodas.UseVisualStyleBackColor = true;
            this.rbtnTodas.CheckedChanged += new System.EventHandler(this.rbtnTodas_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.checkBoxPeriodo);
            this.groupBox3.Controls.Add(this.dateFinResumenFacturas);
            this.groupBox3.Controls.Add(this.dateInicioResumenFacturas);
            this.groupBox3.Location = new System.Drawing.Point(10, 158);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(381, 56);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filtrar período";
            // 
            // dateInicioResumenFacturas
            // 
            this.dateInicioResumenFacturas.Checked = false;
            this.dateInicioResumenFacturas.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateInicioResumenFacturas.Location = new System.Drawing.Point(116, 29);
            this.dateInicioResumenFacturas.Name = "dateInicioResumenFacturas";
            this.dateInicioResumenFacturas.Size = new System.Drawing.Size(114, 20);
            this.dateInicioResumenFacturas.TabIndex = 0;
            // 
            // dateFinResumenFacturas
            // 
            this.dateFinResumenFacturas.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFinResumenFacturas.Location = new System.Drawing.Point(249, 29);
            this.dateFinResumenFacturas.Name = "dateFinResumenFacturas";
            this.dateFinResumenFacturas.Size = new System.Drawing.Size(113, 20);
            this.dateFinResumenFacturas.TabIndex = 1;
            // 
            // checkBoxPeriodo
            // 
            this.checkBoxPeriodo.AutoSize = true;
            this.checkBoxPeriodo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxPeriodo.Location = new System.Drawing.Point(9, 23);
            this.checkBoxPeriodo.Name = "checkBoxPeriodo";
            this.checkBoxPeriodo.Size = new System.Drawing.Size(96, 20);
            this.checkBoxPeriodo.TabIndex = 2;
            this.checkBoxPeriodo.Text = "Aplicar filtro";
            this.checkBoxPeriodo.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(286, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Hasta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(152, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Desde";
            // 
            // frmResumenFacturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 259);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOKResumenFacturas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmResumenFacturas";
            this.Text = "Gestión - Resumen facturas";
            this.Load += new System.EventHandler(this.frmResumenFacturas_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtnTodasLasFacturas;
        private System.Windows.Forms.RadioButton rbtnFactDeUnCliente;
        private System.Windows.Forms.Button btnOKResumenFacturas;
        private System.Windows.Forms.ComboBox cmbClientesFacturas;
        private System.Windows.Forms.PrintDialog printDialogResumenFacturas;
        private System.Drawing.Printing.PrintDocument printResumenFacturas;
        private System.Windows.Forms.PrintDialog printDialogFacturasUnCliente;
        private System.Drawing.Printing.PrintDocument printFacturasUnCliente;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbtnPagas;
        private System.Windows.Forms.RadioButton rbtnTodas;
        private System.Windows.Forms.RadioButton rbtnImpagas;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dateFinResumenFacturas;
        private System.Windows.Forms.DateTimePicker dateInicioResumenFacturas;
        private System.Windows.Forms.CheckBox checkBoxPeriodo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}