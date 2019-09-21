using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class s_RecordManager : s_Singleton<s_RecordManager>
    {
        public enum PB_Direction
        {
            REVERSE = -1,
            PAUSE,
            FORWARD
        }

        [SerializeField]
        public struct TransformRecording
        {
            public List<Vector3> positionsList;
            public List<Quaternion> rotationsList;
        }

        [Header("Object Recordings")]
        [SerializeField]
        private GameObject[] objectToRecord;
        public List<TransformRecording> transformsList;

        [Header("Record Variables")]
        public bool record;
        public float keyframeStep;
        public float keyframeTime;
        public int currentFrame;
        public float recordDuration;

        [Header("Playback Variables")]
        public bool playback;
        private bool inPlayback;
        public PB_Direction playbackDirection;

        void Start()
        {
            // Init starting variables
            currentFrame = 0;
            keyframeTime = 0.0f;
            // Init Objects to record
            GetObjectsToRecord();
        }

        void FixedUpdate()
        {
            UpdateCurrentFrame();
            // If recording
            if (record)
            {
                if (keyframeTime > keyframeStep)
                    RecordFrames();
            }
            // If playback
            else if (playback)
            {
                PlaybackFrames();
            }
        }

        void UpdateCurrentFrame()
        {
            // Update keyframe time
            if (record)
            {
                keyframeTime += Time.fixedDeltaTime;
            }
            if (playback)
                keyframeTime += (float)playbackDirection * Time.fixedDeltaTime;
            // Update to next keyframe
            if (keyframeTime > keyframeStep || keyframeTime <= 0.0f)
            {
                currentFrame += 1 * (int)playbackDirection;
                // Reset Keyframe time
                if(playbackDirection == PB_Direction.FORWARD)
                    keyframeTime = 0.0f;
                else if(playbackDirection == PB_Direction.REVERSE)
                    keyframeTime = keyframeStep;
            }
        }

        void GetObjectsToRecord()
        {
            objectToRecord = GameObject.FindGameObjectsWithTag("Record");
            // Init List
            transformsList = new List<TransformRecording>();
            for (int i = 0; i < objectToRecord.Length; i++)
            {
                TransformRecording tempRecord;
                tempRecord.positionsList = new List<Vector3>();
                tempRecord.rotationsList = new List<Quaternion>();
                transformsList.Add(tempRecord);
            }
        }

        void RecordFrames()
        {
            for (int i = 0; i < objectToRecord.Length; i++)
            {
                transformsList[i].positionsList.Add(objectToRecord[i].transform.position);
                transformsList[i].rotationsList.Add(objectToRecord[i].transform.rotation);
            }
            // Reset keyframeTime
            keyframeTime = 0.0f;
            // Set record duration
            recordDuration = Time.fixedTime;
        }

        void PlaybackFrames()
        {
            if (!inPlayback)
            {
                foreach (GameObject obj in objectToRecord)
                {
                    obj.GetComponent<Rigidbody>().isKinematic = false;
                }
                inPlayback = true;
                playbackDirection = PB_Direction.FORWARD;
                currentFrame = 0;
            }

            if (currentFrame < transformsList[0].positionsList.Count && currentFrame >= 0)
            {
                for (int i = 0; i < objectToRecord.Length; i++)
                {
                    // Set position and rotation to current frame
                    objectToRecord[i].transform.position = transformsList[i].positionsList[currentFrame];
                    objectToRecord[i].transform.rotation = transformsList[i].rotationsList[currentFrame];
                }
            }
            else
            {
                // Reverse playback direction
                if (playbackDirection == PB_Direction.FORWARD)
                {
                    playbackDirection = PB_Direction.REVERSE;
                    keyframeTime = recordDuration;
                }
                else if (playbackDirection == PB_Direction.REVERSE)
                {
                    playbackDirection = PB_Direction.FORWARD;
                    keyframeTime = 0.0f;
                    currentFrame = 0;
                }
            }
        }
    }
}
