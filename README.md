# msmq-inspector-contrib

Example code for extending the behaviour of MSMQ Inspector.

We do make the bold assumption that you have a copy of the software installed (version of at least 1.2.0.77) - see [http://viridissoftware.com.au/Products/MSMQManager/](http://viridissoftware.com.au/Products/MSMQManager/) and have MSMQ configured on the development machine.

The current example is a plugin that adds a custom message deserialiser (or message decoder).

Key things to keep in mind:

- The DLL must be 32 Bit
- It needs to be signed
- The filename by default should be "*.Plugin.dll" (this is what the application looks for at startup)

The example was coded and tested using Visual Studio 2013, Xamarin Studio and SharpDevelop.

See the code comments for more detail.

- The `ExampleXmlMessageDecoderLoader` gets called during start up - this is where you register services etc
- The `ExampleXmlMessageDecoder` does the work
- The `ExampleXmlMessageDecoderTests` just inject an XML stream as an example


