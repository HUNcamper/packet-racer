using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;


namespace PacketRacer
{
    public class MACAddress
    {
        private string address;
        
        //Generates a valid MAC address
        public MACAddress()
        {
            StringBuilder temp = new StringBuilder();
            System.Random random = new System.Random();
            for (int i = 1; i < 13; i++)
            {
                temp.Append(random.Next(0, 16).ToString("X"));
                if (i % 2 == 0 && i != 12)
                {
                    temp.Append(":");
                }
            }
            address = temp.ToString();
        }
    }

}

