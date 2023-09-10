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

    public partial class AnimeGridResult : Grid
    {
        public Anime A = null;


        //Создание тестового представителя
        public AnimeGridResult()
        {
            A = new Anime();
        }


        //Создание представителя из имеющегося аниме
        public AnimeGridResult(Anime _A)
        {
            A = _A;
        }

        //добавка кнопки вкл\выкл в сортировке
        public void AddK1()
        {
            KButtonResult K1 = new KButtonResult(this);
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
            System.Diagnostics.Process.Start("https://shikimori.me/animes/" + A.series_animedb_id);
        }

    }
}
