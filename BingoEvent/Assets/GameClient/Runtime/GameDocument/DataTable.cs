using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;
using UnityEngine;

public class DataTableEntry
{
    public virtual void ParseEntry(string[] objArr)
    {
        
    }  
}

public class DataTable
{
    
}

public class DataTable<T, E> where T : new() where E :DataTableEntry, new()
{
    public static T instance = new T();
    private Dictionary<string, E> mEntryDictionary = new Dictionary<string, E>();
    public void Load(string fileNmaePath)
    {
        if (string.IsNullOrEmpty(fileNmaePath))
        {
            return;   
        }
        FileStream stream = null;
        IExcelDataReader excelReader = null;
        try
        {
            stream = new FileStream(fileNmaePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }
        catch
        {
            return;
        }
        string extension = Path.GetExtension(fileNmaePath);
        if (extension.ToUpper() == ".XLS")
        {
            excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
        }
        else if (extension.ToUpper() == ".XLSX")
        {
            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        }
        else
        {
            return;
        }
        
        int columnCount = excelReader.FieldCount;
        int rowCount = excelReader.RowCount;
        for (int row = 0; row < rowCount; row++)
        {
            excelReader.Read();//每一行读取一次
            string[] objArr = new string[columnCount];
            for (int col = 0; col < columnCount; col++)
            {
                var v = excelReader.GetValue(col);
                objArr[col] = v.ToString();
            }

            E entry = new E();
            entry.ParseEntry(objArr);
            mEntryDictionary.Add(row.ToString(), entry);
        }
    }

    public E GetEntry(string id)
    {
        if (!string.IsNullOrEmpty(id) && mEntryDictionary.ContainsKey(id))
        {
            return mEntryDictionary[id];
        }

        return null;
    }
    public E GetEntry(int id)
    {
        string idCopy = id.ToString();
        if (!string.IsNullOrEmpty(idCopy) && mEntryDictionary.ContainsKey(idCopy))
        {
            return mEntryDictionary[idCopy];
        }

        return null;
    }
}
