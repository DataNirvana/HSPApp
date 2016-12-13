using FormsPopup;
using HSPApp.Code;
using Plugin.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------
namespace HSPApp {
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class AppHelper {

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public static string TitleText = "Test App";
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public static bool IsHomePage = false;

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public static Dictionary<string, string> DocumentList = null;
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public static ListView DocumentListView = null;
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public static Popup MenuPopUp = null;
        public static bool MenuIsVisible = false;

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public static string AssetPath {
            get { return assetPath; }
            set { assetPath = value; }
        }
        private static string assetPath = null;

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public static string AssemblySuffix {
            get { return assemblySuffix; }
            set { assemblySuffix = value; }
        }
        private static string assemblySuffix = "";


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public static List<HSPMenuView> ContextMenuViews = new List<HSPMenuView>() {
            new HSPMenuView( "Welcome", "HSPApp.Layout.Welcome"),
            new HSPMenuView( "Home", "HSPApp.Layout.Home"),
            new HSPMenuView( "About", "HSPApp.Layout.About"),
            new HSPMenuView( "Standards", "HSPApp.Layout.DocumentList"),
            new HSPMenuView( "Download", "HSPApp.Layout.Downloader")
        };

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public static HSPMenuView SelectedDocument = null;
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public static ListView HSPMenu = null;

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public static List<HSPMenuView> Standards = new List<HSPMenuView>() {
            new HSPMenuView( "Sphere handbook", "HSPApp.Layout.DocumentViewer", "sphere", "Humanitarian Charter and Minimum Standards in Humanitarian Response." , "sphere_favicon.png"),
            new HSPMenuView( "Child Protection – Minimum Standards", "HSPApp.Layout.DocumentViewer", "cp", "Child Protection in Humanitarian Action is about preventing and responding to abuse, neglect, exploitation and violence against children in humanitarian settings." ,"alliance_favicon.png" ),
            new HSPMenuView( "Core Humanitarian Standard", "HSPApp.Layout.DocumentViewer", "chs", "Provides clarification and practical challenges relating to the Key Actions and Organisational Responsibilities in the CHS.", "chs_favicon.png"),

            new HSPMenuView( "Minimum Standards for Education: Preparedness, Response, Recovery", "HSPApp.Layout.DocumentViewer", "inee", "Enhances the quality of educational preparedness, response and recovery, increase access to safe and relevant learning opportunities and ensure accountability in providing these services.", "inee_favicon.png"),
            new HSPMenuView( "Livestock Emergency Guidelines & Standards", "HSPApp.Layout.DocumentViewer", "legs", "International guidelines and standards for designing, implementing, and evaluating livestock interventions to help people affected by humanitarian crises.", "legs_favicon.png"),
            new HSPMenuView( "Minimum Economic Recovery Standards", "HSPApp.Layout.DocumentViewer", "mers", "Technical assistance to be provided in promoting the recovery of economies and livelihoods affected by crisis.", "mers_favicon.png"),
            new HSPMenuView( "Minimum Standards for Market Analysis", "HSPApp.Layout.DocumentViewer", "calp", "Market assessments inform response analysis to determine appropriate interventions.", "calp_favicon.png")
        };

        


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public AppHelper() {

        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public static void CreateOptions() {
            if (DocumentList == null) {
                DocumentList = new Dictionary<string, string>();
                DocumentList.Add("Child Protection Minimum Standards", "cp");
                DocumentList.Add("Core Humanitarian Standards", "chs");
                DocumentList.Add("Livestock Emergency Guidance and Standards", "legs");
                DocumentList.Add("Sphere Handbook", "sphere");
            }
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public static Button CreateButton(string text, EventHandler clickEvent) {
            Button b = new Button();
            b.Text = text; // "Click to choose document";
            b.VerticalOptions = LayoutOptions.Center;

            b.Clicked += clickEvent; // DisplayChoices;
            return b;
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public StackLayout GetHeader(bool isHomePage, string headerText) {

            IsHomePage = isHomePage;

            StackLayout header = null;

            try {


                /*
                 * See this page for where the source images should be stored in each App - https://developer.xamarin.com/guides/xamarin-forms/working-with/images/
                    iOS - Place images in the Resources folder with Build Action: BundleResource . Retina versions of the image should also be supplied - two and three times the resolution with a @2x or @3x suffixes on the filename before the file extension (eg. myimage@2x.png).
                    Android - Place images in the Resources/drawable directory with Build Action: AndroidResource. High- and low-DPI versions of an image can also be supplied (in appropriately named Resources subdirectories such as drawable-ldpi , drawable-hdpi , and drawable-xhdpi ).
                    Windows Phone - Place images in the application's root directory with Build Action: Content .
                    Windows/UWP - Place images in the application's root directory with Build Action: Content .
                    
                 */

                // Menu button
                var menuImage = new Image { Aspect = Aspect.AspectFit };
                menuImage.Source = ImageSource.FromFile("menu.png");
                menuImage.VerticalOptions = LayoutOptions.Center;
                menuImage.WidthRequest = 30;
                menuImage.HeightRequest = 30;

                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) => {
                    // handle the tap
                    ShowMenu();
                };
                menuImage.GestureRecognizers.Add(tapGestureRecognizer);



                HSPMenu = new ListView();
                HSPMenu.ItemsSource = ContextMenuViews;
                HSPMenu.ItemSelected += (s, e) => {
                    // handle the tap
                    GoToPage(e.SelectedItem as HSPMenuView);

                    //HSPMenu.SelectedItem = null;
                };

                // And the pop up too...
                MenuPopUp = new Popup {
                    XPositionRequest = 0, //0.5,
                    YPositionRequest = 0.15, //0.2,
                    ContentHeightRequest = 0.6,
                    ContentWidthRequest = 0.6,
                    Padding = 10,

                    Body = new ContentView {
                        BackgroundColor = Color.FromRgb(94, 0, 71),

                        Content = HSPMenu
                        //new ListView {
                        //    ItemsSource = ContextMenuItems,
                        //}

                        //new Label {
                        //            HorizontalTextAlignment = TextAlignment.Center,
                        //            VerticalTextAlignment = TextAlignment.Center,
                        //            TextColor = Color.Black,
                        //            Text = "Hello, World!"
                        //        }
                    }
                };
                MenuPopUp.Unfocused += (s, e) => {
                    MenuPopUp.Hide();
                };


                /*
                string backIm = (isHomePage == true) ? "blank" : "goback";

                var goBackImage = new Image { Aspect = Aspect.AspectFit };
                goBackImage.Source = ImageSource.FromFile(backIm + ".png");
                goBackImage.VerticalOptions = LayoutOptions.Center;
                goBackImage.WidthRequest = 30;
                goBackImage.HeightRequest = 30;

                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) => {
                    // handle the tap
                    GoBack();
                };
                goBackImage.GestureRecognizers.Add(tapGestureRecognizer);
                */
                //Button b = new Button();
                //b.Image = backIm + ".png"; // (FileImageSource) FileImageSource.FromFile(backIm + ".png");
                //b.WidthRequest = 30;
                //b.HeightRequest = 30;
                //b.VerticalOptions = LayoutOptions.Center;
                //b.BackgroundColor = new Color(255, 255, 255, 0);
                //if (isHomePage == false) {
                //    b.Clicked += GoBack;

                //    //goBackImage.GestureRecognizers.Add(new TapGestureRecognizer(GoBack));
                //}

                // Share Button
                //Button shareB = new Button();
                //shareB.Clicked += OnShareClicked;
                //shareB.Text = "Share";

                // Menu button
                var shareImage = new Image { Aspect = Aspect.AspectFit };
                shareImage.Source = ImageSource.FromFile("sharing.png");
                shareImage.VerticalOptions = LayoutOptions.Center;
                shareImage.WidthRequest = 30;
                shareImage.HeightRequest = 30;

                TapGestureRecognizer tapGestureRecognizer2 = new TapGestureRecognizer();
                tapGestureRecognizer2.Tapped += (s, e) => {
                    // handle the tap
                    OnShareClicked(s,e);
                };
                shareImage.GestureRecognizers.Add(tapGestureRecognizer2);




                var testImage = new Image { Aspect = Aspect.AspectFit };
                testImage.Source = ImageSource.FromFile("icon.png");
                //testImage.Scale = 0.4;
                testImage.VerticalOptions = LayoutOptions.Center;
                testImage.HorizontalOptions = LayoutOptions.Center;
                testImage.WidthRequest = 30;
                testImage.HeightRequest = 30;

                Label headerTxt = new Label { Text = " " + headerText };
                headerTxt.VerticalOptions = LayoutOptions.Center;
                headerTxt.HorizontalOptions = LayoutOptions.FillAndExpand;

                header = new StackLayout {
                    BackgroundColor = Color.FromRgb(246, 241, 212),
                    Padding = 2,
                    Orientation = StackOrientation.Horizontal,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Children = {
                        menuImage,
                        //goBackImage,                    
                        //b,
                        testImage,
                        headerTxt,
                        shareImage
                        //shareB

                    }
                };

            } catch (Exception ex) {

                string ohFuck = "";

            }

            return header;
        }




        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public bool GoBack() {
            bool customAction = false;

            // 11-Dec-2016 - Add additional context here too so that if the menu is visible, then back just hides the menu
            if (MenuIsVisible == true) {
                customAction = true;
                MenuIsVisible = false;
                MenuPopUp.Hide();

            } else if ( IsHomePage == false) {
                customAction = true;
                IsHomePage = true;
                //Application.Current.MainPage = new NavigationPage(new Layout.Home());

                //Page p = Layout.Default();

                Layout.Default.GoHome();

                //Application.Current.MainPage = new Layout.Home();

            }

            return customAction;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public void ShowMenu() {

            // This is how to do popup alerts ...
            //Application.Current.MainPage.DisplayAlert("", "", "", "");

            MenuIsVisible = !MenuIsVisible;

            if (MenuIsVisible == true) {
                MenuPopUp.Show();
            } else {
                MenuPopUp.Hide();
            }

            //// Show Menu
            //Application.Current.MainPage = new Layout.Contents();

        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public void GoToPage(HSPMenuView mi) {

            MenuIsVisible = false;
            //MenuPopUp.ClearValue();

            MenuPopUp.Hide();

            // ok and here we need to manage the navigation ....
            string blah = "";

            //Type t = Type.GetType(mi.Navigation);

            //Page p = Activator.CreateInstance(t) as Page;



            //            p.na
            //Application.Current.MainPage = new NavigationPage( p );
            Layout.Default.AddView(mi.Navigation);
            Layout.Default.ShowView();
            //p.Title = "back";
            //p.AddPage(mi.Navigation);
            //p.AddView(mi.Navigation);
            //p.ShowView();
            //Application.Current.MainPage = p;


            //HSPMenu.SelectedItem = null;

        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public void OnShareClicked(object sender, EventArgs e) {

            var title = "Share this page";
            var message = "To do - add the detailed message for sharing.";
            var url = "https://hspadmin.datanirvana.org";

            // Share message and an optional title ...
            //CrossShare.Current.Share(message, title);

            // Share a link and an optional title and message ...
            CrossShare.Current.ShareLink(url, message, title);

            // Share with the Clipboard if there is the possibility there ...
            if (CrossShare.Current.SupportsClipboard) {
                var text = "https://www.xamarin.com/plugins";
                CrossShare.Current.SetClipboardText(text);
            }

        }

    }
}
