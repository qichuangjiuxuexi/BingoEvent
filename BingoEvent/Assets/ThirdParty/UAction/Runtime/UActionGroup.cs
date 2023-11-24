using System.Collections.Generic;
using UnityEngine;

namespace UAction
{
    public class UActionGroup
    {
        public float startTime;
        public float endTime;

        private GameObject mTargetGameObject;
        private List<UAction> mUActionList = new List<UAction>();

        public void DoAwake()
        {
            if (mUActionList.Count > 0)
            {
                int uActionCount = mUActionList.Count;
                for (int i = 0; i < uActionCount; ++i)
                {
                    if (mUActionList[i] != null)
                    {
                        mUActionList[i].DoAwake();
                    }
                }
            }
        }

        public void Apply(float timeSinceEnter)
        {
            if (mUActionList.Count > 0)
            {
                int uActionCount = mUActionList.Count;
                for (int i = 0; i < uActionCount; ++i)
                {
                    if (mUActionList[i] != null)
                    {
                        if (mUActionList[i].active)
                        {
                            if (timeSinceEnter >= mUActionList[i].startTime && timeSinceEnter <= mUActionList[i].endTime)
                            {
                                mUActionList[i].Apply(timeSinceEnter);
                            }
                            else if (timeSinceEnter > mUActionList[i].endTime)
                            {
                                mUActionList[i].Apply(mUActionList[i].endTime);
                                mUActionList[i].active = false;
                            }
                        }
                    }
                }
            }
        }

        public void Reset()
        {
            if (mUActionList.Count > 0)
            {
                int uActionCount = mUActionList.Count;
                for (int i = 0; i < uActionCount; ++i)
                {
                    if (mUActionList[i] != null)
                    {
                        mUActionList[i].Reset();
                        mUActionList[i].active = true;
                    }
                }
            }
        }

        public void Retarget()
        {
            if (mUActionList.Count > 0)
            {
                int uActionCount = mUActionList.Count;
                for (int i = 0; i < uActionCount; ++i)
                {
                    if (mUActionList[i] != null)
                    {
                        mUActionList[i].Retarget();
                    }
                }
            }
        }

        public void DoDestroy()
        {
            if (mUActionList.Count > 0)
            {
                int uActionCount = mUActionList.Count;
                for (int i = 0; i < uActionCount; ++i)
                {
                    if (mUActionList[i] != null)
                    {
                        mUActionList[i].DoDestroy();
                    }
                }
            }
        }

        public void SetTargetGameObject(GameObject targetGameObject)
        {
            mTargetGameObject = targetGameObject;
        }

        public void SetUActionGroupData(UActionGroupData uActionGroupData)
        {
            if (uActionGroupData != null && uActionGroupData.uActionDataArray != null)
            {
                startTime = uActionGroupData.startTime;
                endTime = uActionGroupData.endTime;

                int uActionDataCount = uActionGroupData.uActionDataArray.Length;
                for (int i = 0; i < uActionDataCount; ++i)
                {
                    UActionData uActionData = uActionGroupData.uActionDataArray[i];
                    if (uActionData != null && mTargetGameObject != null)
                    {
                        if (uActionData.uActionDataType == UActionData.UActionDataType.position)
                        {
                            UTweenPositionData uTweenPositionData = uActionData as UTweenPositionData;
                            UTweenPosition uTweenPosition = UTweenPosition.Create(mTargetGameObject, uTweenPositionData);
                            if (uTweenPosition != null)
                            {
                                mUActionList.Add(uTweenPosition);
                            }
                        }
                        else if (uActionData.uActionDataType == UActionData.UActionDataType.rotation)
                        {
                            UTweenRotationData uTweenRotationData = uActionData as UTweenRotationData;
                            UTweenRotation uTweenRotation = UTweenRotation.Create(mTargetGameObject, uTweenRotationData);
                            if (uTweenRotation != null)
                            {
                                mUActionList.Add(uTweenRotation);
                            }
                        }
                        else if (uActionData.uActionDataType == UActionData.UActionDataType.scale)
                        {
                            UTweenScaleData uTweenScaleData = uActionData as UTweenScaleData;
                            UTweenScale uTweenScale = UTweenScale.Create(mTargetGameObject, uTweenScaleData);
                            if (uTweenScale != null)
                            {
                                mUActionList.Add(uTweenScale);
                            }
                        }
                        else if (uActionData.uActionDataType == UActionData.UActionDataType.color)
                        {
                            UTweenColorData uTweenColorData = uActionData as UTweenColorData;
                            UTweenColor uTweenColor = UTweenColor.Create(mTargetGameObject, uTweenColorData);
                            if (uTweenColor != null)
                            {
                                mUActionList.Add(uTweenColor);
                            }
                        }
                        else if (uActionData.uActionDataType == UActionData.UActionDataType.colorPro)
                        {
                            UTweenColorData uTweenColorData = uActionData as UTweenColorData;
                            UTweenColor uTweenColor = UTweenColor.CreatePro(mTargetGameObject, uTweenColorData);
                            if (uTweenColor != null)
                            {
                                mUActionList.Add(uTweenColor);
                            }
                        }
                        else if (uActionData.uActionDataType == UActionData.UActionDataType.alpha)
                        {
                            UTweenAlphaData uTweenAlphaData = uActionData as UTweenAlphaData;
                            UTweenAlpha uTweenAlpha = UTweenAlpha.CreatePro(mTargetGameObject, uTweenAlphaData);
                            if (uTweenAlpha != null)
                            {
                                mUActionList.Add(uTweenAlpha);
                            }
                        }
                    }
                }
            }
        }
    }
}