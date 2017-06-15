using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace System.Framework.Logging
{
    public class Nlog
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Info(string log)
        {
            Logger.Info(log);
        }

        public static void Info(string nLogName, string log)
        {
            Logger.Info(new Exception(nLogName), log);
        }
    }
}

/*
默认的layout属性值： ${longdate}|${level:uppercase=true}|${logger}|${message} ，关于布局的详细介绍请查看Layouts。

关于布局渲染器的详细介绍请查看Layout Renderers，此处介绍几个常用的：

${date:universalTime=Boolean:format=String:culture=Culture}：当前日期和时间。
${shortdate:universalTime=Boolean}：表示格式为 yyyy-MM-dd 的短日期。 
${longdate:universalTime=Boolean}：表示格式为 yyyy-MM-dd HH\:mm\:ss.ffff 的长日期格式。 universalTime 设置是否输出UTC时间代替本地时间，默认为 false 。 例如：2016-02-19 10:33:49.9398
${level}：表示日志等级。  uppercase 值设置大小写，默认为 false ，即首字母大写，其余字母小写。例如（依次为默认、大写）：Trace、TRACE
${logger}：表示日志记录器的名称，也就是方法 LogManager.GetLogger("Program")  的参数值。例如：Program
${message}：表示日志消息。例如：  logger.Trace("Sample trace message");  ，则结果为：Sample trace message
${machinename}：表示服务器名称，即计算机名称。
${newline}：表示换行。
${exception:innerFormat=String:maxInnerExceptionLevel=Integer:innerExceptionSeparator=String:separator=String:format=String}：表示异常信息。 
    format - 设置输出格式，必须是异常的属性：Message、Type、ShortType、ToString、Method、StackTrace、Data，默认值为Message； 
    innerFormat - 设置内部异常的输出格式，可选值：Message, Type, ShortType, ToString, Method, StackTrace； 
    maxInnerExceptionLevel - 设置包含在输出中的最大内部异常个数，默认为0； 
    innerExceptionSeparator - 设置内部异常之间的分隔符，默认为换行； 
    separator - 设置用于连接格式中的多个数据，默认为一个空格，例如： ${exception:format=message,tostring} ，则用一个空格分隔message和string。
*/
