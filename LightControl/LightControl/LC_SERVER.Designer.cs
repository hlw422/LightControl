namespace LightControl
{
    partial class LC_SERVER
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lab_ip = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_port = new System.Windows.Forms.TextBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.datalist = new System.Windows.Forms.DataGridView();
            this.F_IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F_XH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F_IMEI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F_STATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_final = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ts_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.lab_staus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.datalist)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器IP：";
            // 
            // lab_ip
            // 
            this.lab_ip.AutoSize = true;
            this.lab_ip.Location = new System.Drawing.Point(83, 21);
            this.lab_ip.Name = "lab_ip";
            this.lab_ip.Size = new System.Drawing.Size(59, 12);
            this.lab_ip.TabIndex = 1;
            this.lab_ip.Text = "127.0.0.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(148, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "端口：";
            // 
            // txt_port
            // 
            this.txt_port.Location = new System.Drawing.Point(188, 18);
            this.txt_port.Name = "txt_port";
            this.txt_port.Size = new System.Drawing.Size(87, 21);
            this.txt_port.TabIndex = 3;
            this.txt_port.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_port_KeyPress);
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(298, 17);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 4;
            this.btn_start.Text = "开始服务";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // datalist
            // 
            this.datalist.AllowUserToAddRows = false;
            this.datalist.AllowUserToDeleteRows = false;
            this.datalist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.datalist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datalist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.F_IP,
            this.F_XH,
            this.F_IMEI,
            this.F_STATE});
            this.datalist.Location = new System.Drawing.Point(14, 66);
            this.datalist.Name = "datalist";
            this.datalist.RowTemplate.Height = 23;
            this.datalist.Size = new System.Drawing.Size(645, 222);
            this.datalist.TabIndex = 5;
            // 
            // F_IP
            // 
            this.F_IP.DataPropertyName = "F_IP";
            this.F_IP.HeaderText = "终端IP";
            this.F_IP.Name = "F_IP";
            this.F_IP.Width = 150;
            // 
            // F_XH
            // 
            this.F_XH.DataPropertyName = "F_XH";
            this.F_XH.HeaderText = "终端型号";
            this.F_XH.Name = "F_XH";
            this.F_XH.Width = 150;
            // 
            // F_IMEI
            // 
            this.F_IMEI.DataPropertyName = "F_IMEI";
            this.F_IMEI.HeaderText = "终端IMEI";
            this.F_IMEI.Name = "F_IMEI";
            this.F_IMEI.Width = 150;
            // 
            // F_STATE
            // 
            this.F_STATE.DataPropertyName = "F_STATE";
            this.F_STATE.HeaderText = "终端注网状态";
            this.F_STATE.Name = "F_STATE";
            this.F_STATE.Width = 150;
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(462, 308);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(94, 23);
            this.btn_clear.TabIndex = 6;
            this.btn_clear.Text = "清除离线终端";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_final
            // 
            this.btn_final.Location = new System.Drawing.Point(564, 308);
            this.btn_final.Name = "btn_final";
            this.btn_final.Size = new System.Drawing.Size(94, 23);
            this.btn_final.TabIndex = 7;
            this.btn_final.Text = "终端操作";
            this.btn_final.UseVisualStyleBackColor = true;
            this.btn_final.Click += new System.EventHandler(this.btn_final_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(579, 16);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(75, 23);
            this.btn_stop.TabIndex = 8;
            this.btn_stop.Text = "停止服务";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_label});
            this.statusStrip1.Location = new System.Drawing.Point(0, 392);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(730, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "状态";
            // 
            // ts_label
            // 
            this.ts_label.Name = "ts_label";
            this.ts_label.Size = new System.Drawing.Size(32, 17);
            this.ts_label.Text = "状态";
            // 
            // lab_staus
            // 
            this.lab_staus.AutoSize = true;
            this.lab_staus.Location = new System.Drawing.Point(7, 368);
            this.lab_staus.Name = "lab_staus";
            this.lab_staus.Size = new System.Drawing.Size(35, 12);
            this.lab_staus.TabIndex = 10;
            this.lab_staus.Text = "状态:";
            // 
            // LC_SERVER
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 414);
            this.Controls.Add(this.lab_staus);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_final);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.datalist);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.txt_port);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lab_ip);
            this.Controls.Add(this.label1);
            this.Name = "LC_SERVER";
            this.Text = "服务端";
            this.Load += new System.EventHandler(this.LC_SERVER_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datalist)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lab_ip;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_port;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.DataGridView datalist;
        private System.Windows.Forms.DataGridViewTextBoxColumn F_IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn F_XH;
        private System.Windows.Forms.DataGridViewTextBoxColumn F_IMEI;
        private System.Windows.Forms.DataGridViewTextBoxColumn F_STATE;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_final;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ts_label;
        private System.Windows.Forms.Label lab_staus;
    }
}

