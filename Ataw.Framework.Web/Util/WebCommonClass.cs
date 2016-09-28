using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Globalization;
using System.Security;
using System.Security.Cryptography;
using System.IO;
using System.Threading;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net.Mail;
using Microsoft.Win32;
using System.Collections;
using System.Web.UI;
using System.Web.SessionState;
using System.Linq;

namespace Ataw.Framework.Web
{
    /// <summary>
    /// 通用类
    /// </summary>
    public class WebCommonClass
    {
        #region "返回字符串在字符串中出现的次数"
        /// <summary>
        /// 返回字符串在字符串中出现的次数
        /// </summary>
        /// <param name="Char">要检测出现的字符</param>
        /// <param name="String">要检测的字符串</param>
        /// <returns>出现次数</returns>
        public static int GetCharInStringCount(string Char, string String)
        {
            string str = String.Replace(Char, "");
            return (String.Length - str.Length) / Char.Length;

        }
        #endregion

        #region "获得物理路径"
        /// <summary>
        /// 获得物理路径
        /// </summary>
        /// <param name="a">路径</param>
        /// <returns>物理路径</returns>
        public static string GetFullPath(string a)
        {
            string AppDir = System.AppDomain.CurrentDomain.BaseDirectory;
            if (a.IndexOf(":") < 0)
            {
                string str = a.Replace("..\\", "");
                if (str != a)
                {
                    int Num = (a.Length - str.Length) / ("..\\").Length + 1;
                    for (int i = 0; i < Num; i++)
                    {
                        AppDir = AppDir.Substring(0, AppDir.LastIndexOf("\\"));
                    }
                    str = "\\" + str;

                }
                a = AppDir + str;
            }
            return a;
        }
        #endregion

        #region "获得日志文件存放目录"
        /// <summary>
        /// 获得日志文件存放目录
        /// </summary>
        public static string LogDir
        {
            get
            {
                string LogFilePath = GetFullPath(ConfigurationManager.AppSettings["LogDir"]);
                if (!Directory.Exists(LogFilePath))
                    Directory.CreateDirectory(LogFilePath);
                return LogFilePath;
            }
        }
        #endregion


        #region "js信息提示框"
        /// <summary>
        /// js信息提示框
        /// </summary>
        /// <param name="Message">提示信息文字</param>
        /// <param name="ReturnUrl">返回地址</param>
        /// <param name="rq"></param>
        public static void MessBox(Page page, string Message, string ReturnUrl)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script>setTimeout(\"ShowMessageBoxJump('" + Message + "','" + ReturnUrl + "')\",1000);</script>");
        }

