using UnityEngine;

namespace UAction
{
    public class UTweenRotation : UAction
    {
        private static CarListItemCache<UTweenRotation> uTweenRotationCache = new CarListItemCache<UTweenRotation>();

        public static UTweenRotation Create(GameObject targetGameObject, UTweenRotationData uTweenRotationData)
        {
            if (targetGameObject != null && uTweenRotationData != null)
            {
                RectTransform rectTransform = targetGameObject.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    UTweenRotation uTweenRotation = uTweenRotationCache.GetItem();
                    if (uTweenRotation == null)
                    {
                        uTweenRotation = new UTweenRotation();
                    }

                    uTweenRotation.active = true;
                    uTweenRotation.rectTransform = rectTransform;
                    uTweenRotation.startTime = uTweenRotationData.startTime;
                    uTweenRotation.endTime = uTweenRotationData.endTime;
                    uTweenRotation.fromQuaternion = rectTransform.localRotation;
                    Vector3 toEulerAngles = rectTransform.localEulerAngles + uTweenRotationData.relativeRotation;
                    uTweenRotation.toQuaternion = Quaternion.Euler(toEulerAngles);
                    uTweenRotation.animationCurve = uTweenRotationData.animationCurve;

                    return uTweenRotation;
                }
            }

            return null;
        }

        public RectTransform rectTransform;
        public Quaternion fromQuaternion;
        public Quaternion toQuaternion;
        public AnimationCurve animationCurve;

        public override void DoAwake()
        {
            base.DoAwake();
        }

        public override void Apply(float timeSinceEnter)
        {
            if (rectTransform != null)
            {
                if (timeSinceEnter > duration)
                {
                    timeSinceEnter = duration;
                }

                if (animationCurve != null)
                {
                    float percent = Mathf.Clamp01(timeSinceEnter / duration);
                    float curvePercent = animationCurve.Evaluate(percent);

                    rectTransform.localRotation = Quaternion.Slerp(fromQuaternion, toQuaternion, curvePercent);
                }
            }
        }

        public override void Reset()
        {
            if (rectTransform != null)
            {
                rectTransform.localRotation = fromQuaternion;
            }
        }

        public override void Retarget()
        {
            if (rectTransform != null)
            {
                fromQuaternion = rectTransform.localRotation;
            }
        }

        public override void DoDestroy()
        {
            uTweenRotationCache.RecycleItem(this);
        }
    }
}