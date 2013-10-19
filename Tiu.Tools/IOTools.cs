using System;
using System.Collections.Generic;
using System.Text;

namespace Tiu.Tools
{
    /// <summary>
    /// IO工具类
    /// </summary>
    public static class IOTools
    {
        /// <summary>
        /// 检测目录是否存在
        /// </summary>
        /// <param name="path">目录的物理路径</param>
        /// <param name="createWhenNotExist">如果不存在，是否创建</param>
        /// <returns></returns>
        public static bool CheckDirectoryExist(string path,bool createWhenNotExist)
        {
            bool exist = false;

            exist = System.IO.Directory.Exists(path);
            if (!exist && createWhenNotExist)
            {
                System.IO.Directory.CreateDirectory(path);
            }

            return exist;
        }

        /// <summary>
        /// 以指定编码读取文件内容，文件不存在返回null
        /// </summary>
        /// <param name="path">文件的物理路径</param>
        /// <param name="encoding">编码格式</param>
        /// <returns></returns>
        public static string ReadFileText(string path, Encoding encoding)
        {
            if (!System.IO.File.Exists(path))
            {
                return null;
            }

            string content = System.IO.File.ReadAllText(path, encoding);
            return content;
        }

        /// <summary>
        /// 以默认编码读取文件内容，文件不存在返回null
        /// </summary>
        /// <param name="path">文件的物理路径</param>
        /// <returns></returns>
        public static string ReadFileText(string path)
        {
            return ReadFileText(path, Encoding.Default);
        }
    }
}
