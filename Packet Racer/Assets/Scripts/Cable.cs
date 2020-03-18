using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PacketRacer;

public class Cable : MonoBehaviour
{
    public Router startRouter;
    public Router endRouter;

    private NetInterface startInterface;
    private NetInterface endInterface;

    // For rendering the cable in 3D
    LineRenderer lr;

    /// Stack of packets
    // First always has the top priority of being forwarded.
    // TODO
    Stack<Packet> packet_stack = new Stack<Packet>();

    // Start is called before the first frame update
    void Start()
    {
        startInterface = startRouter.interfaceList[0];
        endInterface = endRouter.interfaceList[0];

        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Draw the cable
        Vector3 startPos = startRouter.gameObject.transform.position;
        Vector3 endPos = endRouter.gameObject.transform.position;

        lr.positionCount = 2;
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, endPos);
        lr.useWorldSpace = true;
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
