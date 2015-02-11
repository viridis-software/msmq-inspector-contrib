#region License

// Copyright 2015 VIRIDIS SOFTWARE PTY LTD (http://viridissoftware.com.au/Products/MsmqInspector/). All rights reserved.
// See: http://viridissoftware.com.au/Products/MsmqManager/License
// See: https://github.com/viridis-software/msmq-inspector-contrib

#endregion

using System;
using System.IO;
using System.Collections.Generic;
using MsmqInspector.GUI.Core.Formatting;

namespace MSMQ.Example.Deserialiser.PlugIn
{
    /// <summary>
    /// An example decoder - reads the stream provided by the MSMQ Message and creates a CVV of the bytes.
    /// </summary>
    /// <remarks>
    /// Ensure you have a reference to "System.Messaging".
    /// </remarks>
    public class ExampleBinaryMessageDecoder : MessageDecoderBase, IMessageDecoder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleBinaryMessageDecoder"/> class.
        /// </summary>
        public ExampleBinaryMessageDecoder()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleBinaryMessageDecoder"/> class.
        /// Used for testing.
        /// </summary>
        /// <param name="bodyStream">The body stream - injected for testing.</param>
        public ExampleBinaryMessageDecoder(Stream bodyStream)
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
        /// Transforms the message stream into simple comma seperated list of byte values.
        /// </summary>
        /// <returns>The byte values of the message.</returns>
        public string TransformToString()
        {
            using (var sr = new StreamReader(GetMessageBodyStream()))
            {
                var list = new List<string>();
                int x;

                while ((x = sr.BaseStream.ReadByte()) >= 0)
                {
                    list.Add(x.ToString());
                }

                Result = String.Join(",", list);

                return Result;
            }
        }
    }
}

