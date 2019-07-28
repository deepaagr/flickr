using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ImageSearchWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void OnDownloadFailed(Object sender, ExceptionRoutedEventArgs e)
        { 
            try{
            var image = sender as Image;
                BitmapImage fallbackImage = new BitmapImage(new Uri("Images/ImageNotLoaded.png",UriKind.Relative)); 
                image.Source = fallbackImage;//new Uri("Images/ImageNotLoaded.png", UriKind.Relative);C:\Deepa\preparation\POCs\ImageSearchWPF\ImageSearchWPF\Images\ImageNotLoaded.png
               // image.UriSource = new Uri("pack://application:,,,/ImageSearchWPF;component/Images/ImageNotFound.png");//{pack://application:,,,/ImageSearchWPF;component/mainwindow.xaml}
            }
    catch(Exception ex)
        {}
           
        }
    }
}
