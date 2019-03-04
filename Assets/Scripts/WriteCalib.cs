using UnityEngine;
using GameDevProfi.Utils;
using System.IO;

[System.Serializable] //Mark that this class is able to read/write data
public class WriteCalib{

    public string[] testwriteData = new string[1];
    //public ConfigData configData;

    private static string GetFilename()
    {
        //Path.DirectorySeparatorCha --> / Linux/Mac, \ Windwos
        return Application.persistentDataPath + Path.DirectorySeparatorChar + "PMapCalib.xml";
    }

    //Speichert Spielstand
    public void Save(string[] data)
    {
        Debug.Log("Save Data: " + GetFilename());
        testwriteData = data;
        


        // test = 123;
        string xml = XML.Save(this); //takes the XML Class --> using GameDevProfi.Utils
        File.WriteAllText(GetFilename(), xml);
        Debug.Log(xml);
    }

    public string[] LoadData()
    {
        WriteCalib save = XML.Load<WriteCalib>(File.ReadAllText(GetFilename()));
        return save.testwriteData;
    }

}


