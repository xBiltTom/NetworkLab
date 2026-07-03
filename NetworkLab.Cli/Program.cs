using NetworkLab.Core.Models;

byte prefix = 27;

var mask = IPv4Mask.FromPrefix(prefix);
Console.WriteLine(mask);