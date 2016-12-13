using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace HSPApp.Layout {
    public class About : ContentView { // ContentPage {
        public About() {



            string fileStub = AppHelper.AssetPath;
//            wv.Source = fileStub + "about.html";

            WebView wv = new WebView {
                Source = fileStub + "about.html"
                //Source = "http://xamarin.com"
            };

            ScrollView sv = new ScrollView();
            sv.Content = wv;
            sv.VerticalOptions = LayoutOptions.FillAndExpand;

            //StackLayout sl = new StackLayout();
            //sl.Children.Add(new Label { Text = "Hello About Page.  And here a little bit of longer text too ... It is really not that long ..." });
            //sl.Children.Add(sv);

            //Content = sl;
            Content = sv;

            //HtmlWebViewSource htmlSource = new HtmlWebViewSource();

            //Assembly assembly = typeof(HSPApp.Layout.About).GetTypeInfo().Assembly;
            //Stream stream = assembly.GetManifestResourceStream("Resource"+HSPApp.AppHelper.AssemblySuffix+".About.html");
            //Stream stream = assembly.GetManifestResourceStream("Resource.About.html");
            //string text = "";
            //using (var reader = new System.IO.StreamReader(stream)) {
            //                text = reader.ReadToEnd();
            //            }

            //            htmlSource.Html = text;
            //            wv.Source = htmlSource;

            //Content = new StackLayout {
            //    Children = {

            //        //wv
            //        //new Label { Text = "Hello About Page.  And here a little bit of longer text too ... It is really not that long ..." }                    
            //    }
            //};
        }
    }
}
