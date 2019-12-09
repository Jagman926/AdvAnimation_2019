using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class s_InputManager : s_Singleton<s_InputManager>
    {

        // Axis Input Struct
        //*----------------------------------------------------*/

        public struct AxisInput
        {
            // Variables
            public string axis;
            public float value;

            // Constructor
            public AxisInput(string axisName)
            {
                axis = axisName;
                value = 0.0f;
            }

            public void SetValue(float v)
            {
                value = v;
            }
        }

        // Key Input Struct
        //*----------------------------------------------------*/
        public struct KeyInput
        {
            // Variables
            public KeyCode key;
            public bool isDown;

            // Constructor
            public KeyInput(KeyCode k)
            {
                key = k;
                isDown = false;
            }

            public void SetKey(bool down)
            {
                isDown = down;
            }
        }

        // input lists ~Listeners
        List<AxisInput> inputGetAxis;
        List<KeyInput> inputGetKey;

        void Awake()
        {
            InitInputList();
            InitAxisList();
        }

        void Start()
        {
            StartAxisListen("Horizontal");
            StartAxisListen("Vertical");
            StartInputListen(KeyCode.Q);
            StartInputListen(KeyCode.W);
            StartInputListen(KeyCode.E);
            StartInputListen(KeyCode.R);
        }

        void Update()
        {
            UpdateKeyFromInput();
            UpdateAxisFromInput();
        }

        // Init 
        //*----------------------------------------------------*/

        void InitInputList()
        {
            inputGetKey = new List<KeyInput>();
        }

        void InitAxisList()
        {
            inputGetAxis = new List<AxisInput>();
        }

        void StartInputListen(KeyCode keyCode)
        {
            // Init new KeyInput
            KeyInput key = new KeyInput(keyCode);
            // Add to listener
            inputGetKey.Add(key);
        }

        void StartAxisListen(string axisName)
        {
            // Init new AxisInput
            AxisInput axis = new AxisInput(axisName);
            // Add to listener
            inputGetAxis.Add(axis);
        }

        // Updates
        //*----------------------------------------------------*/

        void UpdateKeyFromInput()
        {
            // For each key being tracked
            for (int i = 0; i < inputGetKey.Count; i++)
            {
                // Update the GetKey bool
                inputGetKey[i].SetKey(Input.GetKey(inputGetKey[i].key));
            }
        }

        void UpdateAxisFromInput()
        {
            // For each axis being tracked
            for (int i = 0; i < inputGetAxis.Count; i++)
            {
                // Update the GetKey bool
                inputGetAxis[i].SetValue(Input.GetAxisRaw(inputGetAxis[i].axis));
            }
        }
    }
}
