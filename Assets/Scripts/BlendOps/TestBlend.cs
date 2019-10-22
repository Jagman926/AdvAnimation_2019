using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlend : MonoBehaviour
{
    protected Transform pose_result = null;

    public bool usingQuaternionRotation = false;

    void Start()
    {
        pose_result = this.gameObject.transform;   
    }
}
