# msmq-inspector-contrib

Example code for extending the behaviour of MSMQ Inspector.

We do make the bold assumption that you have a copy of the software installed (version of at least 1.2.0.77) - see [http://viridissoftware.com.au/Products/MSMQManager/](http://viridissoftware.com.au/Products/MSMQManager/) and have MSMQ configured on the development machine.

Plugin use is restricted to the Premium licensed users for now.

The current example is a plugin that adds a custom message deserialiser (or message decoder).

Key things to keep in mind:

- Target .NET version 4
- The DLL must be 32 Bit
- It needs to be signed
- The filename by default should be "*.Plugin.dll" (this is what the application looks for at startup)

The example was coded and tested using Visual Studio 2013, Xamarin Studio and SharpDevelop.

See the code comments for more detail.

- The `ExampleXmlMessageDecoderLoader` gets called during start up - this is where you register services etc
- The `ExampleXmlMessageDecoder` does the work
- The `ExampleXmlMessageDecoderTests` just inject an XML stream as an example

Installing a Plugin
-------------------

The application will search for plugins (*.plugin.dll) in:

- The current program folder (e.g. `C:\Program Files (x86)\MSMQ Inspector` or the development folder)
- The common data path in the directory `C:\ProgramData\MSMQInspector`
- The local data path in the rirectory `C:\Users\(*username*)\AppData\Local\MSMQInspector`

A binding redirect is used to ensure that plugins build against an older assembly still load on newer versions.

Help!
-----

If you have a particular scenario you would like to implement please [Contact Us](http://viridissoftware.com.au/Home/Contact) with some details. 
There may be existing pieces you can plug together or a simple kick-start on where to look.
