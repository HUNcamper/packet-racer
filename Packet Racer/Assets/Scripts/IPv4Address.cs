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

        public override string ToString()
        {
            string[] octetString = new string[4];

            for (int i = 0; i < octetString.Length; i++)
            {
                octetString[i] = ((int)octets[i]).ToString();
            }

            return String.Join(".", octetString);
        }

        public static byte[] IPStringToOctets(string ipv4, char separator = '.')
        {
            byte[] new_octets = new byte[4];

            // Slice the ipv4 string
            string[] ip_split = ipv4.Split(separator);

            // Has to have exactly 4 octets
            if (ip_split.Length != 4)
            {
                throw new IPv4Exception(String.Format("IPv4 error: IP string does not have 4 octets"));
            }

            for (int i = 0; i < ip_split.Length; i++)
            {
                string octetStr = ip_split[i];
                byte octet;

                try
                {
                    octet = (byte)Convert.ToInt32(octetStr);
                }
                catch
                {
                    throw new IPv4Exception(String.Format("IPv4 error: octet '{0}' is more than one byte", octetStr));
                }

                new_octets[i] = octet;
            }

            return new_octets;
        }
    }
}