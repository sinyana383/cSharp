using System.Linq;
using Markdown.Generator.Core.Markdown;
using Markdown.Generator.Core.Markdown.Elements;
using Xunit;

namespace Markdown.Generator.Core.Tests
{

    public class MarkdownBuilderTests
    {
        [Fact]
        public void Given_MarkdownBuilder_When_CallingCodeQuote_Then_CodeQuoteFallIntoElementsCollection()
        {
            int expectedCount = 1;
            var mB = new MarkdownBuilder();

            mB.CodeQuote("codeQuote1");

            Assert.Equal(expectedCount, mB.Elements.Count());
            Assert.Contains(mB.Elements, item => Equals(item.GetType(), typeof(CodeQuote)));
        }

        [Fact]
        public void Given_MarkdownBuilder_When_CallingCode_Then_CodeFallIntoElementsCollection()
        {
            int expectedCount = 1;
            var mB = new MarkdownBuilder();

            mB.Code("csharp", "code");

            Assert.Equal(expectedCount, mB.Elements.Count());
            Assert.Contains(mB.Elements, item => Equals(item.GetType(), typeof(Code)));
        }

        [Fact]
        public void Given_MarkdownBuilder_When_CallingLink_Then_LinkFallIntoElementsCollection()
        {
            int expectedCount = 1;
            var mB = new MarkdownBuilder();

            mB.Link("title", "url");

            Assert.Equal(expectedCount, mB.Elements.Count());
            Assert.Contains(mB.Elements, item => Equals(item.GetType(), typeof(Link)));
        }

        [Fact]
        public void Given_MarkdownBuilder_When_CallingHeader_Then_HeaderFallIntoElementsCollection()
        {
            int expectedCount = 1;
            var mB = new MarkdownBuilder();

            mB.Header(3, "header");

            Assert.Equal(expectedCount, mB.Elements.Count());
            Assert.Contains(mB.Elements, item => Equals(item.GetType(), typeof(Header)));
        }
    }
}