using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Data;

namespace Tiu.Net
{
    /// <summary>
    /// FTP操作类
    /// </summary>
    public class FTPClient
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestUriString">访问的FTP地址（可以是FTP的某个目录的路径）</param>
        public FTPClient(string requestUriString)
            : this(requestUriString, string.Empty, string.Empty)
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestUriString">访问的FTP地址（可以是FTP的某个目录的路径）</param>
        /// <param name="userName">登录的用户名（不需要用户名的话值为""）</param>
        /// <param name="password">登录的密码 （不需要密码的话值为""）</param>
        public FTPClient(string requestUriString, string userName, string password)
        {
            this._requestUriString = requestUriString;
            this._userName = userName != null ? userName : string.Empty;
            this._password = password != null ? password : string.Empty;
        }

        private string _requestUriString;
        private string _userName;
        private string _password;
        private FtpWebRequest _request;
        private FtpWebResponse _response;

        /// <summary>
        /// 创建FtpWebRequest
        /// </summary>
        /// <param name="uriString">FTP路径</param>
        /// <param name="method">FTP方法</param>
        private void OpenRequest(string uriString, string method)
        {
            var requestUriString = this._requestUriString.Trim('/');
            uriString = uriString.Trim('/');
            var uriStr = requestUriString;
            if (!string.IsNullOrEmpty(uriString))
            {
                uriStr += "/" + uriString;
            }


            this._request = WebRequest.Create(new Uri(uriStr)) as FtpWebRequest;
            this._request.Credentials = new NetworkCredential(_userName, _password);
            this._request.UseBinary = true;
            this._request.Method = method;
        }

