using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace MultiForm
{
    public class CommonFunction
    {
        /// <summary>
        /// 解析接收到的数据
        /// </summary>
        /// <param name="receiveData"></param>
        public static string UnpackByte(byte[] receiveData,DataTable dtClientInfo,string IP)
        {
            /*
帧头	帧长	设备类型	设备名	设备IMEI	帧内容	帧校验
2字节	2字节	5字节	5字节	15字节	n字节	2字节
0xFF00	16进制数字	ASC码	ASC码	ASC码	根据具体协议	帧内容的按字节和校验
           */
            //receiveData = new byte[] {
            //255,0,0,54,77,50,54,0,0,78,65,
            //77,69,0,56,54,50,49,53,49,48,
            //51,51,56,56,50,54,49,50,2,0,
            //48,48,48,48,46,48,48,48,50,50,
            //54,46,54,54,55,48,48,46,48,50,48,4,11
            //};

            byte[] msgType = new byte[2];
            msgType[0] = receiveData[0];
            msgType[1] = receiveData[1];
            //帧头
            string frame_head = SendConvertString(msgType[0].ToString(), 10, 16).PadLeft(2, '0') + SendConvertString(msgType[1].ToString(), 10, 16).PadLeft(2, '0');
            //帧长
            msgType[0] = receiveData[2];
            msgType[1] = receiveData[3];
            int frame_length = Convert.ToInt32(msgType[0]) * 100 + Convert.ToInt32(msgType[1]); //SendConvertString(msgType[0].ToString(), 10, 16).PadLeft(2, '0') + SendConvertString(msgType[1].ToString(), 10, 16).PadLeft(2, '0');

            byte[] sb_type = new byte[5];
            //设备类型
            sb_type[0] = receiveData[4];
            sb_type[1] = receiveData[5];
            sb_type[2] = receiveData[6];
            sb_type[3] = receiveData[7];
            sb_type[4] = receiveData[8];
            string device_type = Encoding.ASCII.GetString(sb_type);

            sb_type[0] = receiveData[9];
            sb_type[1] = receiveData[10];
            sb_type[2] = receiveData[11];
            sb_type[3] = receiveData[12];
            sb_type[4] = receiveData[13];
            //设备名
            string device_name = Encoding.ASCII.GetString(sb_type);

            //IEMI
            byte[] b_imei = new byte[15];
            for(int i=0;i<15;i++)
            {
                b_imei[i] = receiveData[14 + i];
            }
            string IMEI = Encoding.ASCII.GetString(b_imei);

            //命令码
            string command_code = Convert.ToString(receiveData[29]);
            //开关状态
            string turn_on = Convert.ToString(receiveData[30]);
            //功率
            byte[] b_power = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                b_power[i] = receiveData[31 + i];
            }
            string Power = Encoding.ASCII.GetString(b_power);

            //电压
            byte[] b_dy = new byte[7];
            for (int i = 0; i < 7; i++)
            {
                b_dy[i] = receiveData[39 + i];
            }
            string dy = Encoding.ASCII.GetString(b_dy);

            //电流
            byte[] b_dl = new byte[6];
            for (int i = 0; i < 6; i++)
            {
                b_dl[i] = receiveData[46 + i];
            }
            string dl = Encoding.ASCII.GetString(b_dl);

            //帧内容  帧内容：1字节命令码+1字节灯具开关状态+8字节有功功率值+7字节供电电压值+6字节有效电流值

            if (dtClientInfo.Select("F_IMEI='" + IMEI + "'").Length ==0)
            {
                DataRow dr = dtClientInfo.NewRow();
                dr["F_IP"] = IP;
                dr["F_IMEI"] = IMEI;
                dr["F_TIME"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dr["F_POWER"] = Power;
                dr["F_DY"] = dy;
                dr["F_DL"] = dl;
                dr["F_STATE"] = "已连接";
                dr["F_ON"] = int.Parse(turn_on);

                dtClientInfo.Rows.Add(dr);
            }
            else
            {
                DataRow dr = dtClientInfo.Select("F_IMEI='" + IMEI + "'")[0];
                dr["F_IP"] = IP;
                dr["F_IMEI"] = IMEI;
                dr["F_TIME"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dr["F_TIME"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dr["F_POWER"] = Power;
                dr["F_DY"] = dy;
                dr["F_DL"] = dl;
                dr["F_STATE"] = "已连接";
                dr["F_ON"] = int.Parse(turn_on);
            }
            return command_code;
        }

        /// <summary>
        /// 发送BitMap转换
        /// </summary>
        /// <param name="value">需要转换的数据</param>
        /// <param name="fromBase">原进制</param>
        /// <param name="toBase">转换后进制</param>
        /// <returns></returns>
        public static string SendConvertString(string value, int fromBase, int toBase)
        {
            try
            {
                int intValue = Convert.ToInt32(value, fromBase);
                return Convert.ToString(intValue, toBase);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }
        /// <summary>
        /// 写服务器日志
        /// </summary>
        /// <param name="CommandText"></param>
        public static void Write_CommandText(string CommandText)
        {
            string SavePath = "CommandText";
            try
            {
                if (!Directory.Exists(SavePath))
                {
                    Directory.CreateDirectory(SavePath);
                }
                StreamWriter sr1 = new StreamWriter(System.IO.Path.Combine(SavePath, DateTime.Now.ToString("yyyyMMdd") + ".Log"), true);
                sr1.WriteLine(DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString() + " " + CommandText);
                sr1.Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 16进制转10byte数组
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] hexStringToByte(String hex)
        {
            int len = (hex.Length / 2);
            byte[] result = new byte[len];
            char[] achar = hex.ToCharArray();
            for (int i = 0; i < len; i++)
            {
                int pos = i * 2;
                result[i] = (byte)(toByte(achar[pos]) << 4 | toByte(achar[pos + 1]));
            }
            return result;
        }
        private static int toByte(char c)
        {
            byte b = (byte)"0123456789ABCDEF".IndexOf(c);
            return b;
        }
    }
}
