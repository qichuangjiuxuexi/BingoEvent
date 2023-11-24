using System.Collections.Generic;
using UnityEngine;

public class CarStringKeyCacheItem<T>
{
    public string key;
    public T objectValue;
}

public class CarStringObjectListCache<T>
{
    private List<CarStringKeyCacheItem<T>> mStringItemCacheList = new List<CarStringKeyCacheItem<T>>();
    private int mItemLimitCount = 100;

    public void SetItemLimitCount(int itemLimitCount)
    {
        mItemLimitCount = itemLimitCount;
    }

    public void RecycleItem(string key, T objectValue)
    {
        CarStringKeyCacheItem<T> stringKeyCacheItem = new CarStringKeyCacheItem<T>();
        stringKeyCacheItem.key = key;
        stringKeyCacheItem.objectValue = objectValue;

        mStringItemCacheList.Add(stringKeyCacheItem);

        CheckDeleteCacheHead();
    }

    public void CheckDeleteCacheHead()
    {
        if (mStringItemCacheList.Count > mItemLimitCount)
        {
            int extraCount = mStringItemCacheList.Count - mItemLimitCount;
            for (int i = extraCount - 1; i >= 0; i--)
            {
                if (mStringItemCacheList[i] != null && mStringItemCacheList[i].objectValue != null)
                {
                    CheckDestroyObject(mStringItemCacheList[i].objectValue);
                }
            }

            mStringItemCacheList.RemoveRange(0, extraCount);
        }
    }

    public object GetItem(string key)
    {
        for (int i = mStringItemCacheList.Count - 1; i >= 0; --i)
        {
            if (mStringItemCacheList[i] != null && mStringItemCacheList[i].key == key)
            {
                CarStringKeyCacheItem<T> stringKeyCacheItem = mStringItemCacheList[i];
                mStringItemCacheList.RemoveAt(i);

                return stringKeyCacheItem.objectValue;
            }
        }

        return null;
    }

    public void ClearItem()
    {
        for (int i = mStringItemCacheList.Count - 1; i >= 0; --i)
        {
            if (mStringItemCacheList[i] != null && mStringItemCacheList[i].objectValue != null)
            {
                CheckDestroyObject(mStringItemCacheList[i].objectValue);
            }
        }

        mStringItemCacheList.Clear();
    }

    public bool ContainsItem(string key)
    {
        for (int i = mStringItemCacheList.Count - 1; i >= 0; --i)
        {
            if (mStringItemCacheList[i] != null && mStringItemCacheList[i].key == key)
            {
                return true;
            }
        }

        return false;
    }

    public int GetItemCount(string key)
    {
        int count = 0;
        for (int i = mStringItemCacheList.Count - 1; i >= 0; --i)
        {
            if (mStringItemCacheList[i] != null && mStringItemCacheList[i].key == key)
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