using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRandomizer
{
    /// <summary>
    /// Содержит:
    ///     |||2 списка с галочками - типы, статусы
    ///     |||2 галочки - онгоинг, неонгоинг
    ///     |||3 числа - мин эп, макс эп, мин колв-во пересмотров
    ///     |||2 списка чисел с радиобуттон - мин оценка, макс оценка 
    /// </summary>
    public class SortOptions
    {
        public int MaxEp = 100000;
        public int MinEp = 0;
        //public bool Ongoing = true;
        //public bool NoOngoing = true;
        public int MinRewatchTimes = 0;
        public short MaxScore = 10;
        public short MinScore = 0;

        public bool Status_Completed        = true;
        public bool Status_Plan_to_Watch    = true;
        public bool Status_Watching         = true;
        public bool Status_Rewatching       = true;
        public bool Status_On_Hold          = true;
        public bool Status_Dropped          = true;

        public bool Type_tv       = true;
        public bool Type_ova      = true;
        public bool Type_special  = true;
        public bool Type_movie    = true;
        public bool Type_ona      = true;
        public bool Type_music    = true;
        public bool Type_null     = true;


    }
}
