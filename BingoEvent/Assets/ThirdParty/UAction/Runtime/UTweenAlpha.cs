using TMPro;
using UnityEngine;

namespace UAction
{
    public class UTweenAlpha : UAction
    {
        private static CarListItemCache<UTweenAlpha> uTweenColorCache = new CarListItemCache<UTweenAlpha>();

        public static UTweenAlpha CreatePro(GameObject targetGameObject, UTweenAlphaData uTweenAlphaData)
        {
            if (targetGameObject != null && uTweenAlphaData != null)
            {
                UTweenAlpha uTweenAlpha = uTweenColorCache.GetItem();
                if (uTweenAlpha == null)
                {
                    uTweenAlpha = new UTweenAlpha();
                }

                TextMeshProUGUI textMeshPro = targetGameObject.GetComponent<TextMeshProUGUI>();
                if (textMeshPro)
                {
                    uTweenAlpha.active = true;
                    uTweenAlpha.textMeshPro = textMeshPro;
                    uTweenAlpha.startTime = uTweenAlphaData.startTime;
                    uTweenAlpha.endTime = uTweenAlphaData.endTime;
                    uTweenAlpha.fromAlpha = textMeshPro.alpha;
                    uTweenAlpha.toAlpha = uTweenAlphaData.absoluteAlpha;
                    uTweenAlpha.animationCurve = uTweenAlphaData.animationCurve;

                    return uTweenAlpha;
                }
            }

            return null;
        }

        public TextMeshProUGUI textMeshPro;

        public float fromAlpha;
        public float toAlpha;
        public AnimationCurve animationCurve;

        public override void DoAwake()
        {
            base.DoAwake();
        }

        public override void Apply(float timeSinceEnter)
        {
            if (timeSinceEnter > duration)
            {
                timeSinceEnter = duration;
            }

            float percent = Mathf.Clamp01(timeSinceEnter / duration);
            float curvePercent = animationCurve.Evaluate(percent);

            textMeshPro.alpha = Mathf.Lerp(fromAlpha, toAlpha, curvePercent);
        }

        public override void Reset()
        {
            textMeshPro.alpha = fromAlpha;
        }

        public override void Retarget()
        {
            fromAlpha = textMeshPro.color.a;
        }

        public override void DoDestroy()
        {
            uTweenColorCache.RecycleItem(this);
        }
    }
}