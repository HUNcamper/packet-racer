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

    public List<string> interfacelistiplist = new List<string>();

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

        foreach (string item in interfacelistiplist)
        {
            NetInterface interfacetemp = new NetInterface(gameObject, counter);

            interfacetemp.SetIPAddress(item);

            interfaceList.Add(interfacetemp);
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
            IPv4Address network = null;
            IPv4Address netmask = null;
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
        labelName.transform.LookAt(mainCamera.transform.position);
    }

    public void SendPacket(Packet packet)
    {

    }

    public void ReceivePacket(Packet packet)
    {

    }
}
