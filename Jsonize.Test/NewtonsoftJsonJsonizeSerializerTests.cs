using System.Threading.Tasks;
using FluentAssertions;
using Jsonize.Test.Fixtures;
using Xunit;

namespace Jsonize.Test
{
    public class NewtonsoftJsonJsonizeSerializerTests : IClassFixture<NewtonsoftJsonJsonizeSerializerTestFixture>
    {
        private readonly NewtonsoftJsonJsonizeSerializerTestFixture _testFixture;

        public NewtonsoftJsonJsonizeSerializerTests(NewtonsoftJsonJsonizeSerializerTestFixture testFixture)
        {
            _testFixture = testFixture;
        }

        [Fact]
        public async Task Serialize_DefaultSettings_SerializesCorrectly()
        {
            var actual = await _testFixture.JsonizeSerializer.Serialize(JsonizeNodeTestResources.HtmlBodyP);

            actual.Should().Be(StringResources.HtmlBodyPResult);
        }
    }
}