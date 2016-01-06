using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

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
        RadioButton RadioS1;
        RadioButton RadioS2;
        RadioButton RadioS3;
        Button buttonS5;
       // TextView minL1S2;
       // TextView minL2S2;
       // TextView maxL1S2;
       // TextView maxL2S2;


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
            RadioS1 = FindViewById<RadioButton>(Resource.Id.radioButton1);
            RadioS2 = FindViewById<RadioButton>(Resource.Id.radioButton2);
            RadioS3 = FindViewById<RadioButton>(Resource.Id.radioButton3);
            buttonS5 = FindViewById<Button>(Resource.Id.button5);
            

            




            buttonS1.Click += buttonS1_Click;
            buttonS2.Click += buttonS2_Click;
            buttonS3.Click += buttonS3_Click;
            buttonS4.Click += buttonS4_Click;


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
    }
}

