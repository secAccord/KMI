namespace CrcCalc
{
    public class Crc
    {


        private static ushort Update(ushort crc, byte data)
        {

            byte i;

            crc = (ushort)(crc ^ ((ushort)data << 8));

            for (i = 0; i < 8; i++)
            {
                if ((crc & 0x8000) != 0)
                    crc = (ushort)((crc << 1) ^ 0x1021);
                else
                    crc <<= 1;
            }
            return crc;
        }
        private static byte[] dataBlock(ushort crc, List<byte> bytes)
        {

            bytes.Add((byte)(crc & 0xFF));
            bytes.Add((byte)(crc >> 8));





            return bytes.ToArray();


        }

        static private byte[] Calc(byte[] receiveBuffer)
        {
            //Inicjacja zmiennych
            List<byte> blockData = new List<byte>();
            ushort crc;

            ushort i, length;
            byte byteBuf;

            crc = 0;
            length = (ushort)receiveBuffer.Length;

            Console.WriteLine("\nBlok danych: ");
            for (i = 0; i < length; i++)
            {
                byteBuf = receiveBuffer[i];
                blockData.Add(byteBuf);
                Console.Write("0x{0:X}, ", byteBuf);
                crc = Update(crc, byteBuf);
            }

            Console.WriteLine("\nCRC bloku wynosi: 0x{0:X}, ale wysyłamy go w kolejności najpierw bajt młodszy potem starszy", crc);
            Console.WriteLine("Bajty bloku danych:");
            byte[] data = dataBlock(crc, blockData);
            foreach (byte b in data)
            {
                Console.Write("{0:X2} ", b);
            }
            return data;
        }
        public static byte[] GetCRC(string input)
        {

            if (input == null)
            {

                input = "V";

            }


            List<byte> data = new List<byte>
            {
                (byte)input[0]
            };
            if (input.Length > 1)
            {
                string[] a = input[2..].Split(' ');
                foreach (string el in a)
                {
                    byte _el = (byte)Int16.Parse(el);
                    data.Add(_el);

                }

            }


            return Calc(data.ToArray());


        }
    }
}