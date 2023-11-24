using System.Collections.Generic;
using UnityEngine;

public class CarIntKeyCacheItem<T>
{
    public int key;
    public T objectValue;
}

public class CarIntObjectListCache<T>
{
    private List<CarIntKeyCacheItem<T>> mIntItemCacheList = new List<CarIntKeyCacheItem<T>>();
    private int mItemLimitCount = 100;

    public void SetItemLimitCount(int itemLimitCount)
    {
        mItemLimitCount = itemLimitCount;
    }

    public void RecycleItem(int key, T objectValue)
    {
        CarIntKeyCacheItem<T> intKeyCacheItem = new CarIntKeyCacheItem<T>();
        intKeyCacheItem.key = key;
        intKeyCacheItem.objectValue = objectValue;

        mIntItemCacheList.Add(intKeyCacheItem);

        CheckDeleteCacheHead();
    }

    public void CheckDeleteCacheHead()
    {
        if (mIntItemCacheList.Count > mItemLimitCount)
        {
            int extraCount = mIntItemCacheList.Count - mItemLimitCount;
            for (int i = extraCount - 1; i >= 0; i--)
            {
                if (mIntItemCacheList[i] != null && mIntItemCacheList[i].objectValue != null)
                {
                    CheckDestroyObject(mIntItemCacheList[i].objectValue);
                }
            }

            mIntItemCacheList.RemoveRange(0, extraCount);
        }
    }

    public T GetItem(int key)
    {
        for (int i = mIntItemCacheList.Count - 1; i >= 0; --i)
        {
            if (mIntItemCacheList[i] != null && mIntItemCacheList[i].key == key)
            {
                CarIntKeyCacheItem<T> intKeyCacheitem = mIntItemCacheList[i];
                mIntItemCacheList.RemoveAt(i);

                return intKeyCacheitem.objectValue;
            }
        }

        return default;
    }

    public void ClearItem()
    {
        for (int i = mIntItemCacheList.Count - 1; i >= 0; --i)
        {
            if (mIntItemCacheList[i] != null && mIntItemCacheList[i].objectValue != null)
            {
                CheckDestroyObject(mIntItemCacheList[i].objectValue);
            }
        }

        mIntItemCacheList.Clear();
    }

    public bool ContainsItem(int key)
    {
        for (int i = mIntItemCacheList.Count - 1; i >= 0; --i)
        {
            if (mIntItemCacheList[i] != null && mIntItemCacheList[i].key == key)
            {
                return true;
            }
        }

        return false;
    }

    public int GetItemCount(int key)
    {
        int count = 0;
        for (int i = mIntItemCacheList.Count - 1; i >= 0; --i)
        {
            if (mIntItemCacheList[i] != null && mIntItemCacheList[i].key == key)
            {
                ++count;
            }
        }

        return count;
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