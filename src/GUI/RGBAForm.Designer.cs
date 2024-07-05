
namespace Draw.src.GUI
{
    partial class RGBAForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rText = new System.Windows.Forms.TextBox();
            this.gText = new System.Windows.Forms.TextBox();
            this.bText = new System.Windows.Forms.TextBox();
            this.aText = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(476, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Промяна на цвета на примитива, използвайки RGBA модела";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(254, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Стойност за червена светлина: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(245, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Стойност за зелена светлина: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(226, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Стойност за синя светлина: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 220);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(230, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Стойност за прозрачността: ";
            // 
            // rText
            // 
            this.rText.Location = new System.Drawing.Point(351, 107);
            this.rText.Name = "rText";
            this.rText.Size = new System.Drawing.Size(146, 26);
            this.rText.TabIndex = 5;
            this.rText.Text = "0";
            this.rText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gText
            // 
            this.gText.Location = new System.Drawing.Point(351, 147);
            this.gText.Name = "gText";
            this.gText.Size = new System.Drawing.Size(146, 26);
            this.gText.TabIndex = 6;
            this.gText.Text = "0";
            this.gText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bText
            // 
            this.bText.Location = new System.Drawing.Point(351, 185);
            this.bText.Name = "bText";
            this.bText.Size = new System.Drawing.Size(146, 26);
            this.bText.TabIndex = 7;
            this.bText.Text = "0";
            this.bText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // aText
            // 
            this.aText.Location = new System.Drawing.Point(351, 220);
            this.aText.Name = "aText";
            this.aText.Size = new System.Drawing.Size(146, 26);
            this.aText.TabIndex = 8;
            this.aText.Text = "0";
            this.aText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(185, 314);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(198, 32);
            this.button1.TabIndex = 9;
            this.button1.Text = "Избери";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SetRGBAColor);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(101, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(352, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Моля, въведете стойност в диапазона 0-255";
            // 
            // RGBAForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 444);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.aText);
            this.Controls.Add(this.bText);
            this.Controls.Add(this.gText);
            this.Controls.Add(this.rText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "RGBAForm";
            this.Text = "RGBAForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox rText;
        private System.Windows.Forms.TextBox gText;
        private System.Windows.Forms.TextBox bText;
        private System.Windows.Forms.TextBox aText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
    }
}