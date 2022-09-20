namespace FigmaSharp.Maui.Graphics
{
    public class FigmaApplication
    {
        public static void Init(string token)
        {
            var applicationDelegate = new FigmaDelegate();

            AppContext.Current.Configuration(applicationDelegate, token);
        }
    }
}