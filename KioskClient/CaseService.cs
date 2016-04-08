using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskClient
{
    // http://masterrussian.com/aa050601a.shtml

    public class NumericCaseService
    {
        private readonly string nominativeSingular;
        private readonly string genetiveSingular;
        private readonly string genetivePlural;

        public NumericCaseService(string nominativeSingular, string genetiveSingular, string genetivePlural)
        {
            this.nominativeSingular = nominativeSingular;   // минута
            this.genetiveSingular = genetiveSingular;       // минуты
            this.genetivePlural = genetivePlural;           // минуту
        }

        public string GetTemplate(int number)
        {
            var lastDigit = number % 10;

            return "";
        }
    }
}
