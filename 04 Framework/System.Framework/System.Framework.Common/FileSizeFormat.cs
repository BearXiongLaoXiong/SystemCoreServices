using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Framework.Common
{
    public class FileSizeFormat
    {
        private const int Gb = 1024 * 1024 * 1024;//定义GB的计算常量
        private const int Mb = 1024 * 1024;//定义MB的计算常量
        private const int Kb = 1024;//定义KB的计算常量
        public static string Format(long length)
        {
            if (length / Gb >= 1) return $"{Math.Round(length / (float)Gb, 2)} GB";
            if (length / Mb >= 1) return $"{Math.Round(length / (float)Mb, 2)} MB";
            if (length / Kb >= 1) return $"{Math.Round(length / (float)Kb, 2)} KB";
            return $"{length} B";
        }

    }
}
