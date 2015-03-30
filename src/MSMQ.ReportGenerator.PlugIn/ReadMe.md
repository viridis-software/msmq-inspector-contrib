Creating the Plug In

This is the documentation of step by step how to create a plug in for MSMQ Inspector that will iterate a queue and produce a report of all the messages in it. 

This scenario is probably most likely to come up in an error queue where you simply want to see all the data, then you can perform diagnosis etc.

I will do this in VS.NET.

## Create a New Project

New Project - target **.NET Framework 4**

Windows Desktop - Class Library

I am calling it `MSMQ.ReportGenerator.PlugIn`

We need to make sure we are creating a 32 bit application:

* Open the **Project Properties**
* Select "All Configurations" as this applies to debug and release builds
* Change the **Platform Target** to **x86**

## Essential References

Next we will need to reference at least the following to get anywhere:

* System.Messaging
* ViridisSoftware.MsmqInspector.GUI.Core - I browse straight to "C:\Program Files (x86)\MSMQ Inspector\ViridisSoftware.MsmqInspector.GUI.Core.dll"

## Pre-Build Event

Next we can add a "pre-build" event to copy the MSMQ Inspector files to the current build folder.
The reason for this is so that you can simply go to you `bin\Degug` folder and run the application and it will look locally for your DLL. 

If I build the project now I get output similar to:

	1>------ Build started: Project: MSMQ.ReportGenerator.PlugIn, Configuration: Debug Any CPU ------
	1>  C:\Program Files (x86)\MSMQ Inspector\App.ico
	1>  C:\Program Files (x86)\MSMQ Inspector\ChangeLog.txt
	1>  C:\Program Files (x86)\MSMQ Inspector\License-Icons.txt
	1>  C:\Program Files (x86)\MSMQ Inspector\License-ICSharpCode.TextEditor.txt
	1>  C:\Program Files (x86)\MSMQ Inspector\License-MSMQInspector.txt
	1>  C:\Program Files (x86)\MSMQ Inspector\License-WeifenLuo.WinFormsUI.Docking.txt
	1>  C:\Program Files (x86)\MSMQ Inspector\MSMQInspector.exe
	1>  C:\Program Files (x86)\MSMQ Inspector\MSMQInspector.exe.config
	1>  C:\Program Files (x86)\MSMQ Inspector\ReadMe.htm
	1>  C:\Program Files (x86)\MSMQ Inspector\unins000.dat
	1>  C:\Program Files (x86)\MSMQ Inspector\unins000.exe
	1>  C:\Program Files (x86)\MSMQ Inspector\ViridisSoftware.MsmqInspector.GUI.Core.dll
	1>         12 file(s) copied.
	1>  MSMQ.ReportGenerator.PlugIn -> msmq-inspector-contrib\src\MSMQ.ReportGenerator.PlugIn\bin\Debug\MSMQ.ReportGenerator.PlugIn.dll
	========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========

## Creating the Plug In Loader

The application will look for DLL's that contain "loaders". 
These are classes that implement `MsmqInspector.GUI.Core.IPlugIn` interface and this is where you hook into the system and add menu items, register commands etc.

The interface looks like this but ther is a base class (`PluginLoaderBase`) for the boiler plate code:

	namespace MsmqInspector.GUI.Core
	{
	  public interface IPlugIn
	  {
	    string PluginDescription { get; }
	
	    string PluginName { get; }
	
	    int RequestedLoadOrder { get; }
	
	    void InitializePlugIn();
	
	    void LoadPlugIn(IApplicationServices services);
	
	    void UnloadPlugIn();
	  }
	}

All fairly standard information.

A bare bones loader could look like this:

	using System;
	using MsmqInspector.GUI.Core;
	
	namespace MSMQ.ReportGenerator.PlugIn
	{
	    public class ReportGeneratorLoader : PluginLoaderBase, IPlugIn
	    {
	        public ReportGeneratorLoader() : 
	            base("Report Generator", "Generates a report of the contents of all the messages in a queue", 1000)
	        {
	        }
	
	        public override void InitializePlugIn()
	        {
	        }
	    }
	}

When the application loads a plug in, a call to `LoadPlugIn(IApplicationServices services)` is made and the services are passed in. 
The `IApplicationServices` interface is an application shell related item. 
It has pointers to other core parts of the application such as the "Host Window" (`IHostWindow`).
When setting up a plugin most of the interactions will be with either the ***application services*** to register commands and services, or with the ***host window*** to add menu items for example.

To check that the plug in loader is working as expected lets add a call to `IHostWindow.DisplayMessageBox`.
This is a wrapper call to MessageBox.Show so nothing too exciting but will show us that things are working.

Modufy the `InitializePlugIn` method and build:

    public override void InitializePlugIn()
    {
        Services.HostWindow.DisplayMessageBox(
            null /* not relevenat */, 
            "my plugin" /* message text */, 
            "test" /* caption */, 
            MessageBoxButtons.OK,
            MessageBoxIcon.Information, 
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly, 
            null, 
            null);
    }

To run the application go to the build folder, e.g. "src\MSMQ.ReportGenerator.PlugIn\bin\Debug" and if the pre-build events are setup you should have a copy of *MSMQ Inspector* in the folder. 
Simply run the application and you should see the message bos show up.

![The message box displayed while initializing](My-plugin-message-box.png)

If you can't see the message box make sure that you are running the Premium edition of the software (required to run external plugins) and that the Option for "loadExternalPlugins" is set to true.

