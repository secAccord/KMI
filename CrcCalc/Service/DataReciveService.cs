using System.IO.Ports;

namespace CrcCalc.Service
{
    class DataReciveService
    {
        SerialPort com;
        bool status = false;
        char cmd = '0';
        byte[] crc = new byte[0];
        byte[] buffor = new byte[0];
        public DataReciveService(SerialPort com)
        {
            this.com = com;
        }

        public async Task Start()
        {
            status = true;
            while (status)
            {

                byte by = (byte)com.ReadByte();
                if (cmd == '0')
                {
                    ReadCommand(by);

                    Console.WriteLine(cmd);
                    if (cmd == 'I')
                    {
                        ClearBufCrc();
                        continue;
                    }

                }
                if (buffor.Length == 0)
                {
                    CreateBuffor(com.ReadByte());
                    BufforReader.Read(cmd, buffor);
                    ClearBufCrc();

                }

                //Console.WriteLine("{0:X} ", by);
            }

        }
        private void ClearBufCrc()
        {

            Console.Write("CRC: ");
            Console.Write("0x{0:X}", com.ReadByte());
            Console.WriteLine("{0:X} ", com.ReadByte());
            buffor = new byte[0];
            cmd = '0';
        }
        private void ReadCommand(byte by)
        {
            if (by == 0x06)
            {

                Console.WriteLine("Zadanie wykonane pomyślnie");
                cmd = 'I';
                return;
            }
            if (by == 0x21)
            {

                Console.WriteLine("Błędna suma kontrolna (Crc)");
                cmd = 'I';
                return;
            }
            if (by == 0x20)
            {

                Console.WriteLine("Parametr poza zakresem");
                cmd = 'I';
                return;
            }
            cmd = (char)by;

        }
        private void CreateBuffor(int size)
        {
            buffor = new byte[size];
            Console.WriteLine($"Buffor {buffor.Length}");
            for (int i = 0; i < buffor.Length; i++)
            {
                buffor[i] = (byte)com.ReadByte();
            }
        }


        public void Stop()
        {
            status = false;
        }

    }
}
