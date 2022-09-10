using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
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
using System.Windows.Threading;

namespace AnimeRandomizer
{
    class KButtonResult : Button
    {
        public KButtonResult(AnimeGridResult owner_san)
        {
            Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            Cursor = Cursors.ArrowCD;
        }
    }
}
