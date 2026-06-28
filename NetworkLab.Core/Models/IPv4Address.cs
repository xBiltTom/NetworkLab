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
}