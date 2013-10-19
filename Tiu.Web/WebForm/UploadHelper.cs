using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Tiu.Web.WebForm
{
    /// <summary>
    /// 文件上传帮助类
    /// </summary>
    public class UploadHelper
    {
        /// <summary>
        /// 缩略图模式
        /// </summary>
        public enum ThumbnailMode
        {
            /// <summary>
            /// 指定高宽缩放（可能变形）
            /// </summary>
            HW,
            /// <summary>
            /// 指定宽，高按比例
            /// </summary>
            W,
            /// <summary>
            /// 指定高，宽按比例
            /// </summary>
            H,
            /// <summary>
            /// 指定高宽裁减（不变形）
            /// </summary>
            Cut
        }

        ///////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 图片文件文件类型的前缀
        /// </summary>
        private const string imgTypePreStr = "image/";


        /// <summary>
        /// 上传文件，返回保存文件的相对路径
        /// </summary>
        /// <param name="fileUpload">上传控件对象</param>
        /// <param name="vDirectory">文件存放目录的虚拟路径（如果目录不存在，那么创建之）</param>
        /// <returns>文件上传的虚拟路径（如："~/Files/Image/20110321100601333.jpg"）</returns>
        public string SaveFileGetVPath(FileUpload fileUpload, string vDirectory)
        {
            return SaveFile(fileUpload, vDirectory, true);
        }

        /// <summary>
        /// 上传文件，返回保存文件的文件名
        /// </summary>
        /// <param name="fileUpload">上传控件对象</param>
        /// <param name="vDirectory">文件存放目录的虚拟路径（如果目录不存在，那么创建之）</param>
        /// <returns>保存文件的文件名（如："20110321100601333.jpg"）</returns>
        public string SaveFileGetFileName(FileUpload fileUpload, string vDirectory)
        {
            return SaveFile(fileUpload, vDirectory, false);
        }

        /// <summary>
        /// 上传文件，返回保存文件的相对路径
        /// </summary>
        /// <param name="fileUpload">上传控件对象</param>
        /// <param name="vDirectory">文件存放目录的虚拟路径（如果目录不存在，那么创建之）</param>
        /// <param name="returnFileVPath">是否返回文件存放的虚拟路径，设置为false的话就只返回保存的文件名（带后缀）</param>
        /// <returns></returns>
        private string SaveFile(FileUpload fileUpload, string vDirectory,bool returnFileVPath)
        {
            if (!fileUpload.HasFile)
            {
                throw new Exception("没有任何上传文件");
            }

            string vPath;
            string saveName;
            try
            {
                string dirPath = System.Web.HttpContext.Current.Server.MapPath(vDirectory);
                if (!System.IO.Directory.Exists(dirPath))
                {
                    System.IO.Directory.CreateDirectory(dirPath);
                }

                // 获取上传文件的文件名
                string uploadFileName = fileUpload.FileName;
                // 获取原文件的后缀名
                string extension = System.IO.Path.GetExtension(fileUpload.FileName);
                // 生成文件名（当前时间+后缀）
                saveName = string.Format("{0}{1}",DateTime.Now.ToString("yyyyMMddHHmmssfff"),extension);
                // 生成文件上传的虚拟路径
                vPath = string.Format("{0}/{1}", vDirectory, saveName);
                // 获取文件上传的绝对路径
                string fileName = System.Web.HttpContext.Current.Server.MapPath(vPath);

                // 上传文件
                fileUpload.SaveAs(fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnFileVPath? vPath:saveName;
        }


        /// <summary>
        /// 保存图片为缩略图
        /// </summary>
        /// <param name="fileUpload">上传控件对象</param>
        /// <param name="savePath">文件存放的虚拟路径（如："~/Files/Image/20110321100601333-thumbnail.jpg"）</param>
        /// <param name="height">缩略图的高</param>
        /// <param name="width">缩略图的宽</param>
        /// <param name="mode">缩略图模式</param>
        /// <returns>是否成功</returns>
        public bool SaveThumbnail(FileUpload fileUpload, string savePath, int height, int width,ThumbnailMode mode)
        {
            if (!fileUpload.HasFile)
            {
                throw new Exception("没有任何上传文件");
            }

            #region 获取原图片（不是图片的话抛出异常）
            string fileType = fileUpload.PostedFile.ContentType;
            if (fileType.ToLower().IndexOf(imgTypePreStr) != 0)
            {
                throw new Exception("上传的不是图片文件");
            }
            // 原图片
            System.Drawing.Image originalImage = System.Drawing.Image.FromStream(fileUpload.FileContent);
            #endregion
            
            #region 计算宽高
            int towidth = width;   // 缩略图的宽
            int toheight = height; // 缩略图的高
            int x = 0; // 图片截取的起点横坐标
            int y = 0; // 图片截取的起点纵坐标
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            switch (mode)
            {
                case ThumbnailMode.HW: break;
                case ThumbnailMode.W: toheight = originalImage.Height * width / originalImage.Width; break;
                case ThumbnailMode.H: towidth = originalImage.Width * height / originalImage.Height; break;
                case ThumbnailMode.Cut://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }
            #endregion

            #region 绘制
            // 新建一个bmp图片（缩略图）
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            // 新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            // 设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            // 设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            // 清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);
            // 在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage,                                 // 指定一个原图
                new System.Drawing.Rectangle(0, 0, towidth, toheight), // 指定所绘制图像的位置和大小，将图像进行缩放以适合该矩形
                new System.Drawing.Rectangle(x, y, ow, oh),            // 指定 image 对象中要绘制的部分
                System.Drawing.GraphicsUnit.Pixel                      // 将设备像素指定为度量单位
                );
            #endregion

            bool re= false;
            #region 保存
            try
            {
                // 获取原文件的后缀名
                string extension = System.IO.Path.GetExtension(fileUpload.FileName);//uploadFileName.Substring(uploadFileName.LastIndexOf('.') + 1, uploadFileName.Length - (uploadFileName.LastIndexOf('.') + 1));
                // 生成缩略图上传的虚拟路径
                string vPath = string.Format("{0}{1}", savePath, extension);
                // 获取缩略图上传的绝对路径
                string thumbnailPath = System.Web.HttpContext.Current.Server.MapPath(vPath);
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                re = true;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
            #endregion
            return re;
        }
    }
}
