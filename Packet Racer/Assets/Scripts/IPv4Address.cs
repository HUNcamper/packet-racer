using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacketRacer
{
    /// <summary>
    /// Struct for connecting Network - NetMask - Interface
    /// </summary>
    public struct IPv4InterfacePair
    {
        public IPv4Address network;
        public IPv4Address netMask;
        public string interfaceName;
    }

    /// <summary>
    /// Class for handling IPv4 addresses
    /// </summary>
    public class IPv4Address
    {
        private byte[] octets;

        /// <summary>
        /// Initializes an IPv4 address
        /// </summary>
        /// <param name="ip_octets">array of 4 octets</param>
        public IPv4Address(byte[] ip_octets)
        {
            octets = ip_octets;
        }

        /// <summary>
        /// Initializes an IPv4 address
        /// </summary>
        /// <param name="ipv4">String based IPv4 address</param>
        public IPv4Address(string ipv4)
        {
            octets = IPStringToOctets(ipv4);
        }

        /// <summary>
        /// Convert an IPv4 class to a readable string format
        /// </summary>
        /// <returns>IPv4 address in a string format</returns>
        public override string ToString()
        {
            string[] octetString = new string[4];

            for (int i = 0; i < octetString.Length; i++)
            {
                octetString[i] = ((int)octets[i]).ToString();
            }

            return String.Join(".", octetString);
        }

        /// <summary>
        /// Convert a string based IPv4 address to an octet based one
        /// </summary>
        /// <param name="ipv4">IPv4 in string format</param>
        /// <param name="separator">Separator used to separate octets</param>
        /// <returns>IPv4 class</returns>
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