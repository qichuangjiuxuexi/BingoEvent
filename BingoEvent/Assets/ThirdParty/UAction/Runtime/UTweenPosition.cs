using UnityEngine;

namespace UAction
{
    public class UTweenPosition : UAction
    {
        private static CarListItemCache<UTweenPosition> uTweenPositionCache = new CarListItemCache<UTweenPosition>();

        public static UTweenPosition Create(GameObject targetGameObject, UTweenPositionData uTweenPositionData)
        {
            if (targetGameObject != null && uTweenPositionData != null)
            {
                UTweenPosition uTweenPosition = uTweenPositionCache.GetItem();
                if (uTweenPosition == null)
                {
                    uTweenPosition = new UTweenPosition();
                }

                RectTransform rectTransform = targetGameObject.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    uTweenPosition.active = true;
                    uTweenPosition.rectTransform = rectTransform;
                    uTweenPosition.startTime = uTweenPositionData.startTime;
                    uTweenPosition.endTime = uTweenPositionData.endTime;
                    uTweenPosition.fromPosition = rectTransform.localPosition;
                    Vector3 toPosition = rectTransform.localPosition + uTweenPositionData.relativePositionOffset;
                    uTweenPosition.toPosition = toPosition;
                    uTweenPosition.animationCurve = uTweenPositionData.animationCurve;

                    return uTweenPosition;
                }
            }

            return null;
        }

        public RectTransform rectTransform;
        public Vector3 fromPosition;
        public Vector3 toPosition;
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

                    rectTransform.localPosition = Vector3.Lerp(fromPosition, toPosition, curvePercent);
                }
            }
        }

        public override void Reset()
        {
            if (rectTransform != null)
            {
                rectTransform.localPosition = fromPosition;
            }
        }

        public override void Retarget()
        {
            if (rectTransform != null)
            {
                fromPosition = rectTransform.localPosition;
            }
        }

        public override void DoDestroy()
        {
            uTweenPositionCache.RecycleItem(this);
        }
    }
}