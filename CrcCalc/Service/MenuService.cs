namespace CrcCalc.Service
{
    public class MenuService
    {
        SerialPortService com;
        DataReciveService dataReciveService;
        public bool ExitButton = false;
        public MenuService()
        {
            Console.WriteLine("Miernik Izolacji");
            Console.WriteLine("Wersja 0.1a");
            com = new SerialPortService();
            dataReciveService = new(com._serialPort);
            setPort();
            //getCRC();
        }
        public void sendData(string data) => com.SendData(data);
        public void setPort()
        {

            for (int e = 0; e < SerialPortService.portsList().Length; e++)
            {
                string[] ports = SerialPortService.portsList();
                Console.WriteLine($"{e} : {ports[e]}");

            }
            int opt = Int16.Parse(Console.ReadLine());

            com.setName(opt);
            Task.Run(() => dataReciveService.Start());
            Thread.Sleep(300);
            com.SendData("V");
        }


    }
}
