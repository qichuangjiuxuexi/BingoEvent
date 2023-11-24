using UnityEngine;

namespace UAction
{
    public class UTweenScale : UAction
    {
        private static CarListItemCache<UTweenScale> uTweenScaleCache = new CarListItemCache<UTweenScale>();

        public static UTweenScale Create(GameObject targetGameObject, UTweenScaleData uTweenScaleData)
        {
            if (targetGameObject != null && uTweenScaleData != null)
            {
                UTweenScale uTweenScale = uTweenScaleCache.GetItem();
                if (uTweenScale == null)
                {
                    uTweenScale = new UTweenScale();
                }

                RectTransform rectTransform = targetGameObject.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    uTweenScale.active = true;
                    uTweenScale.rectTransform = rectTransform;
                    uTweenScale.startTime = uTweenScaleData.startTime;
                    uTweenScale.endTime = uTweenScaleData.endTime;
                    uTweenScale.fromScale = rectTransform.localScale;
                    Vector3 toScale = rectTransform.localScale + uTweenScaleData.relativeScale;
                    uTweenScale.toScale = toScale;
                    uTweenScale.animationCurve = uTweenScaleData.animationCurve;

                    return uTweenScale;
                }
            }

            return null;
        }

        public RectTransform rectTransform;
        public Vector3 fromScale;
        public Vector3 toScale;
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

                float percent = Mathf.Clamp01(timeSinceEnter / duration);
                float curvePercent = animationCurve.Evaluate(percent);

                rectTransform.localScale = Vector3.Slerp(fromScale, toScale, curvePercent);
            }
        }

        public override void Reset()
        {
            if (rectTransform != null)
            {
                rectTransform.localScale = fromScale;
            }
        }

        public override void Retarget()
        {
            if (rectTransform != null)
            {
                fromScale = rectTransform.localScale;
            }
        }

        public override void DoDestroy()
        {
            uTweenScaleCache.RecycleItem(this);
        }
    }
}