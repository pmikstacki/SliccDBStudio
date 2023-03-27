using System.Windows;
using BlazorTextEditor.RazorLib;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using SliccDBStudio.Services;
using VisNetwork.Blazor;

namespace SliccDBStudio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var services = new ServiceCollection();
            services.AddBlazorTextEditor();
            services.AddMudServices();
            services.AddMudMarkdownServices();
            services.AddWpfBlazorWebView();
            services.AddBlazorWebViewDeveloperTools();
            services.AddSingleton<DatabaseConnectionService>();
            services.AddSingleton<LessonService>();
            services.AddSingleton<GraphDisplayService>();
            services.AddVisNetwork();
            services.AddBlazorWebView();
            Resources.Add("services", services.BuildServiceProvider());

            InitializeComponent();
        }
    }
}