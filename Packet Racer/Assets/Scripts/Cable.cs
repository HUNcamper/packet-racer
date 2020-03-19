using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PacketRacer;
using System;

public class Cable : MonoBehaviour
{
    public Router startRouter;
    public Router endRouter;

    private NetInterface startInterface = null;
    private NetInterface endInterface = null;

    public bool ready = false;

    // For rendering the cable in 3D
    LineRenderer lr = null;

    /// Stack of packets
    // First always has the top priority of being forwarded.
    // TODO
    Stack<Packet> packet_stack = new Stack<Packet>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!ready)
        {
            // if both routers are ready, then the cable is ready
            if (startRouter.ready && endRouter.ready)
            {
                Ready();
            }
        }

        if (lr != null)
        {
            // Draw the cable
            Vector3 startPos = startRouter.gameObject.transform.position;
            Vector3 endPos = endRouter.gameObject.transform.position;

            lr.positionCount = 2;
            lr.SetPosition(0, startPos);
            lr.SetPosition(1, endPos);
            lr.useWorldSpace = true;
        }
    }

    // TODO: make it work when the interfaces are already used up
    /// <summary>
    /// Initiate to connect to NetInterface
    /// </summary>
    /// <param name="netInterface">NetInterface to connect to</param>
    public void ConnectInterface(NetInterface netInterface)
    {
        // If netint already connected, do nothing
        if (startInterface == netInterface || endInterface == netInterface) return;

        if (startInterface == null)
        {
            startInterface.CableConnected(this);
            return;
        }
        else if (endInterface == null)
        {
            endInterface.CableConnected(this);
            return;
        }

        Debug.LogWarning("No free interfaces! PLEASE FIX");
    }

    /// <summary>
    /// Initiate to disconnect from NetInterface
    /// </summary>
    /// <param name="netInterface">NetInterface to disconnect from</param>
    public void DisconnectInterface(NetInterface netInterface)
    {
        if (startInterface == netInterface)
        {
            startInterface.CableDisconnected();
            return;
        }
        else if (endInterface == netInterface)
        {
            endInterface.CableDisconnected();
            return;
        }

        Debug.Log("Tried to disconnect from interface that isn't connected?");
    }

    /// <summary>
    /// NetInterface initiated to disconnect
    /// </summary>
    /// <param name="netInterface">NetInterface calling the disconnection</param>
    public void InterfaceDisconnected(NetInterface netInterface)
    {
        if (startInterface == netInterface)
        {
            startInterface = null;
            return;
        }
        else if (endInterface == netInterface)
        {
            endInterface = null;
            return;
        }
    }

    public void Ready()
    {
        startInterface = startRouter.interfaceList[0];
        endInterface = endRouter.interfaceList[0];

        startInterface.CableConnected(this);
        endInterface.CableConnected(this);

        lr = GetComponent<LineRenderer>();

        Debug.Log(String.Format("Cable connected from {0} to {1}", startInterface.GetName(), endInterface.GetName()));

        ready = true;
    }

    public void HandlePacket(NetInterface sourceInterface, Packet packet)
    {
        if (sourceInterface == startInterface)
        {
            endInterface.ReceivePacket(packet);
        }
        else
        {
            startInterface.ReceivePacket(packet);
        }
    }
}
