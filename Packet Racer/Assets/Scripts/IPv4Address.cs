using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacketRacer
{
    public struct IPv4InterfacePair
    {
        public IPv4Address network;
        public IPv4Address netMask;
        public string interfaceName;
    }

    public class IPv4Address
    {
        private byte[] octets;

        public IPv4Address(byte[] ip_octets)
        {
            octets = ip_octets;
        }

        public IPv4Address(string ipv4)
        {
            octets = IPStringToOctets(ipv4);
        }

        public static byte[] IPStringToOctets(string ipv4, char separator = '.')
        {
            byte[] new_octets = new byte[4];

            // Splice the ipv4 string
            string[] ip_split = ipv4.Split(separator);

            // Has to have exactly 4 octets
            if (ip_split.Length != 4) return null;

            foreach (string octet_str in ip_split)
            {
                byte octet;
                try
                {
                    octet = Convert.ToByte(octet_str);
                }
                catch
                {
                    Debug.LogError("IPv4 error: octet '{0}' is more than one byte");
                    return null;
                }
            }

            return new_octets;
        }
    }
}