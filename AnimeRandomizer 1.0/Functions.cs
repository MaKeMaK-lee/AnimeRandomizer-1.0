using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Net;
using System.Net.Http;
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
    class Functions
    {
        static public ReturningMessage AddAnimeToCurrentAnimeList(StackPanel _CurrentAnimeList, Anime _anime = null)
        {
            AnimeGrid tmp = null;
            if (_anime != null)
                tmp = new AnimeGrid(_anime);
            else
                tmp = new AnimeGrid();

            tmp.Height = 30;
            tmp.Margin = new Thickness(2, 0, 2, 0);

            //Цвет в зависимости от my_status 

            switch (tmp.A.shiki_status)
            {
                case "Completed":
                    tmp.Background = new SolidColorBrush(Color.FromArgb(151, 26, 190, 0));
                    break;
                case "Plan to Watch":
                    tmp.Background = new SolidColorBrush(Color.FromArgb(151, 243, 255, 0));
                    break;
                case "Watching":
                    tmp.Background = new SolidColorBrush(Color.FromArgb(151, 0, 209, 255));
                    break;
                case "Rewatching":
                    tmp.Background = new SolidColorBrush(Color.FromArgb(151, 77, 28, 243));
                    break;
                case "On-Hold":
                    tmp.Background = new SolidColorBrush(Color.FromArgb(151, 174, 146, 21));
                    break;
                case "Dropped":
                    tmp.Background = new SolidColorBrush(Color.FromArgb(151, 178, 24, 24));
                    break;
                default:
                    tmp.Background = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                    break;
            }

            //ColumnDefinitions

            ColumnDefinition cdtmp3 = new ColumnDefinition();
            GridLength gltmp = new GridLength(1, GridUnitType.Star);
            cdtmp3.Width = (gltmp);
            cdtmp3.MinWidth = 74;
            tmp.ColumnDefinitions.Add(cdtmp3);

            ColumnDefinition cdtmp2 = new ColumnDefinition();
            cdtmp2.Width = new GridLength(50);
            tmp.ColumnDefinitions.Add(cdtmp2);

            ColumnDefinition cdtmp1 = new ColumnDefinition();
            cdtmp1.Width = new GridLength(70);
            tmp.ColumnDefinitions.Add(cdtmp1);

            ColumnDefinition cdtmp4 = new ColumnDefinition();
            cdtmp4.Width = new GridLength(65);
            tmp.ColumnDefinitions.Add(cdtmp4);

            ColumnDefinition cdtmp5 = new ColumnDefinition();
            cdtmp5.Width = new GridLength(17);
            tmp.ColumnDefinitions.Add(cdtmp5);


            //Всплывающая подсказка
            tmp.ToolTip = $"{tmp.A.series_title}\n{TranslateShikiStatusEngToRus(tmp.A.shiki_status)}{(tmp.A.my_score == 0 ? "" : $", {tmp.A.my_score}")}\n{tmp.A.my_watched_episodes}/{(tmp.A.series_episodes != 0 ? Convert.ToString(tmp.A.series_episodes) : "?")}\t{TranslateSeriesTypeEngToRus(tmp.A.series_type)}{(tmp.A.my_times_watched == 0 ? "" : $"\nКол-во повторных просмотров:{tmp.A.my_times_watched}")}{(tmp.A.my_comments == "" ? "" : $"\nКомментарий:\n{tmp.A.my_comments}")}";

            //Добавление текстбокса 1
            TextBox TB1 = new TextBox();
            Grid.SetColumn(TB1, 2);
            TB1.Background = null;
            TB1.Margin = new Thickness(1, 1, 1, 1);
            TB1.BorderBrush = null;
            if (tmp.A.series_episodes != 0)
                TB1.Text = tmp.A.my_watched_episodes + "/" + tmp.A.series_episodes;
            else
                TB1.Text = tmp.A.my_watched_episodes + "/(?)";
            TB1.FontFamily = new FontFamily("Calibri");
            TB1.FontSize = 18;
            TB1.TextAlignment = TextAlignment.Center;
            TB1.TextDecorations = null;
            TB1.IsReadOnly = true;
            TB1.SelectionBrush = null;
            TB1.Foreground = new SolidColorBrush(Colors.Black);
            TB1.ClipToBounds = true;
            TB1.BorderThickness = new Thickness(0);
            TB1.SelectionOpacity = 1;
            TB1.Cursor = Cursors.Arrow;
            TB1.AllowDrop = false;

            tmp.Children.Add(TB1);


            //Добавление текстбокса 2
            TextBox TB2 = new TextBox();
            Grid.SetColumn(TB2, 1);
            TB2.Background = null;
            TB2.Margin = new Thickness(1, 1, 1, 1);
            TB2.BorderBrush = null;
            if (tmp.A.my_score != 0)
                TB2.Text = Convert.ToString(Convert.ToInt32(tmp.A.my_score));
            TB2.FontFamily = new FontFamily("Calibri");
            TB2.FontSize = 18;
            TB2.TextAlignment = TextAlignment.Center;
            TB2.TextDecorations = null;
            TB2.IsReadOnly = true;
            TB2.SelectionBrush = null;
            TB2.Foreground = new SolidColorBrush(Colors.Black);
            TB2.ClipToBounds = true;
            TB2.BorderThickness = new Thickness(0);
            TB2.SelectionOpacity = 1;
            TB2.Cursor = Cursors.Arrow;
            TB2.AllowDrop = false;

            tmp.Children.Add(TB2);


            //Добавление текстбокса 3
            TextBox TB3 = new TextBox();
            Grid.SetColumn(TB3, 0);
            TB3.Background = null;
            TB3.Margin = new Thickness(1, 1, 1, 1);
            TB3.BorderBrush = null;
            TB3.Text = tmp.A.series_title;
            TB3.FontFamily = new FontFamily("Calibri");
            TB3.FontSize = 18;
            TB3.TextAlignment = TextAlignment.Center;
            TB3.TextDecorations = null;
            TB3.IsReadOnly = true;
            TB3.SelectionBrush = null;
            TB3.Foreground = new SolidColorBrush(Colors.Black);
            TB3.ClipToBounds = true;
            TB3.BorderThickness = new Thickness(0);
            TB3.SelectionOpacity = 1;
            TB3.Cursor = Cursors.Arrow;
            TB3.AllowDrop = false;

            tmp.Children.Add(TB3);




            //Добавление текстбокса 4
            TextBox TB4 = new TextBox();
            Grid.SetColumn(TB4, 3);
            TB4.Background = null;
            TB4.Margin = new Thickness(1, 1, 1, 1);
            TB4.BorderBrush = null;
            TB4.Text = TranslateSeriesTypeEngToRus(tmp.A.series_type);
            TB4.FontFamily = new FontFamily("Calibri");
            TB4.FontSize = 18;
            TB4.TextAlignment = TextAlignment.Center;
            TB4.TextDecorations = null;
            TB4.IsReadOnly = true;
            TB4.SelectionBrush = null;
            TB4.Foreground = new SolidColorBrush(Colors.Black);
            TB4.ClipToBounds = true;
            TB4.BorderThickness = new Thickness(0);
            TB4.SelectionOpacity = 1;
            TB4.Cursor = Cursors.Arrow;
            TB4.AllowDrop = false;

            tmp.Children.Add(TB4);

            //Добавление кнопки вкл/выкл
            tmp.AddK1();

            //Добавление в стек
            _CurrentAnimeList.Children.Add(tmp);

            ReturningMessage ret = new ReturningMessage();
            return ret;
        }

        static public ReturningMessage AddAnimeToResultAnimeList(ListBox _ResultAnimeList, Anime _anime = null)
        {
            AnimeGridResult tmp = null;
            if (_anime != null)
                tmp = new AnimeGridResult(_anime);
            else
                tmp = new AnimeGridResult();

            tmp.Height = 30;
            //tmp.Margin = new Thickness(2, 0, 2, 0);

            //Цвет в зависимости от my_status 

            switch (tmp.A.shiki_status)
            {
                case "Completed":
                    tmp.Background = new SolidColorBrush(Color.FromArgb(151, 26, 190, 0));
                    break;
                case "Plan to Watch":
                    tmp.Background = new SolidColorBrush(Color.FromArgb(151, 243, 255, 0));
                    break;
                case "Watching":
                    tmp.Background = new SolidColorBrush(Color.FromArgb(151, 0, 209, 255));
                    break;
                case "Rewatching":
                    tmp.Background = new SolidColorBrush(Color.FromArgb(151, 77, 28, 243));
                    break;
                case "On-Hold":
                    tmp.Background = new SolidColorBrush(Color.FromArgb(151, 174, 146, 21));
                    break;
                case "Dropped":
                    tmp.Background = new SolidColorBrush(Color.FromArgb(151, 178, 24, 24));
                    break;
                default:
                    tmp.Background = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                    break;
            }

            //ColumnDefinitions

            ColumnDefinition cdtmp3 = new ColumnDefinition();
            GridLength gltmp = new GridLength(1, GridUnitType.Star);
            cdtmp3.Width = (gltmp);
            cdtmp3.MinWidth = 74;
            tmp.ColumnDefinitions.Add(cdtmp3);

            ColumnDefinition cdtmp2 = new ColumnDefinition();
            cdtmp2.Width = new GridLength(_anime.my_score>0?30:0);
            tmp.ColumnDefinitions.Add(cdtmp2);

            //ColumnDefinition cdtmp1 = new ColumnDefinition();
            //cdtmp1.Width = new GridLength(70);
            //tmp.ColumnDefinitions.Add(cdtmp1);

            ColumnDefinition cdtmp4 = new ColumnDefinition();
            cdtmp4.Width = new GridLength(65);
            tmp.ColumnDefinitions.Add(cdtmp4);

            //ColumnDefinition cdtmp5 = new ColumnDefinition();
            //cdtmp5.Width = new GridLength(17);
            //tmp.ColumnDefinitions.Add(cdtmp5);


            //Всплывающая подсказка
            tmp.ToolTip = $"{tmp.A.series_title}\n{TranslateShikiStatusEngToRus(tmp.A.shiki_status)}{(tmp.A.my_score == 0 ? "" : $", {tmp.A.my_score}")}\n{tmp.A.my_watched_episodes}/{(tmp.A.series_episodes != 0 ? Convert.ToString(tmp.A.series_episodes) : "?")}\t{TranslateSeriesTypeEngToRus(tmp.A.series_type)}{(tmp.A.my_times_watched == 0 ? "" : $"\nКол-во повторных просмотров:{tmp.A.my_times_watched}")}{(tmp.A.my_comments == "" ? "" : $"\nКомментарий:\n{tmp.A.my_comments}")}";

            ////Добавление текстбокса 1
            //TextBox TB1 = new TextBox();
            //Grid.SetColumn(TB1, 2);
            //TB1.Background = null;
            //TB1.Margin = new Thickness(1, 1, 1, 1);
            //TB1.BorderBrush = null;
            //if (tmp.A.series_episodes != 0)
            //    TB1.Text = tmp.A.my_watched_episodes + "/" + tmp.A.series_episodes;
            //else
            //    TB1.Text = tmp.A.my_watched_episodes + "/(?)";
            //TB1.FontFamily = new FontFamily("Calibri");
            //TB1.FontSize = 18;
            //TB1.TextAlignment = TextAlignment.Center;
            //TB1.TextDecorations = null;
            //TB1.IsReadOnly = true;
            //TB1.SelectionBrush = null;
            //TB1.Foreground = new SolidColorBrush(Colors.Black);
            //TB1.ClipToBounds = true;
            //TB1.BorderThickness = new Thickness(0);
            //TB1.SelectionOpacity = 1;
            //TB1.Cursor = Cursors.Arrow;
            //TB1.AllowDrop = false;
            //
            //tmp.Children.Add(TB1);


            //Добавление текстбокса 2
            TextBox TB2 = new TextBox();
            Grid.SetColumn(TB2, 1);
            TB2.Background = null;
            TB2.Margin = new Thickness(1, 1, 1, 1);
            TB2.BorderBrush = null;
            if (tmp.A.my_score != 0)
                TB2.Text = Convert.ToString(Convert.ToInt32(tmp.A.my_score));
            TB2.FontFamily = new FontFamily("Calibri");
            TB2.FontSize = 18;
            TB2.TextAlignment = TextAlignment.Center;
            TB2.TextDecorations = null;
            TB2.IsReadOnly = true;
            TB2.SelectionBrush = null;
            TB2.Foreground = new SolidColorBrush(Colors.Black);
            TB2.ClipToBounds = true;
            TB2.BorderThickness = new Thickness(0);
            TB2.SelectionOpacity = 1;
            TB2.Cursor = Cursors.Arrow;
            TB2.AllowDrop = false;

            tmp.Children.Add(TB2);


            //Добавление текстбокса 3
            TextBox TB3 = new TextBox();
            Grid.SetColumn(TB3, 0);
            TB3.Background = null;
            TB3.Margin = new Thickness(1, 1, 1, 1);
            TB3.BorderBrush = null;
            TB3.Text = tmp.A.series_title;
            TB3.FontFamily = new FontFamily("Calibri");
            TB3.FontSize = 18;
            TB3.TextAlignment = TextAlignment.Center;
            TB3.TextDecorations = null;
            TB3.IsReadOnly = true;
            TB3.SelectionBrush = null;
            TB3.Foreground = new SolidColorBrush(Colors.Black);
            TB3.ClipToBounds = true;
            TB3.BorderThickness = new Thickness(0);
            TB3.SelectionOpacity = 1;
            TB3.Cursor = Cursors.Arrow;
            TB3.AllowDrop = false;

            tmp.Children.Add(TB3);




            //Добавление текстбокса 4
            TextBox TB4 = new TextBox();
            Grid.SetColumn(TB4, 2);//-----//
            TB4.Background = null;
            TB4.Margin = new Thickness(1, 1, 1, 1);
            TB4.BorderBrush = null;
            TB4.Text = TranslateSeriesTypeEngToRus(tmp.A.series_type);
            TB4.FontFamily = new FontFamily("Calibri");
            TB4.FontSize = 18;
            TB4.TextAlignment = TextAlignment.Center;
            TB4.TextDecorations = null;
            TB4.IsReadOnly = true;
            TB4.SelectionBrush = null;
            TB4.Foreground = new SolidColorBrush(Colors.Black);
            TB4.ClipToBounds = true;
            TB4.BorderThickness = new Thickness(0);
            TB4.SelectionOpacity = 1;
            TB4.Cursor = Cursors.Arrow;
            TB4.AllowDrop = false;

            tmp.Children.Add(TB4);

            //Добавление кнопки вкл/выкл
            tmp.AddK1();

            //Добавление в стек
            _ResultAnimeList.Items.Add(tmp);

            return null;
        }

        static public ReturningMessage AddAnimeToCurrentAnimeList(ListBox _CurrentAnimeList, Anime _anime = null)
        {
            AnimeGrid tmp = null;
            if (_anime != null)
                tmp = new AnimeGrid(_anime);
            else
                tmp = new AnimeGrid();

            tmp.Height = 30;
            //tmp.Margin = new Thickness(2, 0, 2, 0);

            //Цвет в зависимости от my_status 

            switch (tmp.A.shiki_status)
            {
                case "Completed":
                    tmp.Background = new SolidColorBrush(Color.FromArgb(151, 26, 190, 0));
                    break;
                case "Plan to Watch":
                    tmp.Background = new SolidColorBrush(Color.FromArgb(151, 243, 255, 0));
                    break;
                case "Watching":
                    tmp.Background = new SolidColorBrush(Color.FromArgb(151, 0, 209, 255));
                    break;
                case "Rewatching":
                    tmp.Background = new SolidColorBrush(Color.FromArgb(151, 77, 28, 243));
                    break;
                case "On-Hold":
                    tmp.Background = new SolidColorBrush(Color.FromArgb(151, 174, 146, 21));
                    break;
                case "Dropped":
                    tmp.Background = new SolidColorBrush(Color.FromArgb(151, 178, 24, 24));
                    break;
                default:
                    tmp.Background = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                    break;
            }

            //ColumnDefinitions

            ColumnDefinition cdtmp3 = new ColumnDefinition();
            GridLength gltmp = new GridLength(1, GridUnitType.Star);
            cdtmp3.Width = (gltmp);
            cdtmp3.MinWidth = 74;
            tmp.ColumnDefinitions.Add(cdtmp3);

            ColumnDefinition cdtmp2 = new ColumnDefinition();
            cdtmp2.Width = new GridLength(50);
            tmp.ColumnDefinitions.Add(cdtmp2);

            ColumnDefinition cdtmp1 = new ColumnDefinition();
            cdtmp1.Width = new GridLength(70);
            tmp.ColumnDefinitions.Add(cdtmp1);

            ColumnDefinition cdtmp4 = new ColumnDefinition();
            cdtmp4.Width = new GridLength(65);
            tmp.ColumnDefinitions.Add(cdtmp4);

            ColumnDefinition cdtmp5 = new ColumnDefinition();
            cdtmp5.Width = new GridLength(17);
            tmp.ColumnDefinitions.Add(cdtmp5);


            //Всплывающая подсказка
            tmp.ToolTip = $"{tmp.A.series_title}\n{TranslateShikiStatusEngToRus(tmp.A.shiki_status)}{(tmp.A.my_score == 0 ? "" : $", {tmp.A.my_score}")}\n{tmp.A.my_watched_episodes}/{(tmp.A.series_episodes != 0 ? Convert.ToString(tmp.A.series_episodes) : "?")}\t{TranslateSeriesTypeEngToRus(tmp.A.series_type)}{(tmp.A.my_times_watched == 0 ? "" : $"\nКол-во повторных просмотров:{tmp.A.my_times_watched}")}{(tmp.A.my_comments == "" ? "" : $"\nКомментарий:\n{tmp.A.my_comments}")}";

            //Добавление текстбокса 1
            TextBox TB1 = new TextBox();
            Grid.SetColumn(TB1, 2);
            TB1.Background = null;
            TB1.Margin = new Thickness(1, 1, 1, 1);
            TB1.BorderBrush = null;
            if (tmp.A.series_episodes != 0)
                TB1.Text = tmp.A.my_watched_episodes + "/" + tmp.A.series_episodes;
            else
                TB1.Text = tmp.A.my_watched_episodes + "/(?)";
            TB1.FontFamily = new FontFamily("Calibri");
            TB1.FontSize = 18;
            TB1.TextAlignment = TextAlignment.Center;
            TB1.TextDecorations = null;
            TB1.IsReadOnly = true;
            TB1.SelectionBrush = null;
            TB1.Foreground = new SolidColorBrush(Colors.Black);
            TB1.ClipToBounds = true;
            TB1.BorderThickness = new Thickness(0);
            TB1.SelectionOpacity = 1;
            TB1.Cursor = Cursors.Arrow;
            TB1.AllowDrop = false;

            tmp.Children.Add(TB1);


            //Добавление текстбокса 2
            TextBox TB2 = new TextBox();
            Grid.SetColumn(TB2, 1);
            TB2.Background = null;
            TB2.Margin = new Thickness(1, 1, 1, 1);
            TB2.BorderBrush = null;
            if (tmp.A.my_score != 0)
                TB2.Text = Convert.ToString(Convert.ToInt32(tmp.A.my_score));
            TB2.FontFamily = new FontFamily("Calibri");
            TB2.FontSize = 18;
            TB2.TextAlignment = TextAlignment.Center;
            TB2.TextDecorations = null;
            TB2.IsReadOnly = true;
            TB2.SelectionBrush = null;
            TB2.Foreground = new SolidColorBrush(Colors.Black);
            TB2.ClipToBounds = true;
            TB2.BorderThickness = new Thickness(0);
            TB2.SelectionOpacity = 1;
            TB2.Cursor = Cursors.Arrow;
            TB2.AllowDrop = false;

            tmp.Children.Add(TB2);


            //Добавление текстбокса 3
            TextBox TB3 = new TextBox();
            Grid.SetColumn(TB3, 0);
            TB3.Background = null;
            TB3.Margin = new Thickness(1, 1, 1, 1);
            TB3.BorderBrush = null;
            TB3.Text = tmp.A.series_title;
            TB3.FontFamily = new FontFamily("Calibri");
            TB3.FontSize = 18;
            TB3.TextAlignment = TextAlignment.Center;
            TB3.TextDecorations = null;
            TB3.IsReadOnly = true;
            TB3.SelectionBrush = null;
            TB3.Foreground = new SolidColorBrush(Colors.Black);
            TB3.ClipToBounds = true;
            TB3.BorderThickness = new Thickness(0);
            TB3.SelectionOpacity = 1;
            TB3.Cursor = Cursors.Arrow;
            TB3.AllowDrop = false;

            tmp.Children.Add(TB3);




            //Добавление текстбокса 4
            TextBox TB4 = new TextBox();
            Grid.SetColumn(TB4, 3);
            TB4.Background = null;
            TB4.Margin = new Thickness(1, 1, 1, 1);
            TB4.BorderBrush = null;
            TB4.Text = TranslateSeriesTypeEngToRus(tmp.A.series_type);
            TB4.FontFamily = new FontFamily("Calibri");
            TB4.FontSize = 18;
            TB4.TextAlignment = TextAlignment.Center;
            TB4.TextDecorations = null;
            TB4.IsReadOnly = true;
            TB4.SelectionBrush = null;
            TB4.Foreground = new SolidColorBrush(Colors.Black);
            TB4.ClipToBounds = true;
            TB4.BorderThickness = new Thickness(0);
            TB4.SelectionOpacity = 1;
            TB4.Cursor = Cursors.Arrow;
            TB4.AllowDrop = false;

            tmp.Children.Add(TB4);

            //Добавление кнопки вкл/выкл
            tmp.AddK1();

            //Добавление в стек
            _CurrentAnimeList.Items.Add(tmp);

            ReturningMessage ret = new ReturningMessage();
            return ret;
        }

        static public async Task<int[]> UseRandomOrgGenerateIntegers_1(int n, int max, int min, Log LogWindow)
        {
            
            //rm.Error = 1;
            //rm.Message = "Рандом был начат, но что-то пошло не так";
            //string jsonText = "{ \"jsonrpc\": \"2.0\", \"method\": \"generateIntegers\", \"params\": { \"apiKey\": \"00d79163-e0e4-405d-ba4e-859df106d6eb\", \"n\": 6, \"min\": 1, \"max\": 6, \"replacement\": true}, \"id\": 42}";
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.random.org/json-rpc/2/invoke");
            string myJson = $"{{ \"jsonrpc\": \"2.0\", \"method\": \"generateIntegers\", \"params\": {{ \"apiKey\": \"00d79163-e0e4-405d-ba4e-859df106d6eb\", \"n\": {n}, \"min\": {min}, \"max\": {max}, \"replacement\": false}}, \"id\": 0}}";
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(
                    "https://api.random.org/json-rpc/2/invoke",
                     new StringContent(myJson, Encoding.UTF8, "application/json"));
                var result = await response.Content.ReadAsStringAsync();

                //rm.Error = 2;
                //rm.Message = "Ответ от random.org:\n";
                //rm.Message += result;
                LogWindow.Loging("Ответ от random.org:\n" + result);

                int op=0, ed=0;
                for (int a = 0; result[a] != '\0'; a++)
                {
                    if (result[a] == '[')
                    {
                        op = a;
                        while (result[a] != ']')
                            a++;
                        ed = a;
                        break;
                    }
                }
                string charmass = result.Substring(op + 1, ed - op - 1);
                int [] mass = charmass.Split(',').Select(N => Convert.ToInt32(N)).ToArray();



                //rm.Error = 0;
                return mass;
            }
        }

        static public ReturningMessage AddAnimeToCurrentAnimeList1(StackPanel _CurrentAnimeList)
        {
            Random r = new Random();
            Grid _grid = new Grid();
            _grid.Background = new SolidColorBrush(r.Next()%2==0?Colors.Red:Colors.Blue);
            _grid.MinWidth = 100;
            _grid.MinHeight = 20;
            _grid.Margin = new Thickness(2, 0, 2, 0);

            _CurrentAnimeList.Children.Add(_grid);       




            ReturningMessage ret = new ReturningMessage();
            return ret;
        }

        static public async Task<ReturningMessage> ExportAnimeListFromShikiToFile(string _Username)
        {
            //if (_Username=="username")
            //{
            //    ReturningMessage ret6 = new ReturningMessage();
            //    ret6.Error = 0;
            //    ret6.Message = "Имя пользователя \"username\", еонечно, существует, но экспортировать его список я не буду))0";
            //    return ret6;
            //}
            //WebRequest wrSHIKI;
            //Stream objStream;
            //try
            //{
            //    string sURL;
            //    sURL = "https://shikimori.one/" + _Username + "/list_export/animes.xml";
            //
            //    
            //    wrSHIKI = WebRequest.Create(sURL);
            //
            //    
            //    objStream = wrSHIKI.GetResponse().GetResponseStream();
            //}
            //catch
            //{
            //    ReturningMessage ret2 = new ReturningMessage();
            //    ret2.Error = 1;
            //    ret2.Message = $"Error: Список пользователя {_Username} импортировать не сумели. Вероятнее всего, Почта России перевозила его ник, и теперь он повреждён. Но это ещё не точно, могут быть и другие причины.";
            //    return ret2;
            //}
            //
            //StreamReader objReader = new StreamReader(objStream);
            //
            //string sLine = "";
            //int i = 0;
            //
            //StreamWriter newfile = new StreamWriter("Latest_export_animelist.txt");
            //
            //while (sLine != null)
            //{
            //    i++;
            //    sLine = objReader.ReadLine();
            //    if (sLine != null)
            //        newfile.WriteLine(sLine);
            //}
            //
            //newfile.Close();
            //objReader.Close();

            if (_Username=="username")
            {
                ReturningMessage ret6 = new ReturningMessage();
                ret6.Error = 0;
                ret6.Message = "Имя пользователя \"username\", еонечно, существует, но экспортировать его список я не буду))0";
                return ret6;
            }
            try
            {
                WebClient client = new WebClient();
                client.DownloadFileAsync(new Uri("https://shikimori.one/" + _Username + "/list_export/animes.xml"), "Latest_export_animelist.xml");
            }
            catch
            {
                ReturningMessage ret2 = new ReturningMessage();
                ret2.Error = 1;
                ret2.Message = $"Error: Список пользователя {_Username} импортировать не сумели. Вероятнее всего, Почта России перевозила его ник, и теперь он повреждён. Но это ещё не точно, могут быть и другие причины.";
                return ret2;
            }


            ReturningMessage ret = new ReturningMessage();
            ret.Message = $"Список пользователя {_Username} успешно (или нет, хз чёт) импортирован в файл Latest_export_animelist.txt";
            return ret;
        }

        /*
        static public async Task<ReturningMessage> ExportAnimeListFromShikiToFile(string _Username)
        {
            if (_Username=="username")
            {
                ReturningMessage ret6 = new ReturningMessage();
                ret6.Error = 0;
                ret6.Message = "Имя пользователя \"username\", еонечно, существует, но экспортировать его список я не буду))0";
                return ret6;
            }
            WebRequest wrSHIKI;
            Stream objStream;
            try
            {
                string sURL;
                sURL = "https://shikimori.one/" + _Username + "/list_export/animes.xml";

                
                wrSHIKI = WebRequest.Create(sURL);

                
                objStream = wrSHIKI.GetResponse().GetResponseStream();
            }
            catch
            {
                ReturningMessage ret2 = new ReturningMessage();
                ret2.Error = 1;
                ret2.Message = $"Error: Список пользователя {_Username} импортировать не сумели. Вероятнее всего, Почта России перевозила его ник, и теперь он повреждён. Но это ещё не точно, могут быть и другие причины.";
                return ret2;
            }

            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            int i = 0;

            StreamWriter newfile = new StreamWriter("Latest_export_animelist.txt");

            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                    newfile.WriteLine(sLine);
            }

            newfile.Close();
            objReader.Close();
            ReturningMessage ret = new ReturningMessage();
            ret.Message = $"Список пользователя {_Username} успешно (или нет, хз чёт) импортирован в файл Latest_export_animelist.txt";
            return ret;
        }

        */

        static public ProfileOptions ProfileXMLNodeToProfileOptions(ProfileOptions _ProfileOptions, XmlNode _ProfileNode)
        {
            //MessageBox.Show(_ProfileOptions.username);
            _ProfileOptions.NAME = _ProfileNode.Attributes[0].Value;

            _ProfileOptions.username = _ProfileNode.ChildNodes[0].Attributes[0].Value;
            _ProfileOptions.transformation = Convert.ToInt16(_ProfileNode.ChildNodes[1].Attributes[0].Value);

            _ProfileOptions.sortOptions.MaxEp = Convert.ToInt32(_ProfileNode.ChildNodes[2].ChildNodes[0].Attributes[0].Value);
            _ProfileOptions.sortOptions.MinEp = Convert.ToInt32(_ProfileNode.ChildNodes[2].ChildNodes[1].Attributes[0].Value);
            //_ProfileOptions.sortOptions.Ongoing = Convert.ToBoolean(_ProfileNode.ChildNodes[2].ChildNodes[2].Attributes[0].Value);
            //_ProfileOptions.sortOptions.NoOngoing = Convert.ToBoolean(_ProfileNode.ChildNodes[2].ChildNodes[3].Attributes[0].Value);
            _ProfileOptions.sortOptions.MinRewatchTimes = Convert.ToInt32(_ProfileNode.ChildNodes[2].ChildNodes[2].Attributes[0].Value);
            _ProfileOptions.sortOptions.MaxScore = Convert.ToInt16(_ProfileNode.ChildNodes[2].ChildNodes[3].Attributes[0].Value);
            _ProfileOptions.sortOptions.MinScore = Convert.ToInt16(_ProfileNode.ChildNodes[2].ChildNodes[4].Attributes[0].Value);

            _ProfileOptions.sortOptions.Status_Completed = Convert.ToBoolean(_ProfileNode.ChildNodes[2].ChildNodes[5].ChildNodes[0].Attributes[0].Value);
            _ProfileOptions.sortOptions.Status_Plan_to_Watch = Convert.ToBoolean(_ProfileNode.ChildNodes[2].ChildNodes[5].ChildNodes[1].Attributes[0].Value);
            _ProfileOptions.sortOptions.Status_Watching = Convert.ToBoolean(_ProfileNode.ChildNodes[2].ChildNodes[5].ChildNodes[2].Attributes[0].Value);
            _ProfileOptions.sortOptions.Status_Rewatching = Convert.ToBoolean(_ProfileNode.ChildNodes[2].ChildNodes[5].ChildNodes[3].Attributes[0].Value);
            _ProfileOptions.sortOptions.Status_On_Hold = Convert.ToBoolean(_ProfileNode.ChildNodes[2].ChildNodes[5].ChildNodes[4].Attributes[0].Value);
            _ProfileOptions.sortOptions.Status_Dropped = Convert.ToBoolean(_ProfileNode.ChildNodes[2].ChildNodes[5].ChildNodes[5].Attributes[0].Value);

            _ProfileOptions.sortOptions.Type_tv = Convert.ToBoolean(_ProfileNode.ChildNodes[2].ChildNodes[6].ChildNodes[0].Attributes[0].Value);
            _ProfileOptions.sortOptions.Type_ova = Convert.ToBoolean(_ProfileNode.ChildNodes[2].ChildNodes[6].ChildNodes[1].Attributes[0].Value);
            _ProfileOptions.sortOptions.Type_special = Convert.ToBoolean(_ProfileNode.ChildNodes[2].ChildNodes[6].ChildNodes[2].Attributes[0].Value);
            _ProfileOptions.sortOptions.Type_movie = Convert.ToBoolean(_ProfileNode.ChildNodes[2].ChildNodes[6].ChildNodes[3].Attributes[0].Value);
            _ProfileOptions.sortOptions.Type_ona = Convert.ToBoolean(_ProfileNode.ChildNodes[2].ChildNodes[6].ChildNodes[4].Attributes[0].Value);
            _ProfileOptions.sortOptions.Type_music = Convert.ToBoolean(_ProfileNode.ChildNodes[2].ChildNodes[6].ChildNodes[5].Attributes[0].Value);
            _ProfileOptions.sortOptions.Type_null = Convert.ToBoolean(_ProfileNode.ChildNodes[2].ChildNodes[6].ChildNodes[6].Attributes[0].Value);

            return _ProfileOptions;
        }

        static public ReturningMessage ParseOptionsFromXmlDocument (List<ProfileOptions> ListProfileOptions, XmlDocument optionsXML)
        {
            ListProfileOptions.Clear();
            XmlNode xroot = optionsXML.DocumentElement;
            foreach (XmlNode xnode in xroot.ChildNodes)
            {
                ProfileOptions tmpPO = new ProfileOptions();
                tmpPO.NAME = xnode.Attributes[0].Value;
                foreach (XmlNode znode in xnode)
                {
                    switch (znode.Name)
                    {
                        case "username":
                            tmpPO.username = znode.Attributes[0].Value;
                            break;
                        case "transformation":
                            tmpPO.transformation = Convert.ToInt16(znode.Attributes[0].Value);
                            break;
                        case "SortOptions":
                            foreach (XmlNode lnode in znode)
                            {
                            switch (lnode.Name)
                                {
                                    case "MaxEp":
                                        tmpPO.sortOptions.MaxEp = Convert.ToInt32(lnode.Attributes[0].Value);
                                        break;
                                    case "MinEp":
                                        tmpPO.sortOptions.MinEp = Convert.ToInt32(lnode.Attributes[0].Value);
                                        break;
                                    case "MinRewatchTimes":
                                        tmpPO.sortOptions.MinRewatchTimes = Convert.ToInt32(lnode.Attributes[0].Value);
                                        break;
                                    case "MaxScore":
                                        tmpPO.sortOptions.MaxScore = Convert.ToInt16(lnode.Attributes[0].Value);
                                        break;
                                    case "MinScore":
                                        tmpPO.sortOptions.MinScore = Convert.ToInt16(lnode.Attributes[0].Value);
                                        break;
                                    case "TypeAllowed":
                                        foreach (XmlNode knode in lnode)
                                        {
                                            switch (knode.Name)
                                            {
                                                case "Tv":
                                                    tmpPO.sortOptions.Type_tv = Convert.ToBoolean(knode.Attributes[0].Value);
                                                    break;
                                                case "Ova":
                                                    tmpPO.sortOptions.Type_ova = Convert.ToBoolean(knode.Attributes[0].Value);
                                                    break;
                                                case "Special":
                                                    tmpPO.sortOptions.Type_special = Convert.ToBoolean(knode.Attributes[0].Value);
                                                    break;
                                                case "Movie":
                                                    tmpPO.sortOptions.Type_movie = Convert.ToBoolean(knode.Attributes[0].Value);
                                                    break;
                                                case "Ona":
                                                    tmpPO.sortOptions.Type_ona = Convert.ToBoolean(knode.Attributes[0].Value);
                                                    break;
                                                case "Music":
                                                    tmpPO.sortOptions.Type_music = Convert.ToBoolean(knode.Attributes[0].Value);
                                                    break;
                                                case "Undefined":
                                                    tmpPO.sortOptions.Type_null = Convert.ToBoolean(knode.Attributes[0].Value);
                                                    break;
                                            }
                                        }
                                        break;
                                    case "StatusAllowed":
                                        foreach (XmlNode gnode in lnode)
                                        {
                                            switch (gnode.Name)
                                            {
                                                case "Completed":
                                                    tmpPO.sortOptions.Status_Completed = Convert.ToBoolean(gnode.Attributes[0].Value);
                                                    break;
                                                case "Plan_to_Watch":
                                                    tmpPO.sortOptions.Status_Plan_to_Watch = Convert.ToBoolean(gnode.Attributes[0].Value);
                                                    break;
                                                case "Watching":
                                                    tmpPO.sortOptions.Status_Watching = Convert.ToBoolean(gnode.Attributes[0].Value);
                                                    break;
                                                case "Rewatching":
                                                    tmpPO.sortOptions.Status_Rewatching = Convert.ToBoolean(gnode.Attributes[0].Value);
                                                    break;
                                                case "On_Hold":
                                                    tmpPO.sortOptions.Status_On_Hold = Convert.ToBoolean(gnode.Attributes[0].Value);
                                                    break;
                                                case "Dropped":
                                                    tmpPO.sortOptions.Status_Dropped = Convert.ToBoolean(gnode.Attributes[0].Value);
                                                    break;
                                            }
                                        }
                                        break;
                                }
                            }
                            break;
                    }
                }
                ListProfileOptions.Add(tmpPO);
            }

            return null;
        }

        //static public ReturningMessage SaveOptionsToXMLFile(List<ProfileOptions> ListProfileOptions, XmlDocument optionsXML)!!!!!!!!!!!____________________________________ADD .Attr[0]. BEFORE USE!!!!!
        //{
        //    try
        //    {
        //        //!!!!!!!!!!!____________________________________ADD .Attr[0]. BEFORE USE!!!!!
        //        //XmlDocument xdoc = new XmlDocument();
        //        XmlElement xroot = optionsXML.DocumentElement;
        //        xroot.RemoveAll();
        //        XmlNode xprofile;
        //        XmlAttribute xatr;
        //        XmlNode xnode, xnode1, xnode2;
        //        foreach (ProfileOptions po in ListProfileOptions)
        //        {
        //            xprofile = optionsXML.CreateElement("Profile");

        //            xatr = optionsXML.CreateAttribute("name");
        //            xatr.Value = po.NAME;
        //            xprofile.Attributes.Append(xatr);

        //            xnode = optionsXML.CreateElement("username");
        //            xatr = optionsXML.CreateAttribute("value");
        //            xatr.Value = po.username;
        //            xnode.Attributes.Append(xatr);
        //            xprofile.AppendChild(xnode);

        //            xnode = optionsXML.CreateElement("transformation");
        //            xatr = optionsXML.CreateAttribute("value");
        //            xatr.Value = po.transformation.ToString();
        //            xnode.Attributes.Append(xatr);
        //            xprofile.AppendChild(xnode);

        //            xnode = optionsXML.CreateElement("SortOptions");
        //            {
        //                xnode1 = optionsXML.CreateElement("MaxEp");
        //                xatr = optionsXML.CreateAttribute("value");
        //                xatr.Value = po.sortOptions.MaxEp.ToString();
        //                xnode1.Attributes.Append(xatr);
        //                xnode.AppendChild(xnode1);

        //                xnode1 = optionsXML.CreateElement("MinEp");
        //                xatr = optionsXML.CreateAttribute("value");
        //                xatr.Value = po.sortOptions.MinEp.ToString();
        //                xnode1.Attributes.Append(xatr);
        //                xnode.AppendChild(xnode1);

        //                xnode1 = optionsXML.CreateElement("MinRewatchTimes");
        //                xatr = optionsXML.CreateAttribute("value");
        //                xatr.Value = po.sortOptions.MinRewatchTimes.ToString();
        //                xnode1.Attributes.Append(xatr);
        //                xnode.AppendChild(xnode1);

        //                xnode1 = optionsXML.CreateElement("MaxScore");
        //                xatr = optionsXML.CreateAttribute("value");
        //                xatr.Value = po.sortOptions.MaxScore.ToString();
        //                xnode1.Attributes.Append(xatr);
        //                xnode.AppendChild(xnode1);

        //                xnode1 = optionsXML.CreateElement("MinScore");
        //                xatr = optionsXML.CreateAttribute("value");
        //                xatr.Value = po.sortOptions.MinScore.ToString();
        //                xnode1.Attributes.Append(xatr);
        //                xnode.AppendChild(xnode1);

        //                xnode1 = optionsXML.CreateElement("StatusAllowed");
        //                {
        //                    xnode2 = optionsXML.CreateElement("Completed");
        //                    xatr = optionsXML.CreateAttribute("value");
        //                    xatr.Value = Convert.ToString(po.sortOptions.Status_Completed);
        //                    xnode2.Attributes.Append(xatr);
        //                    xnode1.AppendChild(xnode2);

        //                    xnode2 = optionsXML.CreateElement("Plan_to_Watch");
        //                    xatr = optionsXML.CreateAttribute("value");
        //                    xatr.Value = po.sortOptions.Status_Plan_to_Watch.ToString();
        //                    xnode2.Attributes.Append(xatr);
        //                    xnode1.AppendChild(xnode2);

        //                    xnode2 = optionsXML.CreateElement("Watching");
        //                    xatr = optionsXML.CreateAttribute("value");
        //                    xatr.Value = po.sortOptions.Status_Watching.ToString();
        //                    xnode2.Attributes.Append(xatr);
        //                    xnode1.AppendChild(xnode2);

        //                    xnode2 = optionsXML.CreateElement("Rewatching");
        //                    xatr = optionsXML.CreateAttribute("value");
        //                    xatr.Value = po.sortOptions.Status_Rewatching.ToString();
        //                    xnode2.Attributes.Append(xatr);
        //                    xnode1.AppendChild(xnode2);

        //                    xnode2 = optionsXML.CreateElement("On_Hold");
        //                    xatr = optionsXML.CreateAttribute("value");
        //                    xatr.Value = po.sortOptions.Status_On_Hold.ToString();
        //                    xnode2.Attributes.Append(xatr);
        //                    xnode1.AppendChild(xnode2);

        //                    xnode2 = optionsXML.CreateElement("Dropped");
        //                    xatr = optionsXML.CreateAttribute("value");
        //                    xatr.Value = po.sortOptions.Status_Dropped.ToString();
        //                    xnode2.Attributes.Append(xatr);
        //                    xnode1.AppendChild(xnode2);
        //                }
        //                xnode.AppendChild(xnode1);

        //                xnode1 = optionsXML.CreateElement("TypeAllowed");
        //                {
        //                    xnode2 = optionsXML.CreateElement("Tv");
        //                    xatr = optionsXML.CreateAttribute("value");
        //                    xatr.Value = po.sortOptions.Type_tv.ToString();
        //                    xnode2.Attributes.Append(xatr);
        //                    xnode1.AppendChild(xnode2);

        //                    xnode2 = optionsXML.CreateElement("Ova");
        //                    xatr = optionsXML.CreateAttribute("value");
        //                    xatr.Value = po.sortOptions.Type_ova.ToString();
        //                    xnode2.Attributes.Append(xatr);
        //                    xnode1.AppendChild(xnode2);

        //                    xnode2 = optionsXML.CreateElement("Special");
        //                    xatr = optionsXML.CreateAttribute("value");
        //                    xatr.Value = po.sortOptions.Type_special.ToString();
        //                    xnode2.Attributes.Append(xatr);
        //                    xnode1.AppendChild(xnode2);

        //                    xnode2 = optionsXML.CreateElement("Movie");
        //                    xatr = optionsXML.CreateAttribute("value");
        //                    xatr.Value = po.sortOptions.Type_movie.ToString();
        //                    xnode2.Attributes.Append(xatr);
        //                    xnode1.AppendChild(xnode2);

        //                    xnode2 = optionsXML.CreateElement("Ona");
        //                    xatr = optionsXML.CreateAttribute("value");
        //                    xatr.Value = po.sortOptions.Type_ona.ToString();
        //                    xnode2.Attributes.Append(xatr);
        //                    xnode1.AppendChild(xnode2);

        //                    xnode2 = optionsXML.CreateElement("Music");
        //                    xatr = optionsXML.CreateAttribute("value");
        //                    xatr.Value = po.sortOptions.Type_music.ToString();
        //                    xnode2.Attributes.Append(xatr);
        //                    xnode1.AppendChild(xnode2);

        //                    xnode2 = optionsXML.CreateElement("Undefined");
        //                    xatr = optionsXML.CreateAttribute("value");
        //                    xatr.Value = po.sortOptions.Type_null.ToString();
        //                    xnode2.Attributes.Append(xatr);
        //                    xnode1.AppendChild(xnode2);
        //                }
        //                xnode.AppendChild(xnode1);
        //            }
        //            xprofile.AppendChild(xnode);

        //            xroot.AppendChild(xprofile);
        //        }



        //        optionsXML.Save("Options.xml");

        //        return null;
        //    }
        //    catch
        //    {
        //        ReturningMessage r = new ReturningMessage();
        //        r.Error = 100;
        //        r.Message = "ERROR: Сохранение опций не удалось";
        //        return r;
        //    }
        //}

        static public ReturningMessage SaveCurrentProfileOptionsToXmlFile(ProfileOptions po, XmlDocument optionsXML)
        {
            try
            {
                XmlElement xroot = optionsXML.DocumentElement;
                xroot.RemoveAll();
                XmlNode xprofile;
                XmlAttribute xatr;
                XmlNode xnode, xnode1, xnode2;
                
                    xprofile = optionsXML.CreateElement("Profile");

                    xatr = optionsXML.CreateAttribute("name");
                    xatr.Value = po.NAME;
                    xprofile.Attributes.Append(xatr);

                    xnode = optionsXML.CreateElement("username");
                    xatr = optionsXML.CreateAttribute("value");
                    xatr.Value = po.username;
                    xnode.Attributes.Append(xatr);
                    xprofile.AppendChild(xnode);

                    xnode = optionsXML.CreateElement("transformation");
                    xatr = optionsXML.CreateAttribute("value");
                    xatr.Value = po.transformation.ToString();
                    xnode.Attributes.Append(xatr);
                    xprofile.AppendChild(xnode);

                    xnode = optionsXML.CreateElement("SortOptions");
                    {
                        xnode1 = optionsXML.CreateElement("MaxEp");
                        xatr = optionsXML.CreateAttribute("value");
                        xatr.Value = po.sortOptions.MaxEp.ToString();
                        xnode1.Attributes.Append(xatr);
                        xnode.AppendChild(xnode1);

                        xnode1 = optionsXML.CreateElement("MinEp");
                        xatr = optionsXML.CreateAttribute("value");
                        xatr.Value = po.sortOptions.MinEp.ToString();
                        xnode1.Attributes.Append(xatr);
                        xnode.AppendChild(xnode1);

                        xnode1 = optionsXML.CreateElement("MinRewatchTimes");
                        xatr = optionsXML.CreateAttribute("value");
                        xatr.Value = po.sortOptions.MinRewatchTimes.ToString();
                        xnode1.Attributes.Append(xatr);
                        xnode.AppendChild(xnode1);

                        xnode1 = optionsXML.CreateElement("MaxScore");
                        xatr = optionsXML.CreateAttribute("value");
                        xatr.Value = po.sortOptions.MaxScore.ToString();
                        xnode1.Attributes.Append(xatr);
                        xnode.AppendChild(xnode1);

                        xnode1 = optionsXML.CreateElement("MinScore");
                        xatr = optionsXML.CreateAttribute("value");
                        xatr.Value = po.sortOptions.MinScore.ToString();
                        xnode1.Attributes.Append(xatr);
                        xnode.AppendChild(xnode1);

                        xnode1 = optionsXML.CreateElement("StatusAllowed");
                        {
                            xnode2 = optionsXML.CreateElement("Completed");
                            xatr = optionsXML.CreateAttribute("value");
                            xatr.Value = Convert.ToString(po.sortOptions.Status_Completed);
                            xnode2.Attributes.Append(xatr);
                            xnode1.AppendChild(xnode2);

                            xnode2 = optionsXML.CreateElement("Plan_to_Watch");
                            xatr = optionsXML.CreateAttribute("value");
                            xatr.Value = po.sortOptions.Status_Plan_to_Watch.ToString();
                            xnode2.Attributes.Append(xatr);
                            xnode1.AppendChild(xnode2);

                            xnode2 = optionsXML.CreateElement("Watching");
                            xatr = optionsXML.CreateAttribute("value");
                            xatr.Value = po.sortOptions.Status_Watching.ToString();
                            xnode2.Attributes.Append(xatr);
                            xnode1.AppendChild(xnode2);

                            xnode2 = optionsXML.CreateElement("Rewatching");
                            xatr = optionsXML.CreateAttribute("value");
                            xatr.Value = po.sortOptions.Status_Rewatching.ToString();
                            xnode2.Attributes.Append(xatr);
                            xnode1.AppendChild(xnode2);

                            xnode2 = optionsXML.CreateElement("On_Hold");
                            xatr = optionsXML.CreateAttribute("value");
                            xatr.Value = po.sortOptions.Status_On_Hold.ToString();
                            xnode2.Attributes.Append(xatr);
                            xnode1.AppendChild(xnode2);

                            xnode2 = optionsXML.CreateElement("Dropped");
                            xatr = optionsXML.CreateAttribute("value");
                            xatr.Value = po.sortOptions.Status_Dropped.ToString();
                            xnode2.Attributes.Append(xatr);
                            xnode1.AppendChild(xnode2);
                        }
                        xnode.AppendChild(xnode1);

                        xnode1 = optionsXML.CreateElement("TypeAllowed");
                        {
                            xnode2 = optionsXML.CreateElement("Tv");
                            xatr = optionsXML.CreateAttribute("value");
                            xatr.Value = po.sortOptions.Type_tv.ToString();
                            xnode2.Attributes.Append(xatr);
                            xnode1.AppendChild(xnode2);

                            xnode2 = optionsXML.CreateElement("Ova");
                            xatr = optionsXML.CreateAttribute("value");
                            xatr.Value = po.sortOptions.Type_ova.ToString();
                            xnode2.Attributes.Append(xatr);
                            xnode1.AppendChild(xnode2);

                            xnode2 = optionsXML.CreateElement("Special");
                            xatr = optionsXML.CreateAttribute("value");
                            xatr.Value = po.sortOptions.Type_special.ToString();
                            xnode2.Attributes.Append(xatr);
                            xnode1.AppendChild(xnode2);

                            xnode2 = optionsXML.CreateElement("Movie");
                            xatr = optionsXML.CreateAttribute("value");
                            xatr.Value = po.sortOptions.Type_movie.ToString();
                            xnode2.Attributes.Append(xatr);
                            xnode1.AppendChild(xnode2);

                            xnode2 = optionsXML.CreateElement("Ona");
                            xatr = optionsXML.CreateAttribute("value");
                            xatr.Value = po.sortOptions.Type_ona.ToString();
                            xnode2.Attributes.Append(xatr);
                            xnode1.AppendChild(xnode2);

                            xnode2 = optionsXML.CreateElement("Music");
                            xatr = optionsXML.CreateAttribute("value");
                            xatr.Value = po.sortOptions.Type_music.ToString();
                            xnode2.Attributes.Append(xatr);
                            xnode1.AppendChild(xnode2);

                            xnode2 = optionsXML.CreateElement("Undefined");
                            xatr = optionsXML.CreateAttribute("value");
                            xatr.Value = po.sortOptions.Type_null.ToString();
                            xnode2.Attributes.Append(xatr);
                            xnode1.AppendChild(xnode2);
                        }
                        xnode.AppendChild(xnode1);
                    }
                    xprofile.AppendChild(xnode);

                    xroot.AppendChild(xprofile);
                



                optionsXML.Save("Options.xml");

                return null;
            }
            catch
            {
                ReturningMessage r = new ReturningMessage();
                r.Error = 100;
                r.Message = "ERROR: Сохранение опций не удалось";
                return r;
            }
        }

        static public string TranslateShikiStatusEngToRus(string shiki_status)
        {
            string tmpStatus;
            switch (shiki_status)
            {
                case "Completed":
                    tmpStatus = "Просмотрено";
                    break;
                case "Plan to Watch":
                    tmpStatus = "Запланировано";
                    break;
                case "Watching":
                    tmpStatus = "Смотрю";
                    break;
                case "Rewatching":
                    tmpStatus = "Пересматриваю";
                    break;
                case "On-Hold":
                    tmpStatus = "Отложено";
                    break;
                case "Dropped":
                    tmpStatus = "Брошено";
                    break;
                default:
                    tmpStatus = "StatusTranslateError";
                    break;
            }
            return tmpStatus;
        }

        static public string TranslateSeriesTypeEngToRus(string series_type)
        {
            string tmpType;
            switch (series_type)
            {
                case "tv":
                    tmpType = "Сериал";
                    break;
                case "ova":
                    tmpType = "OVA";
                    break;
                case "special":
                    tmpType = "Спешл";
                    break;
                case "movie":
                    tmpType = "Фильм";
                    break;
                case "ona":
                    tmpType = "ONA";
                    break;
                case "music":
                    tmpType = "Клип";
                    break;
                case null:
                    tmpType = "";
                    break;
                case "":
                    tmpType = "";
                    break;
                default:
                    tmpType = "TypeTranslateError";
                    break;
            }
            return tmpType;
        }
    }
}