using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Extensions
{
    public static class FileManage
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image/");
        }
        public static bool IsLengthMatches(this IFormFile file, int mb)
        {
            return file.Length / 1024 / 1024 <= mb;
        }
        //public static string SaveImage(this IFormFile file, string root, string folder)
        //{
        //    string filename = Guid.NewGuid().ToString() + file.FileName;
        //    string pathroot = Path.Combine(root,folder);
        //    string fullpath = Path.Combine(pathroot, filename);
        //    using (FileStream filestream = new FileStream(filename, FileMode.Create))
        //    {
        //        file.CopyTo(filestream);
        //    }
        //    return filename;
        //}
        public static string SaveImg(this IFormFile file, string root, string folder)
        {
            string rootPath = Path.Combine(root, folder);
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string fullPath = Path.Combine(rootPath, fileName);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return fileName;
        }
    }
}

