using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Limxc.Tools.Vision.Models;
using Limxc.Tools.Vision.Utils;
using Refit;
using TestHost.ApiInterface;
using TestHost.Extensions;

namespace TestHost;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    { 
    }

    private void Local()
    {
        var base64Image = ImgSource.FromImageControl();
        var gray = CvHelper.ToGray(base64Image);

        if (string.IsNullOrWhiteSpace(gray))
            return;

        ImgTarget.Source =
            gray.ToBitmapImage();
    }

    private async Task Api(string baseUrl)
    {
        var base64Image = ImgSource.FromImageControl();
        var meta = await RestService.For<IVision>(baseUrl).TurnGray(new MetaImage() { Base64 = base64Image });
        var gray = meta.Base64;
        if (string.IsNullOrWhiteSpace(gray))
            return;

        ImgTarget.Source =
            gray.ToBitmapImage(); 
    }

    private void BtnLocal_OnClick(object sender, RoutedEventArgs e)
    {
        Local();
    }

    private async void BtnApi_OnClick(object sender, RoutedEventArgs e)
    {
        try
        { 
            await Api(ConstNames.BaseUrl);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.ToString());
        }
    }

    private async void BtnDockerApi_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            await Api(ConstNames.BaseDockerUrl);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.ToString());
        }
    }
}