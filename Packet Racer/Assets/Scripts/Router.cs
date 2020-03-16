using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PacketRacer;
using TMPro;
using System;

public class Router : MonoBehaviour
{
    public TextMeshPro labelName;

    public List<string> routingTableString = new List<string>();

    // String ipv4 address for editor set IP
    public string ipv4AddressString = "0.0.0.0";

    // Device's display name
    public string displayName = "router";

    // IPv4 Address of the device
    private IPv4Address address_ipv4;

    // List of interfaces on the device
    private List<NetInterface> interfaceList = new List<NetInterface>();

    // Basic routing table
    private List<IPv4InterfacePair> basicRoutingTable = new List<IPv4InterfacePair>();

    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        interfaceList.Add(new NetInterface(gameObject));
        mainCamera = Camera.main;
        SetName(displayName);

        address_ipv4 = new IPv4Address(ipv4AddressString);
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
            }
            catch(Ipv4Exception e)
            {
                Debug.LogError(String.Format("Error reading routing table line: '{0}'", routingTableString[i]));
                Debug.LogError(e.ToString());
            }
            catch (Ipv4InterfaceException e2)
            {

            }
            finally
            {
                IPv4InterfacePair pair = new IPv4InterfacePair();
                pair.network = network;
                pair.netMask = netmask;
                pair.interfaceName = intName;
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

    public void HandlePacket(Packet packet)
    {

    }
}
