using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacketRacer
{
    public class NetInterface
    {
        public GameObject parent;

        public NetInterface(GameObject parentObj)
        {
            parent = parentObj;
        }
    }
}