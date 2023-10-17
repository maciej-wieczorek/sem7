using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minutnik
{
    public class Minutnik
    {
        public Minutnik(DateTime end) { m_end = end; }

        public TimeSpan GetDiff()
        {
            return m_end - DateTime.Now;
        }

        public string GetDiffFormated()
        {
            TimeSpan diff = GetDiff();
            return diff.ToString(@"hh\:mm\:ss");
        }
        public int GetDiffHours()
        {
            return GetDiff().Hours;
        }

        public int GetDiffMinutes()
        {
            return GetDiff().Minutes;
        }

        public int GetDiffSeconds()
        {
            return GetDiff().Seconds;
        }

        private DateTime m_end;
    }
}
