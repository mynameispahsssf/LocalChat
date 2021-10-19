using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;

using System.IO;
using System.Threading;
using System.Security.Cryptography;


namespace Server
{
    class ServerPart
    {
        public static void StartServer()
        {
            using (NamedPipeServerStream server = new NamedPipeServerStream("TestServer", PipeDirection.InOut))
            {
                server.WaitForConnection();
                BaseMessageStream myStream = new SymetricalAesMessageStream(server);
                
                myStream.WriteStream("Let it break, let it break, let it break. Waste away waiting for change. Love denied. Dead inside. No need for compromise. Break away, break away, break away. No need to plan your escape. Get out tonight and save some pride. Get back in touch when the pain subsides.");
                

            }
        }
    }
}

namespace Client
{
    class ClientPart
    {
        public static void StartClient()
        {
            using (var client = new NamedPipeClientStream(".", "TestServer", PipeDirection.InOut))
            {
                client.Connect();
                BaseMessageStream myStream = new SymetricalAesMessageStream(client);

                Console.WriteLine(myStream.ReadStream());
            }
        }
    }
}


interface BaseMessageStream
{
    string ReadStream();
    void WriteStream(string str);
}

class UnencodedMessageStream : BaseMessageStream
{
    private Stream stream;
    StreamReader reader;

    public UnencodedMessageStream(Stream stream)
    {
        this.stream = stream;
        this.reader = new StreamReader(this.stream);
    }

    public string ReadStream()
    {
        return reader.ReadToEnd();
    }

    public void WriteStream(string str)
    {
        //stream.Write(Encoding.ASCII.GetBytes(str), 0, str.Length);
        StreamWriter hi = new StreamWriter(stream);
        hi.Write(str);
    }
}

class SymetricalAesMessageStream : BaseMessageStream
{
    private Stream stream;
    private CryptoStream crypter;
    private CryptoStream decrypter;

    private StreamReader streamReader;

    private string inKey;
    private string outKey;

    private Aes CreateAes(string key)
    {
        Aes inAes = Aes.Create();
        inAes.Key = Encoding.ASCII.GetBytes(key);
        int tmp = inAes.IV.Length;
        return inAes;
    }

    public SymetricalAesMessageStream(Stream stream)
    {
        string Sky = "1234567890123456";

        this.stream = stream;
        this.inKey = Sky;
        this.outKey = Sky;

        this.crypter = new CryptoStream(this.stream, CreateAes(this.inKey).CreateEncryptor(), CryptoStreamMode.Write);
        this.decrypter = new CryptoStream(this.stream, CreateAes(this.outKey).CreateDecryptor(), CryptoStreamMode.Read);

        this.streamReader = new StreamReader(decrypter);
    }

    public string ReadStream()
    {
       
        string text = streamReader.ReadToEnd();
        int a = text.IndexOf("\n\0\n"); //crutch 
        a = a + 3;
        text = text.Substring(a);
        decrypter.Clear();
        return text;
    }

    public void WriteStream(string str)
    {
        var data = new string('0', 18) + "\n\0\n" + str;
        crypter.Write(Encoding.ASCII.GetBytes(data), 0, data.Length);
        crypter.Clear();
    }
}

class AsymmetricalRsaMessageStream
{
    RSACryptoServiceProvider encryptor;
    RSACryptoServiceProvider decryptor;

    public AsymmetricalRsaMessageStream()
    {
        encryptor = new RSACryptoServiceProvider(1024);
        decryptor = new RSACryptoServiceProvider(1024);
    }

    public string Encrypt(string data)
    {
        byte[] encrypted = this.encryptor.Encrypt(Encoding.ASCII.GetBytes(data), false);
        return MakeSTR(encrypted);
    }

    public string Decrypt(string data)
    {
        byte[] decrypted = this.decryptor.Decrypt(MakeBYTE(data), false);
        return Encoding.ASCII.GetString(decrypted);
    }

    public void SetPublicKey(string data)
    {
        encryptor.FromXmlString(data);
    }

    public string GetPublicKey()
    {
        return decryptor.ToXmlString(false);
    }

    private string MakeSTR(byte[] arr)
    {
        char[] data = new char[128];
        for (int f = 0; f < 128; f++)
        {
            data[f] = (char)arr[f];
        }
        string str = new string(data);
        return str;
    }

    private byte[] MakeBYTE(string str)
    {
        byte[] arr = new byte[128];
        for (int f = 0; f < 128; f++)
        {
            arr[f] = (byte)str[f];
        }
        return arr;
    }
}