using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PacketRacer;

public class Cable : MonoBehaviour
{
    // Cable draw stuff
    public Router startRouter;
    public Router endRouter;

    private NetInterface startInterface;

    private NetInterface endInterface;
    LineRenderer lr;

    /// Stack of packets
    // First always has the top priority of being forwarded.
    Stack<Packet> packet_stack = new Stack<Packet>();

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        startInterface = startRouter.interfaceList[0];
        endInterface = endRouter.interfaceList[0];
    }

    // Update is called once per frame
    void Update()
    {
        // Draw the cable
        Vector3 startPos = startRouter.GetGameObject().transform.position;
        Vector3 endPos = endRouter.GetGameObject().transform.position;

        lr.positionCount = 2;
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, endPos);
        lr.useWorldSpace = true;
    }

    public void ProcessPacket(GameObject gameObject)
    {

    }
}
