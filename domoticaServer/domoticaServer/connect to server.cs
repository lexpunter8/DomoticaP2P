using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace domoticaServer
{
	public class connect_to_server
	{
		public static string connect()
		{
			Socket s = open("192.168.0.108", 3300);
			write (s, "hallo wereld");
			string reply = read (s);
			close (s);
			System.Console.WriteLine (reply);
			return reply;
		}

		public static Socket open(string ipaddress, int portnr)
		{
			Socket socket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			IPAddress ip = IPAddress.Parse (ipaddress);
			IPEndPoint endpoint = new IPEndPoint (ip, portnr);
			System.Console.WriteLine (endpoint);
			socket.Connect (endpoint);
			return socket;
		}

		public static void write(Socket socket, string text)
		{
			socket.Send(Encoding.ASCII.GetBytes(text));
		}
		public static string read(Socket socket)
		{
			byte[] bytes = new byte[4096];
			int bytesRec = socket.Receive(bytes);
			string text = Encoding.ASCII.GetString(bytes, 0, bytesRec);
			return text;
		}
		public static void close(Socket socket)
		{
			socket.Close();
		}
	}
}

