using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodePasswordDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CodePasswordDLL.Tests
{
    [TestClass()]
    public class CodePasswordTests
    {
        string strIn;
        string strExpected;

        [TestInitialize()]
        public void TestInitialize()
        {
            strIn = "abc";
            strExpected = "bcd";
            Debug.WriteLine("Test started");
        }
        [TestMethod()]
        public void getCodPassword_abc_bcd()
        {

            // act
            string strActual = CodePassword.GetCodPassword(strIn);
            Debug.WriteLine("Test started");
            //assert
            Assert.AreEqual(strExpected, strActual);
        }

    }
}