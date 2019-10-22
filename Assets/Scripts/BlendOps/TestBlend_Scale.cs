using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlend_Scale : TestBlend
{
    [SerializeField]
    private Transform pose1 = null;

    [Range(0.0f, 1.0f)]
    public float paramater = 1.0f;

    void Update()
    {
        // LOGIC: same as lerp operation as if pose0 is poseIdentity
    }
}
