using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
namespace HSPApp.Layout {
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class Welcome : ContentView { // ContentPage {
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public Welcome() {

            string path = AppHelper.AssetPath;
            int width = 150;
            int height = 50;

            Image im1 = new Image {
                Aspect = Aspect.AspectFit,
                Source = ImageSource.FromFile("sphere_logo.jpg"),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = height,
                WidthRequest = width
            };
            Image im2 = new Image {
                Aspect = Aspect.AspectFit,
                Source = ImageSource.FromFile("calp_logo.png"),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = height,
                WidthRequest = width
            };
            Image im3 = new Image {
                Aspect = Aspect.AspectFit,
                Source = ImageSource.FromFile("alliance_logo.png"),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = height,
                WidthRequest = width
            };
            Image im4 = new Image {
                Aspect = Aspect.AspectFit,
                Source = ImageSource.FromFile("seep_logo.gif"),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = height,
                WidthRequest = width
            };
            Image im5 = new Image {
                Aspect = Aspect.AspectFit,
                Source = ImageSource.FromFile("legs_logo.png"),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = height,
                WidthRequest = width
            };
            Image im6 = new Image {
                Aspect = Aspect.AspectFit,
                Source = ImageSource.FromFile("inee_logo.png"),
                HeightRequest = height,
                WidthRequest = width
            };


            Content = new StackLayout {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                Padding = 2,
                Children = {
                    new StackLayout {
                        BackgroundColor = Color.FromRgb(246, 241, 212),
                        HorizontalOptions = LayoutOptions.Center,
                        WidthRequest = width + 30,
                        Padding = 10,
                        Children = {
                            new Label {
                                HorizontalOptions = LayoutOptions.Center,
                                WidthRequest = width + 10,
                                Text = "HUMANITARIAN STANDARDS PARTNERSHIP",
                                BackgroundColor = Color.FromRgb(246, 241, 212),
                                TextColor = Color.FromRgb(94, 0, 71),
                                FontSize = 20
                            }
                        }
                    },
                    im1,
                    im2,
                    im3,
                    im4,
                    im5,
                    im6
                }
            };
        }
    }
}
