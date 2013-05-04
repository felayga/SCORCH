namespace SCORCH
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
            this.button_newmap = new System.Windows.Forms.Button();
            this.button_digger = new System.Windows.Forms.Button();
            this.pictureBox1 = new SCORCH.display.window();
            this.button_square = new System.Windows.Forms.Button();
            this.button_tracer = new System.Windows.Forms.Button();
            this.button_lbomb = new System.Windows.Forms.Button();
            this.button_spread = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_newmap
            // 
            this.button_newmap.Location = new System.Drawing.Point(617, 25);
            this.button_newmap.Name = "button_newmap";
            this.button_newmap.Size = new System.Drawing.Size(75, 23);
            this.button_newmap.TabIndex = 1;
            this.button_newmap.Text = "regen map";
            this.button_newmap.UseVisualStyleBackColor = true;
            // 
            // button_digger
            // 
            this.button_digger.Location = new System.Drawing.Point(617, 54);
            this.button_digger.Name = "button_digger";
            this.button_digger.Size = new System.Drawing.Size(75, 23);
            this.button_digger.TabIndex = 2;
            this.button_digger.Text = "digger";
            this.button_digger.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button_square
            // 
            this.button_square.Location = new System.Drawing.Point(617, 83);
            this.button_square.Name = "button_square";
            this.button_square.Size = new System.Drawing.Size(75, 23);
            this.button_square.TabIndex = 3;
            this.button_square.Text = "square";
            this.button_square.UseVisualStyleBackColor = true;
            // 
            // button_tracer
            // 
            this.button_tracer.Location = new System.Drawing.Point(617, 112);
            this.button_tracer.Name = "button_tracer";
            this.button_tracer.Size = new System.Drawing.Size(75, 23);
            this.button_tracer.TabIndex = 4;
            this.button_tracer.Text = "tracer";
            this.button_tracer.UseVisualStyleBackColor = true;
            // 
            // button_lbomb
            // 
            this.button_lbomb.Location = new System.Drawing.Point(617, 141);
            this.button_lbomb.Name = "button_lbomb";
            this.button_lbomb.Size = new System.Drawing.Size(75, 23);
            this.button_lbomb.TabIndex = 5;
            this.button_lbomb.Text = "lbomb";
            this.button_lbomb.UseVisualStyleBackColor = true;
            // 
            // button_spread
            // 
            this.button_spread.Location = new System.Drawing.Point(617, 170);
            this.button_spread.Name = "button_spread";
            this.button_spread.Size = new System.Drawing.Size(75, 23);
            this.button_spread.TabIndex = 6;
            this.button_spread.Text = "spread";
            this.button_spread.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 576);
            this.Controls.Add(this.button_spread);
            this.Controls.Add(this.button_lbomb);
            this.Controls.Add(this.button_tracer);
            this.Controls.Add(this.button_square);
            this.Controls.Add(this.button_digger);
            this.Controls.Add(this.button_newmap);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private display.window pictureBox1;
        private System.Windows.Forms.Button button_newmap;
        private System.Windows.Forms.Button button_digger;
        private System.Windows.Forms.Button button_square;
        private System.Windows.Forms.Button button_tracer;
        private System.Windows.Forms.Button button_lbomb;
        private System.Windows.Forms.Button button_spread;
    }
}

