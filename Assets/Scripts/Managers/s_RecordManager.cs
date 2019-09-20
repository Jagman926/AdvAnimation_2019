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

        [Header("Record Variables")]
        public bool record;
        public float recordStep;
        public int currentFrame;
        public float recordDuration;
        public float currentPlaybackTime;

        [Header("Playback Variables")]
        public bool playback;

        void Start()
        {
            currentFrame = 0;
            currentPlaybackTime = 0.0f;
        }

        void FixedUpdate()
        {
            if(record)
            {
                recordDuration = Time.fixedTime;
            }
            if(playback)
            {
                currentPlaybackTime += Time.fixedDeltaTime;
                if(currentPlaybackTime > recordDuration)
                    currentPlaybackTime = 0.0f;
                UpdateCurrentFrame();
            }
        }

        void UpdateCurrentFrame()
        {
            currentFrame = Mathf.RoundToInt(currentPlaybackTime / recordStep);
        }
    }
}
