//using HSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
namespace HSPApp {
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class App : Application {
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
        public App() {
            // The root page of your application

            AppHelper.CreateOptions();


            //MainPage = new HSPApp.Layout.Home();
            //MainPage = new NavigationPage(new HSPApp.Layout.Default());
            MainPage = new HSPApp.Layout.Default();


            //var content = new ContentPage {
            //    Title = "HSPApp",
            //    Content = new StackLayout {
            //        VerticalOptions = LayoutOptions.Center,
            //        Children = {
            //            new Label {
            //                HorizontalTextAlignment = TextAlignment.Center,
            //                Text = "Welcome to Xamarin Forms!"
            //            }
            //        }
            //    }
            //};

            //MainPage = new NavigationPage(content);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
        protected override void OnStart() {
            // Handle when your app starts
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
