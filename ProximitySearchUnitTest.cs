using System;
using NUnit.Framework;
using DilipDocuSignAssignment;

namespace ProximitySearchTest
{
    [TestFixture]
    public class ProximitySearchUnitTest
    {
        [Test]        
        public void ValidateNullInputArguments()
        {
            string result = Program.ValidateArguments(null);
            Assert.AreEqual(result, "No command line arguments found.");
        }

        [Test]
        public void ValidateNoInputArguments()
        {
            string result = Program.ValidateArguments(new string[0]);
            Assert.AreEqual(result, "No command line arguments found.");
        }

        [Test]
        [TestCase("ProximitySearc", "the", "canal", "0", "input1.txt", "Application 'ProximitySearc' cannot be found.")]
        [TestCase("DocuSearch", "the", "canal", "0", "input1.txt", "Application 'DocuSearch' cannot be found.")]
        [TestCase("", "the", "canal", "0", "input1.txt", "Application '' cannot be found.")]        
        public void ValidateWrongApplicationName(string appname, string keyword1, string keyword2, string range, string filename, string answer)
        {
            string result = Program.ValidateArguments(new string[]{appname, keyword1, keyword2, range, filename});            
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void ValidateFewerArgumentCount()
        {
            string result = Program.ValidateArguments(new string[] {"ProximitySearch", "the", "canal", "0"});
            Assert.AreEqual(result, "Fewer arguments passed. ProximitySearch expects the following arguments - <APPLICATION NAME> <KEYWORD1> <KEYWORD2> <RANGE> <FILE NAME>.");
        }

        [Test]
        public void ValidateExtraArgumentCount()
        {
            string result = Program.ValidateArguments(new string[] { "ProximitySearch", "the", "canal", "0", "test.txt", "extra" });
            Assert.AreEqual(result, "Too many arguments passed. ProximitySearch expects the following arguments - <APPLICATION NAME> <KEYWORD1> <KEYWORD2> <RANGE> <FILE NAME>.");
        }

        [Test]
        [TestCase("ProximitySearch", "the", "canal", "0", "input1.txt")]
        [TestCase("ProximitySearch", "the", "canal", "abcd", "inpu.txt")]
        [TestCase("ProximitySearch", "the", "canal", "%$#@", "inpu.txt")]
        public void ValidateCorrectArguments(string appname, string keyword1, string keyword2, string range, string filename)
        {
            string result = Program.ValidateArguments(new string[] { appname, keyword1, keyword2, range, filename });
            Assert.AreEqual(result, string.Empty);
        }

        [Test]
        [TestCase("the", "canal", "0", "input1.txt")]
        [TestCase("the", "canal", "-1", "input1.txt")]
        [TestCase("the", "canal", "1", "input1.txt")]
        [TestCase("the", "canal", "-9223372036854775807", "input2.txt")]
        [TestCase("the", "canal", "9223372036854775808", "input2")]
        [TestCase("the", "canal", "", "input2.txt")]
        public void ValidateWrongRange(string keyword1, string keyword2, string range, string filename)
        {
            ProximitySearch search = new ProximitySearch(keyword1, keyword2, range, filename);
            Assert.AreEqual(range + " is not a valid range. Range should be numeric and between 2 and 9223372036854775807.", search.PerformSearch());
        }

        [Test]
        [TestCase("the", "canal", "2", "input.txt")]
        [TestCase("the", "canal", "100", "inpu.txt")]
        [TestCase("the", "canal", "6", "input1.tx")]
        [TestCase("the", "canal", "9223372036854775807", "input2")]
        [TestCase("the", "canal", "9223372036854775807", "")]
        public void ValidateWrongFile(string keyword1, string keyword2, string range, string filename)
        {
            ProximitySearch search = new ProximitySearch(keyword1, keyword2, range, filename);            
            Assert.AreEqual("File '" + filename + "' does not exist.", search.PerformSearch());
        }

        [Test]
        [TestCase("the", "canal", "2", "input1.txt")]
        [TestCase("the", "canal", "1000", "input1.txt")]
        [TestCase("the", "canal", "6", "input1.txt")]
        [TestCase("the", "canal", "9223372036854775807", "input2.txt")]        
        public void ValidateCorrectInput(string keyword1, string keyword2, string range, string filename)
        {
            ProximitySearch search = new ProximitySearch(keyword1, keyword2, range, filename);
            long n;         
            bool isNumeric = long.TryParse(search.PerformSearch(), out n);
            Assert.AreEqual(isNumeric, true);
        }

        [Test]
        [TestCase("the", "canal", "6", "input1.txt", "3")]
        [TestCase("the", "canal", "3", "input1.txt", "1")]
        [TestCase("the", "canal", "6", "input2.txt", "11")]
        [TestCase("the", "canal", "2", "input2.txt", "3")]
        [TestCase("the", "canal", "3", "input2.txt", "3")]
        [TestCase("the", "canal", "9223372036854775807", "input1.txt", "3")]
        [TestCase("the", "docusign", "9223372036854775807", "input2.txt", "0")]
        [TestCase("dilip", "canal", "9223372036854775807", "input2.txt", "0")]
        [TestCase("dilip", "canal", "2", "input3.txt", "0")]
        [TestCase("the", "canal", "6", "input4.txt", "12")]
        [TestCase("the", "canal", "6", "input6.txt", "18")]
        [TestCase("the", "canal", "6", "input8.txt", "24")]
        [TestCase("the", "canal", "6", "input9.txt", "27")]
        [TestCase("the", "canal", "3", "input9.txt", "9")]
        [TestCase("the", "canal", "3", "input7.txt", "7")]
        [TestCase("the", "canal", "3", "input5.txt", "5")]
        [TestCase("the", "cana", "3", "input9.txt", "0")]
        [TestCase("t", "cana", "3", "input8.txt", "0")]
        [TestCase("the", "cana", "9223372036854775807", "input9.txt", "0")]
        [TestCase("the", "canal", "9223372036854775807", "input9.txt", "243")]        
        public void SearchCountTest(string keyword1, string keyword2, string range, string filename, string answer)
        {
            ProximitySearch search = new ProximitySearch(keyword1, keyword2, range, filename);            
            Assert.AreEqual(answer, search.PerformSearch());            
        }

    }
}