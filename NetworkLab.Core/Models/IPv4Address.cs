using System.Linq.Expressions;

namespace NetworkLab.Core.Models;

public class IPv4Address{
    // Definimos propiedades (es como declarar la variable y crear un método get, pero en csharp se crean los 2 en 1)
    public byte Octet1 { get;}
    public byte Octet2 { get;}
    public byte Octet3 { get;}
    public byte Octet4 { get;}

    public IPv4Address(byte octet1, byte octet2, byte octet3, byte octet4)
    {
        Octet1 = octet1;
        Octet2 = octet2;
        Octet3 = octet3;
        Octet4 = octet4;
    }

    public override string ToString()
    {
        return $"{Octet1}.{Octet2}.{Octet3}.{Octet4}";
    }

    public uint ToUInt32()
    {
        return ((uint)Octet1 << 24)
            | ((uint)Octet2 << 16)
            | ((uint)Octet3 << 8)
            | Octet4;
    }

    public static IPv4Address FromUInt32(uint value)
    {
        byte octet1 = (byte)((value >> 24) & 0xFF);
        byte octet2 = (byte)((value >> 16) & 0xFF);
        byte octet3 = (byte)((value >> 8) & 0xFF);
        byte octet4 = (byte)(value & 0xFF);
        return new IPv4Address(octet1, octet2, octet3, octet4);
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
                    throw new FormatException("No se permiten 0's a la izquierda");
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
}