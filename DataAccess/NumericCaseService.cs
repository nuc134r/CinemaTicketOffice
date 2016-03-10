namespace DataAccess
{
    public static class NumericCaseService
    {
        public static string Minute(int number)
        {
            if (number > 10 && number <= 20) return Resources.MinutesGenetivePlural;

            number = number % 10;

            switch (number)
            {
                case 1:
                    return Resources.MinutesNominative;
                case 2:
                case 3:
                case 4:
                    return Resources.MinutesGenitiveSingular;
                default:
                    return Resources.MinutesGenetivePlural;
            }
        }
    }
}