using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LightControl
{
    public class CommonFunction
    {
        /// <summary>
        /// 解析接收到的数据
        /// </summary>
        /// <param name="receiveData"></param>
        public static void UnpackByte(byte[] receiveData)
        {
            /*
帧头	帧长	设备类型	设备名	设备IMEI	帧内容	帧校验
2字节	2字节	5字节	5字节	15字节	n字节	2字节
0xFF00	16进制数字	ASC码	ASC码	ASC码	根据具体协议	帧内容的按字节和校验
           */
            receiveData = new byte[] {
            255,0,0,53,77,50,54,0,0,78,65,
            77,69,0,56,54,50,49,53,49,48,
            51,51,56,56,50,54,49,50,0,48,
            48,48,48,48,48,48,48,48,48,48,
            46,50,56,52,48,48,46,48,50,48,4,1
            };

            byte[] msgType = new byte[2];
            msgType[0] = receiveData[0];
            msgType[1] = receiveData[1];
            //帧头
            string frame_head = SendConvertString(msgType[0].ToString(), 10, 16).PadLeft(2, '0') + SendConvertString(msgType[1].ToString(), 10, 16).PadLeft(2, '0');
            //帧长
            msgType[0] = receiveData[2];
            msgType[1] = receiveData[3];
            string frame_length = SendConvertString(msgType[0].ToString(), 10, 16).PadLeft(2, '0') + SendConvertString(msgType[1].ToString(), 10, 16).PadLeft(2, '0');

            byte[] sb_type = new byte[5];
            //设备类型
            sb_type[0] = receiveData[4];
            sb_type[1] = receiveData[5];
            sb_type[2] = receiveData[6];
            sb_type[3] = receiveData[7];
            sb_type[4] = receiveData[8];
            string device_type = Encoding.ASCII.GetString(sb_type);
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
        private static byte[] hexStringToByte(String hex)
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
