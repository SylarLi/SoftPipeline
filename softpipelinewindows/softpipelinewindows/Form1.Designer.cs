namespace softpipelinewindows
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rotateY = new System.Windows.Forms.Button();
            this.rotateX = new System.Windows.Forms.Button();
            this.rotateZ = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // rotateY
            // 
            this.rotateY.Location = new System.Drawing.Point(380, 63);
            this.rotateY.Name = "rotateY";
            this.rotateY.Size = new System.Drawing.Size(43, 23);
            this.rotateY.TabIndex = 1;
            this.rotateY.Text = "y";
            this.rotateY.UseVisualStyleBackColor = true;
            this.rotateY.Click += new System.EventHandler(this.rotateY_Click);
            // 
            // rotateX
            // 
            this.rotateX.Location = new System.Drawing.Point(331, 63);
            this.rotateX.Name = "rotateX";
            this.rotateX.Size = new System.Drawing.Size(43, 23);
            this.rotateX.TabIndex = 2;
            this.rotateX.Text = "x";
            this.rotateX.UseVisualStyleBackColor = true;
            this.rotateX.Click += new System.EventHandler(this.rotateX_Click);
            // 
            // rotateZ
            // 
            this.rotateZ.Location = new System.Drawing.Point(429, 63);
            this.rotateZ.Name = "rotateZ";
            this.rotateZ.Size = new System.Drawing.Size(43, 23);
            this.rotateZ.TabIndex = 3;
            this.rotateZ.Text = "z";
            this.rotateZ.UseVisualStyleBackColor = true;
            this.rotateZ.Click += new System.EventHandler(this.rotateZ_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(292, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "旋转";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(484, 462);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rotateZ);
            this.Controls.Add(this.rotateX);
            this.Controls.Add(this.rotateY);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.Color.Coral;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button rotateY;
        private System.Windows.Forms.Button rotateX;
        private System.Windows.Forms.Button rotateZ;
        private System.Windows.Forms.Label label1;
    }
}

