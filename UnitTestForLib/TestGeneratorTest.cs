using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MainPart;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System;
using TestGeneratorLib;

namespace UnitTestForLib
{
    public class TestGeneratorTest
    {
        string SourceFilesPath = "D:\\VS Projects\\SPP_4\\UnitTestForLib\\Files";
        string GeneratedFilesPath = "D:\\VS Projects\\SPP_4\\UnitTestForLib\\Files\\Generated";

        string[] files;
        string[] sourceCodes = new string[2];

        string[] generatedFiles;

        [SetUp]
        public void Setup()
        {
            files = Directory.GetFiles(SourceFilesPath);
            using (var reader = new StreamReader(files[0]))
            {
                sourceCodes[0] = reader.ReadToEnd();
            }
            using (var reader = new StreamReader(files[1]))
            {
                sourceCodes[1] = reader.ReadToEnd();
            }
        }

        [Test]
        public void GeneratedFilesNumberTest()
        {
            if (!Directory.Exists(GeneratedFilesPath))
            {
                Directory.CreateDirectory(GeneratedFilesPath);
            }
            Task task = new Pipeline().Generate(files, GeneratedFilesPath);
            task.Wait();
            generatedFiles = Directory.GetFiles(GeneratedFilesPath);
            DirectoryInfo di = new DirectoryInfo(GeneratedFilesPath);
            FileInfo[] oldFiles = di.GetFiles();
            foreach (FileInfo f in oldFiles)
            {
                f.Delete();
            }
            Directory.Delete(GeneratedFilesPath);
            Assert.AreEqual(generatedFiles.Length, 3, "Wrong number of generated files.");
        }

        [Test]
        public void GeneratedFilesNamesTest()
        {
            if (!Directory.Exists(GeneratedFilesPath))
            {
                Directory.CreateDirectory(GeneratedFilesPath);
            }
            Task task = new Pipeline().Generate(files, GeneratedFilesPath);
            task.Wait();
            generatedFiles = Directory.GetFiles(GeneratedFilesPath);
            Assert.IsTrue("\\TestClass1Test.cs".Equals(generatedFiles[0].Substring(generatedFiles[0].LastIndexOf("\\")))
                && "\\TestClass21Test.cs".Equals(generatedFiles[1].Substring(generatedFiles[0].LastIndexOf("\\")))
                && "\\TestClass22Test.cs".Equals(generatedFiles[2].Substring(generatedFiles[0].LastIndexOf("\\"))));
        }
    }
}