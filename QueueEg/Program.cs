using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;


namespace QueueEg
{
    class Program
    {
        static void Main(string[] args)
        {   //Creates connection string
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=queueeg;AccountKey=B6/042lURYTDyETGKOVR23yepzQ78sqQpue93Acs1Aw0cAJMJ3DsKaXbdqFz3NzhKwhCalu9wXmQqqw9zkQTJA==");
            
            //Creates the queue client
            CloudQueueClient queueClient = cloudStorageAccount.CreateCloudQueueClient();

            //Retrieves a reference to queue
            CloudQueue queue = queueClient.GetQueueReference("myqueue");

            //Creates queue if it does not exists
            queue.CreateIfNotExists();

            //Inserting a message into the queue
            CloudQueueMessage message = new CloudQueueMessage("Hello, World");
            queue.AddMessage(message);

            //Peek at the next message
            CloudQueueMessage peekedMessage = queue.PeekMessage();
            Console.WriteLine(peekedMessage.AsString);


            //Gets message from queue and updates it.
            CloudQueueMessage new_message = queue.GetMessage();
            new_message.SetMessageContent("updated content");
            queue.UpdateMessage(new_message,
            TimeSpan.FromSeconds(0.0), //makes it visible immediately
            MessageUpdateFields.Content | MessageUpdateFields.Visibility);

            //Retrieving length of the queue
            queue.FetchAttributes();
            int? cachedMessageCount = queue.ApproximateMessageCount;
            Console.WriteLine("Number of messages in queue" + cachedMessageCount);

            //Deleting a queue
            //queue.Delete();


            






                
    
        





        }
    }
}
