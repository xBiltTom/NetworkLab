namespace NetworkLab.Core.Models;

public sealed class IPv4Network
{
    private readonly IPv4Address _networkAddress;
    private readonly byte _prefixLength;

    public IPv4Address NetworkAddress => _networkAddress;
    public byte PrefixLength => _prefixLength;

    private IPv4Network(IPv4Address networkAddress,byte prefixLength)
    {
        _networkAddress = networkAddress;
        _prefixLength = prefixLength;
    }

    public static IPv4Network FromAddress(IPv4Address address, byte prefixLength)
    {
        var mask = IPv4Mask.FromPrefix(prefixLength); //se calcula la mascara en uint en base al prefijo en byte
        uint networkValue = address.Value & mask.Value; //operacion and bit a bit entre la direccion del host y el prefijo para hallar el valor de la direccion de red en uint

        var networkAddress = IPv4Address.FromUInt32(networkValue); //objeto IPv4Address hecho en base al valor de la red en uint

        return new IPv4Network(networkAddress,prefixLength); //se retorna el objeto que representa a la direccion de red
    }

    public override string ToString()
    {
        return $"{NetworkAddress}/{PrefixLength}"; 
    }
}