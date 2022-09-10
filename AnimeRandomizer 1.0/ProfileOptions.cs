using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRandomizer
{
    /// <summary>
    /// Содержит: 
    ///     |=|1 Строку - Юзернэйм
    ///     |=|1 нередактируемое явно пользователем число - трансформ фона
    ///     |=|Набор опций сортировки: ...
    ///         |||2 списка с галочками - типы, статусы
    ///         |||2 галочки - онгоинг, неонгоинг
    ///         |||3 числа - мин эп, макс эп, мин колв-во пересмотров
    ///         |||2 списка чисел с радиобуттон - мин оценка, макс оценка 
    /// </summary>
    public class ProfileOptions
    {
        public string NAME; 
        public SortOptions sortOptions = null;
        public string username = "username";
        public short transformation = 0;

        public ProfileOptions ()
        {
            sortOptions = new SortOptions();
        }
    }
}
