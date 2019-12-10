using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardKeyMapping : MonoBehaviour
{
    // Input Manager
    Managers.s_InputManager IM;

    // Keyboard Objects
    // Keyboard Object FBX is provided by Ben Throop 
    // https://github.com/benthroop/KeyboardJamStart
    [SerializeField]
    GameObject keyboardObject;
    [SerializeField]
    List<GameObject> KeyObjectList;
    [HideInInspector]
    public List<KeyCode> KeyCodeList;

    // Keycode/Keylist pair
    List<(KeyCode, GameObject)> KeyCodeObjectPair;

    void Awake()
    {
        // Init Input Manager
        IM = Managers.s_InputManager.Instance;
        KeyCodeList = new List<KeyCode>();
        IM.InitLists();
        InitListeners();
    }

    void Start()
    {
        // Init Key List
        InitKeyboard();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateKeyboard();
    }

    // Init
    //*----------------------------------------------------*/
    void InitKeyboard()
    {
        KeyCodeObjectPair = new List<(KeyCode, GameObject)>();
        for (int i = 0; i < KeyCodeList.Count; i++)
        {
            (KeyCode, GameObject) pair = (KeyCodeList[i], KeyObjectList[i]);
            KeyCodeObjectPair.Add(pair);
        }
    }

    // Update
    //*----------------------------------------------------*/
    void UpdateKeyboard()
    {
        for (int i = 0; i < KeyCodeObjectPair.Count; i++)
        {
            // If key is down
            if (IM.GetKey(KeyCodeObjectPair[i].Item1))
            {
                KeyCodeObjectPair[i].Item2.GetComponent<Renderer>().material.color = Color.magenta;
            }
            // If key is up
            else
            {
                KeyCodeObjectPair[i].Item2.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }

    //Init Listeners
    //*----------------------------------------------------*/
    void InitListeners()
    {
        // Add every key used on BenThroop-StarterKeyboard_FBX2013 model 
        // *** This order is same order that keys are loaded into KeyObjectList
        KeyCodeList.Add(KeyCode.A);
        KeyCodeList.Add(KeyCode.Alpha0);
        KeyCodeList.Add(KeyCode.Alpha1);
        KeyCodeList.Add(KeyCode.Alpha2);
        KeyCodeList.Add(KeyCode.Alpha3);
        KeyCodeList.Add(KeyCode.Alpha4);
        KeyCodeList.Add(KeyCode.Alpha5);
        KeyCodeList.Add(KeyCode.Alpha6);
        KeyCodeList.Add(KeyCode.Alpha7);
        KeyCodeList.Add(KeyCode.Alpha8);
        KeyCodeList.Add(KeyCode.Alpha9);
        KeyCodeList.Add(KeyCode.B);
        KeyCodeList.Add(KeyCode.Backslash);
        KeyCodeList.Add(KeyCode.Backspace);
        KeyCodeList.Add(KeyCode.C);
        KeyCodeList.Add(KeyCode.CapsLock);
        KeyCodeList.Add(KeyCode.Comma);
        KeyCodeList.Add(KeyCode.D);
        KeyCodeList.Add(KeyCode.Delete);
        KeyCodeList.Add(KeyCode.DownArrow);
        KeyCodeList.Add(KeyCode.E);
        KeyCodeList.Add(KeyCode.End);
        KeyCodeList.Add(KeyCode.Equals);
        KeyCodeList.Add(KeyCode.Escape);
        KeyCodeList.Add(KeyCode.F);
        KeyCodeList.Add(KeyCode.F1);
        KeyCodeList.Add(KeyCode.F2);
        KeyCodeList.Add(KeyCode.F3);
        KeyCodeList.Add(KeyCode.F4);
        KeyCodeList.Add(KeyCode.F5);
        KeyCodeList.Add(KeyCode.F6);
        KeyCodeList.Add(KeyCode.F7);
        KeyCodeList.Add(KeyCode.F8);
        KeyCodeList.Add(KeyCode.F9);
        KeyCodeList.Add(KeyCode.F10);
        KeyCodeList.Add(KeyCode.F11);
        KeyCodeList.Add(KeyCode.F12);
        KeyCodeList.Add(KeyCode.G);
        KeyCodeList.Add(KeyCode.H);
        KeyCodeList.Add(KeyCode.Home);
        KeyCodeList.Add(KeyCode.I);
        KeyCodeList.Add(KeyCode.Insert);
        KeyCodeList.Add(KeyCode.J);
        KeyCodeList.Add(KeyCode.K);
        KeyCodeList.Add(KeyCode.L);
        KeyCodeList.Add(KeyCode.LeftAlt);
        KeyCodeList.Add(KeyCode.LeftArrow);
        KeyCodeList.Add(KeyCode.LeftBracket);
        KeyCodeList.Add(KeyCode.LeftControl);
        KeyCodeList.Add(KeyCode.LeftShift);
        KeyCodeList.Add(KeyCode.LeftWindows);
        KeyCodeList.Add(KeyCode.M);
        KeyCodeList.Add(KeyCode.Minus);
        KeyCodeList.Add(KeyCode.N);
        KeyCodeList.Add(KeyCode.O);
        KeyCodeList.Add(KeyCode.P);
        KeyCodeList.Add(KeyCode.PageDown);
        KeyCodeList.Add(KeyCode.PageUp);
        KeyCodeList.Add(KeyCode.Pause);
        KeyCodeList.Add(KeyCode.Period);
        KeyCodeList.Add(KeyCode.Print);
        KeyCodeList.Add(KeyCode.Q);
        KeyCodeList.Add(KeyCode.Quote);
        KeyCodeList.Add(KeyCode.R);
        KeyCodeList.Add(KeyCode.Return);
        KeyCodeList.Add(KeyCode.RightAlt);
        KeyCodeList.Add(KeyCode.RightArrow);
        KeyCodeList.Add(KeyCode.RightBracket);
        KeyCodeList.Add(KeyCode.RightCommand);
        KeyCodeList.Add(KeyCode.RightControl);
        KeyCodeList.Add(KeyCode.RightShift);
        KeyCodeList.Add(KeyCode.RightWindows);
        KeyCodeList.Add(KeyCode.S);
        KeyCodeList.Add(KeyCode.ScrollLock);
        KeyCodeList.Add(KeyCode.Semicolon);
        KeyCodeList.Add(KeyCode.Slash);
        KeyCodeList.Add(KeyCode.Space);
        KeyCodeList.Add(KeyCode.T);
        KeyCodeList.Add(KeyCode.Tab);
        KeyCodeList.Add(KeyCode.Tilde);
        KeyCodeList.Add(KeyCode.U);
        KeyCodeList.Add(KeyCode.UpArrow);
        KeyCodeList.Add(KeyCode.V);
        KeyCodeList.Add(KeyCode.W);
        KeyCodeList.Add(KeyCode.X);
        KeyCodeList.Add(KeyCode.Y);
        KeyCodeList.Add(KeyCode.Z);

        for(int i = 0; i < KeyCodeList.Count; i++)
        {
            IM.StartInputListen(KeyCodeList[i]);
        }
    }
}