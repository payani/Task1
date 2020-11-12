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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TaskDefine oTaskDefine = new TaskDefine();
            oTaskDefine.OnTaskComplete += (s, a) => StatusBlock.Dispatcher.Invoke(new Action(() => StatusBlock.Text += $"Task {a.Item1} completed in {a.Item2}.\n"));
            //await oTaskDefine.StartTasks();
            for (int i = 0; i < 15; i++)
            {
                oTaskDefine.AddTask(i);
            }
        }
    }
}