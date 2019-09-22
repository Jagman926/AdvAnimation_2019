using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Managers
{
    public class s_UIManager : s_Singleton<s_UIManager>
    {
        // Manager Instance
        s_RecordManager RecordManager;

        // UI Elements
        [Header("Button UI")]
        [SerializeField]
        private Image play_pause_image;
        [SerializeField]
        private Image record_image;
        [SerializeField]
        private Image playType_image;
        [SerializeField]
        private Slider scrubber;
        [Header("Play/Pause Swaps")]
        public Sprite play_sprite;
        public Sprite pause_sprite;
        [Header("Play Type Swaps")]
        public Sprite normal_sprite;
        public Sprite loop_sprite;
        public Sprite pingpong_sprite;
        public int currentPlayType;
        [Header("Record Swaps")]
        public Sprite record_red_sprite;
        public Sprite record_black_sprite;

        [Header("Settings")]
        [SerializeField]
        private GameObject bombObject;

        void Start()
        {
            // Set manager instance
            RecordManager = Managers.s_RecordManager.Instance;
            // Init playtype
            currentPlayType = 0;
        }

        void Update()
        {
            UpdateScrubber();
            UpdatePlayPause();
        }

        private void UpdateScrubber()
        {
            // Set scrubber handle location
            scrubber.value = RecordManager.currentFrame * RecordManager.keyframeStep;
            // While recording, update scrubber max
            if (RecordManager.record)
                UpdateScrubberMax();
        }

        private void UpdateScrubberMax()
        {
            // Set scrubber max
            scrubber.maxValue = RecordManager.totalFrames * RecordManager.keyframeStep;
        }

        private void UpdatePlayPause()
        {
            if (RecordManager.playbackDirection == s_RecordManager.PB_Direction.PAUSE)
                // Swap to pause sprite
                play_pause_image.sprite = play_sprite;
            else if (RecordManager.playback)
                // Swap to play sprite
                play_pause_image.sprite = pause_sprite;
        }

        public void Play_Pause()
        {
            // If currently playing, pause
            // If currently paused, continue last play direction
            if (play_pause_image.sprite == pause_sprite)
                RecordManager.Pause();
            // If currently paused, continue last play direction
            else if (play_pause_image.sprite == play_sprite)
                RecordManager.Play(RecordManager.previousPlaybackDirection);
        }

        public void PlayType()
        {
            // Increment playtype
            currentPlayType++;
            if (currentPlayType > 2)
                currentPlayType = 0;
            // Set playtype
            if (currentPlayType == 0)
            {
                RecordManager.Normal();
                playType_image.sprite = normal_sprite;
            }
            else if (currentPlayType == 1)
            {
                RecordManager.Loop();
                playType_image.sprite = loop_sprite;
            }
            else if (currentPlayType == 2)
            {
                RecordManager.PingPong();
                playType_image.sprite = pingpong_sprite;
            }
        }

        public void Reverse()
        {
            RecordManager.Reverse();
        }

        public void Forward()
        {
            RecordManager.Forward();
        }

        public void Record()
        {
            // If currently recording, end record
            if (RecordManager.record == true)
            {
                RecordManager.EndRecord();
                // Set record image to black
                record_image.sprite = record_black_sprite;
                // Upon recording ending, update scrubber max
                UpdateScrubberMax();
            }
            // If not recording, start record
            else if (RecordManager.record == false)
            {
                RecordManager.StartRecord();
                // Set record image to black
                record_image.sprite = record_red_sprite;
            }
        }

        public void Settings()
        {
            bombObject.GetComponent<s_Bomb>().explode = true;
        }
    }
}
