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
        public float currentPlaybackTime;

        [Header("Playback Variables")]
        public bool playback;
        private bool inPlayback;

        void Start()
        {
            // Init starting variables
            currentFrame = 0;
            currentPlaybackTime = 0.0f;
            keyframeTime = 0.0f;
            // Init Objects to record
            GetObjectsToRecord();
        }

        void FixedUpdate()
        {
            if (record)
            {
                // Update keyframe time
                keyframeTime += Time.fixedDeltaTime;
            }
            if (record && keyframeTime > keyframeStep)
            {
                recordDuration = Time.fixedTime;
                RecordFrames();
            }
            else if (!record && playback)
            {
                currentPlaybackTime += Time.fixedDeltaTime;
                if (currentPlaybackTime > recordDuration)
                    currentPlaybackTime = 0.0f;
                UpdateCurrentFrame();
                PlaybackFrames();
            }
            else
            {
                foreach (GameObject obj in objectToRecord)
                {
                    obj.GetComponent<Rigidbody>().isKinematic = false;
                }
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
            }

            if (currentFrame < transformsList[0].positionsList.Count)
            {
                for (int i = 0; i < objectToRecord.Length; i++)
                {
                    // Set position and rotation to current frame
                    objectToRecord[i].transform.position = transformsList[i].positionsList[currentFrame];
                    objectToRecord[i].transform.rotation = transformsList[i].rotationsList[currentFrame];
                }
            }
        }

        void UpdateCurrentFrame()
        {
            currentFrame = Mathf.RoundToInt(currentPlaybackTime / keyframeStep);
        }
    }
}
