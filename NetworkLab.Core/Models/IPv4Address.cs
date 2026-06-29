namespace NetworkLab.Core.Models;

public class IPv4Address{
    private readonly uint _value;

    public uint Value => _value;
    public byte Octet1 => (byte)(_value >> 24);
    public byte Octet2 => (byte)(_value >> 16);
    public byte Octet3 => (byte)(_value >> 8);
    public byte Octet4 => (byte)_value; 


    public IPv4Address(byte octet1, byte octet2, byte octet3, byte octet4)
    {
        _value = Pack(octet1,octet2,octet3,octet4);
    }

    private static uint Pack(byte octet1,byte octet2,byte octet3,byte octet4)
    {
        return ((uint)octet1 << 24)
            | ((uint)octet2 << 16)
            | ((uint)octet3 << 8)
            | octet4;
    }

    public override string ToString()
    {
        return $"{Octet1}.{Octet2}.{Octet3}.{Octet4}";
    } 

    public static IPv4Address Parse(string input)
    {
        if (string.IsNullOrEmpty(input)){
            throw new ArgumentException("La dirección IPv4 no puede estar vacía");
        }

        int currentNumber = 0; // indica el numero actual dentro de un octeto
        int currentOctet = 0; // indica el octeto actual
        bool hasDigits = false; // bandera para indicar que hubo almenos un digito antes del punto (pregunta: ¿He leído almenos un número en el octeto acutal?)
        
        byte[] octets = new byte[4];

        for (int i=0; i<input.Length; i++)
        {
            char currentChar = input[i]; //obtenemos el caracter actual

            if (char.IsDigit(currentChar)) //si el caracter es un número 
            {
                // ========= VARIABLES DE ESTADO ========
                bool isFirstDigit = !hasDigits;
                bool isZero = currentChar == '0'; 
                bool hasNextChar = i < input.Length-1;
                bool nextEndsOctet = hasNextChar && input[i+1] == '.';
                // ======================================

                // Si el caracter actual es el primero, es 0, existe un caracter siguiente y si ese siguiente no es el final del octeto    
                if (isFirstDigit && isZero && hasNextChar && !nextEndsOctet)
                {
                    throw new FormatException("No se permiten ceros a la izquierda");
                }

                int digit = currentChar - '0'; // obtenemos el dígito que representa el caracter actual
                currentNumber = currentNumber * 10 + digit; // lo añadimos al numero actual
                hasDigits = true; // indicamos que hubo almenos un dígito antes del punto

                if (currentNumber > 255) // verificamos que el número esté dentro de los límites válidos antes de pasar a leer el siguiente caracter
                {
                    throw new FormatException("Cada octeto debe estar entre 0 y 255");
                }

            } else if (currentChar == '.') // si el caracter es un punto
            {
                if (!hasDigits)
                {
                    throw new FormatException("Cada octeto debe tener almenos un dígito");
                } 

                if (currentOctet >= 4)
                {
                    throw new FormatException("Solo pueden existir 4 octetos en una ipv4");
                }

                octets[currentOctet] = (byte)currentNumber; // tras validar se guarda el octeto
                currentOctet++; // nos movemos al siguiente octeto
                hasDigits = false; // se desconoce si el siguiente octeto cuenta con dígitos
                currentNumber = 0; // el número actual del siguiente octeto inicia en 0
            } else // si el caracter es cualquier cosa
            {
                throw new FormatException($"Caracter ingresado inválido: '{currentChar}'");
            }
        }

        // Tratamiento del último octeto
        if (!hasDigits)
        {
            throw new FormatException("La dirección IPv4 no puede terminar con un punto");
        }
        if (currentOctet >= 4)
        {
            throw new FormatException("Una IPv4 solo puede tener 4 octetos");
        }
        
        octets[currentOctet] = (byte)currentNumber;
        currentOctet++;

        if (currentOctet != 4)
        {
            throw new FormatException("Una IPv4 debe de tener exactamente 4 octetos");
        }

        return new IPv4Address(octets[0], octets[1], octets[2], octets[3]);
    }

    public static IPv4Address FromUInt32(uint value)
    {
        return new IPv4Address(
            (byte)((value >> 24) & 0xFF),
            (byte)((value >> 16) & 0xFF),
            (byte)((value >> 8) & 0xFF),
            (byte)(value & 0xFF)
        );
    }
}