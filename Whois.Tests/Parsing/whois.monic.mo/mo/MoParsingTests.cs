using NUnit.Framework;
using Whois.Models;
using Whois.Parsers;

namespace Whois.Parsing.Whois.Monic.Mo.Mo
{
    [TestFixture]
    public class MoParsingTests : ParsingTests
    {
        private WhoisParser parser;

        [SetUp]
        public void SetUp()
        {
            SerilogConfig.Init();

            parser = new WhoisParser();
        }

        [Test]
        public void Test_not_found()
        {
            var sample = SampleReader.Read("whois.monic.mo", "mo", "not_found.txt");
            var response = parser.Parse("whois.monic.mo", "mo", sample);

            Assert.Greater(sample.Length, 0);
            Assert.AreEqual(WhoisResponseStatus.NotFound, response.Status);

            Assert.AreEqual(0, response.ParsingErrors);
            Assert.AreEqual("whois.monic.mo/mo/NotFound", response.TemplateName);

            Assert.AreEqual("u34jedzcq.mo", response.DomainName);

            Assert.AreEqual(2, response.FieldsParsed);
        }

        [Test]
        public void Test_found()
        {
            var sample = SampleReader.Read("whois.monic.mo", "mo", "found.txt");
            var response = parser.Parse("whois.monic.mo", "mo", sample);

            Assert.Greater(sample.Length, 0);
            Assert.AreEqual(WhoisResponseStatus.Found, response.Status);

            Assert.AreEqual(0, response.ParsingErrors);
            Assert.AreEqual("whois.monic.mo/mo/Found", response.TemplateName);

            Assert.AreEqual("umac.mo", response.DomainName);

            // Registrar Details
            Assert.AreEqual("MONIC", response.Registrar.Name);
            Assert.AreEqual("whois.monic.mo", response.Registrar.WhoisServerUrl);

            // Nameservers
            Assert.AreEqual(2, response.NameServers.Count);
            Assert.AreEqual("umacsn1.umac.mo", response.NameServers[0]);
            Assert.AreEqual("umacsn2.umac.mo", response.NameServers[1]);

            Assert.AreEqual(6, response.FieldsParsed);
        }
    }
}
