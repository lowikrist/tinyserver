using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace tinyserver {
    class Program
    {
        // port on which the server listens 
        static int port = 8080;
        static void Main(string[] args) {

            IPAddress localhost = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(localhost, port);
            server.Start();
            Console.WriteLine("IP address of server: " + localhost + " \nServer port " + port);

            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                TcpClient client = server.AcceptTcpClient();
                //waits for the client to connect, creates a TcpClient object
                Console.WriteLine("Connected!");
                //da lahko posiljamo in beremo data
                NetworkStream stream = client.GetStream();
                
                int i;  //so it stores number of bytes read from the stream

                Byte[] bytes = new byte[256];
                String data = null;

                //ok so here loop ends when stream.Read returns 0 --> happens
                //when client closes the connection :c 
                //why u disconnected and left me alone (i can feel server's pain
                //yes you hear me right

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);

                    // Process the data sent by the client.
                    data = data.ToUpper();

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine("Sent: {0}", data);
                }
            }
        }
    }
}