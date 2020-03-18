using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacketRacer
{
    public class Packet
    {
        // Source IPv4
        public IPv4Address source_ipv4;

        // Destination IPv4
        public IPv4Address dest_ipv4;

        public string message = "";

        public Packet()
        {

        }

        public Packet(string source_ip, string dest_ip, string packetMessage = "ping")
        {
            source_ipv4 = new IPv4Address(source_ip);
            dest_ipv4 = new IPv4Address(dest_ip);

            message = packetMessage;
        }

        public Packet(IPv4Address source_ip, IPv4Address dest_ip, string packetMessage = "ping")
        {
            source_ipv4 = source_ip;
            dest_ipv4 = dest_ip;

            message = packetMessage;
        }
    }
}
