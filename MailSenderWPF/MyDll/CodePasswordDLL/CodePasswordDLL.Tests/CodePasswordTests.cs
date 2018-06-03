using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Debug.WriteLine("Test Started");
        }
        [TestMethod]
        public void GetCodPassword_abc_bcd()
        {
            string strActual = CodePassword.GetCodPassword(strIn);
            Assert.AreEqual(strExpected, strActual, "не равны");
            Debug.WriteLine("Кодирование прошло успешно");
        }
        [TestMethod()]
        public void GetPassword_bcd_abc()
        {

            string strActual = CodePassword.GetPassword(strExpected);
            Assert.AreEqual(strIn, strActual, "не равны");
            Debug.WriteLine("Расшифровка успешно выполнена");
        }
        [TestMethod]
        public void GetCodPassword_empty_empty()
        { 
            strIn = "";
            strExpected = "";
            string strActual = CodePassword.GetPassword(strIn);
            Assert.AreEqual(strExpected, strActual, "не равны");
            Debug.WriteLine("Тест на пустые строки");
        }



        [TestMethod]
        public void GetCodPasswordString_abc_bcd()
        {
            string strActual = CodePassword.GetCodPassword(strIn);
            StringAssert.Contains(strExpected, strActual, "не равны");
            Debug.WriteLine("Кодирование прошло успешно");
        }
        [TestMethod()]
        public void GetPasswordString_bcd_abc()
        {

            string strActual = CodePassword.GetPassword(strExpected);
            StringAssert.Contains(strIn, strActual, "не равны");
            Debug.WriteLine("Расшифровка успешно выполнена");
        }
        [TestMethod]
        public void GetCodPasswordString_empty_empty()
        {
            strIn = "";
            strExpected = "";
            string strActual = CodePassword.GetPassword(strIn);
            StringAssert.Contains(strExpected, strActual, "не равны");
            Debug.WriteLine("Тест на пустые строки");
        }



        [TestCleanup]
        public void TestClean()
        {
            Debug.Write("Clean");
        }
    }
}
