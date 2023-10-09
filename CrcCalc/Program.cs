using CrcCalc.Service;

public class App
{

    static public void Main()
    {

        MenuService Menu = new MenuService();
        while (true)
        {

            Console.WriteLine("Wyślij polecenie do miernika");

            Console.WriteLine("Napisz quit jeśli chcesz opuścić program");
            string cmd = Console.ReadLine();
            if (cmd == "quit")
            {
                Menu.sendData("S 1");
                Console.WriteLine("Wyłączanie programu");
                break;

            }
            Menu.sendData(cmd);



        }

    }

}
