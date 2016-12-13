using FormsPopup;
using HSPApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------
namespace HSPApp.Layout {
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class Home : ContentView { // ContentPage

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        AppHelper aHelper = new AppHelper();

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
//        public static int NumViews = 0;
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
//        public static int CurrentView = 0;
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
//        public static List<View> HSPViews = new List<View>();

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
//        public static SwipeableContentView MainContent = new SwipeableContentView();


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public Home() {


            //MainContent.GoRight += (s, e) => {
            //    // Go Back ...
            //    GoBack();

            //};
            //MainContent.GoLeft += (s, e) => {
            //    // Go Forwards ...
            //    GoForwards();
            //};


            SetPage(CreatePage());

            /*
            Content = new StackLayout {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
            */

        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public StackLayout CreatePage() {

            //StackLayout header = aHelper.GetHeader(true, AppHelper.TitleText + "                      ");


            StackLayout sl = new StackLayout();
            sl.HorizontalOptions = LayoutOptions.FillAndExpand;
            //sl.Children.Add(header);

            //StackLayout sl2 = new StackLayout();
            sl.Children.Add(new Label { Text = "Welcome! To do - add text ..." });
            sl.Children.Add(new Label { Text = "Browse standards ..." });
            sl.Children.Add(CreateButton("Tap to browse standards", DisplayStandards));
            sl.Children.Add(new Label { Text = "TEST ... download file from Dropbox ..." });
            sl.Children.Add(CreateButton("Tap to download test file", OnFileDownloadChosen));
            //MainContent.Content = sl2;
            //sl.Children.Add(MainContent);

            return sl;
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public void SetPage(StackLayout sl) {
            //ControlTemplate = sl;
            Content = sl;
            //Title = AppHelper.TitleText;

            // Required for the popup to work. It must come after the Content has been set.
            //new PopupPageInitializer(this) { AppHelper.MenuPopUp };

        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //public void AddView(string name) {

        //    Type t = Type.GetType(name);

        //    View v = Activator.CreateInstance(t) as View;

        //    HSPViews.Add(v);
        //    if (HSPViews.Count > 1) {
        //        CurrentView++;
        //    }
        //    //ShowView();
        //}

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //protected void GoBack() {
        //    CurrentView--;
        //    ShowView();
        //}

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //protected void GoForwards() {
        //    CurrentView++;
        //    ShowView();
        //}

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //public void ShowView() {
        //    this.Content = HSPViews[CurrentView];
        //}




        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        protected Button CreateButton(string text, EventHandler clickEvent) {
            Button b = new Button();
            b.Text = text; // "Click to choose document";
            b.VerticalOptions = LayoutOptions.Center;

            b.Clicked += clickEvent; // DisplayChoices;
            return b;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        protected void DisplayStandards(object sender, EventArgs e) {

            HSPApp.Layout.Default.MainContent.Content = new DocumentList();

            // Build the list view
            //AppHelper.DocumentListView = new ListView {
            //    ItemsSource = AppHelper.DocumentList.Keys
            //};
            //AppHelper.DocumentListView.ItemSelected += OnDocChosen;


            //// Build the page
            //Content = new StackLayout {
            //    Children =
            //        {
            //                aHelper.GetHeader(false, AppHelper.TitleText + "                      "),
            //                //picker,
            //                new Label { Text = "Welcome! Choose a document to view from the list below ..." },
            //                CreateButton("Click to choose document", DisplayChoices),
            //                AppHelper.DocumentListView
            //            }
            //};


            //// iOS platform specific padding ...
            //this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);



        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        void OnDocChosen(object sender, EventArgs e) {

            if (AppHelper.DocumentListView.SelectedItem == null) {
                // Do nothing!
            } else {

                string docName = AppHelper.DocumentListView.SelectedItem.ToString();

                string htmlFileStub;
                AppHelper.DocumentList.TryGetValue(docName, out htmlFileStub);

                //App.Current.MainPage = new DocumentViewer(docName, htmlFileStub);
                //MainContent

                HSPApp.Layout.Default.MainContent.Content = new DocumentViewer(docName, htmlFileStub);
                //App.Current.MainPage = new NavigationPage(new DocumentViewer(docName, htmlFileStub));
                
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        void OnFileDownloadChosen(object sender, EventArgs e) {

            HSPApp.Layout.Default.MainContent.Content = new Downloader();
            //App.Current.MainPage = new NavigationPage(new Downloader());

        }
    }
}