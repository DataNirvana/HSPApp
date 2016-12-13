using HSPApp.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------
namespace HSPApp.Layout {
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class DocumentList : ContentView { // NavigationPage {
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public DocumentList() {

            //Content = new Label() { Text = "This will be a list of documents soon" };

            StackLayout sl = new StackLayout();
            sl.Padding = 5;
            sl.VerticalOptions = LayoutOptions.CenterAndExpand;
            sl.HorizontalOptions = LayoutOptions.FillAndExpand;


            sl.Children.Add(new Label {
                Text = "Standards",
                FontSize = 20,
                TextColor = Color.FromRgb(14,145,143)
            });


            foreach ( HSPMenuView hspMV in HSPApp.AppHelper.Standards) {
                View v = hspMV.GetView();

                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) => {
                    // handle the tap
                    DoDrillDown(hspMV.Title);
                };
                v.GestureRecognizers.Add(tapGestureRecognizer);


                sl.Children.Add(v);
            }

            ScrollView sv = new ScrollView();
            sv.VerticalOptions = LayoutOptions.CenterAndExpand;
            sv.HorizontalOptions = LayoutOptions.FillAndExpand;
            sv.Content = sl;

            Content = sv;

            // Lets try pushing a couple of pages in 
            //this.PushAsync(new Layout.About());
            //this.PushAsync(new Layout.Welcome());
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public void DoDrillDown(string title) { // object sender, EventArgs e) {

            string subNav = "";
            foreach (HSPMenuView hspMV in HSPApp.AppHelper.Standards) {
                if ( hspMV.Title.Equals( title, StringComparison.CurrentCultureIgnoreCase)) {
                    subNav = hspMV.SubNavigation;
                    break;
                }
            }

            HSPApp.Layout.Default.MainContent.Content = new Layout.DocumentViewer("", subNav);

        }

    }
}
