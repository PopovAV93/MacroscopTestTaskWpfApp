using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MacroscopTestTaskWpfApp.CustomControls;

public partial class ImageLoader : UserControl
{
    public ImageLoader()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty DelayProperty =
        DependencyProperty.Register("Delay", typeof(int), typeof(ImageLoader));

    public int Delay
    {
        get { return (int)GetValue(DelayProperty); }
        set { SetValue(DelayProperty, value); }
    }

    public static readonly DependencyProperty Button1TextProperty =
        DependencyProperty.Register("Button1Text", typeof(string), typeof(ImageLoader),
            new FrameworkPropertyMetadata(string.Empty));

    public string Button1Text
    {
        get { return (string)GetValue(Button1TextProperty); }
        set { SetValue(Button1TextProperty, value); }
    }

    public static readonly DependencyProperty Button2TextProperty =
        DependencyProperty.Register("Button2Text", typeof(string), typeof(ImageLoader),
            new FrameworkPropertyMetadata(string.Empty));

    public string Button2Text
    {
        get { return (string)GetValue(Button2TextProperty); }
        set { SetValue(Button2TextProperty, value); }
    }

    public static readonly DependencyProperty UriTextProperty =
        DependencyProperty.Register("UriText", typeof(string), typeof(ImageLoader),
            new FrameworkPropertyMetadata(string.Empty));

    public string UriText
    {
        get { return (string)GetValue(UriTextProperty); }
        set { SetValue(UriTextProperty, value); }
    }

    public static readonly DependencyProperty IsImageLoadingProperty =
        DependencyProperty.Register("IsImageLoading", typeof(bool), typeof(ImageLoader),
            new FrameworkPropertyMetadata(false));

    public bool IsImageLoading
    {
        get { return (bool)GetValue(IsImageLoadingProperty); }
        set { SetValue(IsImageLoadingProperty, value); }
    }

    //public event EventHandler<DependencyPropertyChangedEventArgs>? IsImageLoadedPropertyChanged;

    public static readonly DependencyProperty IsImageLoadedProperty =
        DependencyProperty.Register("IsImageLoaded", typeof(bool), typeof(ImageLoader),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)); //,OnIsImageLoadedPropertyChanged));
    /*
    private static void OnIsImageLoadedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((ImageLoader)d).IsImageLoadedPropertyChanged?.Invoke(d,e);
    }
    */

    public bool IsImageLoaded
    {
        get { return (bool)GetValue(IsImageLoadedProperty); }
        set { SetValue(IsImageLoadedProperty, value); }
    }

    private static readonly DependencyProperty ImageSourceProperty =
        DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ImageLoader));

    public ImageSource? ImageSource
    {
        get { return (ImageSource)GetValue(ImageSourceProperty); }
        set { SetValue(ImageSourceProperty, value); }
    }

    public static readonly DependencyProperty Button1CommandProperty =
        DependencyProperty.Register("Button1Command", typeof(ICommand), typeof(ImageLoader));

    public ICommand Button1Command
    {
        get => (ICommand)GetValue(Button1CommandProperty);
        set => SetValue(Button1CommandProperty, value);
    }

    public static readonly DependencyProperty Button1ParameterProperty =
        DependencyProperty.Register("Button1Parameter", typeof(object), typeof(ImageLoader));

    public object Button1Parameter
    {
        get => GetValue(Button1ParameterProperty);
        set => SetValue(Button1ParameterProperty, value);
    }

    public static readonly DependencyProperty Button2CommandProperty =
        DependencyProperty.Register("Button2Command", typeof(ICommand), typeof(ImageLoader));

    public ICommand Button2Command
    {
        get => (ICommand)GetValue(Button2CommandProperty);
        set => SetValue(Button2CommandProperty, value);
    }

    public static readonly DependencyProperty Button2ParameterProperty =
        DependencyProperty.Register("Button2Parameter", typeof(object), typeof(ImageLoader));

    public object Button2Parameter
    {
        get => GetValue(Button2ParameterProperty);
        set => SetValue(Button2ParameterProperty, value);
    }

    public CancellationTokenSource CancellationTokenSource = new ();
}
