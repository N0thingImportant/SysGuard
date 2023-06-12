using System.Windows;
using System.Windows.Input;

namespace ProcessMonitor.Views
{
    /// <summary>
    /// Logika interakcji dla klasy ProcessControlWindow.xaml
    /// </summary>
    public partial class ProcessControlWindow : Window
    {
        public ProcessControlWindow()
        {
            InitializeComponent();
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
