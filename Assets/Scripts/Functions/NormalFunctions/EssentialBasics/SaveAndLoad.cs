using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveAndLoad
{
    readonly string SaveMapFilePath = Application.dataPath + "/SaveMaps";
    readonly string SaveSlotFilePath = Application.dataPath + "/SaveSlots";

    public void SaveMapSerializedFile(
        SavingDatum mapobject,
        GameObject savedirectory,
        GameObject savedone
    )
    {
        if (!Directory.Exists(SaveMapFilePath))
        {
            Directory.CreateDirectory(SaveMapFilePath);
        }
        string savefilename = savedirectory.GetComponent<TMP_InputField>().text;
        string fullpath = SaveMapFilePath + "/" + savefilename + ".ser";
        if (File.Exists(fullpath))
        {
            savedone.GetComponent<TMP_Text>().text = "文件已存在,保存失败";
            return;
        }

        FileStream fs = new(fullpath, FileMode.Create);
        BinaryFormatter bf = new();
        savedone.GetComponent<TMP_Text>().text = "保存中";
        try
        {
            bf.Serialize(fs, mapobject);
        }
        catch (Exception except)
        {
            savedone.GetComponent<TMP_Text>().text = "保存怎么失败了";
            MonoBehaviour.print(except);
        }
        finally
        {
            fs.Close();
            savedone.GetComponent<TMP_Text>().text = "保存成功了";
        }
    }

    public async void SaveMapJSONFile(
        SavingDatum mapobject,
        GameObject savedirectory,
        GameObject savedone,
        bool coverit
    )
    {
        if (!Directory.Exists(SaveMapFilePath))
        {
            Directory.CreateDirectory(SaveMapFilePath);
        }
        string savefilename = savedirectory.GetComponent<TMP_InputField>().text;
        string fullpath = SaveMapFilePath + "/" + savefilename + ".json";
        if (File.Exists(fullpath) && !coverit)
        {
            savedone.GetComponent<TMP_Text>().text = "文件已存在,保存失败";
            return;
        }

        mapobject.MapToSerializeSlot();
        if (coverit)
        {
            savedone.GetComponent<TMP_Text>().text = "覆盖中";
        }
        else
        {
            savedone.GetComponent<TMP_Text>().text = "保存中";
        }
        try
        {
            await File.WriteAllTextAsync(fullpath, JsonUtility.ToJson(mapobject));
            if (coverit)
            {
                savedone.GetComponent<TMP_Text>().text = "覆盖成功了";
            }
            else
            {
                savedone.GetComponent<TMP_Text>().text = "保存成功了";
            }
        }
        catch (Exception except)
        {
            savedone.GetComponent<TMP_Text>().text = "保存怎么失败了";
            MonoBehaviour.print(except);
        }
    }

    public void SaveSlotFile(object gamefile, ref GameObject savedone)
    {
        if (!Directory.Exists(SaveSlotFilePath))
        {
            Directory.CreateDirectory(SaveSlotFilePath);
        }
        string fullpath = SaveSlotFilePath + "/" + DateTime.Now.ToFileTimeUtc() + ".save";
        if (File.Exists(fullpath))
        {
            savedone.GetComponent<TMP_Text>().text = "文件已存在,保存失败";
            return;
        }
        BinaryFormatter binfor = new();
        savedone.GetComponent<TMP_Text>().text = "保存中";
        try
        {
            FileStream fs = new(fullpath, FileMode.Create);
            binfor.Serialize(fs, gamefile);
            fs.Close();
            savedone.GetComponent<TMP_Text>().text = "保存成功了";
        }
        catch (Exception except)
        {
            savedone.GetComponent<TMP_Text>().text = "保存怎么失败了";
            MonoBehaviour.print(except);
        }
    }

    public SavingDatum LoadMapJSONFile(ref GameObject readdirectory, ref GameObject readdone)
    {
        if (!Directory.Exists(SaveMapFilePath))
        {
            readdone.GetComponent<TMP_Text>().text = "地图文件夹不存在";
            Directory.CreateDirectory(SaveMapFilePath);
            return null;
        }
        string loadfilename = readdirectory.GetComponent<TMP_InputField>().text;
        string fullpath = SaveMapFilePath + "/" + loadfilename + ".json";
        if (!File.Exists(fullpath))
        {
            readdone.GetComponent<TMP_Text>().text = "文件不存在,读取失败";
            return null;
        }

        readdone.GetComponent<TMP_Text>().text = "读取中";
        try
        {
            string jsonstr = File.ReadAllText(fullpath);
            readdone.GetComponent<TMP_Text>().text = "读取成功了";
            return JsonUtility.FromJson<SavingDatum>(jsonstr);
        }
        catch (Exception except)
        {
            readdone.GetComponent<TMP_Text>().text = "读取怎么失败了";
            MonoBehaviour.print(except);
        }
        return null;
    }

    public SavingDatum LoadMapJSONFile(string loadfilename)
    {
        if (!Directory.Exists(SaveMapFilePath))
        {
            Directory.CreateDirectory(SaveMapFilePath);
            return null;
        }
        string fullpath = SaveMapFilePath + "/" + loadfilename + ".json";
        if (!File.Exists(fullpath))
        {
            return null;
        }

        try
        {
            string jsonstr = File.ReadAllText(fullpath);
            return JsonUtility.FromJson<SavingDatum>(jsonstr);
        }
        catch (Exception except)
        {
            MonoBehaviour.print(except);
        }
        return null;
    }


    public SavingDatum LoadMapSerializedFile(ref GameObject readdirectory, ref GameObject readdone)
    {
        if (!Directory.Exists(SaveMapFilePath))
        {
            readdone.GetComponent<TMP_Text>().text = "地图文件夹不存在";
            Directory.CreateDirectory(SaveMapFilePath);
            return null;
        }
        string savefilename = readdirectory.GetComponent<TMP_InputField>().text;
        string fullpath = SaveMapFilePath + "/" + savefilename + ".ser";
        if (!File.Exists(fullpath))
        {
            readdone.GetComponent<TMP_Text>().text = "文件不存在,读取失败";
            return null;
        }

        BinaryFormatter binfor = new();
        readdone.GetComponent<TMP_Text>().text = "读取中";
        try
        {
            FileStream fs = File.Open(fullpath, FileMode.Open);
            SavingDatum sd = (SavingDatum)binfor.Deserialize(fs);
            return sd;
        }
        catch (Exception except)
        {
            readdone.GetComponent<TMP_Text>().text = "读取怎么失败了,";
            MonoBehaviour.print(except);
        }
        return null;
    }
}
