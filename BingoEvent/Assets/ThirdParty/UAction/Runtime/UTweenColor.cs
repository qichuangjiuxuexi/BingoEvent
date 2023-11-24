using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UAction
{
    public class UTweenColor : UAction
    {
        private static CarListItemCache<UTweenColor> uTweenColorCache = new CarListItemCache<UTweenColor>();

        public static UTweenColor Create(GameObject targetGameObject, UTweenColorData uTweenColorData)
        {
            if (targetGameObject != null && uTweenColorData != null)
            {
                UTweenColor uTweenColor = uTweenColorCache.GetItem();
                if (uTweenColor == null)
                {
                    uTweenColor = new UTweenColor();
                }

                Graphic graphic = targetGameObject.GetComponent<Graphic>();
                if (graphic)
                {
                    uTweenColor.active = true;
                    uTweenColor.isPro = false;
                    uTweenColor.graphic = graphic;
                    uTweenColor.startTime = uTweenColorData.startTime;
                    uTweenColor.endTime = uTweenColorData.endTime;
                    uTweenColor.fromColor = graphic.color;
                    uTweenColor.toColor = uTweenColorData.absoluteColor;
                    uTweenColor.animationCurve = uTweenColorData.animationCurve;

                    return uTweenColor;
                }
            }

            return null;
        }

        public static UTweenColor CreatePro(GameObject targetGameObject, UTweenColorData uTweenColorData)
        {
            if (targetGameObject != null && uTweenColorData != null)
            {
                UTweenColor uTweenColor = uTweenColorCache.GetItem();
                if (uTweenColor == null)
                {
                    uTweenColor = new UTweenColor();
                }

                TextMeshProUGUI textMeshPro = targetGameObject.GetComponent<TextMeshProUGUI>();
                if (textMeshPro)
                {
                    uTweenColor.active = true;
                    uTweenColor.isPro = true;
                    uTweenColor.textMeshPro = textMeshPro;
                    uTweenColor.startTime = uTweenColorData.startTime;
                    uTweenColor.endTime = uTweenColorData.endTime;
                    uTweenColor.fromColor = textMeshPro.color;
                    uTweenColor.toColor = uTweenColorData.absoluteColor;
                    uTweenColor.animationCurve = uTweenColorData.animationCurve;

                    return uTweenColor;
                }
            }

            return null;
        }

        public bool isPro;
        public Graphic graphic;
        public TextMeshProUGUI textMeshPro;

        public Color fromColor;
        public Color toColor;
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

            if (isPro)
            {
                textMeshPro.color = Color.Lerp(fromColor, toColor, curvePercent);
            }
            else
            {
                graphic.color = Color.Lerp(fromColor, toColor, curvePercent);
            }
        }

        public override void Reset()
        {
            if (isPro)
            {
                textMeshPro.color = fromColor;
            }
            else
            {
                graphic.color = fromColor;
            }
        }

        public override void Retarget()
        {
            if (isPro)
            {
                fromColor = textMeshPro.color;
            }
            else
            {
                fromColor = graphic.color;
            }
        }

        public override void DoDestroy()
        {
            uTweenColorCache.RecycleItem(this);
        }
    }
}