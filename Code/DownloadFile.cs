using PCLStorage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------
namespace HSPApp.Code {
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class DownloadFile {

        public static string FileURLToDownload;
        static int currentFileTotalBytes = 0;

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public static async Task<int> CreateDownloadTask(string urlToDownload, string localFolderName, string localFileName, IProgress<DownloadBytesProgress> progessReporter) {
            int receivedBytes = 0;
//            int totalBytes = 0;
            HttpClient client = new HttpClient();
            //WebClient client = new WebClient();

            // Use PCL Storage to get the current root dir
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            // create a folder, if one does not exist already
            IFolder folder = await rootFolder.CreateFolderAsync(localFolderName, CreationCollisionOption.OpenIfExists);
            // create a file, overwriting any existing file
            IFile file = await folder.CreateFileAsync(localFileName, CreationCollisionOption.ReplaceExisting);

            StringBuilder s = new StringBuilder();

            using (var stream = await client.GetStreamAsync(urlToDownload)) {
                byte[] buffer = new byte[4096];

                //client.
                //int length = int.Parse(client.headhttpInitialResponse.Content.Headers.First(h => h.Key.Equals("Content-Length")).Value.First());
                //                totalBytes = Int32.Parse(client.ResponseHeaders[HttpResponseHeaders.ContentLength]);

                for (;;) {
                    //                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) {
                        await Task.Yield();
                        break;
                    }

                    s.Append(System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length));

                    receivedBytes += bytesRead;
                    if (progessReporter != null) {
                        DownloadBytesProgress args = new DownloadBytesProgress(urlToDownload, receivedBytes, currentFileTotalBytes);
                        progessReporter.Report(args);
                    }
                }
            }
           
            await file.WriteAllTextAsync(s.ToString());

            return receivedBytes;
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public static void SetFileAttributes( int totalBytes) {
            currentFileTotalBytes = totalBytes;
        }

        // Android button handler ...

        // IOS button handler
        //async void StartDownloadHandlerIoS(object sender, EventArgs e) {
        //    ProgressBar.Progress = 0f;

        //    Progress<DownloadBytesProgress> progressReporter = new Progress<DownloadBytesProgress>();
        //    progressReporter.ProgressChanged += (s, args) => ProgressBar.Progress = args.PercentComplete;

        //    Task<int> downloadTask = CreateDownloadTask(DownloadFile.ImageToDownload, progressReporter);
        //    int bytesDownloaded = await downloadTask;
        //    Debug.WriteLine("Downloaded {0} bytes.", bytesDownloaded);
        //}

    }
}
