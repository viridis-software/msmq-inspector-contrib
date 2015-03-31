using System;
using System.Windows.Forms;
using MsmqInspector.GUI.Core.Commands;

namespace MSMQ.ReportGenerator.PlugIn
{
    public class GenerateQueueMessageReportCommand : AppCommandBase
    {
        public GenerateQueueMessageReportCommand()
            : base("Generate Queue Message Report")
        {
        }

        public override void Execute()
        {
            var formatName = HostWindow.QueueInspector.MessageQueueContext.FormatName;

            Services.HostWindow.DisplayMessageBox(
                null /* not relevenat */,
                formatName /* message text */,
                "queue report command" /* caption */,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly,
                null,
                null);
        }
    }
}