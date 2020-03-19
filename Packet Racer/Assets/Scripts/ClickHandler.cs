using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PacketRacer;

public class ClickHandler : MonoBehaviour
{
    // Next click on a device will be the end point for a ping
    private bool pingEnd = false;

    // Start the ping from this device
    private Router pingStartDevice = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked!");
            // Ping tool is selected
            if (Tools.selectedTool == 2)
            {
                Collider rayTrace = Raytrace();

                if (rayTrace.tag == "Device")
                {
                    // First click on a device
                    if (!pingEnd)
                    {
                        pingStartDevice = rayTrace.gameObject.GetComponent<Router>();
                        pingEnd = true;
                    }
                    else
                    {
                        Router pingEndDevice = rayTrace.gameObject.GetComponent<Router>();

                        NetInterface startInterface = pingStartDevice.interfaceList[0];
                        NetInterface endInterface = pingEndDevice.interfaceList[0];

                        Packet pingPacket = new Packet(startInterface.GetIPAddress(), endInterface.GetIPAddress());

                        startInterface.SendPacket(pingPacket);

                        pingStartDevice = null;
                        pingEnd = false;
                    }
                }
            }
        }
    }

    Collider Raytrace()
    {
        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider;
        }

        return null;
    }
}
