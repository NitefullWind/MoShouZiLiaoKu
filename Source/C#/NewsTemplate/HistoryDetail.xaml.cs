using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace NewsTemplate
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class HistoryDetail : NewsTemplate.Common.LayoutAwarePage
    {
        public HistoryDetail()
        {
            this.InitializeComponent();
            readTimeList();
        }

        private int count = 0;
        string[] timeList;
        private async void readTimeList()
        {
            StorageFolder installFolder = Package.Current.InstalledLocation;
            Stream stream = await installFolder.OpenStreamForReadAsync(@"Assets\TimeList\时间线.txt");
            StreamReader sr = new StreamReader(stream);
            string allHistory = await sr.ReadToEndAsync();
            timeList = Regex.Split(allHistory, "\n\r", RegexOptions.IgnoreCase);
            showHistory(0);
        }

        private void btn_front_Click(object sender, RoutedEventArgs e)
        {
            if (count > 0)
            {
                count--;
                showHistory(count);
            }
        }

        private void btn_next_Click(object sender, RoutedEventArgs e)
        {
            if (count < 56)
            {
                count++;
                showHistory(count);
            }
        }

        private void showHistory(int count)
        {
                txt_time.Text = timeList[2*count];
                txt_box.Text = timeList[2*count+1];
                time_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/TimeImages/" + (count+1).ToString()+".jpg", UriKind.RelativeOrAbsolute));
        }

    }
}
