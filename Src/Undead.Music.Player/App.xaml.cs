using CrossCutting.Core.Logging;
using CrossCutting.MainModule.IOC;

namespace Undead.Music.Player
{
    public partial class App : System.Windows.Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            new IocUnityContainer().Resolve<ILogService>().Error("Unhandled Error: " + e.Exception);
            ErrorWindow errorWindow = new ErrorWindow(e.Exception);
            try
            {
                errorWindow.Owner = Current.MainWindow;
            }
            catch
            {
                errorWindow.Owner = null;
            }

            errorWindow.ShowDialog();
            e.Handled = true;
        }
    }
}