using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PacketRacer;
using TMPro;

public class Router : MonoBehaviour
{
    public TextMeshPro labelName;
    Camera mainCamera;

    public string ipv4Address;

    private IPv4Address address_ipv4;
    private List<NetInterface> interface_list = new List<NetInterface>();

    public string displayName = "router";

    // Start is called before the first frame update
    void Start()
    {
        interface_list.Add(new NetInterface(gameObject));
        mainCamera = Camera.main;
        SetName(displayName);

        address_ipv4 = new IPv4Address(ipv4Address);
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
