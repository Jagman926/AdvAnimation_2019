using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlend_Lerp : TestBlend
{
    [SerializeField]
    private Transform pose0 = null;
    [SerializeField]
    private Transform pose1 = null;

    [Range(0.0f, 1.0f)]
    public float parameter = 0.0f;

    void Update()
    {
        // Translation: lteral linear interpolation
        pose_result.localPosition = Vector3.Lerp(pose0.localPosition, pose1.localPosition, parameter);

        // scale: ditto
        pose_result.localScale = Vector3.Lerp(pose0.localScale, pose1.localScale, parameter);

        // rotation: quaternion SLERP or Euler LERP
        if (usingQuaternionRotation)
            pose_result.localRotation = Quaternion.Slerp(pose0.localRotation, pose1.localRotation, parameter);
        else
            pose_result.localEulerAngles = Vector3.Lerp(pose0.localEulerAngles, pose1.localEulerAngles, parameter);
    }
}
