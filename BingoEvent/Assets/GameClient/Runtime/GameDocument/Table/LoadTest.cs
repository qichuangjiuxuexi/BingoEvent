using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TestTable.instance.Load("Assets/Resource/Document/LoadTest.xlsx");
        TestTable.instance.GetEntry(0).TestSay();
    }
    
}
