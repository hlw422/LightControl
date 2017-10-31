namespace LightControl
{
    partial class LC_MESSAGE
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
            this.lab_imei = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_sendMsg = new System.Windows.Forms.Button();
            this.txt_send = new System.Windows.Forms.TextBox();
            this.datalist = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gridvalue = new System.Windows.Forms.DataGridView();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridvalue)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "终端IMEI：";
            // 
            // lab_imei
            // 
            this.lab_imei.AutoSize = true;
            this.lab_imei.Location = new System.Drawing.Point(76, 9);
            this.lab_imei.Name = "lab_imei";
            this.lab_imei.Size = new System.Drawing.Size(95, 12);
            this.lab_imei.TabIndex = 1;
            this.lab_imei.Text = "435365464575757";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(14, 36);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(578, 225);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_sendMsg);
            this.tabPage1.Controls.Add(this.txt_send);
            this.tabPage1.Controls.Add(this.datalist);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(570, 199);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "通信日志";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_sendMsg
            // 
            this.btn_sendMsg.Location = new System.Drawing.Point(463, 170);
            this.btn_sendMsg.Name = "btn_sendMsg";
            this.btn_sendMsg.Size = new System.Drawing.Size(101, 23);
            this.btn_sendMsg.TabIndex = 6;
            this.btn_sendMsg.Text = "发送数据";
            this.btn_sendMsg.UseVisualStyleBackColor = true;
            this.btn_sendMsg.Click += new System.EventHandler(this.btn_sendMsg_Click);
            // 
            // txt_send
            // 
            this.txt_send.Location = new System.Drawing.Point(10, 172);
            this.txt_send.Name = "txt_send";
            this.txt_send.Size = new System.Drawing.Size(342, 21);
            this.txt_send.TabIndex = 5;
            // 
            // datalist
            // 
            this.datalist.Dock = System.Windows.Forms.DockStyle.Top;
            this.datalist.FormattingEnabled = true;
            this.datalist.ItemHeight = 12;
            this.datalist.Location = new System.Drawing.Point(3, 3);
            this.datalist.Name = "datalist";
            this.datalist.Size = new System.Drawing.Size(564, 160);
            this.datalist.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gridvalue);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(570, 199);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "运行状态";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gridvalue
            // 
            this.gridvalue.AllowUserToAddRows = false;
            this.gridvalue.AllowUserToDeleteRows = false;
            this.gridvalue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridvalue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridvalue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.time,
            this.gl,
            this.dy,
            this.dl});
            this.gridvalue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridvalue.Location = new System.Drawing.Point(3, 3);
            this.gridvalue.Name = "gridvalue";
            this.gridvalue.RowTemplate.Height = 23;
            this.gridvalue.Size = new System.Drawing.Size(564, 193);
            this.gridvalue.TabIndex = 0;
            // 
            // time
            // 
            this.time.HeaderText = "时间";
            this.time.Name = "time";
            this.time.Width = 130;
            // 
            // gl
            // 
            this.gl.HeaderText = "功率";
            this.gl.Name = "gl";
            this.gl.Width = 130;
            // 
            // dy
            // 
            this.dy.HeaderText = "电压";
            this.dy.Name = "dy";
            this.dy.Width = 130;
            // 
            // dl
            // 
            this.dl.HeaderText = "电流";
            this.dl.Name = "dl";
            this.dl.Width = 130;
            // 
            // LC_MESSAGE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 359);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lab_imei);
            this.Controls.Add(this.label1);
            this.Name = "LC_MESSAGE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "服务端";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridvalue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lab_imei;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox datalist;
        private System.Windows.Forms.DataGridView gridvalue;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
        private System.Windows.Forms.DataGridViewTextBoxColumn gl;
        private System.Windows.Forms.DataGridViewTextBoxColumn dy;
        private System.Windows.Forms.DataGridViewTextBoxColumn dl;
        private System.Windows.Forms.Button btn_sendMsg;
        private System.Windows.Forms.TextBox txt_send;
    }
}