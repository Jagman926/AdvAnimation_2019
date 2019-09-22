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
        public int totalFrames;

        [Header("Playback Variables")]
        public bool playback;
        private bool inPlayback;
        public bool isNormal;
        public bool isLooping;
        public bool isPingPong;
        public PB_Direction playbackDirection;
        public PB_Direction previousPlaybackDirection;

        void Start()
        {
            // Init starting variables
            currentFrame = 0;
            totalFrames = 0;
            keyframeTime = 0.0f;
            isNormal = true;
            // Set previous play direction
            previousPlaybackDirection = PB_Direction.FORWARD;
            // Init Objects to record
            GetObjectsToRecord();
        }

        void FixedUpdate()
        {
            // Debug Test
            Debug.Log("Current:" + currentFrame);
            Debug.Log("Total - 1:" + totalFrames);
            // If recording
            if (record)
            {
                // Update current frame
                UpdateCurrentFrame_Record();
                // Update keyframe time
                keyframeTime += Time.fixedDeltaTime;
            }
            // If playback
            else if (playback)
            {
                // If going into playback, init playback
                if (!inPlayback)
                    InitPlayback();
                // Update current frame 
                UpdateCurrentFrame_Playback();
                // Update keyframe time by direction
                keyframeTime += (float)playbackDirection * Time.fixedDeltaTime;
            }
        }

        void UpdateCurrentFrame_Record()
        {
            // If above keyframetime or, incerement to next frame
            if (keyframeTime > keyframeStep)
            {
                RecordFrames();
                // Reset keyframeTime
                keyframeTime = 0.0f;
                // Increment frame count
                currentFrame++;
            }
        }

        void UpdateCurrentFrame_Playback()
        {
            // If above keyframetime or below zero, incerement/decrement to next frame
            if (keyframeTime > keyframeStep || keyframeTime < 0.0f)
            {
                PlaybackFrames();
                // Reset Keyframe time
                if (playbackDirection == PB_Direction.FORWARD)
                    keyframeTime = 0.0f;
                else if (playbackDirection == PB_Direction.REVERSE)
                    keyframeTime = keyframeStep;
            }
        }

        void RecordFrames()
        {
            // For each object being recorded, save current position and rotation in list
            for (int i = 0; i < objectToRecord.Length; i++)
            {
                transformsList[i].positionsList.Add(objectToRecord[i].transform.position);
                transformsList[i].rotationsList.Add(objectToRecord[i].transform.rotation);
            }
        }

        void InitPlayback()
        {
            // Disable object kinematic
            foreach (GameObject obj in objectToRecord)
            {
                obj.GetComponent<Rigidbody>().isKinematic = false;
            }
            // Set current frame to 0
            if (playbackDirection == PB_Direction.FORWARD)
                currentFrame = 0;
            else if (playbackDirection == PB_Direction.REVERSE)
                currentFrame = totalFrames;
            // Set in playback
            inPlayback = true;
        }

        void PlaybackFrames()
        {
            // if playing forward and on last frame
            if (playbackDirection == PB_Direction.FORWARD && currentFrame == totalFrames)
            {
                if (isNormal)
                {
                    // Stop playing
                    playbackDirection = PB_Direction.PAUSE;
                    // Playback is done
                    inPlayback = false;
                    playback = false;
                }
                // If set to loop
                else if (isLooping)
                {
                    // Set to first frame
                    currentFrame = 0;
                }
                else if (isPingPong)
                {
                    // Change play direction
                    playbackDirection = PB_Direction.REVERSE;
                    // bounce to previous frame
                    currentFrame -= 1;
                }
            }
            // if playing reverse and on first frame
            else if (playbackDirection == PB_Direction.REVERSE && currentFrame == 0)
            {
                if (isNormal)
                {
                    playbackDirection = PB_Direction.PAUSE;
                }
                // If set to loop
                else if (isLooping)
                {
                    // Set to last frame
                    currentFrame = (totalFrames - 1);
                }
                else if (isPingPong)
                {
                    // Reverse playback direction
                    playbackDirection = PB_Direction.FORWARD;
                    // bounce to next frame
                    currentFrame += 1;
                }
            }
            else if (playbackDirection == PB_Direction.REVERSE && currentFrame == totalFrames)
            {
                    // Play in reverse to previous frame
                    currentFrame -= 1;
            }
            // middle frames
            else if (currentFrame < totalFrames && currentFrame >= 0)
            {
                SetCurrentFrame();
                // Increment/Decrement frame count
                currentFrame += 1 * (int)playbackDirection;
            }
        }

        void SetCurrentFrame()
        {
            for (int i = 0; i < objectToRecord.Length; i++)
            {
                // Set position and rotation to current frame
                objectToRecord[i].transform.position = transformsList[i].positionsList[currentFrame];
                objectToRecord[i].transform.rotation = transformsList[i].rotationsList[currentFrame];
            }
        }

        void FreezeAllObjects(bool freeze)
        {
            foreach (GameObject obj in objectToRecord)
            {
                if (freeze)
                    obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                else
                    obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }

        void GetObjectsToRecord()
        {
            // Get all objects with "Record" tag
            objectToRecord = GameObject.FindGameObjectsWithTag("Record");
            // Init List
            transformsList = new List<TransformRecording>();
            // For each object init new TransformRecording struct with transform and rotation lists
            for (int i = 0; i < objectToRecord.Length; i++)
            {
                TransformRecording tempRecord;
                tempRecord.positionsList = new List<Vector3>();
                tempRecord.rotationsList = new List<Quaternion>();
                transformsList.Add(tempRecord);
            }
        }

        // Function Commands
        //------------------------------------------------------------------

        public void Play(PB_Direction direction)
        {
            if (totalFrames > 0)
            {
                // Set play direction to previous play direction
                playbackDirection = direction;
                // Freeze all objects
                FreezeAllObjects(true);
                // Set playback to play
                playback = true;
            }
            else
            {
                Debug.LogAssertion("Nothing Recorded to Play");
            }
        }

        public void Pause()
        {
            // Set current playback direction as previous
            previousPlaybackDirection = playbackDirection;
            // Set to pause
            playbackDirection = PB_Direction.PAUSE;
            // Set placeback to false
            playback = false;
        }

        public void Reverse()
        {
            // Set to reverse
            Play(PB_Direction.REVERSE);
        }

        public void Forward()
        {
            // Set to forward
            Play(PB_Direction.FORWARD);
        }

        public void Normal()
        {
            isNormal = !isNormal;
            // Disable other types
            isLooping = false;
            isPingPong = false;
        }

        public void Loop()
        {
            isLooping = !isLooping;
            // Disable other types
            isPingPong = false;
            isNormal = false;
        }

        public void PingPong()
        {
            isPingPong = !isPingPong;
            // Disable other types
            isLooping = false;
            isNormal = false;
        }

        public void StartRecord()
        {
            // Set record to true
            record = true;
        }

        public void EndRecord()
        {
            // Set record to false
            record = false;
            // Set total frame count
            totalFrames = transformsList[0].positionsList.Count;
            // Reset keyframe time
            keyframeTime = 0.0f;
            // Set current frame to start
            currentFrame = 0;
        }
    }
}
