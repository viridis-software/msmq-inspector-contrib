using System;
using System.Windows.Forms;
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
    }
}
