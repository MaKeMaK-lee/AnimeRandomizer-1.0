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
    /// <summary>
    /// Класс кнопки включения\выключения анимки для рандома. (Кнопка на Animegrid в списке).
    /// </summary>
    public class KButton : Button
    {
        public KButton(AnimeGrid owner_san)
        {
            if (owner_san.A.IsEnabledInSort == true)
                Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            else
            {
                GradientStopCollection gsc = new GradientStopCollection();
                gsc.Add(new GradientStop()
                {
                    Color = Color.FromArgb(120, 0, 0, 0),
                    Offset = 0.0
                });
                gsc.Add(new GradientStop()
                {
                    Color = Color.FromArgb(210, 0, 0, 0),
                    Offset = 1.0
                });
                Background = new LinearGradientBrush(gsc, 0)
                {
                    StartPoint = new Point(0.5, 0),
                    EndPoint = new Point(0.5, 1)
                };
            }
        }
    }
}
