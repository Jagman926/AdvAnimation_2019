using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlend_Avg : TestBlend
{
    [SerializeField]
    private Transform poseIdentity = null;
    [SerializeField]
    private List<Transform> posesList = null;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private List<float> weightsList = null;

    private List<SpatialPose> poseTransforms;

    void Start()
    {
        // Makes sure there is a weight for every pose and vice versa
        VerifyLists();
        poseTransforms = new List<SpatialPose>();
    }

    void Update()
    {
        VerifyLists();
        // For each pose added into pose list, lerp between identity and current pose by weight (acts as a parameter) and then save the resulting pose
        // and continue for the next pose in the list until the list is completed and all poses have been averaged in
        for (int i = 0; i < posesList.Count; i++)
            poseTransforms.Add(Scale(poseIdentity, posesList[i], weightsList[i]));

        for (int j = 0; j < posesList.Count; j++)
            pose_result = Add(pose_result, poseTransforms[j]);
    }

    private void VerifyLists()
    {
        if (posesList.Count > weightsList.Count)
            Debug.LogWarning("You are missing weights for your poses in TestBlend_Avg");
        else if (posesList.Count < weightsList.Count)
            Debug.LogWarning("You are missing poses for your weights in TestBlend_Avg");
    }

    private SpatialPose Scale(Transform poseIdentity, Transform pose1, float parameter)
    {
        SpatialPose poseTransform = new SpatialPose();

        // Translation: lteral linear interpolation
        poseTransform.localPosition = Vector3.Lerp(poseIdentity.localPosition, pose1.localPosition, parameter);

        // scale: ditto
        poseTransform.localScale = Vector3.Lerp(poseIdentity.localScale, pose1.localScale, parameter);

        // rotation: quaternion SLERP or Euler LERP
        if (usingQuaternionRotation)
            poseTransform.localRotation = Quaternion.Slerp(poseIdentity.localRotation, pose1.localRotation, parameter);
        else
            poseTransform.localEulerAngles = Vector3.Lerp(poseIdentity.localEulerAngles, pose1.localEulerAngles, parameter);

        return poseTransform;
    }

    private SpatialPose Add(Transform pose0, Transform pose1)
    {
        SpatialPose poseTransform = new SpatialPose();

        // add transforms
        poseTransform.position = pose0.position + pose1.position;
        // multiplication of scales
        poseTransform.localScale = pose0.localScale;
        poseTransform.localScale.Scale(pose1.localScale);
        // multiply quaternions
        if (usingQuaternionRotation)
            poseTransform.localRotation = pose0.localRotation * pose1.localRotation;
        else
            poseTransform.localEulerAngles = pose0.localEulerAngles + pose1.localEulerAngles;

        return poseTransform;
    }
}
