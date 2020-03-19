using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnWechatAppLib;

namespace UnWechatApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = string.Empty;
            string outDirectory = string.Empty;
            while (true)
            {
                Console.Write("请输入要解压的小程序名称:");
                fileName = Console.ReadLine();
                if (File.Exists(fileName))
                {
                    break;
                }
                Console.WriteLine("文件名不存在");
            }
            while (true)
            {
                Console.Write("请输入输出路径:");
                outDirectory = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    break;
                }
                Console.WriteLine("文件路径为空");
            }
            var fileStream=File.OpenRead(fileName);
            try
            {
                var reslut = UnWechatAppUtils.Unpack(fileStream);
                var rootDirectory = Path.GetFullPath(outDirectory);
                foreach (var file in reslut)
                {
                    var name = rootDirectory + file.Name;
                    FileInfo fileInfo = new FileInfo(name);
                    fileInfo.Directory.Create();
                    Console.WriteLine($"正在写入{name}");
                    File.WriteAllBytes(name, file.Data);
                }
                Console.WriteLine("写入成功");
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("输入任意键继续");
            Console.ReadKey();
        }
    }
}
