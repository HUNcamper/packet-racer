using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PacketRacer
{
    public static class Tools
    {
        public static int selectedTool = 0;
    }

    public class ToolSelectDropdown : MonoBehaviour
    {
        public TMP_Dropdown dropdown;

        public void Dropdown_IndexChanged(int index)
        {
            Tools.selectedTool = index;
            Debug.Log(string.Format("Changed to {0}", index));
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
