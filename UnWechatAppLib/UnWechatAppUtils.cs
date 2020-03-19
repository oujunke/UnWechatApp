using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnWechatAppLib.Models;

namespace UnWechatAppLib
{
    public class UnWechatAppUtils
    {
        public static List<WxApkgItemModel> Unpack(Stream stream)
        {
            List<WxApkgItemModel> wxApkgItemModelList = new List<WxApkgItemModel>();
            if (stream.ReadByte() != 0xBE)
            {
                throw new Exception("文件类型错误");
            }
            stream.Position = 0xE;
            var count = ReadInt(stream);

            for (int i = 0; i < count; i++)
            {
                var nameLength = ReadInt(stream);
                WxApkgItemModel wxApkgItemModel = new WxApkgItemModel();
                wxApkgItemModel.Name = Encoding.UTF8.GetString(ReadStream(stream, nameLength),0, nameLength);
                wxApkgItemModel.Start = ReadInt(stream);
                wxApkgItemModel.Length = ReadInt(stream);
                wxApkgItemModelList.Add(wxApkgItemModel);
            }
            foreach (var wxApkgItemModel in wxApkgItemModelList)
            {
                stream.Position = wxApkgItemModel.Start;
                wxApkgItemModel.Data = ReadStream(stream, wxApkgItemModel.Length);
            }
            return wxApkgItemModelList;
        }

        private static int ReadInt(Stream stream)
        {
            var src = ReadStream(stream, 4);
            int value;
            value = (int)(((src[0] & 0xFF) << 24)
                    | ((src[0 + 1] & 0xFF) << 16)
                    | ((src[0 + 2] & 0xFF) << 8)
                    | (src[0 + 3] & 0xFF));
            return value;
        }

        private static byte[] ReadStream(Stream stream, int size)
        {
            var bs = new byte[size];
            stream.Read(bs, 0, size);
            return bs;
        }
    }
}
