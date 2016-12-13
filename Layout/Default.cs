using FormsPopup;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
namespace HSPApp.Layout {

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class Default : ContentPage {

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        AppHelper aHelper = new AppHelper();

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //public static int NumViews = 0;

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static int CurrentView = 0;
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //public static List<View> HSPViews = new List<View>();
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static SwipeableContentView MainContent = new SwipeableContentView();


        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public Default() {

            MainContent.GoRight += (s, e) => {
                // Go Back ...
                GoBack();
            };
            MainContent.GoLeft += (s, e) => {
                // Go Forwards ...
                GoForwards();
            };



            StackLayout header = aHelper.GetHeader(true, AppHelper.TitleText + "                      ");

            MainContent.VerticalOptions = LayoutOptions.FillAndExpand;
            MainContent.HorizontalOptions = LayoutOptions.FillAndExpand;
            AddView("HSPApp.Layout.Welcome");
            ShowView();

            StackLayout sl = new StackLayout();
            sl.HorizontalOptions = LayoutOptions.FillAndExpand;

            if ( Device.OS == TargetPlatform.iOS) {
                sl.Children.Add(new Label() {
                    Text = " ",
                    FontSize = 12
                });
            }

            sl.Children.Add(header);
            sl.Children.Add(MainContent);
            this.Content = sl;

            //scv.Content = new Layout.About();

            // Required for the popup to work. It must come after the Content has been set.
            new PopupPageInitializer(this) { AppHelper.MenuPopUp };


        }

        //    //NavigationPage.SetHasNavigationBar(this, false);
        //    //this.Title = "Testing the title";            
        //    this.PushAsync(new Layout.Home());
        //    NumPages++;
        //    //this.PushAsync(new Layout.About());
        //    //this.PushAsync(new Layout.Welcome());

        //    this.Popped += (object sender, NavigationEventArgs e) =>
        //    {
        //        Debug.WriteLine("Page was popped: {0}", e.Page.Title);
        //        NumPages--;
        //        if ( NumPages == 1) {
        //            NavigationPage.SetHasNavigationBar(this, false);
        //        }

        //    };
        //    this.Pushed += (object sender, NavigationEventArgs e) =>
        //    {
        //        Debug.WriteLine("Page was pushed: {0}", e.Page.Title);
        //        NumPages++;

        //        if (NumPages > 1) {
        //            NavigationPage.SetHasNavigationBar(this, true);
        //        }

        //    };
        //}

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     navReference == e.g. HSPApp.Layout.Home ... for a contentView
        /// </summary>
        /// <param name="navReference"></param>
        public static void AddView( string navReference ) {


            //////////////////////////////////////////////////////////////
            // We need to add these views to the menu items ....




            View v = null;
            //int viewIndex = -1;

            for( int i = 0; i < HSPApp.AppHelper.ContextMenuViews.Count; i++) {

                if (HSPApp.AppHelper.ContextMenuViews[i].Navigation.Equals(navReference, StringComparison.CurrentCultureIgnoreCase)) {

                    if (HSPApp.AppHelper.ContextMenuViews[i].ContentView == null) {

                        Type t = Type.GetType(navReference);
                        v = Activator.CreateInstance(t) as View;

                        AppHelper.ContextMenuViews[i].ContentView = v;

                    } else {
                        v = AppHelper.ContextMenuViews[i].ContentView;
                    }


                    CurrentView = i;
                    break;
                }
            }

            //HSPViews.Add(v);
            //if ( HSPViews.Count > 1) {
            //    CurrentView++; 
            //}
            ShowView();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        protected void GoBack() {
            if (CurrentView == 0) {
                CurrentView = AppHelper.ContextMenuViews.Count - 1;
            } else { 
                CurrentView--;
            }

            // if the current view has not yet been loaded, then lets make it happen ...
            if (AppHelper.ContextMenuViews[CurrentView].ContentView == null) {
                AddView(AppHelper.ContextMenuViews[CurrentView].Navigation);
            }

            ShowView();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        protected void GoForwards() {
            if (CurrentView == (AppHelper.ContextMenuViews.Count - 1)) {
                CurrentView = 0;
            } else {
                CurrentView++;
            }

            // if the current view has not yet been loaded, then lets make it happen ...
            if (AppHelper.ContextMenuViews[CurrentView].ContentView == null) {
                AddView(AppHelper.ContextMenuViews[CurrentView].Navigation);
            }

            ShowView();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static void GoHome() {
            CurrentView = 0;
            MainContent.Content = AppHelper.ContextMenuViews[CurrentView].ContentView; // HSPViews[CurrentView];
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static void ShowView() {
            MainContent.Content = AppHelper.ContextMenuViews[CurrentView].ContentView; // HSPViews[CurrentView];
        }


        ////---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //public void AddPage(string name) {

        //    Type t = Type.GetType(name);

        //    Page p = Activator.CreateInstance(t) as Page;

        //    this.PushAsync(p);

        //}

        ////---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //public void RemovePage() {
        //    this.PopAsync();
        //}


    }
}
