using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PacketRacer;

public class Cable : MonoBehaviour
{
    // Cable draw stuff
    public GameObject startObject;
    public GameObject endObject;
    LineRenderer lr;

    /// Stack of packets
    // First always has the top priority of being forwarded.
    Stack<Packet> packet_stack = new Stack<Packet>();

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Draw the cable
        Vector3 startPos = startObject.transform.position;
        Vector3 endPos = endObject.transform.position;

        lr.positionCount = 2;
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, endPos);
        lr.useWorldSpace = true;
    }
}
