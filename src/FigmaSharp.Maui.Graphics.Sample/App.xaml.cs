namespace FigmaSharp.Maui.Graphics.Sample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //var token = "figd_43YuFV2SJ1tA9S7So66nYiU3SEednmsgVt7CNeBw";
            //FigmaApplication.Init(token);

            MainPage = new AppShell();
        }
    }
}