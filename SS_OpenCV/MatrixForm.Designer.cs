namespace SS_OpenCV
{
    partial class MatrixForm
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
            this.comboBoxFilter = new System.Windows.Forms.ComboBox();
            this.labelMean_filter = new System.Windows.Forms.Label();
            this.labelCoeficients = new System.Windows.Forms.Label();
            this.labelWeight = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // comboBoxFilter
            // 
            this.comboBoxFilter.FormattingEnabled = true;
            this.comboBoxFilter.Items.AddRange(new object[] {
            "Mean 3x3"});
            this.comboBoxFilter.Location = new System.Drawing.Point(12, 42);
            this.comboBoxFilter.Name = "comboBoxFilter";
            this.comboBoxFilter.Size = new System.Drawing.Size(373, 24);
            this.comboBoxFilter.TabIndex = 0;
            // 
            // labelMean_filter
            // 
            this.labelMean_filter.AutoSize = true;
            this.labelMean_filter.Location = new System.Drawing.Point(12, 9);
            this.labelMean_filter.Name = "labelMean_filter";
            this.labelMean_filter.Size = new System.Drawing.Size(39, 17);
            this.labelMean_filter.TabIndex = 1;
            this.labelMean_filter.Text = "Filter";
            // 
            // labelCoeficients
            // 
            this.labelCoeficients.AutoSize = true;
            this.labelCoeficients.Location = new System.Drawing.Point(12, 82);
            this.labelCoeficients.Name = "labelCoeficients";
            this.labelCoeficients.Size = new System.Drawing.Size(77, 17);
            this.labelCoeficients.TabIndex = 2;
            this.labelCoeficients.Text = "Coeficients";
            // 
            // labelWeight
            // 
            this.labelWeight.AutoSize = true;
            this.labelWeight.Location = new System.Drawing.Point(56, 346);
            this.labelWeight.Name = "labelWeight";
            this.labelWeight.Size = new System.Drawing.Size(56, 17);
            this.labelWeight.TabIndex = 12;
            this.labelWeight.Text = "Weight:";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(128, 343);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(100, 22);
            this.textBox10.TabIndex = 13;
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Location = new System.Drawing.Point(76, 405);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(75, 23);
            this.buttonConfirm.TabIndex = 14;
            this.buttonConfirm.Text = "Ok";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(251, 405);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(76, 125);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(24, 22);
            this.textBox1.TabIndex = 3;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(184, 125);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(24, 22);
            this.textBox2.TabIndex = 4;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(289, 125);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(24, 22);
            this.textBox3.TabIndex = 5;
            this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(76, 205);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(24, 22);
            this.textBox4.TabIndex = 6;
            this.textBox4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox4_KeyPress);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(184, 205);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(24, 22);
            this.textBox5.TabIndex = 7;
            this.textBox5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox5_KeyPress);
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(289, 205);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(24, 22);
            this.textBox6.TabIndex = 8;
            this.textBox6.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox6_KeyPress);
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(76, 287);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(24, 22);
            this.textBox7.TabIndex = 9;
            this.textBox7.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox7_KeyPress);
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(184, 287);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(24, 22);
            this.textBox8.TabIndex = 10;
            this.textBox8.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox8_KeyPress);
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(289, 287);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(24, 22);
            this.textBox9.TabIndex = 11;
            this.textBox9.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox9_KeyPress);
            // 
            // MatrixForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 450);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonConfirm);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.labelWeight);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelCoeficients);
            this.Controls.Add(this.labelMean_filter);
            this.Controls.Add(this.comboBoxFilter);
            this.Name = "MatrixForm";
            this.Text = "Weight Matrix";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox comboBoxFilter;
        private System.Windows.Forms.Label labelMean_filter;
        private System.Windows.Forms.Label labelCoeficients;
        private System.Windows.Forms.Label labelWeight;
        public System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.TextBox textBox2;
        public System.Windows.Forms.TextBox textBox3;
        public System.Windows.Forms.TextBox textBox4;
        public System.Windows.Forms.TextBox textBox5;
        public System.Windows.Forms.TextBox textBox6;
        public System.Windows.Forms.TextBox textBox7;
        public System.Windows.Forms.TextBox textBox8;
        public System.Windows.Forms.TextBox textBox9;
    }
}