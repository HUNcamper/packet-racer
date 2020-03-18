using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacketRacer
{
    public class IPv4Exception : Exception
    {
        public IPv4Exception()
        {
            
        }

        public IPv4Exception(string message) : base(message)
        {

        }

        public IPv4Exception(string message, Exception inner) : base(message, inner)
        {

        }
    }
    public class IPv4InterfaceException : Exception
    {
        public IPv4InterfaceException()
        {
            
        }

        public IPv4InterfaceException(string message) : base(message)
        {

        }

        public IPv4InterfaceException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