        /// <summary>
        /// 返回FtpWebResponse
        /// </summary>
        /// <param name="uriString">FTP路径</param>
        /// <param name="method">FTP方法</param>
        private void OpenResponse(string uriString, string method)
        {
            this.OpenRequest(uriString, method);
            this._response = this._request.GetResponse() as FtpWebResponse;
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="directoryPath">要创建的目录名称</param>
        public void MakeDirectory(string directoryPath)
        {
            this.OpenResponse(directoryPath, WebRequestMethods.Ftp.MakeDirectory);
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="directoryPath">要删除的目录名称</param>
        public void RemoveDirectory(string directoryPath)
        {
            this.OpenResponse(directoryPath, WebRequestMethods.Ftp.RemoveDirectory);
        }

        /// <summary>
        /// 重命名文件
        /// </summary>
        /// <param name="originalFileName">原文件名</param>
        /// <param name="newFileName">新文件名</param>
        public void Rename(string originalFileName, string newFileName)
        {
            this.OpenResponse(originalFileName, WebRequestMethods.Ftp.Rename);
            this._request.RenameTo = newFileName;
            this._response = this._request.GetResponse() as FtpWebResponse;
        }

        /// <summary>
        /// 上传文件到服务器
        /// </summary>
        /// <param name="localFullPath">要上传的本地文件全路径</param>
        public void UploadFile(string localFullPath)
        {
            this.UploadFile(localFullPath, false);
        }

        /// <summary>
        /// 上传文件到服务器
        /// </summary>
        /// <param name="localFullPath">要上传的本地文件全路径</param>
        /// <param name="overWriteFile">是否要重写服务器上的文件</param>
        public void UploadFile(string localFullPath, bool overWriteFile)
        {
            this.UploadFile(localFullPath, Path.GetFileName(localFullPath), overWriteFile);
        }

        /// <summary>
        /// 上传文件到服务器
        /// </summary>
        /// <param name="localFullPath">要上传的本地文件全路径</param>
        /// <param name="remoteFileName">上传后文件重命名为</param>
        public void UploadFile(string localFullPath, string remoteFileName)
        {
            this.UploadFile(localFullPath, remoteFileName, false);
        }

        /// <summary>
        /// 上传文件到服务器
        /// </summary>
        /// <param name="localFullPath">要上传的本地文件全路径</param>
        /// <param name="remoteFileName">上传后文件重命名为</param>
        /// <param name="overWriteFile">是否要重写服务器上的文件</param>
        public void UploadFile(string localFullPath, string remoteFileName, bool overWriteFile)
        {
            byte[] fileBytes = null;
            using (FileStream fileStream = new FileStream(localFullPath, FileMode.Open, FileAccess.Read))
            {
                fileBytes = new byte[fileStream.Length];
                fileStream.Read(fileBytes, 0, (Int32)fileStream.Length);
            }
            this.UploadFile(fileBytes, remoteFileName, overWriteFile);
        }

        /// <summary>
        /// 上传文件到服务器
        /// </summary>
        /// <param name="fileBytes">上传文件的字节流</param>
        /// <param name="remoteFileName">上传后文件重命名为</param>
        public void UploadFile(byte[] fileBytes, string remoteFileName)
        {
            this.UploadFile(fileBytes, remoteFileName, false);
        }

        /// <summary>
        /// 上传文件到服务器
        /// </summary>
        /// <param name="fileBytes">上传文件的字节流</param>
        /// <param name="remoteFileName">上传后文件重命名为</param>
        /// <param name="overWriteFile">是否要重写服务器上的文件</param>
        public void UploadFile(byte[] fileBytes, string remoteFileName, bool overWriteFile)
        {
            this.OpenResponse(overWriteFile ? remoteFileName : Gadget.ReturnFileNameWithCurrentDate(remoteFileName), WebRequestMethods.Ftp.UploadFile);
            using (Stream stream = this._request.GetRequestStream())
            {
                using (MemoryStream memoryStream = new MemoryStream(fileBytes))
                {
                    byte[] buffer = new byte[Constant.FTP.LenOfBuffer];
                    int bytesRead = 0;
                    int totalRead = 0;
                    while (true)
                    {
                        bytesRead = memoryStream.Read(buffer, 0, buffer.Length);
                        if (bytesRead == 0)
                        {
                            break;
                        }
                        totalRead += bytesRead;
                        stream.Write(buffer, 0, bytesRead);
                    }
                }
                this._response = this._request.GetResponse() as FtpWebResponse;
            }
        }

        /// <summary>
        /// 下载服务器文件到本地
        /// </summary>
        /// <param name="remoteFileName">要下载的服务器文件名</param>
        /// <param name="localPath">下载到本地的路径</param>
        public void DownloadFile(string remoteFileName, string localPath)
        {
            this.DownloadFile(remoteFileName, localPath, false);
        }

        /// <summary>
        /// 下载服务器文件到本地
        /// </summary>
        /// <param name="remoteFileName">要下载的服务器文件名</param>
        /// <param name="localPath">下载到本地的路径</param>
        /// <param name="overWriteFile">是否要重写本地的文件</param>
        public void DownloadFile(string remoteFileName, string localPath, bool overWriteFile)
        {
            this.DownloadFile(remoteFileName, localPath, remoteFileName, overWriteFile);
        }

        /// <summary>
        /// 下载服务器文件到本地
        /// </summary>
        /// <param name="remoteFileName">要下载的服务器文件名</param>
        /// <param name="localPath">下载到本地的路径</param>
        /// <param name="localFileName">下载到本地的文件重命名为</param>
        public void DownloadFile(string remoteFileName, string localPath, string localFileName)
        {
            this.DownloadFile(remoteFileName, localPath, localFileName, false);
        }

        /// <summary>
        /// 下载服务器文件到本地
        /// </summary>
        /// <param name="remoteFileName">要下载的服务器文件名</param>
        /// <param name="localPath">下载到本地的路径</param>
        /// <param name="localFileName">下载到本地的文件重命名为</param>
        /// <param name="overWriteFile">是否要重写本地的文件</param>
        public void DownloadFile(string remoteFileName, string localPath, string localFileName, bool overWriteFile)
        {
            byte[] fileBytes = this.DownloadFile(remoteFileName);
            if (fileBytes != null)
            {
                using (FileStream fileStream = new FileStream(Path.Combine(localPath, overWriteFile ? localFileName : Gadget.ReturnFileNameWithCurrentDate(localFileName)), FileMode.Create))
                {
                    fileStream.Write(fileBytes, 0, fileBytes.Length);
                    fileStream.Flush();
                }
            }
        }

        /// <summary>
        /// 下载服务器文件到本地 ** 
        /// </summary>
        /// <param name="fileVPath">要下载的服务器文件路径（相对于当前访问的目录）</param>
        /// <returns>文件的字节流</returns>
        public byte[] DownloadFile(string fileVPath)
        {
            this.OpenResponse(fileVPath, WebRequestMethods.Ftp.DownloadFile);
            using (Stream stream = this._response.GetResponseStream())
            {
                using (MemoryStream memoryStream = new MemoryStream(Constant.FTP.CapacityofMemeoryStream))
                {
                    byte[] buffer = new byte[Constant.FTP.LenOfBuffer];
                    int bytesRead = 0;
                    int TotalRead = 0;
                    while (true)
                    {
                        bytesRead = stream.Read(buffer, 0, buffer.Length);
                        TotalRead += bytesRead;
                        if (bytesRead == 0)
                            break;
                        memoryStream.Write(buffer, 0, bytesRead);
                    }
                    return memoryStream.Length > 0 ? memoryStream.ToArray() : null;
                }
            }
        }

        /// <summary>
        /// 删除服务器文件 ** 
        /// </summary>
        /// <param name="directoryVPath">要删除的文件路径（相对于当前访问的目录）</param>
        /// <param name="fileVPath">要删除的文件名</param>
        public void DeleteFile(string directoryVPath, string fileVPath)
        {
            this.DeleteFile(directoryVPath + fileVPath);
        }

        /// <summary>
        /// 删除服务器文件 ** 
        /// </summary>
        /// <param name="fileVPath">要删除的路径（相对于当前访问的目录）</param>
        public void DeleteFile(string fileVPath)
        {
            this.OpenResponse(fileVPath, WebRequestMethods.Ftp.DeleteFile);
        }

        /// <summary>
        /// 列出服务器上指定目录下的所有文件 ** 
        /// </summary>
        /// <param name="directoryVPath">指定的目录（相对于当前访问的目录）</param>
        /// <returns>文件名列表</returns>
        public string[] GetFileList(string directoryVPath)
        {
            List<string> filesList = new List<string>();
            this.OpenResponse(directoryVPath, WebRequestMethods.Ftp.ListDirectory);
            using (StreamReader streamReader = new StreamReader(this._response.GetResponseStream()))
            {
                return Gadget.SplitString(streamReader.ReadToEnd(), Constant.TextConstant.FtpNewLine);
            }
        }


        /// <summary>
        /// 列出服务器上指定目录下的所有子目录 ** 
        /// </summary>
        /// <param name="directoryVPath">指定的目录（相对于当前访问的目录）</param>
        /// <returns>子目录名列表</returns>
        public List<string> GetDirectoriesList(string directoryVPath)
        {
            List<string> directoriesList = new List<string>();
            this.OpenResponse(directoryVPath, WebRequestMethods.Ftp.ListDirectoryDetails);
            using (StreamReader streamReader = new StreamReader(this._response.GetResponseStream()))
            {
                string line = streamReader.ReadLine();
                if (line != null)
                {
                    while (line != null)
                    {
                        line = streamReader.ReadLine();
                        if (line != null && line[0] == Constant.TextConstant.Dir)
                        {
                            line = line.Substring(line.LastIndexOf(Constant.TextConstant.Colon) + Constant.FTP.LenToDirectory);
                            if (!line.StartsWith(Constant.FileConstant.FileTypeSeperator))
                            {
                                directoriesList.Add(line);
                            }
                        }
                        
                    }
                    return directoriesList;
                }
            }
            return null;
        }


        /// <summary>
        /// 检查指定的目录是否在服务器上存在 ** 
        /// </summary>
        /// <param name="directoryVPath">指定的目录（相对于当前访问的目录）</param>
        /// <returns>如果存在返回true，否则返回false</returns>
        public bool DirectoryExist(string directoryVPath)
        {
            try
            {
                this.OpenResponse(directoryVPath, WebRequestMethods.Ftp.GetDateTimestamp);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// 检查指定的文件是否在服务器上存在 ** 
        /// </summary>
        /// <param name="fileVPath">文件的路径（相对于当前访问的目录）</param>
        /// <returns></returns>
        public bool FileExist(string fileVPath)
        {
            var fileName = Path.GetFileName(fileVPath);
            var dirPath = fileVPath.Substring(0, fileVPath.LastIndexOf("/"));
            return FileExist(dirPath, fileName);
        }

        /// <summary>
        /// 检查指定的文件是否在服务器上存在 ** 
        /// </summary>
        /// <param name="directoryVPath">指定的目录（相对于当前访问的目录）</param>
        /// <param name="remoteFileName">指定的文件</param>
        /// <returns>如果存在返回true，否则返回false</returns>
        public bool FileExist(string directoryVPath, string remoteFileName)
        {
            string[] fileNames = this.GetFileList(directoryVPath);
            foreach (string fileName in fileNames)
            {
                if (string.Compare(fileName, remoteFileName, true) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 释放资源 ** 
        /// </summary>
        public void Dispose()
        {
            this.Close();
        }

        /// <summary>
        /// 释放链接 ** 
        /// </summary>
        public void Close()
        {
            if (this._response != null)
            {
                this._response.Close();
                this._response = null;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    class Constant
    {
        public class TextConstant
        {
            public const string Tab = "\t";
            public const string NewLine = "\n";
            public const string UnderLine = "_";
            public const string FtpNewLine = "\r\n";
            public const string Colon = ":";
            public const char Dir = 'd';
            public const char NoSet = '-';
            public const char Write = 'w';
            public const char Read = 'r';
        }

        public class HtmlConstant
        {
            public const string NewLine = "<br />";
        }

        public class FileConstant
        {
            public const string FileTypeForTXT = ".txt";
            public const string FileTypeForExcel = ".xls";
            public const string FileTypeForZip = ".zip";
            public const string FileTypeSeperator = ".";
            public const string FileFullMatchSymbol = "*.*";
            public const string FolderSeperator = @"\";
        }

        public class DatetimeFormat
        {
            public const string YYMMDDHHMMSS = "yyMMddHHmmss";
        }

        public class FTP
        {
            public const int LenToDirectory = 4;
            public const int LenOfBuffer = 1024;
            public const int CapacityofMemeoryStream = 1024 * 500;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    class Gadget
    {
        public static string ReturnFileNameWithCurrentDate(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName) + Constant.TextConstant.UnderLine + DateTime.Now.ToString(Constant.DatetimeFormat.YYMMDDHHMMSS) + Constant.FileConstant.FileTypeSeperator + Path.GetExtension(fileName);
        }

        public static string[] SplitString(string str, string split)
        {
            return str.Split(new string[] { split }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
