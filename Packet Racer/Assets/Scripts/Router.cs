using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PacketRacer;

public class Router : MonoBehaviour
{
    private IPv4Address address_ipv4;
    private List<NetInterface> interface_list = new List<NetInterface>();

    // Start is called before the first frame update
    void Start()
    {
        interface_list.Add(new NetInterface(gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandlePacket(Packet packet)
    {

    }
}
