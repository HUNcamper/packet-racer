using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacketRacer
{
    public interface IDevice
    {
        void SendPacket(Packet packet);
        void ReceivePacket(NetInterface sourceInterface, Packet packet);
    }
}