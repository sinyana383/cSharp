using Markdown.Generator.Core.Markdown;
using Xunit;

namespace Markdown.Generator.Core.Tests
{
    public class Sut
    {
        public void PublicMethod(){ }
        private void PrivateMethod(){ }

        private int _privateField;
        public int publicField;

        private int PrivateProperty { get; set; }
        public int PublicProperty { get; set; }
    }
    
    public class MarkdownableTypeTests
    {
        [Fact]
        public void Given_Markdownable_When_CallingGetMethods_Then_PublicMethods()
        {
            var mk = new MarkdownableType(typeof(Sut), null);

            var methods = mk.GetMethods();
            
            Assert.Single(methods);
            Assert.True(methods[0].IsPublic);
        }
        
        [Fact]
        public void Given_Markdownable_When_CallingGetFields_Then_PublicFields()
        {
            var mk = new MarkdownableType(typeof(Sut), null);

            var fields = mk.GetFields();
            
            Assert.Single(fields);
            Assert.True(fields[0].IsPublic);
        }
        
        [Fact]
        public void Given_Markdownable_When_CallingGetProperties_Then_PublicProperties()
        {
            var mk = new MarkdownableType(typeof(Sut), null);

            var properties = mk.GetProperties();
            
            Assert.Single(properties);
            Assert.Equal("PublicProperty", properties[0].Name);
        }
    }
}