using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacketRacer
{
    public class Packet
    {
        /// <summary>
        /// Source IPv4 address
        /// </summary>
        public IPv4Address source_ipv4;

        /// <summary>
        /// Destination IPv4 address
        /// </summary>
        public IPv4Address dest_ipv4;

        // TODO: actual data for packet message
        /// <summary>
        /// Message in the packet
        /// </summary>
        public string message = "";

        public Packet()
        {

        }

        /// <summary>
        /// Initializes a Packet
        /// </summary>
        /// <param name="source_ip">Source IP in the packet</param>
        /// <param name="dest_ip">Destination IP in the packet</param>
        /// <param name="packetMessage">Message of the packet</param>
        public Packet(string source_ip, string dest_ip, string packetMessage = "ping")
        {
            source_ipv4 = new IPv4Address(source_ip);
            dest_ipv4 = new IPv4Address(dest_ip);

            message = packetMessage;
        }

        /// <summary>
        /// Initializes a Packet
        /// </summary>
        /// <param name="source_ip">Source IP in the packet</param>
        /// <param name="dest_ip">Destination IP in the packet</param>
        /// <param name="packetMessage">Message of the packet</param>
        public Packet(IPv4Address source_ip, IPv4Address dest_ip, string packetMessage = "ping")
        {
            source_ipv4 = source_ip;
            dest_ipv4 = dest_ip;

            message = packetMessage;
        }
    }
}
