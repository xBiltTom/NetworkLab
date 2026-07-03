namespace NetworkLab.Core.Models;

public sealed class IPv4Network
{
    private readonly IPv4Address _networkAddress;
    private readonly byte _prefixLength;

    public IPv4Address NetworkAddress => _networkAddress;
    public byte PrefixLength => _prefixLength;

    public IPv4Network(IPv4Address ip,byte prefix)
    {
        
    }


    public uint PrefixToMask(byte prefix)
    {
        return 0;
    }
}