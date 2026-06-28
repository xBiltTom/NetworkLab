using NetworkLab.Core.Models;

var ip = new IPv4Address(255, 255, 255, 255);

Console.WriteLine(ip);

Console.WriteLine(ip.ToUInt32());