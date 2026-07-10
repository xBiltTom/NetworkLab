using NetworkLab.Core.Models;

var input = "192.168.32.255";
byte prefixLenght = 8;
var hostAddress = IPv4Address.Parse(input);
var networkAddress = IPv4Network.FromAddress(hostAddress,prefixLenght);
Console.WriteLine(networkAddress);