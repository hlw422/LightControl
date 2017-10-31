using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace LightControl
{
    public partial class LC_SERVER : Form
    {

        public event EventHandler SendMsgEvent; //使用默认的事件处理委托

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

        /// <summary>
        /// 控件初始化
        /// </summary>
        public LC_SERVER()
        {
            InitializeComponent();
        }
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
        /// <summary>
        ///更新表格的数据
        /// </summary>
        private void UpdateDataSourceEvent()
        {
            datalist.DataSource = dtClientInfo.Copy();
        }
        /// <summary>
        /// IP显示按照配置文件里设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LC_SERVER_Load(object sender, EventArgs e)
        {

            CommonFunction.UnpackByte(new Byte[] { 1 });


            lab_ip.Text = ConfigurationManager.AppSettings["ip"].ToString().Trim();

            datalist.AutoGenerateColumns = false;

            dtClientInfo = new DataTable();
            dtClientInfo.Columns.Add("F_IP", typeof(string));
            dtClientInfo.Columns.Add("F_XH", typeof(string));
            dtClientInfo.Columns.Add("F_IMEI", typeof(string));
            dtClientInfo.Columns.Add("F_STATE", typeof(string));
        }
        /// <summary>
        /// 端口号只能输入数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_port_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
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
                if (dtClientInfo.Select("F_IP='" + RemoteEndPoint + "'").Length == 0)//更新客户端数据列表
                {
                    DataRow dr = dtClientInfo.NewRow();
                    dr["F_IP"] = RemoteEndPoint;
                    dr["F_XH"] = "";
                    dr["F_IMEI"] = "";
                    dr["F_STATE"] = "在线";

                    dtClientInfo.Rows.Add(dr);

                    datalist.Invoke(new UpdateDataSource(UpdateDataSourceEvent));
                }
                

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
                byte[] arrServerRecMsg = new byte[1024 * 1024];
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
                    CommonFunction.Write_CommandText("接收客户端信息:" + "客户端: " + socketServer.RemoteEndPoint + ", time: " + GetCurrentTime() + "\r\n" + strSRecMsg);

                    SendMsgEvent(this, new MyEventArg() { Text = "接收客户端信息:" + "客户端: " + socketServer.RemoteEndPoint + ", time: " + GetCurrentTime() + "\r\n" +"    "+strSRecMsg });
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
                    foreach(DataRow dr in dtClientInfo.Rows)
                    {
                        if(dr["F_IP"].ToString().Trim()==socketServer.RemoteEndPoint.ToString())
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

        private void btn_final_Click(object sender, EventArgs e)
        {
            if(datalist.CurrentRow==null)
            {
                MessageBox.Show("先选择客户端");
                return;
            }
            //子窗体弹出来之前，注册事件，关注主窗体消息的变化，当有多个窗体需要接收信息，只需要在此修改即可
            string selectClient = datalist.CurrentRow.Cells["F_IP"].Value.ToString().Trim();

            LC_MESSAGE childFormA = new LC_MESSAGE(dic[selectClient]);
            SendMsgEvent += childFormA.MainFormTxtChaned;//为子窗体注册事件，在子窗体中事件处理代码中设置文本
            childFormA.Show();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            for(int i=dtClientInfo.Rows.Count-1;i>=0;i--)
            {
                if (dtClientInfo.Rows[i]["F_STATE"].ToString().Trim() == "离线")
                {
                    dtClientInfo.Rows.RemoveAt(i);

                    datalist.Invoke(new UpdateDataSource(UpdateDataSourceEvent));
                }
            }
        }
    }
}
