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
    public class POItem : ComboBoxItem
    {
        public ProfileOptions PO = null;

        public POItem(ProfileOptions _po)
        {
            PO = _po;

            Content = PO.NAME;


        }
    }
}
