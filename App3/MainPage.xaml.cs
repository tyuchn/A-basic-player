using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;//HttpClient所属
using System.Threading.Tasks;//Task所属
using Windows.Storage.Streams;//IBuffer所属
using Windows.Media.Playback;//MediaPlayer所属

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace App3
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// test
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }


        private async void add_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker();

            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.VideosLibrary;
            
            openPicker.FileTypeFilter.Add(".mp4");
            openPicker.FileTypeFilter.Add(".mp3");
            

            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                var mediaSource = MediaSource.CreateFromStorageFile(file);
                MediaPlayer.Source = mediaSource;
                
                
            }
            
          
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            System.Uri manifestUri = new Uri("http://www.neu.edu.cn/indexsource/neusong.mp3");
            MediaPlayer.Source = MediaSource.CreateFromUri(manifestUri);
        }



        private  async void AppButton3_Click(object sender, RoutedEventArgs e)
        {
            
                var httpClient = new HttpClient();
                var buffer = await httpClient.GetBufferAsync(new Uri("http://www.neu.edu.cn/indexsource/neusong.mp3"));
            if (buffer != null && buffer.Length > 0u)
            {

                StorageFile destinationFile = await KnownFolders.MusicLibrary.CreateFileAsync("东北大学校歌.mp3");
                using (var stream = await destinationFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    await stream.WriteAsync(buffer);
                    await stream.FlushAsync();
                }
                var mediaSource = MediaSource.CreateFromStorageFile(destinationFile);
                MediaPlayer.Source = mediaSource;
            }
                
            

        }

      



    }
}
