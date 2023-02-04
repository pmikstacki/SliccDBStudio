namespace SliccDBStudio;

public partial class MainPage : ContentPage
{
    public static MainPage Page; 

    public MainPage()
	{
		InitializeComponent();
        Page = this;
    }
    private void Bwv_BlazorWebViewInitialized(object sender, Microsoft.AspNetCore.Components.WebView.BlazorWebViewInitializedEventArgs e)
    {
#if WINDOWS
        e.WebView.CoreWebView2.Settings.IsZoomControlEnabled = false;
#endif
        
    }
}
