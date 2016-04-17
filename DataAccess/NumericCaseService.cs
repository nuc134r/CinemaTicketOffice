namespace DataAccess
{
    public class NumericCaseService
    {
        private readonly string single;
        private readonly string coupleOf;
        private readonly string many;

        public NumericCaseService(string single, string coupleOf, string many)
        {
            this.single = single;       // минутa, место
            this.coupleOf = coupleOf;   // минуты, места
            this.many = many;           // минут, мест
        }

        public string GetCaseString(int number)
        {
            if (number >= 5 && number <= 20) return many;

            var lastDigit = number % 10;

            if (lastDigit == 1) return single;
            if (lastDigit > 1 && lastDigit < 5) return coupleOf;

            return many;
        }
    }
}
