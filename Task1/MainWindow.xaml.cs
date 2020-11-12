using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Fields

        private TaskDefine oTaskDefine;

        #endregion Private Fields

        #region Public Constructors

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Private Methods

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            oTaskDefine = new TaskDefine();
            oTaskDefine.OnTaskComplete += (s, a) => StatusBlock.Dispatcher.Invoke(new Action(() => StatusBlock.Text += $"Task {a.Item1} completed in {a.Item2}.\n"));
            //await oTaskDefine.StartTasks();
            for (int i = 0; i < 15; i++)
            {
                oTaskDefine.AddTask(i);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            oTaskDefine?.Dispose();
        }

        #endregion Private Methods
    }
}