namespace YapayZeka2
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtVez = new System.Windows.Forms.TextBox();
            this.txtAt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.txtSon = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMax = new System.Windows.Forms.TextBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.btnPut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 360);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // txtVez
            // 
            this.txtVez.Location = new System.Drawing.Point(398, 33);
            this.txtVez.Name = "txtVez";
            this.txtVez.Size = new System.Drawing.Size(90, 20);
            this.txtVez.TabIndex = 1;
            // 
            // txtAt
            // 
            this.txtAt.Location = new System.Drawing.Point(508, 33);
            this.txtAt.Name = "txtAt";
            this.txtAt.Size = new System.Drawing.Size(90, 20);
            this.txtAt.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(398, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Vezir Sayısı";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(508, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "At Sayısı";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(508, 59);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(87, 48);
            this.btnRun.TabIndex = 5;
            this.btnRun.Text = "Çöz";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // txtSon
            // 
            this.txtSon.Location = new System.Drawing.Point(401, 113);
            this.txtSon.Multiline = true;
            this.txtSon.Name = "txtSon";
            this.txtSon.ReadOnly = true;
            this.txtSon.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSon.Size = new System.Drawing.Size(197, 213);
            this.txtSon.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(508, 336);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Max. Hafıza";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(398, 336);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Zaman";
            // 
            // txtMax
            // 
            this.txtMax.Location = new System.Drawing.Point(508, 352);
            this.txtMax.Name = "txtMax";
            this.txtMax.Size = new System.Drawing.Size(90, 20);
            this.txtMax.TabIndex = 8;
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(398, 352);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(90, 20);
            this.txtTime.TabIndex = 7;
            // 
            // btnPut
            // 
            this.btnPut.Location = new System.Drawing.Point(401, 59);
            this.btnPut.Name = "btnPut";
            this.btnPut.Size = new System.Drawing.Size(87, 48);
            this.btnPut.TabIndex = 11;
            this.btnPut.Text = "Yerleştir";
            this.btnPut.UseVisualStyleBackColor = true;
            this.btnPut.Click += new System.EventHandler(this.btnPut_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(623, 384);
            this.Controls.Add(this.btnPut);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMax);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.txtSon);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAt);
            this.Controls.Add(this.txtVez);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "YapayZeka2";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtVez;
        private System.Windows.Forms.TextBox txtAt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox txtSon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMax;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Button btnPut;
    }
}

