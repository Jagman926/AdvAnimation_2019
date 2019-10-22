using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlend_Add : TestBlend
{
    [SerializeField]
    private Transform pose0 = null;
    [SerializeField]
    private Transform pose1 = null;

    void Update()
    {
        // add transforms
        pose_result.position = pose0.position + pose1.position;
        // multiplication of scales
        pose_result.localScale = pose0.localScale;
        pose_result.localScale.Scale(pose1.localScale);
        // multiply quaternions
        if (usingQuaternionRotation)
            pose_result.localRotation = pose0.localRotation * pose1.localRotation;
        else
            pose_result.localEulerAngles = pose0.localEulerAngles + pose1.localEulerAngles;
    }
}
