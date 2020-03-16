using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacketRacer
{
    public class NetInterface
    {
        public GameObject parent;
        private string type;
        private int num;
        private bool state;
        Cable cable;

        public NetInterface(GameObject parentObj, string intType = "g", int intNum = 0)
        {
            parent = parentObj;
            type = intType;
            num = intNum;
            state = false;
        }

        public string GetName()
        {
            return String.Format("{0}{1}", type, num);
        }

        public bool GetState()
        {
            return state;
        }
    }
}