        /// <summary>
        /// js信息提示框
        /// </summary>
        /// <param name="Message">提示信息文字</param>
        /// <param name="rq"></param>
        public static void MessBoxColse(Page page, string Message)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>setTimeout(\"ShowMessageBoxClose('" + Message + "')\",1000);</script>");
        }


        /// <summary>
        /// 带跳转的弹出框
        /// </summary>
        /// <param name="page"></param>
        /// <param name="message"></param>
        /// <param name="strUrl"></param>
        public static void ShowConfirmMessageBox(Page page, string message, string strUrl)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "" + message + "", "<script type=\"text/javascript\">setTimeout(\"ShowConfirmMessageBox('" + message + "','" + strUrl + "')\",1000);</script>");

        }

        /// <summary>
        /// 弹出成功的提示框
        /// </summary>
        /// <param name="page"></param>
        /// <param name="message"></param>
        public static void ShowSuccessMessageBox(Page page, string message)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "" + message + "", "<script type=\"text/javascript\" >setTimeout(\"ShowMessageBox('" + message + "')\",1000);</script>");
        }


        /// <summary>
        /// 弹出失败的提示框
        /// </summary>
        /// <param name="page"></param>
        /// <param name="message"></param>
        public static void ShowErrorMessageBox(Page page, string message)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "" + message + "", "<script type=\"text/javascript\" >setTimeout(\"ShowMessageBox('" + message + "')\",1000);</script>");
        }


        /// <summary>
        /// js信息提示框
        /// </summary>
        /// <param name="Message">提示信息文字</param>
        /// <param name="ReturnUrl">返回地址</param>
        /// <param name="rq"></param>
        public static void MessBox(string Message, string ReturnUrl, HttpContext rq)
        {
            System.Text.StringBuilder msgScript = new System.Text.StringBuilder();
            msgScript.Append("<script language=JavaScript>\n");
            msgScript.Append("alert(\"" + Message + "\");\n");
            msgScript.Append("parent.location.href='" + ReturnUrl + "';\n");
            msgScript.Append("</script>\n");
            rq.Response.Write(msgScript.ToString());
            rq.Response.End();
        }
        /// <summary>
        /// js信息提示框
        /// </summary>
        /// <param name="Message">提示信息文字</param>
        /// <param name="ReturnUrl">返回地址</param>
        /// <param name="rq"></param>
        public static void MessBox(string Message, string ReturnUrl, HttpContext rq, bool IsParent)
        {
            System.Text.StringBuilder msgScript = new System.Text.StringBuilder();
            msgScript.Append("<script language=JavaScript>\n");
            msgScript.Append("alert(\"" + Message + "\");\n");
            if (!IsParent)
                msgScript.Append("location.href='" + ReturnUrl + "';\n");
            else
                msgScript.Append("parent.location.href='" + ReturnUrl + "';\n");
            msgScript.Append("</script>\n");
            rq.Response.Write(msgScript.ToString());
            rq.Response.End();
        }
        /// <summary>
        /// 弹出Alert信息窗
        /// </summary>
        /// <param name="Message">信息内容</param>
        public static void MessBox(Page page, string Message)
        {

            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowMessageBox('" + Message + "','提示框');</script>");
        }
        public static void MessBoxWeb(Page page, string Message)
        {

            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowMessageBox('" + Message + "');</script>");

            //System.Text.StringBuilder msgScript = new System.Text.StringBuilder();
            //msgScript.Append("<script language=JavaScript>\n");
            //msgScript.Append("alert(\"" + Message + "\");\n");
            //msgScript.Append("</script>\n");
            //HttpContext.Current.Response.Write(msgScript.ToString());
        }
        public static void MessBoxWeb(Page page, string Message, string ReturnUrl)
        {

            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowMessageBoxJump('" + Message + "','" + ReturnUrl + "');</script>");

            //System.Text.StringBuilder msgScript = new System.Text.StringBuilder();
            //msgScript.Append("<script language=JavaScript>\n");
            //msgScript.Append("alert(\"" + Message + "\");\n");
            //msgScript.Append("</script>\n");
            //HttpContext.Current.Response.Write(msgScript.ToString());
        }
        #endregion

        #region 格式化字符串,符合SQL语句
        /// <summary>
        /// 格式化字符串,符合SQL语句
        /// </summary>
        /// <param name="formatStr">需要格式化的字符串</param>
        /// <returns>字符串</returns>
        public static string inSQL(string formatStr)
        {
            string rStr = formatStr;
            if (formatStr != null && formatStr != string.Empty)
            {
                rStr = rStr.Replace("'", "''");
                //rStr = rStr.Replace("\"", "\"\"");
            }
            return rStr;
        }
        /// <summary>
        /// 格式化字符串,是inSQL的反向
        /// </summary>
        /// <param name="formatStr"></param>
        /// <returns></returns>
        public static string outSQL(string formatStr)
        {
            string rStr = formatStr;
            if (rStr != null)
            {
                rStr = rStr.Replace("''", "'");
                rStr = rStr.Replace("\"\"", "\"");
            }
            return rStr;
        }

        /// <summary>
        /// 查询SQL语句,删除一些SQL注入问题
        /// </summary>
        /// <param name="formatStr">需要格式化的字符串</param>
        /// <returns></returns>
        public static string querySQL(string formatStr)
        {
            string rStr = formatStr;
            if (rStr != null && rStr != "")
            {
                rStr = rStr.Replace("'", "");
            }
            return rStr;
        }
        #endregion

        #region 截取字符串
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str_value"></param>
        /// <param name="str_len"></param>
        /// <returns></returns>
        public static string leftx(string str_value, int str_len)
        {
            int p_num = 0;
            int i;
            string New_Str_value = "";

            if (str_value == "")
            {
                New_Str_value = "";
            }
            else
            {
                int Len_Num = str_value.Length;
                for (i = 0; i <= Len_Num - 1; i++)
                {
                    if (i > Len_Num) break;
                    char c = Convert.ToChar(str_value.Substring(i, 1));
                    if (((int)c > 255) || ((int)c < 0))
                        p_num = p_num + 2;
                    else
                        p_num = p_num + 1;



                    if (p_num >= str_len)
                    {

                        New_Str_value = str_value.Substring(0, i + 1);
                        break;
                    }
                    else
                    {
                        New_Str_value = str_value;
                    }

                }

            }
            return New_Str_value;
        }
        #endregion

        #region 检测用户提交页面
        /// <summary>
        /// 检测用户提交页面
        /// </summary>
        /// <param name="rq"></param>
        public static void Check_Post_Url(HttpContext rq)
        {
            string WebHost = "";
            if (rq.Request.ServerVariables["SERVER_NAME"] != null)
            {
                WebHost = rq.Request.ServerVariables["SERVER_NAME"].ToString();
            }

            string From_Url = "";
            if (rq.Request.UrlReferrer != null)
            {
                From_Url = rq.Request.UrlReferrer.ToString();
            }

            if (From_Url == "" || WebHost == "")
            {
                rq.Response.Write("禁止外部提交数据!");
                rq.Response.End();
            }
            else
            {
                WebHost = "HTTP://" + WebHost.ToUpper();
                From_Url = From_Url.ToUpper();
                int a = From_Url.IndexOf(WebHost);
                if (From_Url.IndexOf(WebHost) < 0)
                {
                    rq.Response.Write("禁止外部提交数据!");
                    rq.Response.End();
                }
            }

        }
        #endregion

        #region 日期处理
        /// <summary>
        /// 格式化日期为2006-12-22
        /// </summary>
        /// <param name="dTime"></param>
        /// <returns></returns>
        public static string formatDate(DateTime dTime)
        {
            string rStr;
            rStr = dTime.Year + "-" + dTime.Month + "-" + dTime.Day;
            return rStr;
        }

        /// <summary>
        /// 获取日期
        /// </summary>
        /// <param name="sDate"></param>
        /// <returns></returns>
        public static string getWeek(DateTime sDate)
        {
            Calendar myCal = CultureInfo.InvariantCulture.Calendar;


            string rStr = "";
            switch (myCal.GetDayOfWeek(sDate).ToString())
            {
                case "Sunday":
                    rStr = "星期日";
                    break;
                case "Monday":
                    rStr = "星期一";
                    break;
                case "Tuesday":
                    rStr = "星期二";
                    break;
                case "Wednesday":
                    rStr = "星期三";
                    break;
                case "Thursday":
                    rStr = "星期四";
                    break;
                case "Friday":
                    rStr = "星期五";
                    break;
                case "Saturday":
                    rStr = "星期六";
                    break;
            }
            return rStr;
        }
        #endregion

        #region 随机颜色数据

        /// <summary>
        /// 随机颜色数据
        /// </summary>
        /// <returns></returns>
        public static string getStrColor()
        {
            int length = 6;
            byte[] random = new Byte[length / 2];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(random);

            StringBuilder sb = new StringBuilder(length);
            int i;
            for (i = 0; i < random.Length; i++)
            {
                sb.Append(String.Format("{0:X2}", random[i]));
            }
            return sb.ToString();
        }
        #endregion

        #region "隐藏IP地址最后一位用*号代替"
        /// <summary>
        /// 隐藏IP地址最后一位用*号代替
        /// </summary>
        /// <param name="Ipaddress">IP地址:192.168.34.23</param>
        /// <returns></returns>
        public static string HidenLastIp(string Ipaddress)
        {
            return Ipaddress.Substring(0, Ipaddress.LastIndexOf(".")) + ".*";
        }
        #endregion

        #region "防刷新检测"
        /// <summary>
        /// 防刷新检测
        /// </summary>
        /// <param name="Second">访问间隔秒</param>
        /// <param name="UserSession"></param>
        public static bool CheckRefurbish(int Second, HttpSessionState UserSession)
        {

            bool i = true;
            if (UserSession["RefTime"] != null)
            {
                DateTime d1 = Convert.ToDateTime(UserSession["RefTime"]);
                DateTime d2 = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
                TimeSpan d3 = d2.Subtract(d1);
                if (d3.Seconds < Second)
                {
                    i = false;
                }
                else
                {
                    UserSession["RefTime"] = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
                }
            }
            else
            {
                UserSession["RefTime"] = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            }

            return i;
        }
        #endregion

        #region "判断是否是Decimal类型"
        /// <summary>
        /// 判断是否是Decimal类型
        /// </summary>
        /// <param name="TBstr0">判断数据字符</param>
        /// <returns>true是false否</returns>
        public static bool IsDecimal(string TBstr0)
        {
            bool IsBool = false;
            string Intstr0 = "1234567890";
            string IntSign0, StrInt, StrDecimal;
            int IntIndex0, IntSubstr, IndexInt;
            int decimalbool = 0;
            int db = 0;
            bool Bf, Bl;
            if (TBstr0.Length > 2)
            {
                IntIndex0 = TBstr0.IndexOf(".");
                if (IntIndex0 != -1)
                {
                    string StrArr = ".";
                    char[] CharArr = StrArr.ToCharArray();
                    string[] NumArr = TBstr0.Split(CharArr);
                    IndexInt = NumArr.GetUpperBound(0);
                    if (IndexInt > 1)
                    {
                        decimalbool = 1;
                    }
                    else
                    {
                        StrInt = NumArr[0].ToString();
                        StrDecimal = NumArr[1].ToString();
                        //--- 整数部分－－－－－
                        if (StrInt.Length > 0)
                        {
                            if (StrInt.Length == 1)
                            {
                                IntSubstr = Intstr0.IndexOf(StrInt);
                                if (IntSubstr != -1)
                                {
                                    Bf = true;
                                }
                                else
                                {
                                    Bf = false;
                                }
                            }
                            else
                            {
                                for (int i = 0; i <= StrInt.Length - 1; i++)
                                {
                                    IntSign0 = StrInt.Substring(i, 1).ToString();
                                    IntSubstr = Intstr0.IndexOf(IntSign0);
                                    if (IntSubstr != -1)
                                    {
                                        db = db + 0;
                                    }
                                    else
                                    {
                                        db = i + 1;
                                        break;
                                    }
                                }

                                if (db == 0)
                                {
                                    Bf = true;
                                }
                                else
                                {
                                    Bf = false;
                                }
                            }
                        }
                        else
                        {
                            Bf = true;
                        }
                        //----小数部分－－－－
                        if (StrDecimal.Length > 0)
                        {
                            for (int j = 0; j <= StrDecimal.Length - 1; j++)
                            {
                                IntSign0 = StrDecimal.Substring(j, 1).ToString();
                                IntSubstr = Intstr0.IndexOf(IntSign0);
                                if (IntSubstr != -1)
                                {
                                    db = db + 0;
                                }
                                else
                                {
                                    db = j + 1;
                                    break;
                                }
                            }
                            if (db == 0)
                            {
                                Bl = true;
                            }
                            else
                            {
                                Bl = false;
                            }
                        }
                        else
                        {
                            Bl = false;
                        }
                        if ((Bf && Bl) == true)
                        {
                            decimalbool = 0;
                        }
                        else
                        {
                            decimalbool = 1;
                        }

                    }

                }
                else
                {
                    decimalbool = 1;
                }

            }
            else
            {
                decimalbool = 1;
            }

            if (decimalbool == 0)
            {
                IsBool = true;
            }
            else
            {
                IsBool = false;
            }

            return IsBool;
        }
        #endregion

        #region "获取随机数"
        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <param name="length">随机数长度</param>
        /// <returns></returns>
        public static string GetRandomPassword(int length)
        {
            byte[] random = new Byte[length / 2];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(random);

            StringBuilder sb = new StringBuilder(length);
            int i;
            for (i = 0; i < random.Length; i++)
            {
                sb.Append(String.Format("{0:X2}", random[i]));
            }
            return sb.ToString();
        }
        #endregion

        #region "获取用户IP地址"
        /// <summary>
        /// 获取用户IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress()
        {

            string user_IP = string.Empty;
            //if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            //{
            //    if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            //    {
            //        user_IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            //    }
            //    else
            //    {
            //        user_IP = System.Web.HttpContext.Current.Request.UserHostAddress;
            //    }
            //}
            //else
            //{
            //    user_IP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            //}
            user_IP = System.Web.HttpContext.Current.Request.UserHostAddress;
            return user_IP;
        }
        #endregion

        #region 字符串截取补字符函数
        /// <summary>
        /// 字符串截取补字符函数
        /// </summary>
        /// <param name="s">要处理的字符串</param>
        /// <param name="len">长度</param>
        /// <param name="b">补充的字符</param>
        /// <returns>处理后字符</returns>
        public static string splitStringLen(string s, int len, char b)
        {
            if (string.IsNullOrEmpty(s))
                return "";
            if (s.Length >= len)
                return s.Substring(0, len);
            return s.PadRight(len, b);
        }
        #endregion

        #region "3des加密字符串"
        /// <summary>
        /// 3des加密函数(ECB加密模式,PaddingMode.PKCS7,无IV)
        /// </summary>
        /// <param name="encryptValue">加密字符</param>
        /// <param name="key">加密key(24字符)</param>
        /// <returns>加密后Base64字符</returns>
        public static string EncryptString(string encryptValue, string key)
        {
            string enstring = "加密出错!";
            ICryptoTransform ct; //需要此接口才能在任何服务提供程序上调用 CreateEncryptor 方法，服务提供程序将返回定义该接口的实际 encryptor 对象。
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            SymmetricAlgorithm des3 = SymmetricAlgorithm.Create("TripleDES");
            des3.Mode = CipherMode.ECB;
            des3.Key = Encoding.UTF8.GetBytes(splitStringLen(key, 24, '0'));
            //des3.KeySize = 192;
            des3.Padding = PaddingMode.PKCS7;

            ct = des3.CreateEncryptor();

            byt = Encoding.UTF8.GetBytes(encryptValue);//将原始字符串转换成字节数组。大多数 .NET 加密算法处理的是字节数组而不是字符串。

            //创建 CryptoStream 对象 cs 后，现在使用 CryptoStream 对象的 Write 方法将数据写入到内存数据流。这就是进行实际加密的方法，加密每个数据块时，数据将被写入 MemoryStream 对象。

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            try
            {
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                enstring = Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                enstring = ex.ToString();
            }
            finally
            {
                cs.Close();
                cs.Dispose();
                ms.Close();
                ms.Dispose();
                des3.Clear();
                ct.Dispose();
            }
            enstring = Convert.ToBase64String(ms.ToArray());
            return enstring;
        }
        #endregion

        #region "3des解密字符串"
        /// <summary>
        /// 3des解密函数(ECB加密模式,PaddingMode.PKCS7,无IV)
        /// </summary>
        /// <param name="decryptString">解密字符</param>
        /// <param name="key">解密key(24字符)</param>
        /// <returns>解密后字符</returns>
        public static string DecryptString(string decryptString, string key)
        {
            string destring = "解密字符失败!";
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            SymmetricAlgorithm des3 = SymmetricAlgorithm.Create("TripleDES");
            des3.Mode = CipherMode.ECB;
            des3.Key = Encoding.UTF8.GetBytes(splitStringLen(key, 24, '0'));
            //des3.KeySize = 192;
            des3.Padding = PaddingMode.PKCS7;

            ct = des3.CreateDecryptor();

            byt = Convert.FromBase64String(decryptString);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            try
            {
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                destring = Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                destring = ex.ToString();
            }
            finally
            {
                ms.Close();
                cs.Close();
                ms.Dispose();
                cs.Dispose();
                ct.Dispose();
                des3.Clear();
            }
            return destring;
        }
        #endregion

        #region "MD5加密"
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <param name="code">加密位数16/32</param>
        /// <returns></returns>
        public static string md5(string str, int code)
        {
            string strEncrypt = string.Empty;
            if (code == 16)
            {
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").Substring(8, 16);
            }

            if (code == 32)
            {
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            }

            return strEncrypt;
        }
        #endregion

        #region 脚本提示信息,并且跳转到最上层框架
        /// <summary>
        /// 脚本提示信息
        /// </summary>
        /// <param name="Msg">信息内容,可以为空,为空表示不出现提示窗口</param>
        /// <param name="Url">跳转地址</param>
        public static string Hint(string Msg, string Url)
        {
            System.Text.StringBuilder rStr = new System.Text.StringBuilder();

            rStr.Append("<script language='javascript'>");
            if (Msg != "")
                rStr.Append("	alert('" + Msg + "');");

            if (Url != "")
                rStr.Append("	window.top.location.href = '" + Url + "';");

            rStr.Append("</script>");

            return rStr.ToString();
        }
        #endregion

        #region 脚本提示信息,并且跳转到当前框架内
        /// <summary>
        /// 脚本提示信息
        /// </summary>
        /// <param name="Msg">信息内容,可以为空,为空表示不出现提示窗口</param>
        /// <param name="Url">跳转地址,自已可以写入脚本</param>
        /// <returns></returns>
        public static string LocalHintJs(string Msg, string Url)
        {
            System.Text.StringBuilder rStr = new System.Text.StringBuilder();

            rStr.Append("<script language='JavaScript'>\n");
            if (Msg != "")
                rStr.AppendFormat("	alert('{0}');\n", Msg);

            if (Url != "")
                rStr.Append(Url + "\n");
            rStr.Append("</script>");

            return rStr.ToString();
        }

        #endregion

        #region 脚本提示信息,并且跳转到当前框架内,地址为空时,返回上页
        /// <summary>
        /// 脚本提示信息
        /// </summary>
        /// <param name="Msg"></param>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static string LocalHint(string Msg, string Url)
        {
            System.Text.StringBuilder rStr = new System.Text.StringBuilder();

            rStr.Append("<script language='JavaScript'>\n");
            if (Msg != "")
                rStr.AppendFormat("	alert('{0}');\n", Msg);

            if (Url != "")
                rStr.AppendFormat("	window.location.href = '" + Url + "';\n");
            else
                rStr.AppendFormat(" window.history.back();");

            rStr.Append("</script>\n");

            return rStr.ToString();
        }
        #endregion

        #region "按当前日期和时间生成随机数"
        /// <summary>
        /// 按当前日期和时间生成随机数
        /// </summary>
        /// <param name="Num">附加随机数长度</param>
        /// <returns></returns>
        public static string sRndNum(int Num)
        {
            string sTmp_Str = System.DateTime.Today.Year.ToString() + System.DateTime.Today.Month.ToString("00") + System.DateTime.Today.Day.ToString("00") + System.DateTime.Now.Hour.ToString("00") + System.DateTime.Now.Minute.ToString("00") + System.DateTime.Now.Second.ToString("00");
            return sTmp_Str + RndNum(Num);
        }
        #endregion

        #region 生成0-9随机数
        /// <summary>
        /// 生成0-9随机数
        /// </summary>
        /// <param name="VcodeNum">生成长度</param>
        /// <returns></returns>
        public static string RndNum(int VcodeNum)
        {
            StringBuilder sb = new StringBuilder(VcodeNum);
            Random rand = new Random();
            for (int i = 1; i < VcodeNum + 1; i++)
            {
                int t = rand.Next(9);
                sb.AppendFormat("{0}", t);
            }
            return sb.ToString();

        }
        #endregion

        #region "通过RNGCryptoServiceProvider 生成随机数 0-9"
        /// <summary>
        /// 通过RNGCryptoServiceProvider 生成随机数 0-9 
        /// </summary>
        /// <param name="length">随机数长度</param>
        /// <returns></returns>
        public static string RndNumRNG(int length)
        {
            byte[] bytes = new byte[16];
            RNGCryptoServiceProvider r = new RNGCryptoServiceProvider();
            StringBuilder sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                r.GetBytes(bytes);
                sb.AppendFormat("{0}", (int)((decimal)bytes[0] / 256 * 10));
            }
            return sb.ToString();

        }
        #endregion

        #region "在当前路径上创建日期格式目录(20060205)"
        /// <summary>
        /// 在当前路径上创建日期格式目录(20060205)
        /// </summary>
        /// <param name="sPath">返回目录名</param>
        /// <returns></returns>
        public static string CreateDir(string sPath)
        {
            string sTemp = System.DateTime.Today.Year.ToString() + System.DateTime.Today.Month.ToString("00") + System.DateTime.Today.Day.ToString("00");
            sPath += sTemp;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(@sPath); //构造函数创建目录
            if (di.Exists == false)
            {
                di.Create();
            }

            return sTemp;
        }
        #endregion

        #region "检测是否为有效邮件地址格式"
        /// <summary>
        /// 检测是否为有效邮件地址格式
        /// </summary>
        /// <param name="strIn">输入邮件地址</param>
        /// <returns></returns>
        public static bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        #endregion

        #region "邮件发送"
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="strto">接收邮件地址</param>
        /// <param name="strSubject">主题</param>
        /// <param name="strBody">内容</param>
        public static void SendSMTPEMail(string strto, string strSubject, string strBody)
        {
            //string SMTPHost = ConfigurationManager.AppSettings["SMTPHost"];
            //string SMTPPort = ConfigurationManager.AppSettings["SMTPPort"];
            //string SMTPUser = ConfigurationManager.AppSettings["SMTPUser"];
            //string SMTPPassword = ConfigurationManager.AppSettings["SMTPPassword"];
            //string MailFrom = ConfigurationManager.AppSettings["MailFrom"];
            //string MailSubject = ConfigurationManager.AppSettings["MailSubject"];
            string SMTPHost = GlobalVariable.AppContext.ApplicationXml.AppSettings.FirstOrDefault(m => m.Key == "SMTPHost").Value;
            string SMTPPort = GlobalVariable.AppContext.ApplicationXml.AppSettings.FirstOrDefault(m => m.Key == "SMTPPort").Value;
            string SMTPUser = GlobalVariable.AppContext.ApplicationXml.AppSettings.FirstOrDefault(m => m.Key == "SMTPUser").Value;
            string SMTPPassword = GlobalVariable.AppContext.ApplicationXml.AppSettings.FirstOrDefault(m => m.Key == "SMTPPassword").Value;
            //string MailFrom = GlobalVariable.AppContext.ApplicationXml.AppSettings.FirstOrDefault(m => m.Key == "MailFrom").Value;
            //string MailSubject = GlobalVariable.AppContext.ApplicationXml.AppSettings.FirstOrDefault(m => m.Key == "MailSubject").Value;

            SmtpClient client = new SmtpClient(SMTPHost);
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(SMTPUser, SMTPPassword);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage message = new MailMessage(SMTPUser, strto, strSubject, strBody);
            message.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");
            message.IsBodyHtml = true;

            client.Send(message);
        }
        #endregion

        #region "转换编码"
        /// <summary>
        /// 转换编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encode(string str)
        {
            if (str == null)
            {
                return "";
            }
            else
            {

                return System.Web.HttpUtility.UrlEncode(Encoding.GetEncoding(54936).GetBytes(str));
            }
        }
        #endregion

        #region "当前显示应用模组"
        /// <summary>
        /// 显示应用模组
        /// </summary>
        public static int ApplicationID
        {
            get
            {
                try
                {
                    return Convert.ToInt32(ConfigurationManager.AppSettings["ApplicationID"]);
                }
                catch
                {
                    return 0;
                }
            }
        }
        #endregion

        #region "获取登陆用户UserID"
        /// <summary>
        /// 获取登陆用户UserID,如果未登陆为0
        /// </summary>
        public static int Get_UserID
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated ? Convert.ToInt32(HttpContext.Current.User.Identity.Name) : 0;
            }

        }
        #endregion

        #region "获取当前Cookies名称"
        /// <summary>
        /// "获取当前Cookies名称
        /// </summary>
        public static string Get_CookiesName
        {
            get
            {
                return "FrameWork_YOYO_Lzppcc";
            }
        }
        #endregion

        #region "获取WEBCache名称前辍"
        /// <summary>
        /// 获取WEBCache名称前辍
        /// </summary>
        public static string Get_WebCacheName
        {
            get
            {
                return "FrameWork_YOYO_Lzppcc";
            }
        }
        #endregion

        #region "设置页面不被缓存"
        /// <summary>
        /// 设置页面不被缓存
        /// </summary>
        public static void SetPageNoCache()
        {

            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.AddHeader("Pragma", "No-Cache");
        }
        #endregion

        #region "获取页面url"
        /// <summary>
        /// 获取当前访问页面地址
        /// </summary>
        public static string GetScriptName
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString();
            }
        }

        /// <summary>
        /// 检测当前url是否包含指定的字符
        /// </summary>
        /// <param name="sChar">要检测的字符</param>
        /// <returns></returns>
        public static bool CheckScriptNameChar(string sChar)
        {
            bool rBool = false;
            if (GetScriptName.ToLower().LastIndexOf(sChar) >= 0)
                rBool = true;
            return rBool;
        }

        /// <summary>
        /// 获取当前页面的扩展名
        /// </summary>
        public static string GetScriptNameExt
        {
            get
            {
                return GetScriptName.Substring(GetScriptName.LastIndexOf(".") + 1);
            }
        }

        /// <summary>
        /// 获取当前访问页面地址参数
        /// </summary>
        public static string GetScriptNameQueryString
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["QUERY_STRING"].ToString();
            }
        }

        /// <summary>
        /// 获得页面文件名和参数名
        /// </summary>
        public static string GetScriptNameUrl
        {
            get
            {
                string Script_Name = WebCommonClass.GetScriptName;
                Script_Name = Script_Name.Substring(Script_Name.LastIndexOf("/") + 1);
                Script_Name += "?" + GetScriptNameQueryString;
                return Script_Name;
            }
        }

        /// <summary>
        /// 获取当前访问页面Url
        /// </summary>
        public static string GetScriptUrl
        {
            get
            {
                return WebCommonClass.GetScriptNameQueryString == "" ? WebCommonClass.GetScriptName : string.Format("{0}?{1}", WebCommonClass.GetScriptName, WebCommonClass.GetScriptNameQueryString);
            }
        }

        /// <summary>
        /// 返回当前页面目录的url
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <returns></returns>
        public static string GetHomeBaseUrl(string FileName)
        {
            string Script_Name = WebCommonClass.GetScriptName;
            return string.Format("{0}/{1}", Script_Name.Remove(Script_Name.LastIndexOf("/")), FileName);
        }

        /// <summary>
        /// 返回当前网站网址
        /// </summary>
        /// <returns></returns>
        public static string GetHomeUrl()
        {
            return HttpContext.Current.Request.Url.Authority;
        }

        /// <summary>
        /// 获取当前访问文件物理目录
        /// </summary>
        /// <returns>路径</returns>
        public static string GetScriptPath
        {
            get
            {
                string Paths = HttpContext.Current.Request.ServerVariables["PATH_TRANSLATED"].ToString();
                return Paths.Remove(Paths.LastIndexOf("\\"));
            }
        }
        #endregion

        #region "按字符串位数补0"
        /// <summary>
        /// 按字符串位数补0
        /// </summary>
        /// <param name="CharTxt">字符串</param>
        /// <param name="CharLen">字符长度</param>
        /// <returns></returns>
        public static string FillZero(string CharTxt, int CharLen)
        {
            if (CharTxt.Length < CharLen)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < CharLen - CharTxt.Length; i++)
                {
                    sb.Append("0");
                }
                sb.Append(CharTxt);
                return sb.ToString();
            }
            else
            {
                return CharTxt;
            }
        }

        #endregion

        #region "替换JS中特殊字符"
        /// <summary>
        /// 将JS中的特殊字符替换
        /// </summary>
        /// <param name="str">要替换字符</param>
        /// <returns></returns>
        public static string ReplaceJs(string str)
        {

            if (str != null)
            {
                str = str.Replace("\"", "&quot;");
                str = str.Replace("(", "&#40;");
                str = str.Replace(")", "&#41;");
                str = str.Replace("%", "&#37;");
            }

            return str;

        }
        #endregion

        #region "正式表达式验证"
        /// <summary>
        /// 正式表达式验证
        /// </summary>
        /// <param name="C_Value">验证字符</param>
        /// <param name="C_Str">正式表达式</param>
        /// <returns>符合true不符合false</returns>
        public static bool CheckRegEx(string C_Value, string C_Str)
        {
            Regex objAlphaPatt;
            objAlphaPatt = new Regex(C_Str, RegexOptions.Compiled);


            return objAlphaPatt.Match(C_Value).Success;
        }
        #endregion

        #region "检测当前字符是否在以,号分开的字符串中(xx,sss,xaf,fdsf)"
        /// <summary>
        /// 检测当前字符是否在以,号分开的字符串中(xx,sss,xaf,fdsf)
        /// </summary>
        /// <param name="TempChar">需检测字符</param>
        /// <param name="TempStr">待检测字符串</param>
        /// <returns>存在true,不存在false</returns>
        public static bool Check_Char_Is(string TempChar, string TempStr)
        {
            bool rBool = false;
            if (TempChar != null && TempStr != null)
            {
                string[] TempStrArray = TempStr.Split(',');
                for (int i = 0; i < TempStrArray.Length; i++)
                {
                    if (TempChar == TempStrArray[i].Trim())
                    {
                        rBool = true;
                        break;
                    }
                }
            }
            return rBool;
        }
        #endregion



        #region "前台设置"

        /// <summary>
        /// 菜单风格 0:经典 1:流行 2:朴素
        /// </summary>
        public static int MenuStyle
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["MenuStyle"] == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(HttpContext.Current.Request.Cookies["MenuStyle"].Value);
                }
            }
            set
            {
                HttpContext.Current.Response.Cookies["MenuStyle"].Value = value.ToString();
            }
        }

        /// <summary>
        /// 分页每页记录数(默认10)
        /// </summary>
        public static int PageSize
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["PageSize"] == null)
                {
                    return 10;
                }
                else
                {
                    return Convert.ToInt32(HttpContext.Current.Request.Cookies["PageSize"].Value);
                }
            }
            set
            {
                HttpContext.Current.Response.Cookies["PageSize"].Value = value.ToString();
            }
        }

        /// <summary>
        /// 表格样式(默认default)
        /// </summary>
        public static string TableSink
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["TableSink"] == null)
                {
                    return "default";
                }
                else
                {
                    return HttpContext.Current.Request.Cookies["TableSink"].Value.ToString();
                }
            }
            set
            {
                HttpContext.Current.Response.Cookies["TableSink"].Value = value;
            }
        }

        /// <summary>
        /// 用户在线过期时间 (分)默认30分 如果用户在当前设定的时间内没有任何操作,将会被系统自动退出
        /// </summary>
        public static int OnlineMinute
        {
            get
            {
                try
                {
                    int _onlineminute = Convert.ToInt32(ConfigurationManager.AppSettings["OnlineMinute"]);
                    if (_onlineminute == 0)
                        return 10000;
                    else
                        return _onlineminute;
                }
                catch
                {
                    return 30;
                }
            }
        }

        #endregion

        #region "数据库设置"

        /// <summary>
        /// 获取数据库类型
        /// </summary>
        public static string GetDBType
        {
            get
            {
                return ConfigurationManager.AppSettings["DBType"];
            }
        }

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        public static string GetConnString
        {
            get
            {
                return ConfigurationManager.AppSettings[GetDBType];
            }
        }
        #endregion

        #region "产生GUID"
        /// <summary>
        /// 获取一个GUID字符串
        /// </summary>
        public static string GetGUID
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }
        #endregion

        #region "生成刷新部门列表js"
        /// <summary>
        /// 生成刷新部门列表js
        /// </summary>
        public static string BuildJs
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<script language=javascript>");
                sb.Append("window.parent.leftbody.location.reload();");
                sb.Append("</script>");

                return sb.ToString();
            }
        }
        #endregion

        #region "获取服务器IP"
        /// <summary>
        /// 获取服务器IP
        /// </summary>
        public static string GetServerIp
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"].ToString();
            }
        }
        #endregion

        #region "获取服务器操作系统"
        /// <summary>
        /// 获取服务器操作系统
        /// </summary>
        public static string GetServerOS
        {
            get
            {
                return Environment.OSVersion.VersionString;
            }
        }
        #endregion

        #region "获取服务器域名"
        /// <summary>
        /// 获取服务器域名
        /// </summary>
        public static string GetServerHost
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
            }
        }
        #endregion

        #region "显示出错详细信息在用户页面(用户开发调试,在生产环境请设置为false)"
        /// <summary>
        /// 显示出错详细信息在用户页面(用户开发调试,在生产环境请设置为false)
        /// </summary>
        public static bool DispError
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["DispError"]);
            }
        }
        #endregion



        #region "根据文件扩展名获取当前目录下的文件列表"
        /// <summary>
        /// 根据文件扩展名获取当前目录下的文件列表
        /// </summary>
        /// <param name="FileExt">文件扩展名</param>
        /// <returns>返回文件列表</returns>
        public static List<string> GetDirFileList(string FileExt)
        {
            List<string> FilesList = new List<string>();
            string[] Files = Directory.GetFiles(GetScriptPath, string.Format("*.{0}", FileExt));
            foreach (string var in Files)
            {
                FilesList.Add(System.IO.Path.GetFileName(var).ToLower());
            }
            return FilesList;
        }
        #endregion



        #region "根据文件扩展名获得文件的content-type"
        /// <summary>
        /// 根据文件扩展名获得文件的content-type
        /// </summary>
        /// <param name="fileextension">文件扩展名如.gif</param>
        /// <returns>文件对应的content-type 如:application/gif</returns>
        public static string GetFileMIME(string fileextension)
        {
            //set the default content-type
            const string DEFAULT_CONTENT_TYPE = "application/unknown";

            RegistryKey regkey, fileextkey;
            string filecontenttype;

            //the file extension to lookup


            try
            {
                //look in HKCR
                regkey = Registry.ClassesRoot;

                //look for extension
                fileextkey = regkey.OpenSubKey(fileextension);

                //retrieve Content Type value
                filecontenttype = fileextkey.GetValue("Content Type", DEFAULT_CONTENT_TYPE).ToString();

                //cleanup
                fileextkey = null;
                regkey = null;
            }
            catch
            {
                filecontenttype = DEFAULT_CONTENT_TYPE;
            }

            return filecontenttype;
        }
        #endregion

        #region "返回状态字符"
        /// <summary>
        /// 根据状态值返回状态字符
        /// </summary>
        /// <param name="i">状态值</param>
        /// <returns>返回字符</returns>
        public static string ReturnStatusInt(int i)
        {
            string rString = "未知";
            switch (i)
            {
                case 0:
                    rString = "正常";
                    break;
                case 1:
                    rString = "禁用";
                    break;
            }
            return rString;
        }
        #endregion

        #region "删除文件"
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="FilePath">删除的文件路径</param>
        /// <param name="PathType">删除文件路径类型</param>
        /// <returns>成功/失败</returns>
        public static bool DeleteFile(string FilePath, DeleteFilePathType PathType)
        {
            bool rBool = false;
            switch (PathType)
            {
                case DeleteFilePathType.DummyPath:
                    FilePath = HttpContext.Current.Server.MapPath(FilePath);
                    break;
                case DeleteFilePathType.NowDirectoryPath:
                    FilePath = HttpContext.Current.Server.MapPath(FilePath);
                    break;
                case DeleteFilePathType.PhysicsPath:
                    break;
            }
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
                rBool = true;
            }
            return rBool;
        }
        #endregion

        #region "获得操作系统"
        /// <summary>
        /// 获得操作系统
        /// </summary>
        /// <returns>操作系统名称</returns>
        public static string GetSystem
        {
            get
            {
                string s = HttpContext.Current.Request.UserAgent.Trim().Replace("(", "").Replace(")", "");
                string[] sArray = s.Split(';');
                switch (sArray[2].Trim())
                {
                    case "Windows 4.10":
                        s = "Windows 98";
                        break;
                    case "Windows 4.9":
                        s = "Windows Me";
                        break;
                    case "Windows NT 5.0":
                        s = "Windows 2000";
                        break;
                    case "Windows NT 5.1":
                        s = "Windows XP";
                        break;
                    case "Windows NT 5.2":
                        s = "Windows 2003";
                        break;
                    case "Windows NT 6.0":
                        s = "Windows Vista";
                        break;
                    default:
                        s = "Other";
                        break;
                }
                return s;
            }
        }


        #endregion

        #region "获得状态文字"
        /// <summary>
        /// 获得状态文字
        /// </summary>
        /// <param name="Status">状态类型</param>
        /// <param name="AddWord">附加文字</param>
        /// <returns></returns>
        public static string GetStatTxt(int Status, string AddWord)
        {
            if (Status == 0)
                return "未" + AddWord;
            else
                return "己" + AddWord;
        }
        #endregion



        #region "获得sessionid"
        /// <summary>
        /// 获得sessionid
        /// </summary>
        public static string GetSessionID
        {
            get
            {
                return HttpContext.Current.Session.SessionID;
            }
        }
        #endregion


        #region "进行base64编码"
        /// <summary>
        /// 进行base64编码
        /// </summary>
        /// <param name="s">字符</param>
        /// <returns></returns>
        public static string EnBase64(string s)
        {
            return Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(s));
        }
        #endregion

        #region "进行Base64解码"
        /// <summary>
        /// 进行Base64解码
        /// </summary>
        /// <param name="s">字符</param>
        /// <returns></returns>
        public static string DeBase64(string s)
        {
            return System.Text.Encoding.Default.GetString(Convert.FromBase64String(s));
        }
        #endregion

        #region "获得缓存类配置(命名空间)"
        /// <summary>
        /// 获得缓存类配置(命名空间)
        /// </summary>
        public static string GetCachenamespace
        {
            get
            {
                return ConfigurationManager.AppSettings["Cachenamespace"];
            }
        }
        #endregion

        #region "获得缓存类配置(类名)"
        /// <summary>
        /// 获得缓存类配置(类名)
        /// </summary>
        public static string GetCacheclassName
        {
            get
            {
                return ConfigurationManager.AppSettings["CacheclassName"];
            }
        }
        #endregion

        #region "将日期类型转换成字符"
        /// <summary>
        /// 将日期类型转换成字符
        /// </summary>
        /// <param name="s">日期</param>
        /// <returns>字符</returns>
        public static string ConvertDate(DateTime? s)
        {
            if (s.HasValue)
                return s.Value.ToString("yyyy/MM/dd");
            else
                return "";
        }
        #endregion

        #region "格式化TextArea输入内容为html显示"
        /// <summary>
        /// 格式化TextArea输入内容为html显示
        /// </summary>
        /// <param name="s">要格式化内容</param>
        /// <returns>完成内容</returns>
        public static string FormatTextArea(string s)
        {
            s = s.Replace("\n", "<br>");
            s = s.Replace("\x20", "&nbsp;");
            return s;
        }
        #endregion

        #region "检测Ip地址是否正确"
        /// <summary>
        /// 检测Ip地址是否正确
        /// </summary>
        /// <param name="ip">ip字符串</param>
        /// <returns>正确/不正确</returns>
        public static bool CheckIp(string ip)
        {
            System.Net.IPAddress ipa;
            if (System.Net.IPAddress.TryParse(ip, out ipa))
            {
                ipa = null;
                return true;
            }
            else
            {
                ipa = null;
                return false;
            }
        }
        #endregion

        #region "格式化日期24小时制为字符串如:2008/12/12 21:22:33"
        /// <summary>
        /// 格式化日期24小时制为字符串如:2008/12/12 21:22:33
        /// </summary>
        /// <param name="d">日期</param>
        /// <returns>字符</returns>
        public static string FormatDateToString(DateTime d)
        {
            return d.ToString("yyyy/MM/dd HH:mm:ss");
        }
        #endregion

        #region "格式化日期显示为字符"
        /// <summary>
        /// 格式化日期显示为字符
        /// </summary>
        /// <param name="d">日期</param>
        /// <returns></returns>
        public static string FormatDateToDispString(DateTime d)
        {
            return d.ToString("yyyy/MM/dd HH:mm:ss");
        }
        #endregion

        #region "格式化为UTC时间"
        /// <summary>
        /// 格式化为UTC时间
        /// </summary>
        /// <param name="d">时间</param>
        /// <returns>格式化日期</returns>
        public static DateTime FormatDateToUTC(DateTime d)
        {
            return d.ToUniversalTime();
        }

        /// <summary>
        /// 格式化为UTC时间
        /// </summary>
        /// <param name="d">时间字符</param>
        /// <returns>时间</returns>
        public static DateTime FormatDateToUTC(string d)
        {
            return Convert.ToDateTime(d).ToUniversalTime();
        }
        #endregion

        #region sql防注入

        /// <summary>
        /// 过滤参数,防止SQL语句注入gxb
        /// </summary>
        /// <param name="strSql">string</param>
        /// <returns> string</returns>
        public static string filterSql(string str)
        {
            string strs = str;
            if (!string.IsNullOrEmpty(strs))
            {
                strs.ToLower();
                strs = strs.Replace(@"exec", "");
                strs = strs.Replace("master", "");
                strs = strs.Replace("truncate", "");
                strs = strs.Replace("declare", "");
                strs = strs.Replace("create", "");
                strs = strs.Replace("xp_", "no");
                //  strs = strs.Replace(" ", "");     //菜鸟搜索需要空格
                strs = strs.Replace("'", "");
                strs = strs.Replace("=", "");
                strs = strs.Replace("%", "");
                strs = strs.Replace("<", "");
                strs = strs.Replace(">", "");
                strs = strs.Replace("(", "");
                strs = strs.Replace(")", "");
                strs = strs.Replace("/", "");
                strs = strs.Replace(@"\", "");
                strs = strs.Replace("<br>", "");
                strs = strs.Replace("insert", "");
                strs = strs.Replace("update", "");
                strs = strs.Replace("select", "");
                strs = strs.Replace("delete", "");
                strs = strs.Replace("<>", "");
                strs = strs.Replace("in", "");
                strs = strs.Replace("or", "");
                strs = strs.Replace("and", "");
                strs = strs.Replace("not", "");
                strs = strs.Replace("+", "");
                strs = strs.Replace("&", "");
                strs = strs.Replace("&lt", "");
                strs = strs.Replace("&gt", "");
                strs = strs.Replace("&#39", "");
                strs = strs.Replace("--", "");
                strs = strs.Replace("*", "");
                strs = strs.Replace("&", "");
            }
            else
            {
                strs = "";
            }
            return strs.Trim();
        }
        /// <summary>
        /// 判断是否含有特殊字符
        /// </summary>
        /// <param name="strSql">string</param>
        /// <returns> string</returns>
        public static bool filterText(string str)
        {
            string strs = str;
            if (strs != "" && strs != null)
            {
                strs.ToLower();
                strs = strs.Replace(@"exec", "");
                strs = strs.Replace("master", "");
                strs = strs.Replace("truncate", "");
                strs = strs.Replace("declare", "");
                strs = strs.Replace("create", "");
                strs = strs.Replace("xp_", "no");
                strs = strs.Replace(" ", "");
                strs = strs.Replace("'", "");
                strs = strs.Replace("=", "");
                strs = strs.Replace("%", "");
                strs = strs.Replace("<", "");
                strs = strs.Replace(">", "");
                strs = strs.Replace("(", "");
                strs = strs.Replace(")", "");
                strs = strs.Replace("/", "");
                strs = strs.Replace(@"\", "");
                strs = strs.Replace("<br>", "");
                strs = strs.Replace("insert", "");
                strs = strs.Replace("update", "");
                strs = strs.Replace("select", "");
                strs = strs.Replace("delete", "");
                strs = strs.Replace("<>", "");
                strs = strs.Replace("in", "");
                strs = strs.Replace("or", "");
                strs = strs.Replace("and", "");
                strs = strs.Replace("not", "");
                strs = strs.Replace("+", "");
                strs = strs.Replace("&", "");
                strs = strs.Replace("&lt", "");
                strs = strs.Replace("&gt", "");
                strs = strs.Replace("&#39", "");
                strs = strs.Replace("--", "");
                strs = strs.Replace("*", "");
                strs = strs.Replace("&", "");
            }
            else
            {
                strs = "";
            }
            if (strs == str)
                return true;
            else
                return false;

        }
        #endregion

        /// <summary>
        /// 获取当前的主机名
        /// </summary>
        /// <returns>主机名</returns>
        public static string GetStoreLocation()
        {
            string result = GetStoreHost();
            if (result.EndsWith("/"))
                result = result.Substring(0, result.Length - 1);
            result = result + HttpContext.Current.Request.ApplicationPath;
            if (!result.EndsWith("/"))
                result += "/";

            return result.ToLowerInvariant();
        }

        /// <summary>
        /// 获取当前站点的目录
        /// </summary>
        /// <returns></returns>
        public static string GetStoreLocationDirectory()
        {
            return HttpContext.Current.Request.Url.AbsoluteUri.Substring(0, HttpContext.Current.Request.Url.AbsoluteUri.LastIndexOf('/') + 1);

        }


        /// <summary>
        /// 获取主机
        /// </summary>
        /// <param name="useSsl">Use SSL</param>
        /// <returns>Store host location</returns>
        public static string GetStoreHost()
        {
            string result = "http://" + ServerVariables("HTTP_HOST");
            if (!result.EndsWith("/"))
                result += "/";

            if (!result.EndsWith("/"))
                result += "/";

            return result.ToLowerInvariant();
        }

        /// <summary>
        /// Gets server variable by name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Server variable</returns>
        public static string ServerVariables(string name)
        {
            string tmpS = string.Empty;
            try
            {
                if (HttpContext.Current.Request.ServerVariables[name] != null)
                {
                    tmpS = HttpContext.Current.Request.ServerVariables[name].ToString();
                }
            }
            catch
            {
                tmpS = string.Empty;
            }
            return tmpS;
        }

        #region 验证格式
        /// <summary>
        /// 验证附件（如果不通过返回错误原因，通过返回 "" 字符串）
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        public static string FileVerify(HttpPostedFile objFile)
        {
            string strFileLength = ConfigurationManager.AppSettings["FileLength"];
            if (string.IsNullOrEmpty(strFileLength))
                return "未配置文件最大长度！";
            string strFilePath = objFile.FileName;
            string strType = "," + ConfigurationManager.AppSettings["UploadFileType"] + ",";
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["UploadFileType"]))
                return "未配置上传文件类型！";
            string strFileType = "," + strFilePath.Substring(strFilePath.LastIndexOf(".") + 1).ToLower() + ",";
            if (strType.IndexOf(strFileType) != -1)
            {
                if (objFile.ContentLength > Convert.ToInt32(strFileLength) * 1048576)
                    return "请上传" + strFileLength + "M以内文件！";
                return "";
            }
            else
            {
                return "文件类型不合法，请上传" + ConfigurationManager.AppSettings["UploadFileType"] + "等类型文件！";
            }
        }
        #endregion

        #region 计算工作日天数
        public static int GetWeekDays(DateTime beginTime, DateTime endTime)
        {
            TimeSpan ts = beginTime.Subtract(endTime);//TimeSpan得到dt1和dt2的时间间隔
            int countday = ts.Days;//获取两个日期间的总天数
            if (countday < 0)
                countday = 0 - countday;
            countday = countday + 2;//算头尾2天
            int weekday = 0;//工作日
            //循环用来扣除总天数中的双休日
            for (int i = 0; i < countday; i++)
            {
                DateTime tempdt = beginTime.Date.AddDays(i);
                if (tempdt.DayOfWeek != System.DayOfWeek.Saturday && tempdt.DayOfWeek != System.DayOfWeek.Sunday)
                {
                    weekday++;
                }
            }
            return weekday;
        }
        #endregion


        #region ========加密========

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Encrypt(string Text)
        {
            return Encrypt(Text, "ohmygod");
        }
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        #endregion

        #region ========解密========


        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Decrypt(string Text)
        {
            return Decrypt(Text, "ohmygod");
        }
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Decrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion

        /// <summary>
        /// 过滤html
        /// </summary>
        /// <param name="html">html</param>
        /// <param name="length">截取的长度</param>
        /// <param name="end">截取后用来替补的如(...)</param>
        /// <returns></returns>
        public static string CheckStr(string html, int length, string end)
        {
            if (!string.IsNullOrEmpty(html))
            {
                System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[/s/S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[/s/S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" no[/s/S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[/s/S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[/s/S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"/<img[^/>]+/>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regex10 = new System.Text.RegularExpressions.Regex(@"&nbsp;", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                html = regex1.Replace(html, ""); //过滤<script></script>标记
                html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
                html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
                html = regex4.Replace(html, ""); //过滤iframe
                html = regex5.Replace(html, ""); //过滤frameset
                html = regex6.Replace(html, ""); //过滤frameset
                html = regex7.Replace(html, ""); //过滤frameset
                html = regex8.Replace(html, ""); //过滤frameset
                html = regex9.Replace(html, "");
                html = regex10.Replace(html, "");
                html = html.Replace(" ", "");
                html = html.Replace("</strong>", "");
                html = html.Replace("<strong>", "");
                if (html.Length > length)
                {
                    return html.Substring(0, length) + end;
                }
                return html;
            }
            return "";
        }

        /// <summary>
        /// 按字节长度截取字符串(支持截取带HTML代码样式的字符串)
        /// </summary>
        /// <param name="param">将要截取的字符串参数</param>
        /// <param name="length">截取的字节长度</param>
        /// <param name="end">字符串末尾补上的字符串</param>
        /// <returns>返回截取后的字符串</returns>
        public static string SubstringToHTML(string param, int length, string end)
        {
            string Pattern = null;
            MatchCollection m = null;
            StringBuilder result = new StringBuilder();
            int n = 0;
            char temp;
            bool isCode = false; //是不是HTML代码
            bool isHTML = false; //是不是HTML特殊字符,如&nbsp;
            char[] pchar = param.ToCharArray();
            for (int i = 0; i < pchar.Length; i++)
            {
                temp = pchar[i];
                if (temp == '<')
                {
                    isCode = true;
                }
                else if (temp == '&')
                {
                    isHTML = true;
                }
                else if (temp == '>' && isCode)
                {
                    n = n - 1;
                    isCode = false;
                }
                else if (temp == ';' && isHTML)
                {
                    isHTML = false;
                }

                if (!isCode && !isHTML)
                {
                    n = n + 1;
                    //UNICODE码字符占两个字节
                    if (System.Text.Encoding.Default.GetBytes(temp + "").Length > 1)
                    {
                        n = n + 1;
                    }
                }

                result.Append(temp);
                if (n >= length)
                {
                    break;
                }
            }
            result.Append(end);
            //取出截取字符串中的HTML标记
            string temp_result = result.ToString().Replace("(>)[^<>]*(<?)", "$1$2");
            //去掉不需要结素标记的HTML标记
            temp_result = temp_result.Replace(@"</?(AREA|BASE|BASEFONT|BODY|BR|COL|COLGROUP|DD|DT|FRAME|HEAD|HR|HTML|IMG|INPUT|ISINDEX|LI|LINK|META|OPTION|P|PARAM|TBODY|TD|TFOOT|TH|THEAD|TR|area|base|basefont|body|br|col|colgroup|dd|dt|frame|head|hr|html|img|input|isindex|li|link|meta|option|p|param|tbody|td|tfoot|th|thead|tr)[^<>]*/?>",
             "");
            //去掉成对的HTML标记
            temp_result = temp_result.Replace(@"<([a-zA-Z]+)[^<>]*>(.*?)<//1>", "$2");
            //用正则表达式取出标记
            Pattern = ("<([a-zA-Z]+)[^<>]*>");
            m = Regex.Matches(temp_result, Pattern);

            ArrayList endHTML = new ArrayList();

            foreach (Match mt in m)
            {
                endHTML.Add(mt.Result("$1"));
            }
            //补全不成对的HTML标记
            for (int i = endHTML.Count - 1; i >= 0; i--)
            {
                result.Append("</");
                result.Append(endHTML[i]);
                result.Append(">");
            }

            return result.ToString();
        }


    }

    #region "枚举类型"
    /// <summary>
    /// 删除文件路径类型
    /// </summary>
    public enum DeleteFilePathType
    {
        /// <summary>
        /// 物理路径
        /// </summary>
        PhysicsPath = 1,
        /// <summary>
        /// 虚拟路径
        /// </summary>
        DummyPath = 2,
        /// <summary>
        /// 当前目录
        /// </summary>
        NowDirectoryPath = 3
    }

    /// <summary>
    /// 获取方式
    /// </summary>
    public enum MethodType
    {
        /// <summary>
        /// Post方式
        /// </summary>
        Post = 1,
        /// <summary>
        /// Get方式
        /// </summary>
        Get = 2
    }

    /// <summary>
    /// 获取数据类型
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 字符
        /// </summary>
        Str = 1,
        /// <summary>
        /// 日期
        /// </summary>
        Dat = 2,
        /// <summary>
        /// 整型
        /// </summary>
        Int = 3,
        /// <summary>
        /// 长整型
        /// </summary>
        Long = 4,
        /// <summary>
        /// 双精度小数
        /// </summary>
        Double = 5,
        /// <summary>
        /// 只限字符和数字
        /// </summary>
        CharAndNum = 6,
        /// <summary>
        /// 只限邮件地址
        /// </summary>
        Email = 7,
        /// <summary>
        /// 只限字符和数字和中文
        /// </summary>
        CharAndNumAndChinese = 8

    }

    /// <summary>
    /// 表操作方法
    /// </summary>
    public enum DataTable_Action
    {
        /// <summary>
        /// 插入
        /// </summary>
        Insert = 0,
        /// <summary>
        /// 更新
        /// </summary>
        Update = 1,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 2
    }
    /// <summary>
    /// 缓存类型
    /// </summary>
    public enum OnlineCountType
    {
        /// <summary>
        /// 缓存
        /// </summary>
        Cache = 0,
        /// <summary>
        /// 数据库
        /// </summary>
        DataBase = 1
    }
    #endregion

}