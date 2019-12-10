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
        List<AxisInput> axisListeners;
        List<KeyInput> keyListeners;

        AxisInput[] inputGetAxis;
        KeyInput[] inputGetKey;

        // Update input
        public bool updateInput;

        void Awake()
        {
            InitInputList();
            InitAxisList();
        }

        void Start()
        {
            inputGetAxis = axisListeners.ToArray();
            inputGetKey = keyListeners.ToArray();
            updateInput = true;
        }

        void Update()
        {
            if (updateInput)
            {
                UpdateKeyFromInput();
                UpdateAxisFromInput();
            }
        }

        // Init 
        //*----------------------------------------------------*/

        void InitInputList()
        {
            keyListeners = new List<KeyInput>();
        }

        void InitAxisList()
        {
            axisListeners = new List<AxisInput>();
        }

        public void StartInputListen(KeyCode keyCode)
        {
            // Init new KeyInput
            KeyInput key = new KeyInput(keyCode);
            // Add to listener
            keyListeners.Add(key);
        }

        void StartAxisListen(string axisName)
        {
            // Init new AxisInput
            AxisInput axis = new AxisInput(axisName);
            // Add to listener
            axisListeners.Add(axis);
        }

        // Updates
        //*----------------------------------------------------*/

        void UpdateKeyFromInput()
        {
            // For each key being tracked
            for (int i = 0; i < inputGetKey.Length; i++)
            {
                // Update the GetKey bool
                inputGetKey[i].SetKey(Input.GetKey(inputGetKey[i].key));
            }
        }

        void UpdateAxisFromInput()
        {
            // For each axis being tracked
            for (int i = 0; i < inputGetAxis.Length; i++)
            {
                // Update the GetKey bool
                inputGetAxis[i].SetValue(Input.GetAxisRaw(inputGetAxis[i].axis));
            }
        }

        // Getters/Setters
        //*----------------------------------------------------*/

        public bool GetKey(KeyCode key)
        {
            // Loop through list of listeners until keycode is found
            for (int i = 0; i < inputGetKey.Length; i++)
            {
                // When found, return it's down state
                if (inputGetKey[i].key == key)
                    return inputGetKey[i].isDown;
            }
            // If not found, throw error and return false
            Debug.LogError("Keycode " + key.ToString() + " is not being tracked in s_InputManager. " +
                            "Add a listener for the Keycode using StartInputListen() in s_InputManager");
            return false;
        }

        public void SetKey(KeyCode key, bool value)
        {
            bool done = false;
            // Loop through list of listeners until keycode is found
            for (int i = 0; i < inputGetKey.Length; i++)
            {
                // When found, return it's down state
                if (inputGetKey[i].key == key)
                {
                    inputGetKey[i].isDown = value;
                    done = true;
                }
            }
            if (!done)
            {
                // If not found, throw error and return false
                Debug.LogError("KeyCode " + key.ToString() + " is not being tracked in s_InputManager. " +
                                "Add a listener for the Keycode using StartInputListen() in s_InputManager");
            }
        }

        public void SetKeyAtIndex(int i, bool value)
        {
            inputGetKey[i].isDown = value;
        }

        public KeyInput[] GetKeyArray()
        {
            return inputGetKey;
        }

        public KeyInput GetKeyAtIndex(int i)
        {
            return inputGetKey[i];
        }

        public int GetKeyArrayLength()
        {
            return inputGetKey.Length;
        }
    }
}
