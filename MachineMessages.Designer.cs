namespace sale_system_new
{
    partial class MachineMessages
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MachineMessages));
            this.lblName = new System.Windows.Forms.Label();
            this.dgvShowWater = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.bwLoad = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvShowAir = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowWater)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowAir)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Location = new System.Drawing.Point(61, 11);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(101, 37);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            // 
            // dgvShowWater
            // 
            this.dgvShowWater.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvShowWater.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShowWater.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvShowWater.Location = new System.Drawing.Point(57, 109);
            this.dgvShowWater.Name = "dgvShowWater";
            this.dgvShowWater.RowHeadersWidth = 51;
            this.dgvShowWater.RowTemplate.Height = 27;
            this.dgvShowWater.Size = new System.Drawing.Size(1190, 147);
            this.dgvShowWater.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(52, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "该用户的净水器购买信息如下";
            // 
            // bwLoad
            // 
            this.bwLoad.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoad_DoWork);
            this.bwLoad.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoad_RunWorkerCompleted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(52, 282);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(312, 27);
            this.label1.TabIndex = 4;
            this.label1.Text = "该用户的空气净化器购买信息如下";
            // 
            // dgvShowAir
            // 
            this.dgvShowAir.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvShowAir.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShowAir.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvShowAir.Location = new System.Drawing.Point(57, 322);
            this.dgvShowAir.Name = "dgvShowAir";
            this.dgvShowAir.RowHeadersWidth = 51;
            this.dgvShowAir.RowTemplate.Height = 27;
            this.dgvShowAir.Size = new System.Drawing.Size(1190, 147);
            this.dgvShowAir.TabIndex = 3;
            // 
            // MachineMessages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 510);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvShowAir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvShowWater);
            this.Controls.Add(this.lblName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MachineMessages";
            this.Text = "净水器&空气净化器";
            this.Load += new System.EventHandler(this.WaterMessage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowWater)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowAir)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.DataGridView dgvShowWater;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.BackgroundWorker bwLoad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvShowAir;
    }
}