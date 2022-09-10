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
using System.Windows.Shapes;

namespace AnimeRandomizer
{
    /// <summary>
    /// Логика взаимодействия для Winners.xaml
    /// </summary>
    public partial class Winners : Window
    {
        public Winners(int id, List<Anime> list, Window owner_san)
        {
            InitializeComponent();
            switch (id)
            {
                case 1:
                    Title = "Winners List GOLD";
                    break;
                case 2:
                    Title = "Winners List SILVER";
                    break;
                case 3:
                    Title = "Winners List BRASS";
                    break;
                default:
                    Title = "Winners List #" + id;
                    break;
            }
            Background = owner_san.Background;
            foreach (var A in list)
                Functions.AddAnimeToResultAnimeList(ResultAnimeList, new Anime(A));
            Show();
        }
    }
}
