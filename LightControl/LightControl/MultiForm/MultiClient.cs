using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace MultiForm
{
    public partial class MultiClient : Form
    {
        public delegate void setlab_status_new(Control c,string msg);
        /// <summary>
        /// 更新lab_status的数据
        /// </summary>
        public delegate void setlab_status(string msg);
        /// <summary>
        /// 更新DataGridView的数据
        /// </summary>
        public delegate void UpdateDataSource();
        //设置Button的Enabled属性
        public delegate void setEnabledState(Button control, bool enabled);
        string RemoteEndPoint;     //客户端的网络结点
        Thread threadwatch = null;//负责监听客户端的线程
        Socket socketwatch = null;//负责监听客户端的套接字
        //创建一个和客户端通信的套接字
        Dictionary<string, Socket> dic = new Dictionary<string, Socket> { };   //定义一个集合，存储客户端信息
        /// <summary>
        /// 客户端IP列表
        /// </summary>
        DataTable dtClientInfo;

        const int MaxPanelNum = 4;

        /// <summary>
        /// 设置按钮enabled属性
        /// </summary>
        /// <param name="button"></param>
        /// <param name="enabled"></param>
        private void setEnabledStateEvent(Button button, bool enabled)
        {
            button.Enabled = enabled;
        }
        private void setlab_statusEvent(string msg)
        {
            lab_staus.Text = msg;
        }
        public MultiClient()
        {
            InitializeComponent();
        }
        private void setlab_status_newEvent(Control c,string msg)
        {
            c.Text = msg;
        }
        /// <summary>
        ///更新表格的数据
        /// </summary>
        private void UpdateDataSourceEvent()
        {
            datalist.DataSource = dtClientInfo.Copy();
        }
        //监听客户端发来的连接请求
        private void watchconnecting()
        {
            Socket connection = null;
            while (true)  //持续不断监听客户端发来的请求   
            {
                try
                {
                    connection = socketwatch.Accept();
                }
                catch (Exception ex)
                {
                    //datalist.Invoke(new showMsg(SetValue), ex.ToString());
                    //ts_label.Text = ex.ToString();
                    lab_staus.Invoke(new setlab_status(setlab_statusEvent), ex.ToString());
                    CommonFunction.Write_CommandText(ex.ToString());
                    break;
                }
                //获取客户端的IP和端口号
                IPAddress clientIP = (connection.RemoteEndPoint as IPEndPoint).Address;
                int clientPort = (connection.RemoteEndPoint as IPEndPoint).Port;

                //让客户显示"连接成功的"的信息
                //string sendmsg = "连接服务端成功！\r\n" + "本地IP:" + clientIP + "，本地端口" + clientPort.ToString();
                //byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendmsg);
                //connection.Send(arrSendMsg);
                ////ts_label.Text = sendmsg;
                //lab_staus.Invoke(new setlab_status(setlab_statusEvent), sendmsg);

                //CommonFunction.Write_CommandText(sendmsg);


                RemoteEndPoint = connection.RemoteEndPoint.ToString(); //客户端网络结点号
                //textBox3.AppendText("成功与" + RemoteEndPoint + "客户端建立连接！\t\n");     //显示与客户端连接情况
                //datalist.Invoke(new showMsg(SetValue), "成功与" + RemoteEndPoint + "客户端建立连接！");
                CommonFunction.Write_CommandText("成功与" + RemoteEndPoint + "客户端建立连接！");
                //ts_label.Text = "成功与" + RemoteEndPoint + "客户端建立连接！";
                lab_staus.Invoke(new setlab_status(setlab_statusEvent), "成功与" + RemoteEndPoint + "客户端建立连接！");
                dic.Add(RemoteEndPoint, connection);    //添加客户端信息
                //OnlineList_Disp(RemoteEndPoint);    //显示在线客户端
                //clientList.Invoke(new showMsg(OnlineList_Disp), RemoteEndPoint);
                //if (dtClientInfo.Select("F_IP='" + RemoteEndPoint + "'").Length == 0)//更新客户端数据列表
                //{
                //    DataRow dr = dtClientInfo.NewRow();
                //    dr["F_IP"] = RemoteEndPoint;
                //    dr["F_STATE"] = "在线";

                //    dtClientInfo.Rows.Add(dr);

                //    datalist.Invoke(new UpdateDataSource(UpdateDataSourceEvent));
                //}


                //IPEndPoint netpoint = new IPEndPoint(clientIP,clientPort);

                IPEndPoint netpoint = connection.RemoteEndPoint as IPEndPoint;

                //创建一个通信线程    
                ParameterizedThreadStart pts = new ParameterizedThreadStart(recv);
                Thread thread = new Thread(pts);
                thread.IsBackground = true;//设置为后台线程，随着主线程退出而退出   
                //启动线程   
                thread.Start(connection);
            }
        }
        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        private string GetCurrentTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 接收客户端发来的信息
        /// </summary>
        /// <param name="socketclientpara"></param>
        private void recv(object socketclientpara)
        {
            Socket socketServer = socketclientpara as Socket;
            while (true)
            {
                //创建一个内存缓冲区 其大小为1024*1024字节  即1M   
                byte[] arrServerRecMsg = new byte[128*128 ];
                //将接收到的信息存入到内存缓冲区,并返回其字节数组的长度  
                try
                {
                    int length = socketServer.Receive(arrServerRecMsg);

                    //  CommonFunction.Write_CommandText("二进制数组转16进制：" + CommonFunction.byteToHexStr(arrServerRecMsg));
                    //将机器接受到的字节数组转换为人可以读懂的字符串   
                    string strSRecMsg = Encoding.ASCII.GetString(arrServerRecMsg, 0, length);

                    //将发送的字符串信息附加到文本框txtMsg上   
                    //textBox3.AppendText("客户端:" + socketServer.RemoteEndPoint + ",time:" + GetCurrentTime() + "\r\n" + strSRecMsg + "\r\n\n");
                    //datalist.Invoke(new showMsg(SetValue), "客户端: " + socketServer.RemoteEndPoint + ", time: " + GetCurrentTime() + "\r\n" + strSRecMsg);
                    //ts_label.Text = "接收客户端数据: " + socketServer.RemoteEndPoint + ", time: " + GetCurrentTime() + " " + strSRecMsg;
                    lab_staus.Invoke(new setlab_status(setlab_statusEvent), "接收客户端数据: " + socketServer.RemoteEndPoint + ", time: " + GetCurrentTime() + " " + strSRecMsg);
                    CommonFunction.Write_CommandText("接收客户端信息:" + "客户端: " + socketServer.RemoteEndPoint + ", time: " + GetCurrentTime() + "\r\n" +CommonFunction.byteToHexStr(arrServerRecMsg));

                    //解析数据
                    
                    string command_code=CommonFunction.UnpackByte(arrServerRecMsg, dtClientInfo, RemoteEndPoint);
                    datalist.Invoke(new UpdateDataSource(UpdateDataSourceEvent));
                    SetClientInfo();

                    if (command_code == "02" || command_code == "2")//定时上传的数据做出回应
                    {
                        string sendMsg = "FF00" + "000402" + CommonFunction.byteToHexStr(Encoding.ASCII.GetBytes("GET"));
                        //0xFF00+0x0004+0x02+”GET”
                        byte[] bytes = CommonFunction.hexStringToByte(sendMsg); //;System.Text.Encoding.UTF8.GetBytes(sendMsg);   //将要发送的信息转化为字节数组，因为Socket发送数据时是以字节的形式发送的
                        socketServer.Send(bytes);   //发送数据
                    }
                    else if (command_code == "131" || command_code == "132")
                    {
                        panel1.Tag = null;
                        btn_on.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("返回命令码错误:"+command_code);
                    }
                    //SendMsgEvent(this, new MyEventArg() { Text = "接收客户端信息:" + "客户端: " + socketServer.RemoteEndPoint + ", time: " + GetCurrentTime() + "\r\n" + "    " + strSRecMsg });
                }
                catch (Exception ex)
                {
                    //textBox3.AppendText("客户端" + socketServer.RemoteEndPoint + "已经中断连接" + "\r\n"); //提示套接字监听异常 
                    //datalist.Invoke(new showMsg(SetValue), "客户端" + socketServer.RemoteEndPoint + "已经中断连接");
                    //ts_label.Text = "客户端" + socketServer.RemoteEndPoint + "已经中断连接";
                    lab_staus.Invoke(new setlab_status(setlab_statusEvent), "客户端" + socketServer.RemoteEndPoint + "已经中断连接");
                    CommonFunction.Write_CommandText("客户端" + socketServer.RemoteEndPoint + "已经中断连接");
                    //clientList.Items.Remove(socketServer.RemoteEndPoint.ToString());//从listbox中移除断开连接的客户端
                    //DataRow dr_find = dtClientInfo.Select("F_IP='"+socketServer.RemoteEndPoint+"'")[0];
                    //dr_find
                    foreach (DataRow dr in dtClientInfo.Rows)
                    {
                        if (dr["F_IP"].ToString().Trim() == socketServer.RemoteEndPoint.ToString())
                        {
                            dr["F_STATE"] = "离线";
                        }
                    }
                    datalist.Invoke(new UpdateDataSource(UpdateDataSourceEvent));

                    socketServer.Close();//关闭之前accept出来的和客户端进行通信的套接字
                    break;
                }
            }


        }

        private void MultiClient_Load(object sender, EventArgs e)
        {
            datalist.AutoGenerateColumns = false;

            dtClientInfo = new DataTable();
            dtClientInfo.Columns.Add("F_IP", typeof(string));
            dtClientInfo.Columns.Add("F_STATE", typeof(string));
            dtClientInfo.Columns.Add("F_IMEI", typeof(string));
            dtClientInfo.Columns.Add("F_POWER", typeof(string));
            dtClientInfo.Columns.Add("F_DY", typeof(string));
            dtClientInfo.Columns.Add("F_DL", typeof(string));
            dtClientInfo.Columns.Add("F_TIME", typeof(string));
            dtClientInfo.Columns.Add("F_ON", typeof(string));
        }
        /// <summary>
        /// 设置客户端信息
        /// </summary>
        private void SetClientInfo()
        {
            //for (int i = 1; i <= MaxPanelNum; i++)
            //{
            //    foreach (Control c in this.Controls)
            //    {
            //        if (c is Panel && c.Name == "panel" + i)//找到对应panel
            //        {

            //        }
            //    }
            //}

            if (dtClientInfo.Rows.Count > 0)
            {
                lab_IP.Invoke(new setlab_status_new(setlab_status_newEvent), lab_IP, "IP：" + dtClientInfo.Rows[0]["F_IP"].ToString().Trim());
                lab_time.Invoke(new setlab_status_new(setlab_status_newEvent), lab_time, "时间：" + dtClientInfo.Rows[0]["F_TIME"].ToString().Trim());
                lab_IMEI.Invoke(new setlab_status_new(setlab_status_newEvent), lab_IMEI, "IMEI：" + dtClientInfo.Rows[0]["F_IMEI"].ToString().Trim());
                lab_gl.Invoke(new setlab_status_new(setlab_status_newEvent), lab_gl, "功率：" + dtClientInfo.Rows[0]["F_POWER"].ToString().Trim());
                lab_dy.Invoke(new setlab_status_new(setlab_status_newEvent), lab_dy, "电压：" + dtClientInfo.Rows[0]["F_DY"].ToString().Trim());
                lab_dl.Invoke(new setlab_status_new(setlab_status_newEvent), lab_dl, "电流：" + dtClientInfo.Rows[0]["F_DL"].ToString().Trim());
                lab_conn.Invoke(new setlab_status_new(setlab_status_newEvent), lab_conn, "连接状态：" + dtClientInfo.Rows[0]["F_STATE"].ToString().Trim());
                lab_on.Invoke(new setlab_status_new(setlab_status_newEvent), lab_on, dtClientInfo.Rows[0]["F_ON"].ToString().Trim() == "1" ? "开关状态：开" : "开关状态：关");

                //btn_on.Enabled = dtClientInfo.Rows[0]["F_ON"].ToString().Trim() == "1" ? false : true;
                //btn_off.Enabled = !btn_on.Enabled;

                btn_on.Invoke(new setEnabledState(setEnabledStateEvent),btn_on,true);
                btn_on.Invoke(new setlab_status_new(setlab_status_newEvent), btn_on, dtClientInfo.Rows[0]["F_ON"].ToString().Trim() == "1" ? "关灯" : "开灯");
                //btn_off.Invoke(new setEnabledState(setEnabledStateEvent), btn_on, dtClientInfo.Rows[0]["F_ON"].ToString().Trim() == "1" ? true : false);

                //lab_IP.Text = "IP："+dtClientInfo.Rows[0]["F_IP"].ToString().Trim();
                //lab_time.Text= "时间："+dtClientInfo.Rows[0]["F_TIME"].ToString().Trim();
                //lab_IMEI.Text = "IMEI："+dtClientInfo.Rows[0]["F_IMEI"].ToString().Trim();
                //lab_gl.Text = "功率：" + dtClientInfo.Rows[0]["F_POWER"].ToString().Trim();
                //lab_dy.Text = "电压：" + dtClientInfo.Rows[0]["F_DY"].ToString().Trim();
                //lab_dl.Text = "电流：" + dtClientInfo.Rows[0]["F_DL"].ToString().Trim();
                //lab_conn.Text = "连接状态：" + dtClientInfo.Rows[0]["F_STATE"].ToString().Trim();

            }

        }
        

        private void txt_port_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            //定义一个套接字用于监听客户端发来的消息，包含三个参数（IP4寻址协议，流式连接，Tcp协议）
            socketwatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //服务端发送信息需要一个IP地址和端口号
            //IPAddress address = IPAddress.Parse("127.0.0.1");//获取文本框输入的IP地址
            IPAddress address = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1];

            //将IP地址和端口号绑定到网络节点point上
            IPEndPoint point = new IPEndPoint(address, int.Parse(txt_port.Text.Trim()));//获取文本框上输入的端口号
            //此端口专门用来监听的

            //监听绑定的网络节点
            socketwatch.Bind(point);

            //将套接字的监听队列长度限制为20
            socketwatch.Listen(20);

            //创建一个监听线程
            threadwatch = new Thread(watchconnecting);


            //将窗体线程设置为与后台同步，随着主线程结束而结束
            threadwatch.IsBackground = true;

            //启动线程   
            threadwatch.Start();

            //启动线程后 textBox3文本框显示相应提示
            //textBox3.AppendText("开始监听客户端传来的信息!" + "\r\n");

            //datalist.Invoke(new showMsg(SetValue), "开始监听客户端传来的信息");

            CommonFunction.Write_CommandText("开始监听客户端传来的信息");
            //ts_label.Text = "开始监听客户端传来的信息";
            lab_staus.Invoke(new setlab_status(setlab_statusEvent), "开始监听客户端传来的信息");
            btn_stop.Enabled = true;
            btn_start.Enabled = false;
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {

        }

        private void btn_on_Click(object sender, EventArgs e)
        {
            if (btn_on.Text == "开灯")
            {
                string sendMsg = "FF00" + "000383" + CommonFunction.byteToHexStr(Encoding.ASCII.GetBytes("ON"));

                byte[] bytes = CommonFunction.hexStringToByte(sendMsg); //;System.Text.Encoding.UTF8.GetBytes(sendMsg);   //将要发送的信息转化为字节数组，因为Socket发送数据时是以字节的形式发送的
                Socket clientSocket = dic[dtClientInfo.Rows[0]["F_IP"].ToString().Trim()];
                clientSocket.Send(bytes);   //发送数据

                btn_on.Enabled = false;

                panel1.Tag = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                string sendMsg = "FF00" + "000484" + CommonFunction.byteToHexStr(Encoding.ASCII.GetBytes("OFF"));
                byte[] bytes = CommonFunction.hexStringToByte(sendMsg); //;System.Text.Encoding.UTF8.GetBytes(sendMsg);   //将要发送的信息转化为字节数组，因为Socket发送数据时是以字节的形式发送的
                Socket clientSocket = dic[dtClientInfo.Rows[0]["F_IP"].ToString().Trim()];
                clientSocket.Send(bytes);   //发送数据
                btn_on.Enabled = false;
                panel1.Tag = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (panel1.Tag != null)
            {
                if (DateTime.Now.AddSeconds(-5).CompareTo(Convert.ToDateTime(panel1.Tag)) > 0)//5秒了
                {
                    panel1.Tag = null;
                    btn_on.Enabled = true;
                    MessageBox.Show("客户端未应答开关灯操作");
                }
            }
        }
    }
}
