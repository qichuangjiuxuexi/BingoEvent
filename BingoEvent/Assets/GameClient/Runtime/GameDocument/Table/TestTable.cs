
using System;
using System.Collections.Generic;
using UnityEngine;

public class TestTableEntry : DataTableEntry
{
    private List<string> l = new List<string>();
    public override void ParseEntry(string[] objArr)
    {
        for (int i = 0; i < objArr.Length; i++)
        {
            l.Add(objArr[i]);
        }
    }

    public void TestSay()
    {
        string log = "";
        for (int i = 0; i < l.Count; i++)
        {
            log += l[i];
        }
        Debug.Log(log);
    }
}

public class TestTable : DataTable<TestTable, TestTableEntry>
{
    
}
