using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace LightControl
{
    public partial class LC_MESSAGE : Form
    {

        /// <summary>
        /// 获取主窗体的句柄
        /// </summary>
        private Socket socket; 
        /// <summary>
        /// 更新datalist的数据信息
        /// </summary>
        /// <param name="msg"></param>
        public delegate void showMsg(string msg);
        private void SetValue(string msg)
        {
            datalist.Items.Add(msg);
        }

        public LC_MESSAGE()
        {
            InitializeComponent();
        }
        public LC_MESSAGE(Socket clientSocket)
        {
            InitializeComponent();
            this.socket = clientSocket;
            lab_imei.Text = socket.RemoteEndPoint.ToString();
        }
        public void SendMsg(Socket clientSocket, string sendMsg)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sendMsg);   //将要发送的信息转化为字节数组，因为Socket发送数据时是以字节的形式发送的
            clientSocket.Send(bytes);   //发送数据
            CommonFunction.Write_CommandText("发送给客户端:" +clientSocket.RemoteEndPoint+ DateTime.Now.ToString("yyyy-MM-dd") + "\r\n" + sendMsg);
        }
        internal void MainFormTxtChaned(object sender, EventArgs e)
        {
            //取到主窗体的传来的文本
            MyEventArg arg = e as MyEventArg;
            datalist.Invoke(new showMsg(SetValue), arg.Text);
        }
        private void btn_sendMsg_Click(object sender, EventArgs e)
        {
            if (txt_send.Text.Trim() == "")
            {
                return;
            }
            SendMsg(socket, txt_send.Text.Trim());
            datalist.Invoke(new showMsg(SetValue), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"  发送给客户端:"+socket.RemoteEndPoint+" ...."+txt_send.Text.Trim());


            CommonFunction.Write_CommandText("发送给客户端:" + socket.RemoteEndPoint + " ...." + txt_send.Text.Trim());
        }
    }
}
