using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------
namespace HSPApp.Code {
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class HSPMenuView {

        

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public HSPMenuView() {
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public HSPMenuView(string title, string navigation) {
            this.title = title;
            this.navigation = navigation;
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public HSPMenuView(string title, string navigation, string subNavigation, string description, string imageFileName) {
            this.title = title;
            this.navigation = navigation;
            this.subNavigation = subNavigation;
            this.description = description;
            this.imageFileName = imageFileName;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public string Title {
            get { return title; }
            set { title = value; }
        }
        private string title;

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public string Navigation {
            get { return navigation; }
            set { navigation = value; }
        }
        private string navigation;

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public string SubNavigation {
            get { return subNavigation; }
            set { subNavigation = value; }
        }
        private string subNavigation;


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public string Description {
            get { return description; }
            set { description = value; }
        }
        private string description;

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public string ImageFileName {
            get { return imageFileName; }
            set { imageFileName = value; }
        }
        private string imageFileName;


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public View ContentView {
            get { return contentView; }
            set { contentView = value; }
        }
        private View contentView = null;


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public override string ToString() {
            return Title;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public View GetView() {
            StackLayout slV = new StackLayout();

            StackLayout slH = new StackLayout();
            slH.Orientation = StackOrientation.Horizontal;
            // Add the image ...
            slH.Children.Add(new Image {
                Aspect = Aspect.AspectFit,
                Source = ImageSource.FromFile(ImageFileName),
                HeightRequest = 40,
                WidthRequest = 40
            });
            // Add the label ...
            slH.Children.Add(new Label {
                Text = Description,                
                FontSize = 12,
                TextColor = Color.FromRgb(28, 28, 28)
            });

            // Now add the title and then the image and text desc
            slV.Children.Add(new Label() {
                Text = Title,
                FontSize = 15,
                TextColor = Color.FromRgb(94,0,71)
            });
            slV.Children.Add(slH);



            return slV;
        }

    }
}
