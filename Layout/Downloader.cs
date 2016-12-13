using HSPApp.Code;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
namespace HSPApp.Layout {
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class Downloader : ContentView {

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        AppHelper aHelper = new AppHelper();
        //DownloadFile df = new DownloadFile();

        Label result = new Label();
        BoxView progressBar = null;
        Label txt = new Label();

        public int currentProgress = 0;
        string localFolder = "HSPApp";
        string localFileName = "Directory.xml";

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------
        public Downloader() {

            DownloadFile.SetFileAttributes(29000); // 556000
            DownloadFile.FileURLToDownload = "https://www.dropbox.com/s/6joh2behije38en/Testing.txt?dl=1";

            progressBar = new BoxView {
                Color = Color.Accent,
                WidthRequest = 10,
                HeightRequest = 5,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Start
            };

            SetPage(CreatePage());






            //df.StartDownloadHandlerAndroid

//            SetPage(CreatePage());

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

            result.Text = "0%";

//            Image i = new Image();
//            i.Source = 

            StackLayout sl = new StackLayout();
            sl.HorizontalOptions = LayoutOptions.FillAndExpand;
            //sl.Children.Add(header);
            sl.Children.Add(new Label { Text = "Blah Blah" });
            sl.Children.Add(AppHelper.CreateButton("Start download", StartDownloadHandlerAndroid));

            sl.Children.Add(result);
            sl.Children.Add(progressBar);
            sl.Children.Add(txt);
            return sl;
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public void SetPage(StackLayout sl) {

            Content = sl;
            //Title = AppHelper.TitleText;

        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public async void StartDownloadHandlerAndroid(object sender, System.EventArgs e) {
            //_progressBar.Progress = 0;
            currentProgress = 0;

            

            Progress<DownloadBytesProgress> progressReporter = new Progress<DownloadBytesProgress>();
            progressReporter.ProgressChanged += (s, args) => {
                currentProgress = (int)(100 * args.PercentComplete);  // _progressBar.Progress = (int)(100 * args.PercentComplete);
                result.Text = currentProgress.ToString();
                if (progressBar.Width != currentProgress) {
                    progressBar.WidthRequest = currentProgress;
                }
            };

            Task<int> downloadTask = DownloadFile.CreateDownloadTask(DownloadFile.FileURLToDownload, localFolder, localFileName, progressReporter);
            int bytesDownloaded = await downloadTask;
            System.Diagnostics.Debug.WriteLine("Downloaded {0} bytes.", bytesDownloaded);

            ReadFile(localFolder, localFileName);

        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public async void ReadFile(string localFolder, string localFileName) {

            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFile myFile = await (await rootFolder.GetFolderAsync(localFolder)).GetFileAsync(localFileName);

            txt.Text = await myFile.ReadAllTextAsync();

            string blah = "";

        }

    }
}
