﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PacketRacer;
using TMPro;
using System;

public class Workstation : MonoBehaviour, IDevice
{
    /// <summary>
    /// Name label above the router
    /// </summary>
    public TextMeshPro labelName;

    /// <summary>
    /// Device's display name
    /// </summary>
    public string displayName = "router";

    /// <summary>
    /// List of string based IPs as interfaces
    /// </summary>
    public List<string> interfaceIpList = new List<string>();

    /// <summary>
    /// List of interfaces on the device
    /// </summary>
    public List<NetInterface> interfaceList = new List<NetInterface>();

    public bool ready = false;

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

        Ready();
    }

    /// <summary>
    /// Tells the router that it's ready and is properly deployed.
    /// </summary>
    public void Ready()
    {
        foreach (NetInterface netInterface in interfaceList)
        {
            netInterface.Ready();

            Debug.Log(String.Format("Set up interface for {0}", netInterface.GetIPAddress()));
        }

        ready = true;
    }


    /// <summary>
    /// Changes the name of the router
    /// </summary>
    /// <param name="name">Name to change to</param>
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

    /// <summary>
    /// Sends a packet with a "ping" message
    /// </summary>
    /// <param name="ip">Destination IPv4</param>
    public void SendPingPacket(string ip)
    {
        NetInterface firstInterface = interfaceList[0];

        // Assemble ping packet
        IPv4Address dest = new IPv4Address(ip);
        Packet newPacket = new Packet(firstInterface.GetIPAddress(), dest, "ping");

        SendPacket(newPacket);
    }

    /// <summary>
    /// Sends a packet through the router's first NetInterface
    /// </summary>
    /// <param name="packet">Packet</param>
    public void SendPacket(Packet packet)
    {
        NetInterface firstInterface = interfaceList[0];

        // Finally send the packet to the interface
        firstInterface.SendPacket(packet);
    }

    /// <summary>
    /// Receive a packet and handle it
    /// </summary>
    /// <param name="sourceInterface">Interface the packet is coming from</param>
    /// <param name="packet">Packet</param>
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
