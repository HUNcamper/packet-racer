using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PacketRacer;

public class EndDevice : MonoBehaviour, IDevice
{
    private NetInterface netInterface;
    private IPv4Address defaultDevice;

    public void ReceivePacket(NetInterface sourceInterface, Packet packet)
    {
        throw new System.NotImplementedException();
    }

    public void SendPacket(Packet packet)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Sends a packet with a "ping" message
    /// </summary>
    /// <param name="destination">Destination IPv4</param>
    private void SendPingPacket(IPv4Address destination)
    {
        IPv4Address source = netInterface.GetIPAddress();

        netInterface.SendPacket(new Packet(source, destination));
    }
}
