using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyStarter
{
    enum TimeType
    {
        Sec,
        Min,
        Hour
    }

    class Period
    {
        public static Period SEC3 = new Period(3, TimeType.Sec);
        public static Period SEC5 = new Period(5, TimeType.Sec);
        public static Period SEC10 = new Period(10, TimeType.Sec);
        public static Period SEC15 = new Period(15, TimeType.Sec);
        public static Period SEC30 = new Period(30, TimeType.Sec);

        public static Period MIN1 = new Period(1, TimeType.Min);
        public static Period MIN3 = new Period(3, TimeType.Min);
        public static Period MIN5 = new Period(5, TimeType.Min);
        public static Period MIN10 = new Period(10, TimeType.Min);
        public static Period MIN15 = new Period(15, TimeType.Min);
        public static Period MIN30 = new Period(30, TimeType.Min);

        public static Period HOUR1 = new Period(1, TimeType.Hour);


        public readonly uint Interval;

        public long TotalSeconds
        {
            get
            {
                if (Type == TimeType.Sec) return Interval;
                if (Type == TimeType.Min) return Interval * 60;
                else return Interval * 60 * 60;                
            }
        }

        public readonly TimeType Type;

        public Period(uint period, TimeType type)
        {
            Interval = period;
            Type = type;
        }

        public override string ToString()
        {
            string typeStr;
            if (Type == TimeType.Sec) typeStr = "S";
            else if (Type == TimeType.Min) typeStr = "M";
            else typeStr = "H";

            return Interval + typeStr;
        }
    }
}
