using System.Text;

namespace CrcCalc.Service
{
    class BufforReader
    {
        public static void Read(char cmd, byte[] buffor)
        {
            if (cmd == 'T')
            {

                Console.WriteLine($"Rezystancja: {BitConverter.ToSingle(buffor[..4])}");
                Console.WriteLine($"Napięcie: {BitConverter.ToSingle(buffor[4..8])}");
                Console.WriteLine($"Ampery?: {BitConverter.ToSingle(buffor[8..12])}");
                return;
            }
            if (cmd == 'S')
            {

                Console.WriteLine($"Pojemność: {BitConverter.ToSingle(buffor[..4])}");
                return;
            }
            if (cmd == 'V')
            {
                Console.WriteLine($"Moduł: {Encoding.ASCII.GetString(buffor[..6])}");
                Console.WriteLine($"Firmware: {Encoding.ASCII.GetString(buffor[7..13])}");
                Console.WriteLine($"Wersja: {Encoding.ASCII.GetString(buffor[13..])}");
                return;
            }
            if (cmd == 'p')
            {
                if ((int)buffor[0] == 8) Console.WriteLine($"Napięcie jest ustawione na: {((int)buffor[1]) * 10}V");
                else Console.WriteLine($"Parametr pracy [{(int)buffor[0]}] jest ustawiony na: {(int)buffor[1]}");
                return;
            }

            if (cmd == 'M')
            {
                if (((int)buffor[0] == 0)) Console.WriteLine($"Pomiar rozpoczyna się");
                if (((int)buffor[0] == 1)) Console.WriteLine($"Pomiar w toku");
                if (((int)buffor[0] == 2)) Console.WriteLine($"Miernik rozładowywuje się");
                if (((int)buffor[0] == 255)) Console.WriteLine($"Stałe kalibracyjne uszkodzone");
                return;
            }
        }
    }
}
