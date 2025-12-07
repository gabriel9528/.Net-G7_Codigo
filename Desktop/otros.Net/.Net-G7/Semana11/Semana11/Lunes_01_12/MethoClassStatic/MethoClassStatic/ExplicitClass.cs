namespace MethoClassStatic
{
    internal class ExplicitClass
    {
        private static string[] desserts = { 
            "pay de manzana", 
            "pastel de chocolate", 
            "manzana caramelizada", 
            "fresas con crema" 
        };

        private static IEnumerable<string> founds = from n in desserts
                                                    where n.Contains("manzana")
                                                    orderby n
                                                    select n;

        public static IEnumerable<string> ListFounds { get { return founds; } }

        public static IEnumerable<string> GetDesserts()
        {
            return founds;
        }


        //--------------------------------Number Even---------------------------------

        public static IEnumerable<int> GetNumbersEvenDefered()
        {
            int[] numbers = { 1, 20, 15, 8, 11, 18, 6, 13 };

            IEnumerable<int> numbersEven = from n in numbers
                                           where n % 2 == 0
                                           select n;
            return numbersEven;
        }

        public static IEnumerable<int> GetNumbersOddExecuted()
        {
            int[] numbers = { 1, 20, 15, 8, 11, 18, 6, 13 };

            IEnumerable<int> numbersOdd = from n in numbers
                                           where n % 2 != 0
                                           select n;
            return numbersOdd.ToArray();
        }



    }
}
