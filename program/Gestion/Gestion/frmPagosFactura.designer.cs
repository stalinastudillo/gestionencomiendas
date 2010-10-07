namespace Gestion
{
    partial class frmPagosFactura
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPagosFactura));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEliminarCheque = new System.Windows.Forms.Button();
            this.btnAgregarContado = new System.Windows.Forms.Button();
            this.btnEliminarContado = new System.Windows.Forms.Button();
            this.btnAgregarCheque = new System.Windows.Forms.Button();
            this.txtSaldo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSUSS = new System.Windows.Forms.TextBox();
            this.txtGanancias = new System.Windows.Forms.TextBox();
            this.txtIBB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvContado = new System.Windows.Forms.DataGridView();
            this.dgvCheques = new System.Windows.Forms.DataGridView();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheques)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnEliminarCheque);
            this.groupBox1.Controls.Add(this.btnAgregarContado);
            this.groupBox1.Controls.Add(this.btnEliminarContado);
            this.groupBox1.Controls.Add(this.btnAgregarCheque);
            this.groupBox1.Controls.Add(this.txtSaldo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtSUSS);
            this.groupBox1.Controls.Add(this.txtGanancias);
            this.groupBox1.Controls.Add(this.txtIBB);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dgvContado);
            this.groupBox1.Controls.Add(this.dgvCheques);
            this.groupBox1.Location = new System.Drawing.Point(9, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 566);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnEliminarCheque
            // 
            this.btnEliminarCheque.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminarCheque.Image")));
            this.btnEliminarCheque.Location = new System.Drawing.Point(64, 174);
            this.btnEliminarCheque.Name = "btnEliminarCheque";
            this.btnEliminarCheque.Size = new System.Drawing.Size(40, 40);
            this.btnEliminarCheque.TabIndex = 2;
            this.btnEliminarCheque.UseVisualStyleBackColor = true;
            this.btnEliminarCheque.Click += new System.EventHandler(this.btnEliminarCheque_Click);
            // 
            // btnAgregarContado
            // 
            this.btnAgregarContado.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarContado.Image")));
            this.btnAgregarContado.Location = new System.Drawing.Point(19, 385);
            this.btnAgregarContado.Name = "btnAgregarContado";
            this.btnAgregarContado.Size = new System.Drawing.Size(40, 40);
            this.btnAgregarContado.TabIndex = 4;
            this.btnAgregarContado.UseVisualStyleBackColor = true;
            this.btnAgregarContado.Click += new System.EventHandler(this.btnAgregarContado_Click);
            // 
            // btnEliminarContado
            // 
            this.btnEliminarContado.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminarContado.Image")));
            this.btnEliminarContado.Location = new System.Drawing.Point(64, 385);
            this.btnEliminarContado.Name = "btnEliminarContado";
            this.btnEliminarContado.Size = new System.Drawing.Size(40, 40);
            this.btnEliminarContado.TabIndex = 5;
            this.btnEliminarContado.UseVisualStyleBackColor = true;
            this.btnEliminarContado.Click += new System.EventHandler(this.btnEliminarContado_Click);
            // 
            // btnAgregarCheque
            // 
            this.btnAgregarCheque.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarCheque.Image")));
            this.btnAgregarCheque.Location = new System.Drawing.Point(19, 174);
            this.btnAgregarCheque.Name = "btnAgregarCheque";
            this.btnAgregarCheque.Size = new System.Drawing.Size(40, 40);
            this.btnAgregarCheque.TabIndex = 1;
            this.btnAgregarCheque.UseVisualStyleBackColor = true;
            this.btnAgregarCheque.Click += new System.EventHandler(this.btnAgregarCheque_Click);
            // 
            // txtSaldo
            // 
            this.txtSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaldo.Location = new System.Drawing.Point(402, 452);
            this.txtSaldo.Name = "txtSaldo";
            this.txtSaldo.Size = new System.Drawing.Size(124, 26);
            this.txtSaldo.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(328, 455);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "SALDO:";
            // 
            // txtSUSS
            // 
            this.txtSUSS.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSUSS.Location = new System.Drawing.Point(134, 516);
            this.txtSUSS.Name = "txtSUSS";
            this.txtSUSS.Size = new System.Drawing.Size(124, 26);
            this.txtSUSS.TabIndex = 9;
            this.txtSUSS.Leave += new System.EventHandler(this.txtSUSS_Leave);
            // 
            // txtGanancias
            // 
            this.txtGanancias.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGanancias.Location = new System.Drawing.Point(134, 484);
            this.txtGanancias.Name = "txtGanancias";
            this.txtGanancias.Size = new System.Drawing.Size(124, 26);
            this.txtGanancias.TabIndex = 8;
            this.txtGanancias.Leave += new System.EventHandler(this.txtGanancias_Leave);
            // 
            // txtIBB
            // 
            this.txtIBB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIBB.Location = new System.Drawing.Point(134, 452);
            this.txtIBB.Name = "txtIBB";
            this.txtIBB.Size = new System.Drawing.Size(124, 26);
            this.txtIBB.TabIndex = 6;
            this.txtIBB.Leave += new System.EventHandler(this.txtIBB_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(70, 519);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "SUSS:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(38, 487);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Ganancias:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(88, 455);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "IBB:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 227);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Contado:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cheques:";
            // 
            // dgvContado
            // 
            this.dgvContado.AllowUserToAddRows = false;
            this.dgvContado.AllowUserToDeleteRows = false;
            this.dgvContado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContado.Location = new System.Drawing.Point(16, 250);
            this.dgvContado.MultiSelect = false;
            this.dgvContado.Name = "dgvContado";
            this.dgvContado.ReadOnly = true;
            this.dgvContado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvContado.Size = new System.Drawing.Size(548, 129);
            this.dgvContado.TabIndex = 3;
            // 
            // dgvCheques
            // 
            this.dgvCheques.AllowUserToAddRows = false;
            this.dgvCheques.AllowUserToDeleteRows = false;
            this.dgvCheques.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCheques.Location = new System.Drawing.Point(16, 39);
            this.dgvCheques.MultiSelect = false;
            this.dgvCheques.Name = "dgvCheques";
            this.dgvCheques.ReadOnly = true;
            this.dgvCheques.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCheques.Size = new System.Drawing.Size(548, 129);
            this.dgvCheques.TabIndex = 0;
            // 
            // btnGrabar
            // 
            this.btnGrabar.Image = ((System.Drawing.Image)(resources.GetObject("btnGrabar.Image")));
            this.btnGrabar.Location = new System.Drawing.Point(303, 583);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(40, 40);
            this.btnGrabar.TabIndex = 10;
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // frmPagosFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 635);
            this.Controls.Add(this.btnGrabar);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPagosFactura";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión - Pagos Factura";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPagosFactura_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheques)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvCheques;
        private System.Windows.Forms.DataGridView dgvContado;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSUSS;
        private System.Windows.Forms.TextBox txtGanancias;
        private System.Windows.Forms.TextBox txtIBB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSaldo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.Button btnEliminarCheque;
        private System.Windows.Forms.Button btnAgregarContado;
        private System.Windows.Forms.Button btnEliminarContado;
        private System.Windows.Forms.Button btnAgregarCheque;
    }
}