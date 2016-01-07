using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;


namespace App3
{
    [Activity(Label = "App3", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button buttonS1;
        Button buttonS2;
        Button buttonS3;
        Button buttonS4;
        ToggleButton ToggleS1;
        ToggleButton ToggleS2;
        ToggleButton ToggleS3;
        RadioButton RadioS1;
        RadioButton RadioS2;
        RadioButton RadioS3;
        Button pastoeS1;
        Button pastoeS2;
        Button pastoeS3;
        TextView tekstS1;
        TextView tekstS2;
        EditText editS1;
        EditText editS2;
        EditText editS3;
        EditText editS4;
        EditText editS5;
        EditText editS6;
        string aanuitS1;
        string aanuitS2;
        string aanuitS3;
        string minS2;
        string maxS2;
        string sensorS2;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            buttonS1 = FindViewById<Button>(Resource.Id.button1);
            buttonS2 = FindViewById<Button>(Resource.Id.button2);
            buttonS3 = FindViewById<Button>(Resource.Id.button3);
            buttonS4 = FindViewById<Button>(Resource.Id.button4);
            ToggleS1 = FindViewById<ToggleButton>(Resource.Id.toggleButton1);
            ToggleS2 = FindViewById<ToggleButton>(Resource.Id.toggleButton2);
            ToggleS3 = FindViewById<ToggleButton>(Resource.Id.toggleButton3);
            RadioS1 = FindViewById<RadioButton>(Resource.Id.radioButton1);
            RadioS2 = FindViewById<RadioButton>(Resource.Id.radioButton2);
            RadioS3 = FindViewById<RadioButton>(Resource.Id.radioButton3);
            pastoeS1 = FindViewById<Button>(Resource.Id.button5);
            pastoeS2 = FindViewById<Button>(Resource.Id.button25);
            pastoeS3 = FindViewById<Button>(Resource.Id.button35);
            tekstS1 = FindViewById<TextView>(Resource.Id.textView1);
            tekstS2 = FindViewById<TextView>(Resource.Id.textView2);
            editS1 = FindViewById<EditText>(Resource.Id.editText11);
            editS2 = FindViewById<EditText>(Resource.Id.editText12);
            editS3 = FindViewById<EditText>(Resource.Id.editText21);
            editS4 = FindViewById<EditText>(Resource.Id.editText22);
            editS5 = FindViewById<EditText>(Resource.Id.editText31);
            editS6 = FindViewById<EditText>(Resource.Id.editText32);









            buttonS1.Click += buttonS1_Click;
            buttonS2.Click += buttonS2_Click;
            buttonS3.Click += buttonS3_Click;
            buttonS4.Click += buttonS4_Click;





            pastoeS2.Click += pastoe;

            //    while(true)
            //    {
            //        if(ToggleS2.Checked == true)
            //        {
            //            aanuitS2 = "aan";

            //        }
            //        else
            //        {
            //            aanuitS2 = "uit";
            //        }

            //        pastoeS2.Text = aanuitS2;
            //    }
        }

        void pastoe(object sender, System.EventArgs e)
        {
            
            sensorS2 = "g";
            minS2 = "000";
            maxS2 = "000";

            aanuitS2 = "aan";

            string x = "2" + aanuitS2 + sensorS2 + minS2 + maxS2;
            connect(x);
        }
        void buttonS1_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.Switch1);

            Addr();

            buttonS1.Click += buttonS1_Click;
            buttonS2.Click += buttonS2_Click;
            buttonS3.Click += buttonS3_Click;
            buttonS4.Click += buttonS4_Click;
        }

        void buttonS2_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.Switch2);

            Addr();

            buttonS1.Click += buttonS1_Click;
            buttonS2.Click += buttonS2_Click;
            buttonS3.Click += buttonS3_Click;
            buttonS4.Click += buttonS4_Click;

            connect("1aanl100300");
        }

        void buttonS3_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.Switch3);

            Addr();

            buttonS1.Click += buttonS1_Click;
            buttonS2.Click += buttonS2_Click;
            buttonS3.Click += buttonS3_Click;
            buttonS4.Click += buttonS4_Click;
        }

        void buttonS4_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.Main);

            Addr();

            buttonS1.Click += buttonS1_Click;
            buttonS2.Click += buttonS2_Click;
            buttonS3.Click += buttonS3_Click;
            buttonS4.Click += buttonS4_Click;
        }

        void Addr()
        {
            buttonS1 = FindViewById<Button>(Resource.Id.button1);
            buttonS2 = FindViewById<Button>(Resource.Id.button2);
            buttonS3 = FindViewById<Button>(Resource.Id.button3);
            buttonS4 = FindViewById<Button>(Resource.Id.button4);
        }

        public static string connect(string a)
        {
            Socket s = open("192.168.0.106", 3300);
            write(s, a);
            string reply = read(s);
            close(s);
            System.Console.WriteLine(reply);
            return reply;
        }

        public static Socket open(string ipaddress, int portnr)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(ipaddress);
            IPEndPoint endpoint = new IPEndPoint(ip, portnr);
            System.Console.WriteLine(socket.Connected);
            socket.Connect(endpoint);
            if (socket.Connected)
            {
                System.Console.WriteLine("Connected to server!");
            }
            else {
                System.Console.WriteLine("Connection failed");
            }
            return socket;
        }

        public static void write(Socket socket, string text)
        {
            socket.Send(Encoding.ASCII.GetBytes(text));
            System.Console.WriteLine("write");
        }
        public static string read(Socket socket)
        {
            byte[] bytes = new byte[4096];
            int bytesRec = socket.Receive(bytes);
            string text = Encoding.ASCII.GetString(bytes, 0, bytesRec);
            System.Console.WriteLine("read");
            return text;
        }
        public static void close(Socket socket)
        {
            socket.Close();
            System.Console.WriteLine("close");
        }
    }
}

