namespace CrcCalc.Service
{
    class Len
    {
        public static int Licz(string v)
        {
            int i = 0;
            foreach (char c in v)
            {
                i++;
            }



            return i;
        }
    }
}
