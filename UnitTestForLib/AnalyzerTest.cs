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
    public class AnalyzerTest
    {
        string Path = "D:\\VS Projects\\SPP_4\\UnitTestForLib\\Files";

        string[] files;
        string[] sourceCodes = new string[2];


        [SetUp]
        public void Setup()
        {
            files = Directory.GetFiles(Path);
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
        public void ClassesNumberTest()
        {
            TestGeneratorLib.Info.FileInfo fileInfo1 = CodeAnalyzer.GetFileInfo(sourceCodes[0]);
            TestGeneratorLib.Info.FileInfo fileInfo2 = CodeAnalyzer.GetFileInfo(sourceCodes[1]);
            Assert.IsTrue(fileInfo1.Classes.Count == 1 && fileInfo2.Classes.Count == 2);
        }

        [Test]
        public void ClassesNameTest()
        {
            TestGeneratorLib.Info.FileInfo fileInfo1 = CodeAnalyzer.GetFileInfo(sourceCodes[0]);
            TestGeneratorLib.Info.FileInfo fileInfo2 = CodeAnalyzer.GetFileInfo(sourceCodes[1]);
            Assert.IsTrue(fileInfo1.Classes[0].ClassName.Equals("TestClass1")
                && fileInfo2.Classes[0].ClassName.Equals("TestClass21")
                && fileInfo2.Classes[1].ClassName.Equals("TestClass22"));
        }

        [Test]
        public void MethodsNumberTest()
        {
            TestGeneratorLib.Info.FileInfo fileInfo1 = CodeAnalyzer.GetFileInfo(sourceCodes[0]);
            TestGeneratorLib.Info.FileInfo fileInfo2 = CodeAnalyzer.GetFileInfo(sourceCodes[1]);
            Assert.IsTrue(fileInfo1.Classes[0].Methods.Count == 1
                && fileInfo2.Classes[0].Methods.Count == 1
                && fileInfo2.Classes[1].Methods.Count == 1);
        }

        [Test]
        public void MethodsNameTest()
        {
            TestGeneratorLib.Info.FileInfo fileInfo1 = CodeAnalyzer.GetFileInfo(sourceCodes[0]);
            TestGeneratorLib.Info.FileInfo fileInfo2 = CodeAnalyzer.GetFileInfo(sourceCodes[1]);
            Assert.IsTrue(fileInfo1.Classes[0].Methods[0].Name.Equals("met")
                && fileInfo2.Classes[0].Methods[0].Name.Equals("met")
                && fileInfo2.Classes[0].Methods[0].Name.Equals("met"));
        }
    }
}