namespace NetworkLab.Core.Models;

public sealed class IPv4Mask
{
    private readonly uint _value;
    public uint Value => _value;

    private IPv4Mask(uint value)
    {
        _value = value;
    }

    public static IPv4Mask FromPrefix(byte prefix)
    {
        if (prefix > 32)
        {
            throw new ArgumentOutOfRangeException(
                nameof(prefix),
                "El prefijo debe ser mayor o igual a 0 y menor o igual a 32");
        }       

        uint value = prefix == 0 ? 0 : uint.MaxValue << (32 - prefix); //operador ternario-> si el prefijo es 0 entonces value es 0, sino se realiza la operacion para hallar value

        return new IPv4Mask(value);
    }

    public override string ToString()
    {
        return IPv4Address.FromUInt32(_value).ToString();
    }

}