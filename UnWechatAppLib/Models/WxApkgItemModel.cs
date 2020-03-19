using System;
using System.Collections.Generic;
using System.Text;

namespace UnWechatAppLib.Models
{
    public class WxApkgItemModel
    {
        public string Name { set; get; }
        internal int Start { set; get; }
        internal int Length { set; get; }

        public byte[] Data { set; get; }

    }
}
