using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Timers;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Android.Graphics;
using System.Threading.Tasks;


namespace test33
{
	[Activity (Label = "test33", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int switchIndex;
		RadioGroup aanUitS2;
		RadioButton aanUit;
		RadioButton r2, radioLicht, radioTemp, radioGeen, radioAan, radioUit;
		RadioGroup rg2;
		CheckBox countDown;
		TextView lichtSensor, tempSensor, switch1Status, switch2Status, switch3Status, ip1, ip2, ip3, ip4;
		EditText et1, et2, uur, minute, dag, maand, jaar;
		Button infoButton, switch1Button, switch2Button, switch3Button, pasToe, connectButton;
		TextView errorTV;
		string IPadres;
		bool connected = false;
		string temperatuur, lichtSensorWaarde;
		Socket socket = null;
		Timer timerSockets, checkTimeTimer;

		public class timer
		{
			public int uur { get; set; }
			public int min { get; set; }
			public int day { get; set; }
			public int month { get; set; }
			public int year  { get; set; }

		}
		public class pageInfo
		{
			public string Switch { get; set; }
			public string status { get; set; }
			public string sensor { get; set; }
			public string minVal { get; set; }
			public string maxVal { get; set; }
			public bool timer { get; set;}
			public string send { get; set;}
		
		}
	

		private static List<pageInfo> values = new List<pageInfo>(){
			new pageInfo() {Switch = "1", status = "uit", sensor = "", minVal = "", maxVal = "", timer = false, send = ""},
			new pageInfo() {Switch = "2", status = "uit", sensor = "", minVal = "", maxVal = "", timer = false, send = ""},
			new pageInfo() {Switch = "3", status = "uit", sensor = "", minVal = "", maxVal = "", timer = false, send = ""}
		};

		private static List<timer> setTimer = new List<timer>(){
			new timer() {uur = DateTime.Now.Hour, min = DateTime.Now.Minute, day = DateTime.Now.Day,
				month = DateTime.Now.Month, year = DateTime.Now.Year}
		};

			
			protected override void OnCreate (Bundle savedInstanceState)
		{
			//Xamarin.Insights.Initialize (XamarinInsights.ApiKey, this);
			base.OnCreate (savedInstanceState);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			// Get our button from the layout resource,
			// and attach an event to it
			setPage ();
		}

		void Switch3Button_Click (object sender, EventArgs e)
		{
			switchIndex = 2;
			SetContentView (Resource.Layout.switch3);
			setPage ();
		}

		void Switch2Button_Click (object sender, EventArgs e)
		{
			switchIndex = 1;
			SetContentView (Resource.Layout.switch2);
			setPage ();
		}

		void Switch1Button_Click (object sender, EventArgs e)
		{
			timerSockets.Stop ();
			switchIndex = 0;
			SetContentView (Resource.Layout.switch1);
			setPage ();
		}

		void InfoButton_Click (object sender, EventArgs e)
		{
			switchIndex = -1;
			SetContentView (Resource.Layout.Main);
			if (connected) {
				timerSockets.Enabled = true;
			} else {
				timerSockets.Enabled = false;
			}
			setPage ();
			tempSensor.Text = temperatuur;
			lichtSensor.Text = lichtSensorWaarde;
		}

		void setValues(){
			if (rg2 != null) {
				if (values [switchIndex].status == "aan") {
					radioAan.Checked = true;
				}
				if (values [switchIndex].status == "uit") {
					radioUit.Checked = true;
				}

				if (values [switchIndex].sensor == "l") {
					radioLicht.Checked = true;
				}
				if (values [switchIndex].sensor == "t") {
					radioTemp.Checked = true;
				}
				if (values [switchIndex].sensor == "g") {
					radioGeen.Checked = true;
				}
				et1.Text = values [switchIndex].minVal;
				et2.Text = values [switchIndex].maxVal;
				if (switchIndex == 2) {
					if (values [2].timer == true) {
						countDown.Checked = true;
					} else {
						countDown.Checked = false;

					}
						
				}
			}

		}

		void update (int switchnr, string min, string max)
		{
				r2 = FindViewById<RadioButton>(rg2.CheckedRadioButtonId);
				aanUit = FindViewById<RadioButton> (aanUitS2.CheckedRadioButtonId);
				string sensor = r2.Text;
			if (aanUit.Text == "Aan") {
				values[switchIndex].status = "aan";
				if (sensor == "Licht") {
					values[switchIndex].sensor = "l";
				} else if (sensor == "Temperatuur") {
					values[switchIndex].sensor = "t";
				} else {
					values[switchIndex].sensor = "g";
					min = "000";
					max = "000";
				}
				if (min.Length == 0 || max.Length == 0) {
					errorTV.Text = "Geen waardes!";
				}else {
					errorTV.Text = " ";
					values[switchIndex].minVal = checkValueLenght (min);
					values[switchIndex].maxVal = checkValueLenght (max);
					values[switchIndex].send = values [switchIndex].Switch + values [switchIndex].status + values [switchIndex].sensor +
												values [switchIndex].minVal + values [switchIndex].maxVal; 
					if (values [switchIndex].timer == true) {
						values[switchIndex].status = values[switchIndex].status + ", op timer";
					}
					if (values [switchIndex].timer == true) {
						setTimer [0].uur = Convert.ToInt16(uur.Text);
						setTimer [0].min = Convert.ToInt16(minute.Text);
						setTimer [0].day = Convert.ToInt16(dag.Text);
						setTimer [0].month = Convert.ToInt16(maand.Text);
						setTimer [0].year = Convert.ToInt16(jaar.Text);
						checkTimeTimer.Enabled = true;
					} else {
						write (socket, values[switchIndex].send);
					}

				}

			} 
			else if(aanUit.Text == "Uit")
			{
				errorTV.Text = "";
				values[switchIndex].status = "uit";
				values[switchIndex].send = values[switchIndex].Switch + "uitg000000";
				if (values [switchIndex].timer == true) {
					checkTimeTimer.Enabled = true;
					setTimer [0].uur = Convert.ToInt16(uur.Text);
					setTimer [0].min = Convert.ToInt16(minute.Text);
					setTimer [0].day = Convert.ToInt16(dag.Text);
					setTimer [0].month = Convert.ToInt16(maand.Text);
					setTimer [0].year = Convert.ToInt16(jaar.Text);
				} else {
					write(socket, values[switchIndex].send);
				}

			}
				
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
			System.Console.WriteLine ("close");
		}

		public string checkValueLenght(string val){

			if (val.Length == 0) {
				return "000";
			}
			if (val.Length == 1) {
				return "00" + val;
			}
			if (val.Length == 2) {
				return "0" + val;
			}

			return val;
		}

		void convert(String v){
			String index = v.Substring(0,1);
			int sIndex = Convert.ToInt16(index);
			values[sIndex].status = v.Substring(1, 4);
			values[sIndex].sensor = Convert.ToString(v[4]);
			values[sIndex].minVal = v.Substring(5,8);
			values[sIndex].maxVal = v.Substring(8,11);
		}

		void socketConnect(string ip, int portnr){

			RunOnUiThread(() => 
				{
					if(socket == null){
						connectButton.Text = "Connecting";
						try{
							socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
							socket.Connect(new IPEndPoint(IPAddress.Parse(ip), Convert.ToInt32(portnr)));
							if(socket.Connected)
							{
								connectButton.Text = "Disconnect";
								errorTV.Text = "";
								write(socket, "s1");
								convert(read(socket));
								write(socket, "s2");
								convert(read(socket));
								write(socket, "s3");
								convert(read(socket));
								setPage();
								connected = true;
								timerSockets.Enabled = true;
							} 
						}catch (Exception exception) {
							timerSockets.Enabled = false;
							System.Console.WriteLine("catch");
							if (socket != null)
							{
								System.Console.WriteLine(exception.Message);
								errorTV.Text = "Connection error, check ip";
								connectButton.Text = "Connect";
								socket.Close();
								socket = null;
							}
						}
					} else {
						connectButton.Text = "Connect";
						socket.Close();
						timerSockets.Enabled = false;
						connected = false;
						socket = null;
					}

				});
		
		}

		private bool CheckValidIpAddress(string ip)
		{
			if (ip != "") {
				//Check user input against regex (check if IP address is not empty).
				Regex regex = new Regex("\\b((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\\.|$)){4}\\b");
				Match match = regex.Match(ip);
				return match.Success;
			} else return false;
		}

		//Check if the entered port is valid.
		private bool CheckValidPort(string port)
		{
			//Check if a value is entered.
			if (port != "")
			{
				Regex regex = new Regex("[0-9]+");
				Match match = regex.Match(port);

				if (match.Success)
				{
					int portAsInteger = Int32.Parse(port);
					//Check if port is in range.
					return ((portAsInteger >= 0) && (portAsInteger <= 65535));
				}
				else return false;
			} else return false;
		}

		void setPage()
		{
			// set all controls by name 
			rg2 = FindViewById<RadioGroup> (Resource.Id.radioGroup1);
			aanUitS2 = FindViewById<RadioGroup> (Resource.Id.radioGroup2);

			radioGeen = FindViewById<RadioButton> (Resource.Id.radioButton3);
			radioLicht = FindViewById<RadioButton> (Resource.Id.radioButton1);
			radioTemp = FindViewById<RadioButton> (Resource.Id.radioButton2);
			radioAan = FindViewById<RadioButton> (Resource.Id.radioButton4);
			radioUit = FindViewById<RadioButton> (Resource.Id.radioButton5);

			pasToe = FindViewById<Button> (Resource.Id.button1);
			infoButton = FindViewById<Button>(Resource.Id.button3);
			switch1Button = FindViewById<Button>(Resource.Id.button4);
			switch2Button = FindViewById<Button>(Resource.Id.button5);
			switch3Button = FindViewById<Button>(Resource.Id.button2);
			connectButton = FindViewById<Button>(Resource.Id.buttonConnect);

			ip1 = FindViewById<TextView> (Resource.Id.editTextip1);
			ip2 = FindViewById<TextView> (Resource.Id.editTextip2);
			ip3 = FindViewById<TextView> (Resource.Id.editTextip3);
			ip4 = FindViewById<TextView> (Resource.Id.editTextip4);
			tempSensor = FindViewById<TextView> (Resource.Id.tempstatus);
			lichtSensor = FindViewById<TextView> (Resource.Id.lichtstatus);
			switch1Status = FindViewById<TextView> (Resource.Id.s1status);
			switch2Status = FindViewById<TextView> (Resource.Id.s2status);
			switch3Status = FindViewById<TextView> (Resource.Id.s3status);
			errorTV = FindViewById<TextView> (Resource.Id.textView5);

			countDown = FindViewById<CheckBox> (Resource.Id.checkBox1);

			et1 = FindViewById<EditText> (Resource.Id.editText1);
			et2 = FindViewById<EditText> (Resource.Id.editText2);
			uur = FindViewById<EditText> (Resource.Id.editText3);
			minute = FindViewById<EditText> (Resource.Id.editText4);
			dag = FindViewById<EditText> (Resource.Id.editText5);
			maand = FindViewById<EditText> (Resource.Id.editText6);
			jaar = FindViewById<EditText> (Resource.Id.editText7);

			setValues ();

			// check is status info control consists, and set values
			if (switch1Status != null) {
				switch1Status.Text = values [0].status;
				if (values [0].status == "aan") {
					switch1Status.SetTextColor(Color.Green);
				} else {
					switch1Status.SetTextColor(Color.Red);
				}
				switch2Status.Text = values [1].status;
				if (values [1].status == "aan") {
					switch2Status.SetTextColor(Color.Green);
				} else {
					switch2Status.SetTextColor(Color.Red);
				}
				switch3Status.Text = values [2].status;
				if (values [2].status == "aan") {
					switch3Status.SetTextColor(Color.Green);
				} else {
					switch3Status.SetTextColor(Color.Red);
				}
			
			}
			// timer for connection with arduino to get sensor values	
			timerSockets = new System.Timers.Timer() { Interval = 1000, Enabled = false  }; // Interval >= 750
			timerSockets.Elapsed += (obj, args) =>
			{
				if(connected)
				{
					write(socket, "light");
					lichtSensorWaarde = read(socket);
					RunOnUiThread(() => 
						{
							if(lichtSensor != null)
							{
							lichtSensor.Text = lichtSensorWaarde;
							}
						}); 
					write(socket, "temp");
					temperatuur = read(socket);
					RunOnUiThread(() =>
						{
							if(tempSensor != null)
							{
								tempSensor.Text = temperatuur;
							}
						}); 
				}
			};

			// if button consists
			if (connectButton != null) 
			{
				// set value 
				if (connected) 
				{
					connectButton.Text = "Disconnect";
				} 
				else 
				{
					connectButton.Text = "Connect";
				}
				// set click handler 
				connectButton.Click += (sender, e) => 
				{
					// get ip from input
					IPadres = ip1.Text + "." + ip2.Text + "." + ip3.Text + "." + ip4.Text;
					// if ip adress is valid, connect socket 
					if(CheckValidIpAddress(IPadres))
					{
						socketConnect(IPadres, 3300);
					} else {
						errorTV.Text = "Ip not correct";
					}
				};
			}
			// timer to check if input time is equal to now time
			checkTimeTimer = new System.Timers.Timer() { Interval = 1000, Enabled = false  }; // Interval >= 750
			checkTimeTimer.Elapsed += (obj, args) =>
			{
				if(setTimer[0].year == DateTime.Now.Year)
				{
					jaar.SetTextColor(Color.Green);
					if(setTimer[0].month == DateTime.Now.Month)
					{
						maand.SetTextColor(Color.Green);
						if(setTimer[0].day == DateTime.Now.Day)
						{
							dag.SetTextColor(Color.Green);
							if(setTimer[0].uur == DateTime.Now.Hour)
							{
								uur.SetTextColor(Color.Green);
								if(setTimer[0].min == DateTime.Now.Minute)
								{
									minute.SetTextColor(Color.Green);
									values[2].status = "aan";
									write(socket, values[2].send);
									checkTimeTimer.Stop();
									values[2].timer = false;
								}
							}
						}
					}
				}
			};
			// if button consists
			if (pasToe != null) 
			{
				// set click handler
				pasToe.Click += (sender, e) =>
				{
					if(connected)
					{
						if(switchIndex == 2)
						{
							if(countDown.Checked)
							{
								values[2].timer = true;
								errorTV.Text = "Succesfull";
							}
						} 
						update(switchIndex, et1.Text, et2.Text);
					} else 
					{
						errorTV.Text = "Not connected";
					}
				};
			}
			if (uur != null) 
			{
				uur.Text = Convert.ToString(setTimer [0].uur);
				minute.Text = Convert.ToString(setTimer [0].min);
				dag.Text = Convert.ToString(setTimer [0].day);
				maand.Text = Convert.ToString(setTimer [0].month);
				jaar.Text = Convert.ToString(setTimer [0].year);
			}

			// set click handler for page buttons 
			infoButton.Click += InfoButton_Click;
			switch1Button.Click += Switch1Button_Click;
			switch2Button.Click += Switch2Button_Click;
			switch3Button.Click += Switch3Button_Click;

		}
	}
}