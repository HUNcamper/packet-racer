using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacketRacer
{
    public class NetInterface
    {
        /// <summary>
        /// Parent device of the interface
        /// </summary>
        public IDevice parentDevice;

        /// <summary>
        /// Is the interface ready?
        /// </summary>
        public bool ready = false;

        private string type;
        private int num;
        private bool state;
        Cable cable;

        private MACAddress mac;
        private IPv4Address address_ipv4;

        /// <summary>
        /// Initializes a NetInterface
        /// </summary>
        /// <param name="device">Parent device</param>
        /// <param name="intNum">NetInterface index number</param>
        /// <param name="intType">NetInterface type</param>
        public NetInterface(IDevice device, int intNum, string intType = "g")
        {
            parentDevice = device;
            type = intType;
            num = intNum;
            state = false;
            mac = new MACAddress();
            address_ipv4 = new IPv4Address("0.0.0.0");
        }

        /// <summary>
        /// Tells the NetInterface that it's ready and is properly deployed.
        /// </summary>
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

        /// <summary>
        /// Get a string based name of the NetInterface in a [type]/[number] format
        /// </summary>
        /// <returns>Name of the interface</returns>
        public string GetName()
        {
            return String.Format("{0}{1}", type, num);
        }

        /// <summary>
        /// Checks if the NetInterface is turned on or off
        /// </summary>
        /// <returns>State of the NetInterface</returns>
        public bool GetState()
        {
            return state;
        }

        /// <summary>
        /// Get the IP address of the NetInterface
        /// </summary>
        /// <returns>IPv4 address</returns>
        public IPv4Address GetIPAddress()
        {
            return address_ipv4;
        }

        /// <summary>
        /// Set the IP address of the NetInterface
        /// </summary>
        public void SetIPAddress(string ip)
        {
            address_ipv4 = new IPv4Address(ip);
        }

        /// <summary>
        /// Send a Packet through this NetInterface
        /// </summary>
        /// <param name="packet">Packet</param>
        public void SendPacket(Packet packet)
        {
            cable.HandlePacket(this, packet);
        }


        /// <summary>
        /// Receive a Packet on this NetInterface
        /// </summary>
        /// <param name="packet">Packet</param>
        public void ReceivePacket(Packet packet)
        {
            parentDevice.ReceivePacket(this, packet);
        }
    }
}