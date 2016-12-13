using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------
namespace HSPApp.Layout {
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class Contents : ContentView {
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public Contents() {

            List<string> items = new List<string>() {
                "Test 1",
                "Test 2"
            };

            Content = new ListView {
                ItemsSource = items
            };

        }
    }
}
