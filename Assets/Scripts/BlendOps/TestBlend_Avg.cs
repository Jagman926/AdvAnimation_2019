using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlend_Avg : TestBlend
{
    [SerializeField]
    private Transform poseIdentity;
    [SerializeField]
    private List<Transform> posesList;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private List<float> weightsList;

    void Start()
    {
        // Makes sure there is a weight for every pose and vice versa
        VerifyLists();
    }

    void Update()
    {
        // For each pose added into pose list, lerp between identity and current pose by weight (acts as a parameter) and then save the resulting pose
        // and continue for the next pose in the list until the list is completed and all poses have been averaged in
    }

    private void VerifyLists()
    {
        if (posesList.Count > weightsList.Count)
            Debug.LogWarning("You are missing weights for your poses in TestBlend_Avg");
        else if (posesList.Count < weightsList.Count)
            Debug.LogWarning("You are missing poses for your weights in TestBlend_Avg");
    }
}
