using System.Collections.Generic;
using UnityEngine;

public class CarListItemCache<T> where T : new()
{
    private List<T> mItemCacheList = new List<T>();
    private int mItemLimitCount = 100;

    public void SetItemLimitCount(int itemLimitCount)
    {
        mItemLimitCount = itemLimitCount;
    }

    public void RecycleItem(T t)
    {
        if (t != null)
        {
            mItemCacheList.Add(t);

            CheckDeleteCacheHead();
        }
    }

    public void CheckDeleteCacheHead()
    {
        if (mItemCacheList.Count > mItemLimitCount)
        {
            int extraCount = mItemCacheList.Count - mItemLimitCount;
            for (int i = extraCount - 1; i >= 0; i--)
            {
                CheckDestroyObject(mItemCacheList[i]);
            }

            mItemCacheList.RemoveRange(0, extraCount);
        }
    }

    public T GetItem()
    {
        if (mItemCacheList.Count > 0)
        {
            int lastIndex = mItemCacheList.Count - 1;
            T t = mItemCacheList[lastIndex];
            mItemCacheList.RemoveAt(lastIndex);

            return t;
        }

        return default;
    }

    public void ClearItem()
    {
        for (int i = mItemCacheList.Count - 1; i >= 0; --i)
        {
            CheckDestroyObject(mItemCacheList[i]);
        }

        mItemCacheList.Clear();
    }

    public void CheckDestroyObject(T t)
    {
        if (t != null)
        {
            if (t is Component)
            {
                Component component = t as Component;
                if (component != null)
                {
                    Object.Destroy(component.gameObject);
                }
            }
            else if (t is Object)
            {
                Object objectValue = t as Object;
                if (objectValue != null)
                {
                    Object.Destroy(objectValue);
                }
            }
        }
    }
}