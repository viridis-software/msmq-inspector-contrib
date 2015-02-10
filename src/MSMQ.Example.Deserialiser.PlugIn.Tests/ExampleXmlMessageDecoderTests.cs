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
    public class ExampleXmlMessageDecoderTests
    {
        [Fact]
        public void SimpleTransform()
        {
            const string xml = @"
                <SubmitMessage>
                  <request>foo</request>
                </SubmitMessage>";
            var ms = new MemoryStream(Encoding.ASCII.GetBytes(xml));

            var target = new ExampleXmlMessageDecoder(ms);
            var result = target.TransformToString();

            //Console.WriteLine(result);
            Assert.Contains("<request>foo</request>", result);

            // The example decoder simply adds a comment at the end:
            Assert.Contains("<!-- test -->", result);
        }
    }
}