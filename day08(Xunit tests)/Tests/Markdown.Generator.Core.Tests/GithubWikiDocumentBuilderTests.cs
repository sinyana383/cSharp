using System;
using System.Reflection;
using Markdown.Generator.Core.Documents;
using Markdown.Generator.Core.Markdown;
using Xunit;
using Moq;

namespace Markdown.Generator.Core.Tests
{
    public class GithubWikiDocumentBuilderTests
    {
        [Fact]
        public void Given_GithubWikiDocumentBuilder_When_Generate_Then_twoParametersOnce()
        {
            var obj = new Mock<IMarkdownGenerator>();
            var dB = new GithubWikiDocumentBuilder<IMarkdownGenerator>(obj.Object);

            var types = new Type[] { typeof(String) };
            var folder = "test";
            
            dB.Generate(types, folder);
            
            obj.Verify(m => m.Load(types), Times.Once);
        }
        
        [Fact]
        public void Given_GithubWikiDocumentBuilder_When_Generate_Then_threeParametersOnce()
        {
            var obj = new Mock<IMarkdownGenerator>();
            var dB = new GithubWikiDocumentBuilder<IMarkdownGenerator>(obj.Object);

            var types = new Type[] { typeof(String) };
            var dllPath = "test/dll";
            var namespaceMatch = "testNamespace";
            var folder = "test";
            
            dB.Generate(dllPath, namespaceMatch, folder);
            
            obj.Verify(m => m.Load(dllPath, namespaceMatch), Times.Once);
        }
        
        [Fact]
        public void Given_GithubWikiDocumentBuilder_When_Generate_Then_AssemblyParametersOnce()
        {
            var obj = new Mock<IMarkdownGenerator>();
            var dB = new GithubWikiDocumentBuilder<IMarkdownGenerator>(obj.Object);

            var assemblies = new Assembly[] { Assembly.GetExecutingAssembly() };
            var namespaceMatch = "testNamespace";
            var folder = "test";
            
            dB.Generate(assemblies, namespaceMatch, folder);
            
            obj.Verify(m => m.Load(assemblies, namespaceMatch), Times.Once);
        }
    }
}