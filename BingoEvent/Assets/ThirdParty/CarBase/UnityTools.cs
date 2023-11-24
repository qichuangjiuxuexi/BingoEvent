using UnityEngine;

public class UnityTools
{
    public static GameObject AddChild(GameObject parent, GameObject assetGameObject)
    {
        if (parent != null && assetGameObject != null)
        {
            GameObject prefabGameObject = Object.Instantiate(assetGameObject);
            if (prefabGameObject != null)
            {
                Transform prefabTransform = prefabGameObject.transform;
                prefabTransform.SetParent(parent.transform);
                prefabTransform.localPosition = Vector3.zero;
                prefabTransform.localRotation = Quaternion.identity;
                prefabTransform.localScale = Vector3.one;
                prefabGameObject.layer = parent.layer;
            }

            return prefabGameObject;
        }

        return null;
    }

    public static Transform ResetTransform(Transform targetTransform)
    {
        if (targetTransform != null)
        {
            targetTransform.localPosition = Vector3.zero;
            targetTransform.localRotation = Quaternion.identity;
            targetTransform.localScale = Vector3.one;
        }

        return targetTransform;
    }
}