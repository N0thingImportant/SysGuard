using System.Diagnostics;
using System.IO;
using System.Windows.Controls;

namespace ProcessMonitor.Views
{
    /// <summary>
    /// Interaction logic for ProcessDetails.xaml
    /// </summary>
    public partial class ProcessDetails : UserControl
    {
        public ProcessDetails()
        {
            InitializeComponent();
        }

        private void TextBox_SelectProcessPath(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var pName = (sender as TextBox).Text;
            if (File.Exists(pName))
            {
                Process.Start("explorer", $"/select,\"{pName}\"");
            }
        }
    }
}
