using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------
namespace HSPApp.Layout {
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class DocumentViewer : ContentView {
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        AppHelper aHelper = new AppHelper();

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public DocumentViewer(string docName, string htmlFileStub) {
            /*
            Content = new StackLayout {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
            */

            LoadDocumentView(docName, htmlFileStub);

        }

                
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public void LoadDocumentView(string docName, string htmlFileStub) {

            try {

                string fileStub = AppHelper.AssetPath;

                /*
                //string fileStub = "";
                if (Device.OS == TargetPlatform.Android) {
                    //fileStub = "file:///android_asset/";
                    //-----------------------------------------------fileStub = "file:///Assets/";
                } else if (Device.OS == TargetPlatform.iOS) {
                    fileStub = NSBundle.MainBundle.BundlePath + "/";
                } else if (Device.OS == TargetPlatform.Windows) {
                    fileStub = "ms-appx-web:///Assets/";
                } else if (Device.OS == TargetPlatform.WinPhone) {
                    fileStub = "";
                } else {
                    string ohFuck = "UNSUPPORTED OS";
                }
                */

                WebView wv = new WebView {
                    Source = fileStub + "html_" + htmlFileStub + ".html",
                    
                };

                
                // This is not working in this context ...
                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.NumberOfTapsRequired = 2;
                tapGestureRecognizer.Tapped += (s, e) => {
                    // handle the tap
                    OnWebViewTapped(s, e);
                };
                //wv.GestureRecognizers.Add(tapGestureRecognizer);


                ScrollView sv = new ScrollView();
                sv.Content = wv;
                sv.VerticalOptions = LayoutOptions.FillAndExpand;
                //sv.GestureRecognizers.Add(tapGestureRecognizer);
                //sv.Focus();

                //StackLayout header = aHelper.GetHeader(false, docName);

                StackLayout sl = new StackLayout();
                //sl.Children.Add(header);
                sl.Children.Add(sv);


                // And then lets assign the content
                Content = sl;
                //Title = docName;
                //Content.GestureRecognizers.Add(tapGestureRecognizer);

                //bool svIsFocused = sv.IsFocused;
                //sv.Focus();

            } catch (Exception ex) {

                string ohFuck = "";

            }


        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public void OnWebViewTapped(object sender, EventArgs e) {

            // Do something here ....

            string blah = "";

        }


        }
    }
