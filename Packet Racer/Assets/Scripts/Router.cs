using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PacketRacer;
using TMPro;
using System;

public class Router : MonoBehaviour, IDevice
{
    public TextMeshPro labelName;

    public List<string> routingTableString = new List<string>();

    // Device's display name
    public string displayName = "router";

    // List of string based IPs as interfaces
    public List<string> interfaceIpList = new List<string>();

    // List of interfaces on the device
    public List<NetInterface> interfaceList = new List<NetInterface>();

    // Basic routing table
    private List<IPv4InterfacePair> basicRoutingTable = new List<IPv4InterfacePair>();

    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        SetName(displayName);
        
        int counter = 0;

        foreach (string item in interfaceIpList)
        {
            Debug.Log("Processing " + item);
            NetInterface interfaceTemp = new NetInterface(this, counter);

            interfaceTemp.SetIPAddress(item);

            interfaceList.Add(interfaceTemp);
            counter++;
        }
    }

    /// <summary>
    /// Fills up the routing table
    /// </summary>
    void FillRoutingTable()
    {
        // network;netmask;interface
        for (int i = 0; i < routingTableString.Count; i++)
        {
            string[] line = routingTableString[i].Split(';');
            IPv4Address network;
            IPv4Address netmask;
            string intName = "";

            try
            {
                network = new IPv4Address(line[0]);
                netmask = new IPv4Address(line[1]);
                intName = line[2];
                
                IPv4InterfacePair pair = new IPv4InterfacePair();
                pair.network = network;
                pair.netMask = netmask;
                pair.interfaceName = intName;
            }
            catch(IPv4Exception e)
            {
                Debug.LogError(String.Format("IPv4 error reading routing table line: '{0}'", routingTableString[i]));
                Debug.LogError(e.ToString());
            }
            catch (IPv4InterfaceException e)
            {
                Debug.LogError(String.Format("InterfacePair error reading routing table line: '{0}'", routingTableString[i]));
                Debug.LogError(e.ToString());
            }
        }
    }

    public void SetName(string name)
    {
        labelName.text = name;
        displayName = name;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the label towards the camera
        labelName.transform.LookAt(mainCamera.transform.position);
    }

    public void SendPingPacket(string ip)
    {
        NetInterface firstInterface = interfaceList[0];

        // Assemble ping packet
        IPv4Address dest = new IPv4Address(ip);
        Packet newPacket = new Packet(firstInterface.GetIPAddress(), dest, "ping");

        SendPacket(newPacket);
    }

    public void SendPacket(Packet packet)
    {
        NetInterface firstInterface = interfaceList[0];

        // Finally send the packet to the interface
        firstInterface.SendPacket(packet);
    }

    public void ReceivePacket(NetInterface sourceInterface, Packet packet)
    {
        IPv4Address interfaceIP = sourceInterface.GetIPAddress();

        if (packet.message == "ping")
        {
            // Assemble pong packet
            IPv4Address dest = packet.source_ipv4;
            Packet newPacket = new Packet(interfaceIP, dest, "pong");

            // Finally send the packet to the interface
            sourceInterface.SendPacket(newPacket);
        }
        else if (packet.message == "pong")
        {
            Debug.Log(String.Format("[{0}]SUCCESS! Received pong packet from {1}", interfaceIP, packet.source_ipv4));
        }
    }
}
