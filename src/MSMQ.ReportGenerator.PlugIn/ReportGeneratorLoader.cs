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
            Services.HostWindow.QueueInspector.QueueMenu.Items.Add(
                CommandControlBuilder.CreateToolStripMenuItem<GenerateQueueMessageReportCommand>());
        }
    }
}
