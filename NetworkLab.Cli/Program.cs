using NetworkLab.Core.Models;

var ip = new IPv4Address(192,168,1,34);

uint value = ip.ToUInt32();

Console.WriteLine(value);

var newip = IPv4Address.FromUInt32(value);

Console.WriteLine(newip);

