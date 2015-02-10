#region License

// Copyright 2015 VIRIDIS SOFTWARE PTY LTD (http://viridissoftware.com.au/Products/MsmqInspector/). All rights reserved.
// See: http://viridissoftware.com.au/Products/MsmqManager/License
// See: https://github.com/viridis-software/msmq-inspector-contrib

#endregion

using System;
using MsmqInspector.GUI.Core;
using MsmqInspector.GUI.Core.Formatting;

namespace MSMQ.Example.Deserialiser.PlugIn
{
    /// <summary>
    /// An example plugin loader.
    /// </summary>
    /// <remarks>
    /// The project will need to reference:
    ///  * System.Messaging
    ///  * ViridisSoftware.MsmqInspector.GUI.Core
    /// 
    /// To aid testing etc, add a "pre-build" event such as:
    ///   COPY /Y "C:\Program Files (x86)\MSMQ Inspector\*.*" $(TargetDir)"
    /// 
    /// This will copy the application to the build folder first, your build puts the DLL next to it and then you can run the
    /// application that will look for files called "*.PlugIn.dll".
    /// 
    /// An easy way to run in debug is to use "bin\Debug\MSMQInspector.exe" as the Debug / Start External Program value in the
    /// project properties.
    /// </remarks>
    public class ExampleXmlMessageDecoderLoader : PluginLoaderBase, IPlugIn
    {
        public ExampleXmlMessageDecoderLoader()
            : base(
                "Example XML Message Decoder Plugin",
                "Example",
                500)
        {
        }

        /// <summary>
        /// Initializes the plug in. This is where you can add services etc.
        /// </summary>
        public override void InitializePlugIn()
        {
            // Get an instance of the IMessageDecoderFactory:
            var messageDecoderFactory = MsmqInspector.GUI.Core.ApplicationServices.Instance.Resolve<IMessageDecoderFactory>();

            // Register the custom decoder - the name "xml2" will show up in the deserialiser dropdown when viewing a message:
            messageDecoderFactory.Register("xml2", typeof (ExampleXmlMessageDecoder));
        }
    }
}