using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace AnimeRandomizer
{
    /// <summary>
    /// Логика взаимодействия для Log.xaml
    /// </summary>
    public partial class Log : Window
    {
        public int Loging (string str)
        {
            TextLog.Text += ("• " + DateTime.Now.ToLongTimeString() + " =|| " + str + "\n");
            return 1;
        }

        public Log()
        {
            InitializeComponent();
            Loging("Начало");
            
        }

        private void LogWindow1_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
            LogWindow1.Loging("Журналь был скрыт");
        }
    }
}
