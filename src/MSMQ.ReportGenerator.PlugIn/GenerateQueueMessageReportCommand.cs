using System;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Windows.Forms;
using MsmqInspector.GUI.Core;
using MsmqInspector.GUI.Core.Commands;
using MsmqInspector.GUI.Core.Services;
using Message = System.Messaging.Message;

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
            // Get the "Format Name":
            var formatName = HostWindow.QueueInspector.MessageQueueContext.FormatName;

            // Get an instance of "IMsmqQueueManager" for better access to the queues:
            var msmqQueueManager = Services.Resolve<IMsmqQueueManager>();

            // Set the path to the format name of the selected queue:
            msmqQueueManager.QueuePath = formatName;

            // Create an MSMQ List Command - a wrapper that peaks the message data on the queue:
            var listCommand = new MsmqListCommand();

            // By default the body of the message is not loaded, set "DisplayBody" to true and 
            // this will be retrieved with the message:
            listCommand.DisplayBody = true;
            
            // Execute the list command, this *will* enumerate the messages in the queue:
            listCommand.Execute(msmqQueueManager);

            // Use the "Name" of the queue to lookup the preferred deserialiser (if any), the default
            // is "guess" (note that the format of the key used to lookup the Preferred Deserialiser
            // is "<machine>\<queue-name>", e.g. "server\private$\orders".
            var preferredDeserialiser = Services.HostWindow.QueueInspector.GetPreferredDeserialiserForQueue(
                HostWindow.QueueInspector.MessageQueueContext.Name);

            // Grab the first message - render the contents
            var message = listCommand.Result.First();

            // Get an instance of the rendering service:
            var messageBodyRenderService = Services.Resolve<IMessageBodyRenderService>();

            // We need the message data in byte format, convert the stream to bytes:
            byte[] bytes;
            using (var sr = new StreamReader(message.BodyStream, true))
            {
                var length = (int)sr.BaseStream.Length;
                bytes = new byte[length];
                sr.BaseStream.Read(bytes, 0, length);
            }

            // Use the Render Service to change the message bytes into a string:
            var msg = messageBodyRenderService.Render(bytes, preferredDeserialiser, null);

            // For now lets just report the Count:
            Services.HostWindow.DisplayMessageBox(
                null /* not relevant */,
                msg /* message text */,
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