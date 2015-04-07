using System;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Windows.Forms;
using MsmqInspector.GUI.Core;
using MsmqInspector.GUI.Core.Commands;
using MsmqInspector.GUI.Core.Services;
using Message = System.Messaging.Message;
using WeifenLuo.WinFormsUI.Docking;

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
            // We will be using this a bit:
            var queueInspector = HostWindow.QueueInspector;

            // Get the "Format Name":
            var formatName = queueInspector.MessageQueueContext.FormatName;

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
            var preferredDeserialiser = HostWindow.QueueInspector.GetPreferredDeserialiserForQueue(
                queueInspector.MessageQueueContext.Name);

            // Get an instance of the rendering service:
            var messageBodyRenderService = Services.Resolve<IMessageBodyRenderService>();

            // Store up the report:
            var sb = new StringBuilder();

            // Some basic report details:
            sb.Append("Server: ");
            sb.AppendLine(queueInspector.MessageQueueContext.MachineName);
            sb.Append("Queue:  ");
            sb.AppendLine(queueInspector.MessageQueueContext.QueueName);
            sb.Append("Count:  ");
            sb.AppendLine(listCommand.Result.Count.ToString());
            sb.AppendLine();
            sb.AppendLine("---");
            sb.AppendLine();

            // Grab the first message - render the contents
            foreach (Message message in listCommand.Result)
            {
                // We need the message data in byte format, convert the stream to bytes:
                byte[] bytes;
                using (var sr = new StreamReader(message.BodyStream, true))
                {
                    var length = (int) sr.BaseStream.Length;
                    bytes = new byte[length];
                    sr.BaseStream.Read(bytes, 0, length);
                }

                // Use the Render Service to change the message bytes into a string:
                var msg = messageBodyRenderService.Render(bytes, preferredDeserialiser, null);

                sb.Append("Arrived Time: ");
                sb.AppendLine(message.ArrivedTime.ToString("u"));
                sb.AppendLine("Message Body:");
                sb.AppendLine();
                sb.AppendLine(msg);
                sb.AppendLine();
                sb.AppendLine("---");
                sb.AppendLine();
            }

            // Get an instance of basic text editor:
            var editor = Services.Resolve<IEditor>("txt-editor");

            // Set "all the text" of the editor window the the generated report:
            editor.AllText = sb.ToString();

            // This command displays the editor window in the application:
            HostWindow.DisplayDockedForm(editor as DockContent);
        }
    }
}