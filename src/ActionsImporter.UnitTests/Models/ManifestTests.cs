using System.Text.Json;
using ActionsImporter.Models.Docker;
using NUnit.Framework;

namespace ActionsImporter.UnitTests.Models;

[TestFixture]
public class ManifestTests
{
    private readonly string manifestResult = @"
{
        ""schemaVersion"": 2,
        ""mediaType"": ""application/vnd.docker.distribution.manifest.v2+json"",
        ""config"": {
                ""mediaType"": ""application/vnd.docker.container.image.v1+json"",
                ""size"": 8933,
                ""digest"": ""sha256:4256ea72fd01deac3e967f6b19f907587dcd6f0a976301f1aecc73dc6f146a4a""
        },
        ""layers"": [
                {
                        ""mediaType"": ""application/vnd.docker.image.rootfs.diff.tar.gzip"",
                        ""size"": 2811969,
                        ""digest"": ""sha256:540db60ca9383eac9e418f78490994d0af424aab7bf6d0e47ac8ed4e2e9bcbba""
                },
                {
                        ""mediaType"": ""application/vnd.docker.image.rootfs.diff.tar.gzip"",
                        ""size"": 1218679,
                        ""digest"": ""sha256:98a867505730167ce0636f0811cb765ebbde11bf979c1a9839f6915f2fc3b85b""
                },
                {
                        ""mediaType"": ""application/vnd.docker.image.rootfs.diff.tar.gzip"",
                        ""size"": 222,
                        ""digest"": ""sha256:69c77620f6108dc0610cba72f77d68c271fb1b14cb0800a7a8b6aaeb8950fec9""
                },
                {
                        ""mediaType"": ""application/vnd.docker.image.rootfs.diff.tar.gzip"",
                        ""size"": 23207774,
                        ""digest"": ""sha256:9b370d66bb9903810edd365042a2f34b7a59947c942741ab947cec1b00dc738d""
                },
                {
                        ""mediaType"": ""application/vnd.docker.image.rootfs.diff.tar.gzip"",
                        ""size"": 171,
                        ""digest"": ""sha256:d9f4ad4e4f54edfb60dcbf3da39a0d79bb818be470a76b7aa5d940214fd6817b""
                },
                {
                        ""mediaType"": ""application/vnd.docker.image.rootfs.diff.tar.gzip"",
                        ""size"": 26977223,
                        ""digest"": ""sha256:d75779add5fb23230f53fc7b04edd78f0ac04328d54ae8b10ffc24ae8ce53fae""
                },
                {
                        ""mediaType"": ""application/vnd.docker.image.rootfs.diff.tar.gzip"",
                        ""size"": 442547,
                        ""digest"": ""sha256:8e1571077aa30d750261fb58d29aca90235515a7ee2b35160bc907bfed0a0353""
                },
                {
                        ""mediaType"": ""application/vnd.docker.image.rootfs.diff.tar.gzip"",
                        ""size"": 4346366,
                        ""digest"": ""sha256:c7ca3c3f89d58546450869925f28bcb901a395d5b2f7fbf013bba9d8bca353af""
                },
                {
                        ""mediaType"": ""application/vnd.docker.image.rootfs.diff.tar.gzip"",
                        ""size"": 32,
                        ""digest"": ""sha256:4f4fb700ef54461cfa02571ae0db9a0dc1e0cdb5577484a6d75e68dc38e8acc1""
                }
        ]
}";
    [Test]
    public void Initialize()
    {
        Manifest? manifest = JsonSerializer.Deserialize<Manifest>(manifestResult);

        Assert.AreEqual(2, manifest?.SchemaVersion);
        Assert.AreEqual("application/vnd.docker.distribution.manifest.v2+json", manifest?.MediaType);
        Assert.IsInstanceOf(typeof(ManifestConfig), manifest?.Config);
        Assert.AreEqual(9, manifest?.Layers?.Count);
        Assert.IsInstanceOf(typeof(ManifestConfig), manifest?.Layers?[0]);
    }

    [Test]
    public void GetDigest()
    {
        Manifest? manifest = JsonSerializer.Deserialize<Manifest>(manifestResult);

        Assert.AreEqual("4256ea72fd01deac3e967f6b19f907587dcd6f0a976301f1aecc73dc6f146a4a", manifest?.GetDigest());
    }
}
