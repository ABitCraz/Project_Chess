using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveAndLoad
{
    readonly string SaveMapFilePath = Application.dataPath + "/SaveMaps";
    readonly string SaveSlotFilePath = Application.dataPath + "/SaveSlots";

    public async void SaveMapFile(
        List<Slot> mapobject,
        GameObject savedirectory,
        GameObject savedone
    )
    {
        if (!Directory.Exists(SaveMapFilePath))
        {
            Directory.CreateDirectory(SaveMapFilePath);
        }
        string savefilename = savedirectory.GetComponent<TMP_InputField>().text;
        string fullpath = SaveMapFilePath + "/" + savefilename + ".save";
        if (File.Exists(fullpath))
        {
            savedone.GetComponent<TMP_Text>().text = "文件已存在,保存失败";
            return;
        }

        savedone.GetComponent<TMP_Text>().text = "保存中";
        try
        {
            await File.WriteAllTextAsync(fullpath,JsonUtility.ToJson(mapobject));
            savedone.GetComponent<TMP_Text>().text = "保存成功了";
        }
        catch (Exception except)
        {
            savedone.GetComponent<TMP_Text>().text = "保存怎么失败了," + except;
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
            FileStream fs = File.Create(fullpath);
            binfor.Serialize(fs, gamefile);
            fs.Close();
            savedone.GetComponent<TMP_Text>().text = "保存成功了";
        }
        catch (Exception except)
        {
            savedone.GetComponent<TMP_Text>().text = "保存怎么失败了," + except;
        }
    }

    public List<Slot> LoadMapFile(ref GameObject readdirectory, ref GameObject readdone)
    {
        if (!Directory.Exists(SaveMapFilePath))
        {
            readdone.GetComponent<TMP_Text>().text = "地图文件夹不存在";
            Directory.CreateDirectory(SaveMapFilePath);
            return null;
        }
        string savefilename = readdirectory.GetComponent<TMP_InputField>().text;
        string fullpath = SaveMapFilePath + "/" + savefilename + ".save";
        if (!File.Exists(fullpath))
        {
            readdone.GetComponent<TMP_Text>().text = "文件不存在,读取失败";
            return null;
        }

        readdone.GetComponent<TMP_Text>().text = "读取中";
        try
        {
            List<Slot> readslot = JsonUtility.FromJson<List<Slot>>(fullpath);
            return readslot;
        }
        catch (Exception except)
        {
            readdone.GetComponent<TMP_Text>().text = "读取怎么失败了," + except;
        }
        return null;
    }
}
