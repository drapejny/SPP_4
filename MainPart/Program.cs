using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MainPart
{
    class Program
    {
        static void Main(string[] args)
        {
            var pathToFolder = "D:\\VS Projects\\SPP_4\\MainPart\\Files";
            var pathToGenerated = "D:\\VS Projects\\SPP_4\\GeneratedTests\\GeneratedFiles";
            
            if (!Directory.Exists(pathToFolder))
            {
                Console.WriteLine($"Couldn't find directory {pathToFolder}");
                return;
            }
            if (!Directory.Exists(pathToGenerated))
            {
                Directory.CreateDirectory(pathToGenerated);
            }

            if (Directory.Exists(pathToGenerated))
            {
                DirectoryInfo di = new DirectoryInfo(pathToGenerated);
                FileInfo[] oldFiles = di.GetFiles();
                foreach(FileInfo f in oldFiles)
                {
                    f.Delete();
                }
            }

            var allFiles = Directory.GetFiles(pathToFolder);

            var files = from file in allFiles
                    where file.Substring(file.Length - 3) == ".cs"
                    select file;

            Task task =  new Pipeline().Generate(files, pathToGenerated);

            task.Wait();

            /*
            while (!task.IsCompleted)
            {
              
            }
             */
            Console.WriteLine("\nText files successfully generated and written");
        }
    }
}
