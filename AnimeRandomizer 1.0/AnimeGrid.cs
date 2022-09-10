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
using System.Windows.Threading;


namespace AnimeRandomizer
{

    public partial class AnimeGrid : Grid
    {
        public Anime A = null;


        //Создание тестового представителя
        public AnimeGrid()
        {
            A = new Anime();
        }


        //Создание представителя из имеющегося аниме
        public AnimeGrid(Anime _A)
        {
            A = _A;
        }

        //добавка кнопки вкл\выкл в сортировке
        public void AddK1()
        {
            KButton K1 = new KButton(this);
            K1.BorderBrush = null;
            SetColumn(K1, 0);
            SetColumnSpan(K1, 5);
            K1.Click += AnimeInList_Click;
            K1.Style = (Style)Application.Current.Resources["ButtonStyle1"];

            Children.Add(K1);
        }

        //Обработчик нажатия по названию для вкл\выкл в сортировке
        public void AnimeInList_Click(object sender, RoutedEventArgs e)
        {
            if (A.IsEnabledInSort == true)
            {
                A.IsEnabledInSort = false;
                foreach (KButton KB in Children.OfType<KButton>())
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
                    KB.Background = new LinearGradientBrush(gsc, 0)
                    {
                        StartPoint = new Point(0.5, 0),
                        EndPoint = new Point(0.5, 1)
                    };
                }
            }
            else
            {
                A.IsEnabledInSort = true;
                foreach (KButton KB in Children.OfType<KButton>())
                {
                    KB.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                }
            }
        }

        public void AnimeInList_RandomOFF()
        {
            A.IsEnabledInSort = false;
            foreach (KButton KB in Children.OfType<KButton>())
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
                KB.Background = new LinearGradientBrush(gsc, 0)
                {
                    StartPoint = new Point(0.5, 0),
                    EndPoint = new Point(0.5, 1)
                };
            }
        }

        public void AnimeInList_RandomON()
        {
            A.IsEnabledInSort = true;
            foreach (KButton KB in Children.OfType<KButton>())
            {
                KB.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }
        }

    }
}
