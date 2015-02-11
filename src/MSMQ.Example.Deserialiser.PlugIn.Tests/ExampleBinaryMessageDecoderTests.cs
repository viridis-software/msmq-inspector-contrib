#region License

// Copyright 2015 VIRIDIS SOFTWARE PTY LTD (http://viridissoftware.com.au/Products/MsmqInspector/). All rights reserved.
// See: http://viridissoftware.com.au/Products/MsmqManager/License
// See: https://github.com/viridis-software/msmq-inspector-contrib

#endregion

using System;
using System.IO;
using System.Text;
using Xunit;

namespace MSMQ.Example.Deserialiser.PlugIn.Tests
{
    public class ExampleBinaryMessageDecoderTests
    {
        [Fact]
        public void SimpleTransform()
        {
            byte[] bytes = new [] { (byte)65, (byte)66, (byte)67 };
            var ms = new MemoryStream(bytes);

            var target = new ExampleBinaryMessageDecoder(ms);
            var result = target.TransformToString();

            Assert.Equal("65,66,67", result);
        }
    }
}