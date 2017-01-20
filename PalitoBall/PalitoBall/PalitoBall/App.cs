﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;

using Xamarin.Forms;

namespace PalitoBall
{
	public class App : Application
	{
		public App ()
		{
            // The root page of your application
            // The root page of your application
            MobileCenter.Start(typeof(Analytics), typeof(Crashes));
            MainPage = new GamePage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
