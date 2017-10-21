using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace HkEbPortal.Filters
{
    public class Des
    {
        /// <summary>
        /// 长度必须24位
        /// </summary>
        private static readonly byte[] Key = { 11, 22, 35, 11, 255, 44, 129, 189, 23, 56, 224, 178, 156, 2, 1, 8, 5, 219, 33, 120, 21, 18, 22, 16 };
        private static readonly byte[] Iv = { 11, 23, 35, 11, 255, 44, 129, 189 };

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text">明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(string text)
        {
            // Check arguments.
            if (text == null || text.Length <= 0)
                throw new ArgumentNullException(nameof(text));

            string result;
            using (var tdsAlg = new TripleDESCryptoServiceProvider { Key = Key, IV = Iv })
            using (MemoryStream msEncrypt = new MemoryStream())
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, tdsAlg.CreateEncryptor(tdsAlg.Key, tdsAlg.IV), CryptoStreamMode.Write))
            {
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    swEncrypt.Write(text);
                result = Convert.ToBase64String(msEncrypt.ToArray());
            }
            return result;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="text">密文</param>
        /// <returns>明文</returns>
        public static string Decrypt(string text)
        {
            if (text == null || text.Length <= 0)
                throw new ArgumentNullException(nameof(text));

            string plaintext;

            using (TripleDESCryptoServiceProvider tdsAlg = new TripleDESCryptoServiceProvider { Key = Key, IV = Iv })
            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(text)))
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, tdsAlg.CreateDecryptor(tdsAlg.Key, tdsAlg.IV), CryptoStreamMode.Read))
            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                plaintext = srDecrypt.ReadToEnd();

            return plaintext;

        }

        /// <summary>
        ///  生成随机数
        /// </summary>
        /// <returns></returns>
        public static string GetDesStr()
        {
            Random randrom = new Random((int)DateTime.Now.Ticks);
            string strA = randrom.Next(10).ToString(); //数字
            string strB = "abcdefghijklmnopqrstuvwxyz".Substring(0 + randrom.Next(26), 1);
            string strC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(0 + randrom.Next(26), 1);
            string strD = "!#$&*_".Substring(0 + randrom.Next(6), 1);
            Random randrom1 = new Random();
            string strAA = randrom.Next(10).ToString(); //数字
            string strBB = "abcdefghijklmnopqrstuvwxyz".Substring(0 + randrom1.Next(26), 1);
            string strCC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(0 + randrom1.Next(26), 1);
            string strDD = "!#$&*_".Substring(0 + randrom1.Next(6), 1);
            List<string> arrString = new List<string>() { strA, strB, strC, strD, strAA, strBB, strCC, strDD };
            arrString = GetRandomList(arrString);
            string resultStr = "";
            for (int i = 0; i < arrString.Count; i++)
            {
                resultStr += arrString[i].ToString();
            }
            return resultStr;
        }

        private static List<T> GetRandomList<T>(List<T> inputList)
        {
            //Copy to a array
            T[] copyArray = new T[inputList.Count];
            inputList.CopyTo(copyArray);

            //Add range
            List<T> copyList = new List<T>();
            copyList.AddRange(copyArray);

            //Set outputList and random
            List<T> outputList = new List<T>();
            Random rd = new Random(DateTime.Now.Millisecond);

            while (copyList.Count > 0)
            {
                //Select an index and item
                int rdIndex = rd.Next(0, copyList.Count - 1);
                T remove = copyList[rdIndex];

                //remove it from copyList and add it to output
                copyList.Remove(remove);
                outputList.Add(remove);
            }
            return outputList;
        }

        /// <summary>
        /// 严格根据 Ascii码 来生成
        /// </summary>
        /// <param name="intLength">生成字符长度</param>
        /// <param name="booNumber">是否包含数字</param>
        /// <param name="booSign">是否包含符号</param>
        /// <param name="booSmallword">是否包小写字母</param>
        /// <param name="booBigword">是否包大写字母</param>
        /// <returns></returns>
        public string getRandomizer(int intLength, bool booNumber, bool booSign, bool booSmallword, bool booBigword)
        {
            //定义
            Random ranA = new Random();
            int intResultRound = 0;
            int intA = 0;
            string strB = "";
            while (intResultRound < intLength)
            {
                //生成随机数A，表示生成类型
                //1=数字，2=符号，3=小写字母，4=大写字母
                intA = ranA.Next(1, 5);
                //如果随机数A=1，则运行生成数字
                //生成随机数A，范围在0-10
                //把随机数A，转成字符
                //生成完，位数+1，字符串累加，结束本次循环
                if (intA == 1 && booNumber)
                {
                    intA = ranA.Next(0, 10);
                    strB = intA.ToString() + strB;
                    intResultRound = intResultRound + 1;
                    continue;
                }
                //如果随机数A=2，则运行生成符号
                //生成随机数A，表示生成值域
                //1：33-47值域，2：58-64值域，3：91-96值域，4：123-126值域
                if (intA == 2 && booSign == true)
                {
                    intA = ranA.Next(1, 5);
                    //如果A=1
                    //生成随机数A，33-47的Ascii码
                    //把随机数A，转成字符
                    //生成完，位数+1，字符串累加，结束本次循环
                    if (intA == 1)
                    {
                        intA = ranA.Next(33, 48);
                        strB = ((char)intA).ToString() + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }

                    //如果A=2
                    //生成随机数A，58-64的Ascii码
                    //把随机数A，转成字符
                    //生成完，位数+1，字符串累加，结束本次循环
                    if (intA == 2)
                    {
                        intA = ranA.Next(58, 65);
                        strB = ((char)intA).ToString() + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }

                    //如果A=3
                    //生成随机数A，91-96的Ascii码
                    //把随机数A，转成字符
                    //生成完，位数+1，字符串累加，结束本次循环
                    if (intA == 3)
                    {
                        intA = ranA.Next(91, 97);
                        strB = ((char)intA).ToString() + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }

                    //如果A=4
                    //生成随机数A，123-126的Ascii码
                    //把随机数A，转成字符
                    //生成完，位数+1，字符串累加，结束本次循环
                    if (intA == 4)
                    {
                        intA = ranA.Next(123, 127);
                        strB = ((char)intA).ToString() + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }
                }

                //如果随机数A=3，则运行生成小写字母
                //生成随机数A，范围在97-122
                //把随机数A，转成字符
                //生成完，位数+1，字符串累加，结束本次循环
                if (intA == 3 && booSmallword == true)
                {
                    intA = ranA.Next(97, 123);
                    strB = ((char)intA).ToString() + strB;
                    intResultRound = intResultRound + 1;
                    continue;
                }

                //如果随机数A=4，则运行生成大写字母
                //生成随机数A，范围在65-90
                //把随机数A，转成字符
                //生成完，位数+1，字符串累加，结束本次循环
                if (intA == 4 && booBigword == true)
                {
                    intA = ranA.Next(65, 89);
                    strB = ((char)intA).ToString() + strB;
                    intResultRound = intResultRound + 1;
                    continue;
                }
            }
            return strB;
        }

    }
}