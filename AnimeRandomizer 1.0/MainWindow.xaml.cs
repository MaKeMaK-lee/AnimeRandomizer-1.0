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
using System.Diagnostics;

namespace AnimeRandomizer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Log LogWindow1 = new Log();
        public bool IsExistUsernameUnsavedChanges = false;
        public bool IsExistSortUnsavedChanges = false;
        public bool IsRatingModifyChance { get; set; } = false;
        public int WinnersCount { get; set; } = 1;
        public bool IsPreview { get; set; } = true;
        public bool IsShowFiltredList { get; set; } = true;
        public bool IsSortShikiGroupMetod { get; set; } = true;
        List<Anime> AllAnimesList = new List<Anime>();
        List<Anime> SortedAnimesList = new List<Anime>();
        List<Anime> FiltredAnimesList = new List<Anime>();
        List<Anime> WinnersAnimesList = new List<Anime>();
        bool SortFlagEpisodes = true;
        bool SortFlagStar = true;
        bool SortFlagTitle = true;
        bool SortFlagType = true;
        ProfileOptions CurrentProfileOptions = new ProfileOptions();
        XmlDocument optionsXML = new XmlDocument();
        List<ProfileOptions> ListProfileOptions = new List<ProfileOptions>();
        int[] randMassResult;
        int WinnersWindowsCountInc = 0;




        public MainWindow()
        {
            InitializeComponent();
            Show();
            
            LogWindow1.Owner = this;
            LogWindow1.Closing += LogWindow1_Closing;
            
            //Загрузка опций из файла
            try
            {
                optionsXML.Load("Options.xml");
                if (optionsXML.DocumentElement.ChildNodes.Count < 1)
                {
                    optionsXML.LoadXml(
                "<Profiles> \n" +
                "   <Profile name= \"default\"> \n" +
                $"      <username value= \"metka1{CurrentProfileOptions.username}\" /> \n" +
                $"      <transformation value= \"{CurrentProfileOptions.transformation}\" /> \n" +
                "       <SortOptions> \n" +
                $"        <MaxEp value= \"{CurrentProfileOptions.sortOptions.MaxEp}\" /> \n" +
                $"        <MinEp value= \"{CurrentProfileOptions.sortOptions.MinEp}\" /> \n" +
                //$"        <Ongoing value= \"{CurrentProfileOptions.sortOptions.Ongoing}\" /> \n" +
                //$"        <NoOngoing value= \"{CurrentProfileOptions.sortOptions.NoOngoing}\" /> \n" +
                $"        <MinRewatchTimes value= \"{CurrentProfileOptions.sortOptions.MinRewatchTimes}\" /> \n" +
                $"        <MaxScore value= \"{CurrentProfileOptions.sortOptions.MaxScore}\" /> \n" +
                $"        <MinScore value= \"{CurrentProfileOptions.sortOptions.MinScore}\" /> \n" +
                "         <StatusAllowed> \n" +
                "            <Completed          value=\"True\"/> \n" +
                "            <Plan_to_Watch      value=\"True\"/> \n" +
                "            <Watching           value=\"True\"/> \n" +
                "            <Rewatching         value=\"True\"/> \n" +
                "            <On_Hold            value=\"True\"/> \n" +
                "            <Dropped            value=\"True\"/> \n" +
                "         </StatusAllowed> \n" +
                "         <TypeAllowed> \n" +
                "            <Tv                 value=\"True\"/> \n" +
                "            <Ova                value=\"True\"/> \n" +
                "            <Special            value=\"True\"/> \n" +
                "            <Movie              value=\"True\"/> \n" +
                "            <Ona                value=\"True\"/> \n" +
                "            <Music              value=\"True\"/> \n" +
                "            <Undefined          value=\"True\"/> \n" +
                "         </TypeAllowed> \n" +
                "       </SortOptions> \n" +
                "   </Profile> \n" +
                "</Profiles>");
                }
                Functions.ParseOptionsFromXmlDocument(ListProfileOptions, optionsXML);
                if (ListProfileOptions.Count > 0)
                    CurrentProfileOptions = ListProfileOptions.First();
                else
                    throw null;



            }
            catch (System.IO.FileNotFoundException)
            {
                LogWindow1.Loging("WARNING: Файл опций не найден, инициализированны дефолтные настроечки");

                optionsXML.LoadXml(
                    "<Profiles> \n" +
                    "</Profiles>"
                    );

                //optionsXML.LoadXml(
                //"<Profiles> \n" +
                //"   <Profile name= \"default\"> \n" +
                //$"      <username value= \"{CurrentProfileOptions.username}\" /> \n" +
                //$"      <transformation value= \"{CurrentProfileOptions.transformation}\" /> \n" +
                //"       <SortOptions> \n" +
                //$"        <MaxEp value= \"{CurrentProfileOptions.sortOptions.MaxEp}\" /> \n" +
                //$"        <MinEp value= \"{CurrentProfileOptions.sortOptions.MinEp}\" /> \n" +
                ////$"        <Ongoing value= \"{CurrentProfileOptions.sortOptions.Ongoing}\" /> \n" +
                ////$"        <NoOngoing value= \"{CurrentProfileOptions.sortOptions.NoOngoing}\" /> \n" +
                //$"        <MinRewatchTimes value= \"{CurrentProfileOptions.sortOptions.MinRewatchTimes}\" /> \n" +
                //$"        <MaxScore value= \"{CurrentProfileOptions.sortOptions.MaxScore}\" /> \n" +
                //$"        <MinScore value= \"{CurrentProfileOptions.sortOptions.MinScore}\" /> \n" +
                //"         <StatusAllowed> \n" +
                //"            <Completed          value=\"True\"/> \n" +
                //"            <Plan_to_Watch      value=\"True\"/> \n" +
                //"            <Watching           value=\"True\"/> \n" +
                //"            <Rewatching         value=\"True\"/> \n" +
                //"            <On_Hold            value=\"True\"/> \n" +
                //"            <Dropped            value=\"True\"/> \n" +
                //"         </StatusAllowed> \n" +
                //"         <TypeAllowed> \n" +
                //"            <Tv                 value=\"True\"/> \n" +
                //"            <Ova                value=\"True\"/> \n" +
                //"            <Special            value=\"True\"/> \n" +
                //"            <Movie              value=\"True\"/> \n" +
                //"            <Ona                value=\"True\"/> \n" +
                //"            <Music              value=\"True\"/> \n" +
                //"            <Undefined          value=\"True\"/> \n" +
                //"         </TypeAllowed> \n" +
                //"       </SortOptions> \n" +
                //"   </Profile> \n" +
                //"</Profiles>");
            }
            catch (Exception e)
            {
                LogWindow1.Loging($"ERROR: Файл опций не смог в загрузку -\n__________\n{e.ToString()}\n__________\nИнициализированны дефолтные настроечки");
            }


            //загрузка импортированных опций
            RefreshVisualProfileOptions();

            Opacity = 100;

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(1500000);
            dispatcherTimer.Start();

            TBUsername.LostFocus += UsernameWasChanged;
            TBMaxCountEp.LostFocus += SortWasChanged;
            TBMaxScore.LostFocus += SortWasChanged;
            TBMinCountEp.LostFocus += SortWasChanged;
            TBMinScore.LostFocus += SortWasChanged;
            CBTypeMov.Checked += SortWasChanged;
            CBTypeMus.Checked += SortWasChanged;
            CBTypeNull.Checked += SortWasChanged;
            CBTypeOna.Checked += SortWasChanged;
            CBTypeOva.Checked += SortWasChanged;
            CBTypeSP.Checked += SortWasChanged;
            CBTypeTV.Checked += SortWasChanged;
            CBStatusCompl.Checked += SortWasChanged;
            CBStatusDrop.Checked += SortWasChanged;
            CBStatusOnHold.Checked += SortWasChanged;
            CBStatusPlan.Checked += SortWasChanged;
            CBStatusRewatch.Checked += SortWasChanged;
            CBStatusWatch.Checked += SortWasChanged;
            CBTypeMov.Unchecked += SortWasChanged;
            CBTypeMus.Unchecked += SortWasChanged;
            CBTypeNull.Unchecked += SortWasChanged;
            CBTypeOna.Unchecked += SortWasChanged;
            CBTypeOva.Unchecked += SortWasChanged;
            CBTypeSP.Unchecked += SortWasChanged;
            CBTypeTV.Unchecked += SortWasChanged;
            CBStatusCompl.Unchecked += SortWasChanged;
            CBStatusDrop.Unchecked += SortWasChanged;
            CBStatusOnHold.Unchecked += SortWasChanged;
            CBStatusPlan.Unchecked += SortWasChanged;
            CBStatusRewatch.Unchecked += SortWasChanged;
            CBStatusWatch.Unchecked += SortWasChanged;

            TBUsername.PreviewGotKeyboardFocus += TBUsername_GotFocus;
            TBUsername.PreviewLostKeyboardFocus += TBUsername_LostFocus;
            TBMaxCountEp.PreviewGotKeyboardFocus += TBMaxCountEp_GotFocus;
            TBMaxCountEp.PreviewLostKeyboardFocus += TBMaxCountEp_LostFocus;
            TBMinCountEp.PreviewGotKeyboardFocus += TBMinCountEp_GotFocus;
            TBMinCountEp.PreviewLostKeyboardFocus += TBMinCountEp_LostFocus;
            TBMaxScore.PreviewGotKeyboardFocus += TBMaxScore_GotFocus;
            TBMaxScore.PreviewLostKeyboardFocus += TBMaxScore_LostFocus;
            TBMinScore.PreviewGotKeyboardFocus += TBMinScore_GotFocus;
            TBMinScore.PreviewLostKeyboardFocus += TBMinScore_LostFocus;

            TBUsername.KeyDown += TBUsername_KeyDown;
            TBWinnersCount.KeyDown += TBWinnersCount_KeyDown;

            TBMaxCountEp.KeyDown += OnSortPanel_KeyDown;
            TBMinCountEp.KeyDown += OnSortPanel_KeyDown;
            TBMaxScore.KeyDown += OnSortPanel_KeyDown;
            TBMinScore.KeyDown += OnSortPanel_KeyDown;

            


            if (IsPreview)
            {
                TBPreviewIsOFF.Visibility = Visibility.Hidden;
                GridPreviewIsON.Visibility = Visibility.Visible;
            }
            else
            {
                GridPreviewIsON.Visibility = Visibility.Hidden;
                TBPreviewIsOFF.Visibility = Visibility.Visible;
            }
            //Первичный экспорт с шики и\или загрузка в анимелист
            if (TBUsername.Text != "username")
            {
                bool _IsComplete = true;
                try
                {
                    if (TBUsername.Text == null || TBUsername.Text == "")
                    {
                        Exception eeee = new Exception("Поле логина не заполнено");
                        throw eeee;
                    }
                }
                catch (Exception eeee)
                {
                    MessageBox.Show(this, eeee.Message, "Список не был экспортирован с шикимори");
                    _IsComplete = false;
                }
                if (_IsComplete)
                {
                    CurrentProfileOptions.username = TBUsername.Text;
                    if (CurrentProfileOptions.sortOptions.MaxScore <= 10)
                        AsyncExportList();
                    
                    ImportAnimeListFromFile();
                }
            }
            else
                ImportAnimeListFromFile();// (Тут же внутри заполняются и рефрешатся списки)




        }

        private void OnSortPanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                ChangeCurrentSettings_Click(sender, null);
        }

        private async void AsyncExportList ()
        {
            ReturningMessage _msg1 = await Functions.ExportAnimeListFromShikiToFile(CurrentProfileOptions.username);
            LogWindow1.Loging(_msg1.Message);
        }

        private void LogWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        Functions.SaveCurrentProfileOptionsToXmlFile(CurrentProfileOptions, optionsXML);
        }

        private void TBWinnersCount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                RandomizeSorted_Click(sender, null);
        }

        private void TBUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                ExportFromShikiButton_Click(sender, null);
        }

        private void TBMinScore_LostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                if (TBMinScore.Text == "" || Convert.ToInt32(TBMinScore.Text) < 0)
                    TBMinScore.Text = CurrentProfileOptions.sortOptions.MinScore.ToString();
            }
            catch
            {
                TBMinScore.Text = CurrentProfileOptions.sortOptions.MinScore.ToString();
            }
        }

        private void TBMinScore_GotFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TBMinScore.Text = "";
        }

        private void TBMaxScore_LostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                if (TBMaxScore.Text == "" || Convert.ToInt32(TBMaxScore.Text) < 0)
                    TBMaxScore.Text = CurrentProfileOptions.sortOptions.MaxScore.ToString();
            }
            catch
            {
                TBMaxScore.Text = CurrentProfileOptions.sortOptions.MaxScore.ToString();
            }
        }

        private void TBMaxScore_GotFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TBMaxScore.Text = "";
        }

        private void TBMinCountEp_LostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                if (TBMinCountEp.Text == "" || Convert.ToInt32(TBMinCountEp.Text) < 0)
                    TBMinCountEp.Text = CurrentProfileOptions.sortOptions.MinEp.ToString();
            }
            catch
            {
                TBMinCountEp.Text = CurrentProfileOptions.sortOptions.MinEp.ToString();
            }
        }

        private void TBMinCountEp_GotFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TBMinCountEp.Text = "";
        }

        private void TBMaxCountEp_LostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                if (TBMaxCountEp.Text == "" || Convert.ToInt32(TBMaxCountEp.Text) < 0)
                    TBMaxCountEp.Text = CurrentProfileOptions.sortOptions.MaxEp.ToString();
            }
            catch
            {
                TBMaxCountEp.Text = CurrentProfileOptions.sortOptions.MaxEp.ToString();
            }
        }

        private void TBMaxCountEp_GotFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TBMaxCountEp.Text = "";
        }

        private void TBUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TBUsername.Text == "")
                TBUsername.Text = CurrentProfileOptions.username;
            //throw new NotImplementedException();
        }

        private void TBUsername_GotFocus(object sender, RoutedEventArgs e)
        {
                TBUsername.Text = "";
            //throw new NotImplementedException();
        }

        private void UsernameWasChanged(object sender, RoutedEventArgs e)
        {
            if (TBUsername.Text != "username" && TBUsername.Text != CurrentProfileOptions.username)
            {
                if (!IsExistUsernameUnsavedChanges)
                {
                    IsExistUsernameUnsavedChanges = true;
                    ExportFromShikiButton.Background = new SolidColorBrush(Color.FromArgb(165, 255, 136, 18));
                }
            }
        }

        private void SortWasChanged(object sender, RoutedEventArgs e)
        {
            //if (!IsExistSortUnsavedChanges)
            try
            {
                if (Convert.ToInt32(TBMaxCountEp.Text) != CurrentProfileOptions.sortOptions.MaxEp || Convert.ToInt32(TBMinCountEp.Text) != CurrentProfileOptions.sortOptions.MinEp || Convert.ToInt32(TBMaxScore.Text) != CurrentProfileOptions.sortOptions.MaxScore || Convert.ToInt32(TBMinScore.Text) != CurrentProfileOptions.sortOptions.MinScore || CBTypeMov.IsChecked != CurrentProfileOptions.sortOptions.Type_movie || CBTypeMus.IsChecked != CurrentProfileOptions.sortOptions.Type_music || CBTypeNull.IsChecked != CurrentProfileOptions.sortOptions.Type_null || CBTypeOna.IsChecked != CurrentProfileOptions.sortOptions.Type_ona || CBTypeOva.IsChecked != CurrentProfileOptions.sortOptions.Type_ova || CBTypeSP.IsChecked != CurrentProfileOptions.sortOptions.Type_special || CBTypeTV.IsChecked != CurrentProfileOptions.sortOptions.Type_tv || CBStatusCompl.IsChecked != CurrentProfileOptions.sortOptions.Status_Completed || CBStatusDrop.IsChecked != CurrentProfileOptions.sortOptions.Status_Dropped || CBStatusOnHold.IsChecked != CurrentProfileOptions.sortOptions.Status_On_Hold || CBStatusPlan.IsChecked != CurrentProfileOptions.sortOptions.Status_Plan_to_Watch || CBStatusRewatch.IsChecked != CurrentProfileOptions.sortOptions.Status_Rewatching || CBStatusWatch.IsChecked != CurrentProfileOptions.sortOptions.Status_Watching)
                {
                    IsExistSortUnsavedChanges = true;
                    ChangeCurrentSettings.Background = new SolidColorBrush(Color.FromArgb(165, 255, 136, 18));
                }
                else
                {
                    ChangeCurrentSettings.Background = new SolidColorBrush(Color.FromArgb(89, 140, 202, 255));
                    IsExistSortUnsavedChanges = false;
                }
            }
            catch
            {
                IsExistSortUnsavedChanges = true;
                ChangeCurrentSettings.Background = new SolidColorBrush(Color.FromArgb(165, 255, 136, 18));
            }

        }

        private void FiltrateList()
        {
            FiltredAnimesList.Clear();
            var _SelectedAnimes = from A in SortedAnimesList
                                  where
                                  A.my_score <= CurrentProfileOptions.sortOptions.MaxScore && A.my_score >= CurrentProfileOptions.sortOptions.MinScore
                                  where
                                  A.series_episodes <= CurrentProfileOptions.sortOptions.MaxEp && A.series_episodes >= CurrentProfileOptions.sortOptions.MinEp
                                  where
                                  A.shiki_status == "Plan to Watch" ? CurrentProfileOptions.sortOptions.Status_Plan_to_Watch : false ||
                                  A.shiki_status == "Watching" ? CurrentProfileOptions.sortOptions.Status_Watching : false ||
                                  A.shiki_status == "Rewatching" ? CurrentProfileOptions.sortOptions.Status_Rewatching : false ||
                                  A.shiki_status == "Completed" ? CurrentProfileOptions.sortOptions.Status_Completed : false ||
                                  A.shiki_status == "On-Hold" ? CurrentProfileOptions.sortOptions.Status_On_Hold : false ||
                                  A.shiki_status == "Dropped" ? CurrentProfileOptions.sortOptions.Status_Dropped : false
                                  where
                                  A.series_type == "tv" ? CurrentProfileOptions.sortOptions.Type_tv : false ||
                                  A.series_type == "ova" ? CurrentProfileOptions.sortOptions.Type_ova : false ||
                                  A.series_type == "special" ? CurrentProfileOptions.sortOptions.Type_special : false ||
                                  A.series_type == "movie" ? CurrentProfileOptions.sortOptions.Type_movie : false ||
                                  A.series_type == "ona" ? CurrentProfileOptions.sortOptions.Type_ona : false ||
                                  A.series_type == "music" ? CurrentProfileOptions.sortOptions.Type_music : false ||
                                  A.series_type == "" ? CurrentProfileOptions.sortOptions.Type_null : false
                                  where
                                  A.my_times_watched >= CurrentProfileOptions.sortOptions.MinRewatchTimes
                                  select A;
            foreach (Anime A in _SelectedAnimes)
                FiltredAnimesList.Add(A);
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (LogWindow1.IsVisible)
                LogVisibleButton.Content = "Скрыть Журналь";
            if (!LogWindow1.IsVisible)
                LogVisibleButton.Content = "Показать Журналь";
            //if (IsShowSortedList)
            //    ShowSortedListButtom.Content = "Отфильтрованные";
            //if (!IsShowSortedList)
            //    ShowSortedListButtom.Content = "Без фильтрации";




            //    if (ScrollViewer1.VerticalScrollBarVisibility == ScrollBarVisibility.Hidden)
            //        LogWindow1.Loging("hidden");
            //    if (ScrollViewer1.VerticalScrollBarVisibility == ScrollBarVisibility.Visible)
            //        LogWindow1.Loging("visible");
            //    if (ScrollViewer1.VerticalScrollBarVisibility == ScrollBarVisibility.Auto)
            //        LogWindow1.Loging("auto");
        }

        private void ImportAnimeListFromFile()
        {

            XmlDocument XmlFileImportedAnimes = new XmlDocument();
            while (true)
            {
                try
                {
                    XmlFileImportedAnimes.Load("Latest_export_animelist.xml");
                    AllAnimesList.Clear();
                    XmlElement xRoot = XmlFileImportedAnimes.DocumentElement;
                    int ImportedAnimeCount = 0;
                    foreach (XmlNode xNodeAnime in xRoot)
                    {
                        if (xNodeAnime.Name == "anime")
                        {
                            Anime tmpAnime = new Anime();
                            foreach (XmlNode xNodePropertie in xNodeAnime)
                            {
                                if (xNodePropertie.Name == "series_title")
                                    tmpAnime.series_title = xNodePropertie.InnerText;
                                if (xNodePropertie.Name == "series_type")
                                    tmpAnime.series_type = xNodePropertie.InnerText;
                                if (xNodePropertie.Name == "series_episodes")
                                    tmpAnime.series_episodes = Convert.ToInt32(xNodePropertie.InnerText);
                                if (xNodePropertie.Name == "series_animedb_id")
                                    tmpAnime.series_animedb_id = Convert.ToInt32(xNodePropertie.InnerText);
                                if (xNodePropertie.Name == "my_watched_episodes")
                                    tmpAnime.my_watched_episodes = Convert.ToInt32(xNodePropertie.InnerText);
                                if (xNodePropertie.Name == "my_times_watched")
                                    tmpAnime.my_times_watched = Convert.ToInt32(xNodePropertie.InnerText);
                                if (xNodePropertie.Name == "my_score")
                                    tmpAnime.my_score = Convert.ToByte(xNodePropertie.InnerText);
                                if (xNodePropertie.Name == "my_status")
                                    tmpAnime.my_status = xNodePropertie.InnerText;
                                if (xNodePropertie.Name == "shiki_status")
                                    tmpAnime.shiki_status = xNodePropertie.InnerText;
                                if (xNodePropertie.Name == "my_comments")
                                    tmpAnime.my_comments = xNodePropertie.InnerText;
                            }
                            AllAnimesList.Add(tmpAnime);
                            ImportedAnimeCount++;
                        }

                    }

                    {
                        SortedAnimesList.Clear();
                        IEnumerable<Anime> An = from Anime in AllAnimesList orderby Anime.my_score descending select Anime;
                        foreach (Anime A in An)
                            SortedAnimesList.Add(A);
                        SortFlagStar = false;
                        SortByStarButton.Cursor = Cursors.ScrollN;
                        SortByEpisodesButton.Cursor = Cursors.ScrollS;
                        SortByTitleButton.Cursor = Cursors.ScrollS;
                        SortByTypeButton.Cursor = Cursors.ScrollS;
                        SortFlagEpisodes = true;
                        SortFlagTitle = true;
                        SortFlagType = true;
                    }
                    FiltredAnimesList.Clear();
                    FiltrateList();
                    RefreshCurrentAnimeList();
                }
                catch (System.IO.FileNotFoundException)
                {
                    LogWindow1.Loging("ERROR: Ошибка загрузки списка аниме из экспортированного файла: файл не был найден, лист не загружен. Попробуйте потыкать там на кнопки, ну, вы поняли...");
                }
                catch (System.IO.IOException)
                {
                    continue;
                } 
                //    MessageBox.Show($"Импортировано аниме:\nПо счётчику - {ImportedAnimeCount}\nПо факту (в списке находится) - {AllAnimesList.Count}");
                break;
            }
        }

        private void Menubutton1_Click(object sender, RoutedEventArgs e)
        {
            MainWindow1.Background = new SolidColorBrush(Color.FromArgb(200, 32, 243, 147));
            CurrentProfileOptions.transformation = 2;
            LogWindow1.Loging("Вы стали фейкой винкс!");
        }

        private void Menubutton2_Click(object sender, RoutedEventArgs e)
        {
            MainWindow1.Background = new SolidColorBrush(Color.FromArgb(200, 240, 135, 9));
            CurrentProfileOptions.transformation = 3;
            LogWindow1.Loging("ЭнЧАнТИИИИКС!!1!1");
        }

        private void Menubutton3_Click(object sender, RoutedEventArgs e)
        {
            MainWindow1.Background = new SolidColorBrush(Color.FromArgb(200, 88, 50, 255));
            CurrentProfileOptions.transformation = 4;
            LogWindow1.Loging("Годы тренировок в горах, и вуаля! Тебе доступен Беливикс!!!!!!!!");
        }

        private void Menubutton4_Click(object sender, RoutedEventArgs e)
        {
            MainWindow1.Background = new SolidColorBrush(Color.FromArgb(200, 245, 11, 224));
            CurrentProfileOptions.transformation = 5;
            LogWindow1.Loging("~Сияй");
        }

        private void Menubutton5_Click(object sender, RoutedEventArgs e)
        {
            MainWindow1.Background = new SolidColorBrush(Color.FromArgb(200, 137, 223, 255));
            CurrentProfileOptions.transformation = 6;
            LogWindow1.Loging("GRAND BLUUUUUUUUUUUUUUUUE!!!1!!1!111! >.< >.< >>>.<<< >>>>>>.<<<<<<");
        }

        private void Menubutton6_Click(object sender, RoutedEventArgs e)
        {
            LogWindow1.Loging("Завершение работы приложения...");
            MainWindow1.Close();
        }

        private void Menubutton7_Click(object sender, RoutedEventArgs e)
        {
            MainWindow1.Background = new SolidColorBrush(Color.FromArgb(178, 255, 255, 255));
            CurrentProfileOptions.transformation = 0;
            LogWindow1.Loging("*Возвращение к истокам*");
        }

        private void Menubutton0_Click(object sender, RoutedEventArgs e)
        {
            GradientStopCollection gsc = new GradientStopCollection();
            gsc.Add(new GradientStop()
            {
                Color = Color.FromArgb(255, 23, 54, 66),
                Offset = 0.0
            });
            gsc.Add(new GradientStop()
            {
                Color = Color.FromArgb(255, 80, 175, 255),
                Offset = 1.0
            });
            MainWindow1.Background = new LinearGradientBrush(gsc, 0)
            {
                StartPoint = new Point(0.5, 0),
                EndPoint = new Point(0.5, 1)
            };
            CurrentProfileOptions.transformation = 1;
            LogWindow1.Loging("Иеловечество.");

        }

        private void LogVisibleButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Windows.OfType<Log>().Count() == 0)
            {
                MainWindow1.Background = new SolidColorBrush(Color.FromArgb(165, 230, 50, 50));
                MessageBox.Show("И на что ты надеешься, глупец!? Ты же сам убил его!?... ТЫ нажал на крестик, и у тебя был шанс спасти его, но ТЫ УБИЛ ЕГО! ЭТО ВСЁ ТВОЯ ВИНА!!", "Не смей произносить его имя!");
            }
            else
            {
                if (LogWindow1.IsVisible == false)
                {
                    LogWindow1.Show();
                    LogWindow1.Loging("Журналь был показанъ");
                }
                else
                {
                    LogWindow1.Hide();
                    LogWindow1.Loging("Журналь был скрыт");
                }

            }
        }

        private void SortByStarButton_Click(object sender, RoutedEventArgs e)
        {
            SortedAnimesList.Clear();
            if (SortFlagStar)
            {
                IEnumerable<Anime> An = from Anime in AllAnimesList orderby Anime.my_score descending select Anime;
                foreach (Anime A in An)
                    SortedAnimesList.Add(A);
                SortFlagStar = false;
                SortByStarButton.Cursor = Cursors.ScrollN;
            }
            else
            {
                IEnumerable<Anime> An = from Anime in AllAnimesList orderby Anime.my_score select Anime;
                foreach (Anime A in An)
                    SortedAnimesList.Add(A);
                SortFlagStar = true;
                SortByStarButton.Cursor = Cursors.ScrollS;
            }
            SortByEpisodesButton.Cursor = Cursors.ScrollS;
            SortByTitleButton.Cursor = Cursors.ScrollS;
            SortByTypeButton.Cursor = Cursors.ScrollS;
            SortFlagEpisodes = true;
            SortFlagTitle = true;
            SortFlagType = true;
            FiltrateList();
            RefreshCurrentAnimeList();
        }

        private void SortByEpisodesButton_Click(object sender, RoutedEventArgs e)
        {
            SortedAnimesList.Clear();
            if (SortFlagEpisodes)
            {
                IEnumerable<Anime> An = from Anime in AllAnimesList orderby Anime.series_episodes descending select Anime;
                foreach (Anime A in An)
                    SortedAnimesList.Add(A);
                SortFlagEpisodes = false;
                SortByEpisodesButton.Cursor = Cursors.ScrollN;
            }
            else
            {
                IEnumerable<Anime> An = from Anime in AllAnimesList orderby Anime.series_episodes select Anime;
                foreach (Anime A in An)
                    SortedAnimesList.Add(A);
                SortFlagEpisodes = true;
                SortByEpisodesButton.Cursor = Cursors.ScrollS;
            }
            SortByStarButton.Cursor = Cursors.ScrollS;
            SortByTitleButton.Cursor = Cursors.ScrollS;
            SortByTypeButton.Cursor = Cursors.ScrollS;
            SortFlagStar = true;
            SortFlagTitle = true;
            SortFlagType = true;
            FiltrateList();
            RefreshCurrentAnimeList();
        }

        private void SortByTitleButton_Click(object sender, RoutedEventArgs e)
        {
            SortedAnimesList.Clear();
            if (SortFlagTitle)
            {
                IEnumerable<Anime> An = from Anime in AllAnimesList orderby Anime.series_title select Anime;
                foreach (Anime A in An)
                    SortedAnimesList.Add(A);
                SortFlagTitle = false;
                SortByTitleButton.Cursor = Cursors.ScrollN;
            }
            else
            {
                IEnumerable<Anime> An = from Anime in AllAnimesList orderby Anime.series_title descending select Anime;
                foreach (Anime A in An)
                    SortedAnimesList.Add(A);
                SortFlagTitle = true;
                SortByTitleButton.Cursor = Cursors.ScrollS;
            }
            SortByStarButton.Cursor = Cursors.ScrollS;
            SortByEpisodesButton.Cursor = Cursors.ScrollS;
            SortByTypeButton.Cursor = Cursors.ScrollS;
            SortFlagEpisodes = true;
            SortFlagStar = true;
            SortFlagType = true;
            FiltrateList();
            RefreshCurrentAnimeList();
        }

        private void SortByTypeButton_Click(object sender, RoutedEventArgs e)
        {
            SortedAnimesList.Clear();
            if (SortFlagType)
            {
                IEnumerable<Anime> An = from Anime in AllAnimesList orderby Anime.series_type select Anime;
                foreach (Anime A in An)
                    SortedAnimesList.Add(A);
                SortFlagType = false;
                SortByTypeButton.Cursor = Cursors.ScrollN;
            }
            else
            {
                IEnumerable<Anime> An = from Anime in AllAnimesList orderby Anime.series_type descending select Anime;
                foreach (Anime A in An)
                    SortedAnimesList.Add(A);
                SortFlagType = true;
                SortByTypeButton.Cursor = Cursors.ScrollS;
            }
            SortByStarButton.Cursor = Cursors.ScrollS;
            SortByEpisodesButton.Cursor = Cursors.ScrollS;
            SortByTitleButton.Cursor = Cursors.ScrollS;
            SortFlagEpisodes = true;
            SortFlagStar = true;
            SortFlagTitle = true;
            FiltrateList();
            RefreshCurrentAnimeList();
        }

        private void RefreshCurrentAnimeList()
        {
            CurrentAnimeList.Items.Clear();
            if (IsSortShikiGroupMetod)
            {
                List<Anime> ShikisortedList = new List<Anime>();
                foreach (var _A in IsShowFiltredList ? FiltredAnimesList : SortedAnimesList)
                    if (_A.shiki_status == "Plan to Watch")
                        ShikisortedList.Add(_A);
                foreach (var _A in IsShowFiltredList ? FiltredAnimesList : SortedAnimesList)
                    if (_A.shiki_status == "Watching")
                        ShikisortedList.Add(_A);
                foreach (var _A in IsShowFiltredList ? FiltredAnimesList : SortedAnimesList)
                    if (_A.shiki_status == "Rewatching")
                        ShikisortedList.Add(_A);
                foreach (var _A in IsShowFiltredList ? FiltredAnimesList : SortedAnimesList)
                    if (_A.shiki_status == "Completed")
                        ShikisortedList.Add(_A);
                foreach (var _A in IsShowFiltredList ? FiltredAnimesList : SortedAnimesList)
                    if (_A.shiki_status == "On-Hold")
                        ShikisortedList.Add(_A);
                foreach (var _A in IsShowFiltredList ? FiltredAnimesList : SortedAnimesList)
                    if (_A.shiki_status == "Dropped")
                        ShikisortedList.Add(_A);
                if (IsPreview)
                    foreach (var A in ShikisortedList)
                        Functions.AddAnimeToCurrentAnimeList(CurrentAnimeList, A);
            }
            else
                if (IsPreview)
                foreach (var A in IsShowFiltredList ? FiltredAnimesList : SortedAnimesList)
                    Functions.AddAnimeToCurrentAnimeList(CurrentAnimeList, A);

            //if (CurrentAnimeList.Items.Count > 12)
            //{
            //    ScrollViewer1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            //    foreach (AnimeGrid Bb in CurrentAnimeList.Items)
            //        Bb.ColumnDefinitions.Last().Width = new GridLength(0);
            //    CBScrollChanger.IsChecked = true;
            //}
            //if (CurrentAnimeList.Items.Count <= 12)
            //{
            //    ScrollViewer1.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            //    foreach (AnimeGrid Bb in CurrentAnimeList.Items)
            //        Bb.ColumnDefinitions.Last().Width = new GridLength(17);
            //    CBScrollChanger.IsChecked = false;
            //}
            //PodognatRazmerAnimeInListPodScroll();
        }

        //private void ScrollBarVisible_ON(object sender, RoutedEventArgs e)
        //{
        //    ScrollViewer1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
        //
        //    foreach (AnimeGrid Bb in CurrentAnimeList.Items)
        //        Bb.ColumnDefinitions.Last().Width = new GridLength(0);
        //
        //}
        //
        //private void ScrollBarVisible_OFF(object sender, RoutedEventArgs e)
        //{
        //    ScrollViewer1.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
        //
        //    foreach (AnimeGrid Bb in CurrentAnimeList.Items)
        //        Bb.ColumnDefinitions.Last().Width = new GridLength(17);
        //}
        //
        //public void PodognatRazmerAnimeInListPodScroll()
        //{
        //    if (CurrentAnimeList........ == ScrollBarVisibility.Hidden)
        //        foreach (AnimeGrid Bb in CurrentAnimeList.Items)
        //            Bb.ColumnDefinitions.Last().Width = new GridLength(17);
        //    if (ScrollViewer1.VerticalScrollBarVisibility == ScrollBarVisibility.Visible)
        //        foreach (AnimeGrid Bb in CurrentAnimeList.Items)
        //            Bb.ColumnDefinitions.Last().Width = new GridLength(0);
        //}

        private void AllAnimeisOFF(object sender, RoutedEventArgs e)
        {
            foreach (AnimeGrid AG in CurrentAnimeList.Items)
                AG.AnimeInList_RandomOFF();
            //LogWindow1.Loging("Все анимки помечены как не участвующие в рандоме. Вроде бы. Я не проверял...");
        }

        private void AllAnimeisON(object sender, RoutedEventArgs e)
        {
            foreach (AnimeGrid AG in CurrentAnimeList.Items)
                AG.AnimeInList_RandomON();
            //LogWindow1.Loging("Все анимки помечены как участвующие в рандоме. Вроде бы. Я не проверял...");
        }

        private void ExportFromShikiButton_Click(object sender, RoutedEventArgs e)
        {
            bool _IsComplete = true;
            try
            {
                if (TBUsername.Text == null || TBUsername.Text == "")
                {
                    Exception eeee = new Exception("Поле логина не заполнено");
                    throw eeee;
                }
            }
            catch (Exception eeee)
            {
                MessageBox.Show(this, eeee.Message, "Список не был экспортирован с шикимори");
                _IsComplete = false;
            }
            if (_IsComplete)
            {
                if (TBUsername.Text != "username")
                    CurrentProfileOptions.username = TBUsername.Text;
                AsyncExportList();
                ImportAnimeListFromFile();

                ExportFromShikiButton.Background = new SolidColorBrush(Color.FromArgb(89, 140, 202, 255));
                IsExistUsernameUnsavedChanges = false;
            }

        }

        private void ChangeCurrentSettings_Click(object sender, RoutedEventArgs e)
        {
            bool _IsComplete = true;
            try
            {
                if (TBMinScore.Text == null || TBMaxScore.Text == null || TBMaxCountEp.Text == null || TBMinCountEp.Text == null || TBUsername.Text == null)
                {
                    Exception eeee = new Exception("Одно или более поле ввода параметров фильтрации не заполнено");
                    throw eeee;
                }

                CurrentProfileOptions.sortOptions.Status_Plan_to_Watch = (bool)CBStatusPlan.IsChecked;
                CurrentProfileOptions.sortOptions.Status_Watching = (bool)CBStatusWatch.IsChecked;
                CurrentProfileOptions.sortOptions.Status_Rewatching = (bool)CBStatusRewatch.IsChecked;
                CurrentProfileOptions.sortOptions.Status_Completed = (bool)CBStatusCompl.IsChecked;
                CurrentProfileOptions.sortOptions.Status_On_Hold = (bool)CBStatusOnHold.IsChecked;
                CurrentProfileOptions.sortOptions.Status_Dropped = (bool)CBStatusDrop.IsChecked;

                CurrentProfileOptions.sortOptions.Type_tv = (bool)CBTypeTV.IsChecked;
                CurrentProfileOptions.sortOptions.Type_movie = (bool)CBTypeMov.IsChecked;
                CurrentProfileOptions.sortOptions.Type_ova = (bool)CBTypeOva.IsChecked;
                CurrentProfileOptions.sortOptions.Type_special = (bool)CBTypeSP.IsChecked;
                CurrentProfileOptions.sortOptions.Type_ona = (bool)CBTypeOna.IsChecked;
                CurrentProfileOptions.sortOptions.Type_music = (bool)CBTypeMus.IsChecked;
                CurrentProfileOptions.sortOptions.Type_null = (bool)CBTypeNull.IsChecked;

                CurrentProfileOptions.sortOptions.MinScore = Convert.ToInt16(TBMinScore.Text);
                CurrentProfileOptions.sortOptions.MaxScore = Convert.ToInt16(TBMaxScore.Text);
                CurrentProfileOptions.sortOptions.MaxEp = Convert.ToInt32(TBMaxCountEp.Text);
                CurrentProfileOptions.sortOptions.MinEp = Convert.ToInt32(TBMinCountEp.Text);
                CurrentProfileOptions.username = TBUsername.Text;
            }
            catch (OverflowException)
            {
                MessageBox.Show("Дружище, мы, конечно понимаем твою любовь к максимализму, но кое-какое число там в настроечках уж слишком... Напиши что-то вменяемое и try again)", "Чёт число не годное");
                _IsComplete = false;
            }
            catch (FormatException)
            {
                _IsComplete = false;
            }
            catch (Exception eeee)
            {
                MessageBox.Show(this, eeee.Message, "Что-то не так...");
                _IsComplete = false;
            }
            if (_IsComplete)
            {
                //применение фильтрации и заполнение листа
                FiltrateList();
                RefreshCurrentAnimeList();
                ChangeCurrentSettings.Background = new SolidColorBrush(Color.FromArgb(89, 140, 202, 255));
                IsExistSortUnsavedChanges = false;
            }
        }

        private void RefreshVisualBackgroundTransform()
        {
            switch (CurrentProfileOptions.transformation)
            {
                case 1:
                    GradientStopCollection gsc = new GradientStopCollection();
                    gsc.Add(new GradientStop()
                    {
                        Color = Color.FromArgb(255, 23, 54, 66),
                        Offset = 0.0
                    });
                    gsc.Add(new GradientStop()
                    {
                        Color = Color.FromArgb(255, 80, 175, 255),
                        Offset = 1.0
                    });
                    MainWindow1.Background = new LinearGradientBrush(gsc, 0)
                    {
                        StartPoint = new Point(0.5, 0),
                        EndPoint = new Point(0.5, 1)
                    };
                    break;
                case 2:
                    MainWindow1.Background = new SolidColorBrush(Color.FromArgb(200, 32, 243, 147));
                    break;
                case 3:
                    MainWindow1.Background = new SolidColorBrush(Color.FromArgb(200, 240, 135, 9));
                    break;
                case 4:
                    MainWindow1.Background = new SolidColorBrush(Color.FromArgb(200, 88, 50, 255));
                    break;
                case 5:
                    MainWindow1.Background = new SolidColorBrush(Color.FromArgb(200, 245, 11, 224));
                    break;
                case 6:
                    MainWindow1.Background = new SolidColorBrush(Color.FromArgb(200, 137, 223, 255));
                    break;
            }
        }

        private void RefreshVisualSortOptions()
        {
            TBMinScore.Text = CurrentProfileOptions.sortOptions.MinScore.ToString();
            TBMaxScore.Text = CurrentProfileOptions.sortOptions.MaxScore.ToString();
            TBMaxCountEp.Text = CurrentProfileOptions.sortOptions.MaxEp.ToString();
            TBMinCountEp.Text = CurrentProfileOptions.sortOptions.MinEp.ToString();

            CBStatusPlan.IsChecked = CurrentProfileOptions.sortOptions.Status_Plan_to_Watch;
            CBStatusWatch.IsChecked = CurrentProfileOptions.sortOptions.Status_Watching;
            CBStatusRewatch.IsChecked = CurrentProfileOptions.sortOptions.Status_Rewatching;
            CBStatusCompl.IsChecked = CurrentProfileOptions.sortOptions.Status_Completed;
            CBStatusOnHold.IsChecked = CurrentProfileOptions.sortOptions.Status_On_Hold;
            CBStatusDrop.IsChecked = CurrentProfileOptions.sortOptions.Status_Dropped;

            CBTypeTV.IsChecked = CurrentProfileOptions.sortOptions.Type_tv;
            CBTypeMov.IsChecked = CurrentProfileOptions.sortOptions.Type_movie;
            CBTypeOva.IsChecked = CurrentProfileOptions.sortOptions.Type_ova;
            CBTypeSP.IsChecked = CurrentProfileOptions.sortOptions.Type_special;
            CBTypeOna.IsChecked = CurrentProfileOptions.sortOptions.Type_ona;
            CBTypeMus.IsChecked = CurrentProfileOptions.sortOptions.Type_music;
            CBTypeNull.IsChecked = CurrentProfileOptions.sortOptions.Type_null;
        }

        private void RefreshVisualProfileOptions()
        {
            TBUsername.Text = CurrentProfileOptions.username;
            RefreshVisualBackgroundTransform();
            RefreshVisualSortOptions();
            
        }

        private void SetCurrentSortOptionToDefaultButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentProfileOptions.sortOptions = new SortOptions();
            RefreshVisualSortOptions();

        }

        private void CBShowSortedList_Checked(object sender, RoutedEventArgs e)
        {
            RefreshCurrentAnimeList();
        }

        private void CBShikiGroupMetodButton_Checked(object sender, RoutedEventArgs e)
        {
            RefreshCurrentAnimeList();
        }

        private void CBIsPreview_Checked(object sender, RoutedEventArgs e)
        {
            TBPreviewIsOFF.Visibility = Visibility.Hidden;
            GridPreviewIsON.Visibility = Visibility.Visible;
        }

        private void CBIsPreview_Unchecked(object sender, RoutedEventArgs e)
        {
            GridPreviewIsON.Visibility = Visibility.Hidden;
            TBPreviewIsOFF.Visibility = Visibility.Visible;
        }

        private async void RandomizeSorted_Click(object sender, RoutedEventArgs e)
        {
            List<Anime> tmplist = new List<Anime>();
            IEnumerable<Anime> oldien = from Anime in IsShowFiltredList ? FiltredAnimesList : SortedAnimesList where Anime.IsEnabledInSort == true select Anime;
            foreach (var old in oldien)
                tmplist.Add(old);

            int n = Convert.ToInt32(TBWinnersCount.Text);

            if (n <= tmplist.Count)
            {
                if (IsRatingModifyChance)
                {


                    int maxrandnum = tmplist.Count;
                    foreach (var A in tmplist)
                        maxrandnum += A.my_score;

                    randMassResult = await Functions.UseRandomOrgGenerateIntegers_1(n, maxrandnum, 1, LogWindow1);

                    WinnersAnimesList.Clear();
                    int x = 0, l = 0;
                    for (int a = 0; a < n; a++)
                    {
                        x = randMassResult[a];
                        l = 0;
                        while (true)
                        {
                            x -= (tmplist[l].my_score + 1);
                            if (x <= 0)
                            {
                                break;
                            }
                            l++;
                        }
                        WinnersAnimesList.Add(new Anime(tmplist[l]));
                    }

                    ResultAnimeList.Items.Clear();
                    foreach (var A in WinnersAnimesList)
                        Functions.AddAnimeToResultAnimeList(ResultAnimeList, A);


                }
                else
                {


                    //ReturningMessage rm = new ReturningMessage();
                    randMassResult = await Functions.UseRandomOrgGenerateIntegers_1(n, tmplist.Count, 1, LogWindow1);

                    WinnersAnimesList.Clear();
                    for (int a = 0; a < n; a++)
                    {
                        WinnersAnimesList.Add(new Anime(tmplist[randMassResult[a] - 1]));
                    }

                    ResultAnimeList.Items.Clear();
                    foreach (var A in WinnersAnimesList)
                        Functions.AddAnimeToResultAnimeList(ResultAnimeList, A);

                    //MessageBox.Show(randMass[0] + " " + randMass[1] + " " + randMass[2] + " " + randMass[3] + " " + randMass[4] + " " + randMass[5]);

                
                }
            }
            else
                MessageBox.Show("Количество зарандомленных аниме не может быть больше количества выбранных аниме.", "Нит.");

        }

        private void OpenWinnersInNewWindowButton_Click(object sender, RoutedEventArgs e)
        {
            if (WinnersAnimesList.Count != 0)
            {
                Winners NewWinners = new Winners(++WinnersWindowsCountInc, WinnersAnimesList, this);
            }
            else
            {
                MessageBox.Show("Чёт не осталось сведений о последних победителях.\n Чтобы развеять тоску могу посоветовать вознесение путём многократного пересмотра: \nhttps://www.youtube.com/watch?v=7aDLksTDqcE", "Онии-сама!!1");
            }
        }

        private void MenuItem_Click()
        {

        }
    }
}