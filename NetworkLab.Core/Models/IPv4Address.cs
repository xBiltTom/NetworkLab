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
}