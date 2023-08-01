using System;
using Markdown.Generator.Core.Markdown.Elements;
using Xunit;


namespace Markdown.Generator.Core.Tests
{
    public class ElementsTests
    {
        private string nL = Environment.NewLine;
        
        [Fact]
        public void Given_Code_When_LanguageAndCodeAsParameter_Then_ReturnMarkdownCodeMarkup()
        {
            // Arrange
            var expected = $"```csharp{nL}some code{nL}```{nL}";

            // Act
            var c = new Code("csharp", "some code");
            string actual = c.Create();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Given_CodeQuote_When_CodeAsParameter_Then_ReturnMarkdownCodeMarkup()
        {
            var expected = "`code`";

            var cQ = new CodeQuote("code");
            string actual = cQ.Create();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Given_Link_When_TextAndUrlAsParameter_Then_ReturnMarkdownLinkMarkup()
        {
            var expected = "[title](url)";

            var l = new Link("title", "url");
            string actual = l.Create();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Given_Image_When_TextAndImageUrlAsParameter_Then_ReturnMarkdownImageMarkup()
        {
            var expected = "![title](url)";

            var im = new Image("title", "url");
            string actual = im.Create();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Given_List_When_TextAsParameter_Then_ReturnMarkdownListMarkup()
        {
            // Link is treated like ordinary text in list.Create(), so no need for checking
            var expectedText = $"- list{nL}";

            var listText = new List("list");
            string actualText = listText.Create();

            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public void Given_Header_When_LevelAndTextAsParameter_Then_ReturnMarkdownHeaderMarkup()
        {
            // Link, Header are treated like ordinary texts in header.Create(), so no need for checking
            var expected = $"## header{nL}";

            var header = new Header(2, "header");
            string actual = header.Create();

            Assert.Equal(expected, actual);
        }
    }
}