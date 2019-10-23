using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlend_Scale : TestBlend
{
    [SerializeField]
    private Transform poseIdentity = null;
    [SerializeField]
    private Transform pose1 = null;

    [Range(0.0f, 1.0f)]
    public float parameter = 0.0f;

    void Update()
    {
        // Translation: lteral linear interpolation
        pose_result.localPosition = Vector3.Lerp(poseIdentity.localPosition, pose1.localPosition, parameter);

        // scale: ditto
        pose_result.localScale = Vector3.Lerp(poseIdentity.localScale, pose1.localScale, parameter);

        // rotation: quaternion SLERP or Euler LERP
        if (usingQuaternionRotation)
            pose_result.localRotation = Quaternion.Slerp(poseIdentity.localRotation, pose1.localRotation, parameter);
        else
            pose_result.localEulerAngles = Vector3.Lerp(poseIdentity.localEulerAngles, pose1.localEulerAngles, parameter);
    }
}
