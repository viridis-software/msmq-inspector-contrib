#region License

// Copyright 2015 VIRIDIS SOFTWARE PTY LTD (http://viridissoftware.com.au/Products/MsmqInspector/). All rights reserved.
// See: http://viridissoftware.com.au/Products/MsmqManager/License
// See: https://github.com/viridis-software/msmq-inspector-contrib

#endregion

using System;
using System.IO;
using MsmqInspector.GUI.Core.Formatting;

namespace MSMQ.Example.Deserialiser.PlugIn
{
    /// <summary>
    /// An example decoder - reads the stream provided by the MSMQ Message and adds an XML comment to the end.
    /// </summary>
    /// <remarks>
    /// Ensure you have a reference to "System.Messaging".
    /// </remarks>
    public class ExampleXmlMessageDecoder : MessageDecoderBase, IMessageDecoder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleXmlMessageDecoder"/> class.
        /// </summary>
        public ExampleXmlMessageDecoder()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleXmlMessageDecoder"/> class.
        /// Used for testing.
        /// </summary>
        /// <param name="bodyStream">The body stream - injected for testing.</param>
        public ExampleXmlMessageDecoder(Stream bodyStream)
            : base(bodyStream)
        {
        }

        /// <summary>
        /// Sets a generic property value supplied by the GUI.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        public void SetProperty(string propertyName, string value)
        {
        }

        /// <summary>
        /// Transforms the message stream into and XML string.
        /// </summary>
        /// <returns>The XML of the message.</returns>
        public string TransformToString()
        {
            using (var sr = new StreamReader(GetMessageBodyStream()))
            {
                string ms = string.Empty;

                while (sr.Peek() >= 0)
                {
                    ms += sr.ReadLine() + Environment.NewLine;
                }
                ms += "<!-- test -->";
                Result = ms;

                return Result;
            }
        }
    }
}