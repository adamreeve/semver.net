using Xunit;
using Xunit.Extensions;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization;

namespace SemVer.Tests
{
    public class Serialization
    {
        [Fact]
        public void SerializeVersion()
        {
            var input = new Version("1.2.3-alpha2+test");
            var output = DeserializeFromXml<Version>(SerializeToXml(input));

            Assert.Equal(1, output.Major);
            Assert.Equal(2, output.Minor);
            Assert.Equal(3, output.Patch);
            Assert.Equal("alpha2", output.PreRelease);
            Assert.Equal("test", output.Build);
        }

        [Fact]
        public void SerializeVersionWithNewtonsoft()
        {
            var input = new Version("1.2.3-alpha2+test");
            var output = JsonConvert.DeserializeObject<Version>(JsonConvert.SerializeObject(input));

            Assert.Equal(1, output.Major);
            Assert.Equal(2, output.Minor);
            Assert.Equal(3, output.Patch);
            Assert.Equal("alpha2", output.PreRelease);
            Assert.Equal("test", output.Build);
        }

        private static string SerializeToXml<T>(T input)
        {
            using (var memoryStream = new MemoryStream())
            using(StreamReader reader = new StreamReader(memoryStream))
            {
                var serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(memoryStream, input);
                memoryStream.Position = 0;
                return reader.ReadToEnd();
            }
        }

        private static T DeserializeFromXml<T>(string xml)
        {
            var deserializer = new DataContractSerializer(typeof(T));
            var bytes = System.Text.Encoding.UTF8.GetBytes(xml);
            using (var memoryStream = new MemoryStream(bytes))
            {
                memoryStream.Position = 0;
                return (T)deserializer.ReadObject(memoryStream);
            }
        }
    }
}

