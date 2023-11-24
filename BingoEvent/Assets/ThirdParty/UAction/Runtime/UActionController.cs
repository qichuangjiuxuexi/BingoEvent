using System.Collections.Generic;
using UnityEngine;

namespace UAction
{

    [System.Serializable]
    public class UActionGroupData
    {
        public float startTime;
        public float endTime;
        public UActionData[] uActionDataArray;
    }

    public class UActionController : MonoBehaviour
    {
        public System.Action OnStopEvent;

        public bool active;
        public GameObject targetGameObject;
        public UActionGroupData[] uActionGroupDataArray;

        private List<UActionGroup> uActionGroupList = new List<UActionGroup>();
        private float mEndTime;
        private float mTimeSinceEnter;

        void Awake()
        {
            StartComponent();
        }

        void Update()
        {
            if (active)
            {
                mTimeSinceEnter += Time.deltaTime;
                if (mTimeSinceEnter > mEndTime)
                {
                    mTimeSinceEnter = mEndTime;
                    UpdateFrame();
                    Stop();
                }
                else
                {
                    UpdateFrame();
                }
            }
        }

        private void UpdateFrame()
        {
            for (int i = 0; i < uActionGroupList.Count; ++i)
            {
                if (uActionGroupList[i] != null && mTimeSinceEnter >= uActionGroupList[i].startTime && mTimeSinceEnter <= uActionGroupList[i].endTime)
                {
                    uActionGroupList[i].Apply(mTimeSinceEnter - uActionGroupList[i].startTime);
                }
            }
        }

        public void StartComponent()
        {
            if (uActionGroupDataArray != null)
            {
                int uActionGroupDataCount = uActionGroupDataArray.Length;
                for (int i = 0; i < uActionGroupDataCount; ++i)
                {
                    UActionGroup uActionGroup = new UActionGroup();
                    uActionGroup.SetTargetGameObject(targetGameObject);
                    uActionGroup.SetUActionGroupData(uActionGroupDataArray[i]);

                    uActionGroupList.Add(uActionGroup);

                    if (uActionGroup.endTime > mEndTime)
                    {
                        mEndTime = uActionGroup.endTime;
                    }
                }

                int uGroupCount = uActionGroupList.Count;
                for (int i = 0; i < uGroupCount; ++i)
                {
                    uActionGroupList[i].DoAwake();
                }
            }
        }

        public void DestroyComponent()
        {
            if (uActionGroupDataArray != null)
            {
                int uGroupCount = uActionGroupList.Count;
                for (int i = 0; i < uGroupCount; ++i)
                {
                    uActionGroupList[i].DoDestroy();
                }
            }
        }

        public void Retarget()
        {
            if (uActionGroupDataArray != null)
            {
                int uGroupCount = uActionGroupList.Count;
                for (int i = 0; i < uGroupCount; ++i)
                {
                    uActionGroupList[i].Retarget();
                }
            }
        }

        public void Play()
        {
            active = true;
        }

        public void Pause()
        {
            active = false;
        }

        public void Stop()
        {
            active = false;

            if (OnStopEvent != null)
            {
                OnStopEvent();
            }
        }

        public void Reset()
        {
            mTimeSinceEnter = 0.0f;
            active = true;

            if (uActionGroupDataArray != null)
            {
                int uGroupCount = uActionGroupList.Count;
                for (int i = 0; i < uGroupCount; ++i)
                {
                    uActionGroupList[i].Reset();
                }
            }
        }
    }
}