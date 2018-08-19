using System;
using System.Windows;

namespace Undead.Music.Player
{
    public partial class ErrorWindow : Window
    {
        public ErrorWindow(Exception ex)
        {
            InitializeComponent();
            ShowException(ex);
        }

        public void ShowException(Exception ex)
        {
            txtErrorText.Text = ex.Message;
            txtErrorText.Text += ex.StackTrace;
            txtErrorText.Text += ex.InnerException;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void lblCopyToClipboard_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Clipboard.SetText(txtErrorText.Text);
        }
    }
}