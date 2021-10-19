using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using System.Threading;

namespace Test
{
    class MainClass
    {
        static void Main(string[] args)
        {
            Console.WriteLine("HELLO");

            Thread server = new Thread(Server.ServerPart.StartServer);
            server.Start();
            Thread client = new Thread(Client.ClientPart.StartClient);
            client.Start();

            Thread.Sleep(2000);
            server.Join();
            client.Join();



            //AsymmetricalMessageStream_ yo = new AsymmetricalMessageStream_();
            //string a = yo.Encrypt("Data To Be Encrypted string");
            //Console.WriteLine(a);
            //string b = yo.Decrypt(a);
            //Console.WriteLine(b);



            //AsymmetricalRsaMessageStream client = new AsymmetricalRsaMessageStream();
            //AsymmetricalRsaMessageStream server = new AsymmetricalRsaMessageStream();

            //server.SetPublicKey(client.GetPublicKey());
            //client.SetPublicKey(server.GetPublicKey());

            //string message = server.Encrypt("Data To Be Encrypted!");
            //string d_message = client.Decrypt(message);
            //Console.WriteLine(message);
            //Console.WriteLine(d_message);

            Console.ReadKey();
        }
    }
}
