using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacketRacer
{
    public class NetInterface
    {
        public IDevice parentDevice;
        public bool ready = false;
        private string type;
        private int num;
        private bool state;
        Cable cable;

        private MACAddress mac;
        private IPv4Address address_ipv4;

        public NetInterface(IDevice device, int intNum, string intType = "g")
        {
            parentDevice = device;
            type = intType;
            num = intNum;
            state = false;
            mac = new MACAddress();
            address_ipv4 = new IPv4Address("0.0.0.0");
        }

        public void Ready()
        {
            if (cable != null) cable.Ready();

            ready = true;
        }

        /// <summary>
        /// Cable initiated to connect
        /// </summary>
        /// <param name="newCable">Cable calling the connection</param>
        public void CableConnected(Cable newCable)
        {
            if (cable == null)
            {
                cable = newCable;
            }
            else
            {
                // A cable is already connected, disconnect it
                cable.InterfaceDisconnected(this);

                // and connect the new one
                cable = newCable;
                cable.Ready();
            }
        }

        /// <summary>
        /// Cable initiated to disconnect
        /// </summary>
        /// <param name="newCable">Cable calling the disconnection</param>
        public void CableDisconnected()
        {
            cable = null;
        }

        public string GetName()
        {
            return String.Format("{0}{1}", type, num);
        }

        public bool GetState()
        {
            return state;
        }

        public IPv4Address GetIPAddress()
        {
            return address_ipv4;
        }

        public void SetIPAddress(string ip)
        {
            address_ipv4 = new IPv4Address(ip);
        }

        public void SendPacket(Packet packet)
        {
            cable.HandlePacket(this, packet);
        }

        public void ReceivePacket(Packet packet)
        {
            parentDevice.ReceivePacket(this, packet);
        }
    }
}