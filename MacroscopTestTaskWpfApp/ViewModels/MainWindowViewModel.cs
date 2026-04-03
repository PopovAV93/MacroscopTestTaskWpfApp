using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MacroscopTestTaskWpfApp.CustomControls;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MacroscopTestTaskWpfApp.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private int progress;

    [ObservableProperty]
    private bool isImage1Loaded;

    [ObservableProperty]
    private bool isImage2Loaded;

    [ObservableProperty]
    private bool isImage3Loaded;

    private static readonly string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".svg" };

    private static bool IsImageUrl(string url) => imageExtensions.Any(ext => url.EndsWith(ext, StringComparison.OrdinalIgnoreCase));

    partial void OnIsImage1LoadedChanged(bool value) => UpdateProgress();

    partial void OnIsImage2LoadedChanged(bool value) => UpdateProgress();

    partial void OnIsImage3LoadedChanged(bool value) => UpdateProgress();

    private  void UpdateProgress()
    {
        var array = new bool[3] { IsImage1Loaded, IsImage2Loaded, IsImage3Loaded };
        Progress = array.Count(isTrue => isTrue);
    }

    [RelayCommand]
    private static void StartLoadingImage(ImageLoader imgLoader)
    {
        imgLoader.Dispatcher.InvokeAsync(async () =>
        {
            if (!string.IsNullOrEmpty(imgLoader.UriText) && !imgLoader.IsImageLoading)
            {
                await StartLoading(imgLoader);
            }
        });        
    }

    private static async Task StartLoading(ImageLoader imgLoader)
    {
        imgLoader.IsImageLoading = true;
        imgLoader.IsImageLoaded = false;
        imgLoader.ImageSource = null;
        imgLoader.CancellationTokenSource = new();
        var cancellationToken = imgLoader.CancellationTokenSource.Token;
        var isImageUrl = IsImageUrl(imgLoader.UriText);
        if (!isImageUrl)
        {
            imgLoader.IsImageLoading = false;
            return;
        }

        await Task.Delay(imgLoader.Delay, cancellationToken);

        var bi = new BitmapImage();
        bi.BeginInit();
        bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
        bi.CacheOption = BitmapCacheOption.OnLoad;
        try
        {
            bi.UriSource = new Uri(imgLoader.UriText, UriKind.Absolute);
        }
        catch (Exception ex)
        {
            cancellationToken.ThrowIfCancellationRequested();
            imgLoader.IsImageLoading = false;
            return;
        }
        bi.EndInit();
        imgLoader.ImageSource = bi;
        imgLoader.IsImageLoaded = true;
        imgLoader.IsImageLoading = false;
    }

    [RelayCommand]
    private static void StopLoadingImage(ImageLoader imgLoader)
    {
        imgLoader.Dispatcher.Invoke(() =>
        {
            imgLoader.CancellationTokenSource.Cancel();
            imgLoader.IsImageLoading = false;
        });
    }

    [RelayCommand]
    private static async Task LoadAll(Grid grid)
    { 
        List<Task> tasks = new();

        var imageLoaders = grid?.Children?.OfType<ImageLoader>().ToList();
        if (imageLoaders is not null && imageLoaders.Count > 0)
        {
            foreach (var imageLoader in imageLoaders)
            {
                tasks.Add(Task.Run(() =>
                    {
                        StopLoadingImage(imageLoader);
                        StartLoadingImage(imageLoader);
                    }));
            }
            await Task.WhenAll(tasks);
        }
    }
}
