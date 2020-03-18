using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacketRacer
{
    public interface IDevice
    {
        public void SendPacket(Packet packet);
        public void ReceivePacket(Packet packet);
    }
}