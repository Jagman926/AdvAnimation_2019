using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_Record : MonoBehaviour
{
    // Managers
    Managers.s_RecordManager RM;

    // Rigidbody
    Rigidbody rb;

    // Transform Lists
    [Header("Transform Lists")]
    [SerializeField]
    private List<Vector3> positionList;
    [SerializeField]
    private List<Quaternion> rotationList;

    // Time Variable
    private float keyframeTime;
    private bool inPlayback;

    void Start()
    {
        // Set instance to Record Manager
        RM = Managers.s_RecordManager.Instance;
        Debug.Assert(RM);
        // Set Rigidbody
        rb = gameObject.GetComponent<Rigidbody>();
        // Set keyframeTime
        keyframeTime = 0.0f;
    }

    void FixedUpdate()
    {
        // Update keyframe time
        keyframeTime += Time.fixedDeltaTime;
        // Record frame if time step has passed
        if (RM.record && keyframeTime > RM.recordStep)
            RecordFrame();
        else if (!RM.record && RM.playback)
            PlaybackFrame();
        else
            rb.isKinematic = false;
    }

    void RecordFrame()
    {
        // Save position and rotation
        positionList.Add(transform.position);
        rotationList.Add(transform.rotation);
        // Reset keyframeTime
        keyframeTime = 0.0f;
    }

    void PlaybackFrame()
    {
        if(!inPlayback)
        {
            rb.isKinematic = true;
            inPlayback = true;
        }
        // Set position and rotation to current frame
        int currentFrame = Mathf.Min(RM.currentFrame, positionList.Count - 1);
        transform.position = positionList[currentFrame];
        transform.rotation = rotationList[currentFrame];
    }
}
