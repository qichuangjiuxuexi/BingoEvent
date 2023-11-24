using UnityEngine;

namespace UAction
{
    public class UActionData : ScriptableObject
    {
        public enum UActionDataType
        {
            position,
            rotation,
            scale,
            color,
            colorPro,
            alpha,
        }

        public float startTime;
        public float endTime;
        public UActionDataType uActionDataType;
    }
}