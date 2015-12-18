using Android.App;
using Android.Widget;
using Android.OS;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace domoticaServer
{
	[Activity (Label = "domoticaServer", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			System.Console.WriteLine ("hello");
			// Get our button from the layout resource,
			// and attach an event to it
			string conn = domoticaServer.connect_to_server.connect ();
			//EditText text1 = FindViewById<EditText> (Resource.Id.textView);
			
			System.Console.WriteLine (conn);


		}
	}
}



