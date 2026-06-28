using NetworkLab.Core.Models;

var ip = new IPv4Address(192, 168, 1, 34);

Console.WriteLine(ip.Octet1);
Console.WriteLine(ip.Octet2);
Console.WriteLine(ip.Octet3);
Console.WriteLine(ip.Octet4);