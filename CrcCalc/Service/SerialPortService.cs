using System.IO.Ports;

namespace CrcCalc.Service
{
    public class SerialPortService
    {
        public SerialPort _serialPort;

        public SerialPortService()
        {

            _serialPort = new SerialPort();
            _serialPort.BaudRate = 38400;
            _serialPort.Parity = Parity.Even;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = StopBits.One;


        }
        public void setName(int idx)
        {

            _serialPort.PortName = SerialPort.GetPortNames()[idx];
            try
            {
                _serialPort.Open();

            }
            catch
            {
                Console.WriteLine($"Błąd połącznie z portem ({_serialPort.PortName})");
            }
            finally
            {
                Console.WriteLine($"Wybrano i połączono z Portem {_serialPort.PortName}");

            }


        }

        public void SendData(string rawData)
        {
            try
            {

                byte[] data = Crc.GetCRC(rawData);
                _serialPort.Write(data, 0, data.Length);

            }
            finally
            {
                Console.WriteLine("Wysłano dane");
            }

        }


        static public string[] portsList() => SerialPort.GetPortNames();

    }
}
