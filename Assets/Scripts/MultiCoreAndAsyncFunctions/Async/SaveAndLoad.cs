using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;

public class SaveAndLoad
{
    readonly string SaveMapFilePath = Application.dataPath + "/SaveMaps";
    readonly string SaveSlotFilePath = Application.dataPath + "/SaveSlots";
    readonly string LogFilePath = Application.dataPath + "/Logs";
    readonly string fileName;

    public SaveAndLoad(string file_name)
    {
        fileName = file_name;
        if (!Directory.Exists(SaveMapFilePath))
        {
            Directory.CreateDirectory(SaveMapFilePath);
        }
        if (!Directory.Exists(SaveSlotFilePath))
        {
            Directory.CreateDirectory(SaveSlotFilePath);
        }
        if (!Directory.Exists(LogFilePath))
        {
            Directory.CreateDirectory(LogFilePath);
        }
    }

    public async Awaitable<int> SaveMapSerializedFileAsync(
        SavingDatum map_object,
        params bool[] cover
    )
    {
        string full_path = SaveMapFilePath + "/" + fileName + ".serialized";
        if (File.Exists(full_path) || cover.Length > 0 || !cover[0])
        {
            await File.AppendAllTextAsync(
                $"{LogFilePath}WriteSerializeMapLog.txt",
                "File Exist;\r\n"
            );
            return 0;
        }
        FileStream fs = new(full_path, FileMode.Create);
        BinaryFormatter bf = new();
        int return_code = 0;
        await Task.Run(async () =>
        {
            try
            {
                bf.Serialize(fs, map_object);
                fs.Close();
                return_code = 1;
            }
            catch (Exception except)
            {
                await File.AppendAllTextAsync(
                    $"{LogFilePath}WriteSerializeMapLog.txt",
                    except.ToString() + "\r\n"
                );
                fs.Close();
            }
        });
        return return_code;
    }

    public async Awaitable<int> SaveMapJSONFileAsync(SavingDatum map_object, bool cover)
    {
        string full_path = SaveMapFilePath + "/" + fileName + ".json";
        if (File.Exists(full_path) && !cover)
        {
            await File.AppendAllTextAsync($"{LogFilePath}/WriteJSONMapLog.txt", "File Exist;\r\n");
            return 0;
        }
        map_object.MapToSerializeSlot();
        int return_code = 0;
        try
        {
            await File.WriteAllTextAsync(full_path, JsonUtility.ToJson(map_object));
            return_code = 1;
        }
        catch (Exception except)
        {
            await File.AppendAllTextAsync(
                $"{LogFilePath}/WriteJSONMapLog.txt",
                except.ToString() + "\r\n"
            );
        }
        return return_code;
    }

    public async Awaitable<int> SaveSlotFileAsync(object game_file, bool cover)
    {
        string full_path = SaveSlotFilePath + "/" + DateTime.Now.ToFileTimeUtc() + ".save";
        BinaryFormatter bin_for = new();
        int return_code = 0;
        await Task.Run(async () =>
        {
            try
            {
                FileStream fs = new(full_path, FileMode.Create);
                bin_for.Serialize(fs, game_file);
                fs.Close();
                return_code = 1;
            }
            catch (Exception except)
            {
                await File.AppendAllTextAsync(
                    $"{LogFilePath}/WriteSaveSlotLog.txt",
                    except.ToString() + "\r\n"
                );
            }
        });
        return return_code;
    }

    public async Awaitable<SavingDatum> LoadMapSerializedFileAsync()
    {
        string full_path = SaveMapFilePath + "/" + fileName + ".serialized";
        if (!File.Exists(full_path))
        {
            await File.AppendAllTextAsync(
                $"{LogFilePath}/WriteSerializeMapLog.txt",
                "File Exist;\r\n"
            );
            return null;
        }

        BinaryFormatter bin_for = new();
        try
        {
            FileStream fs = File.Open(full_path, FileMode.Open);
            SavingDatum sd = (SavingDatum)bin_for.Deserialize(fs);
            return sd;
        }
        catch (Exception except)
        {
            await File.AppendAllTextAsync(
                $"{LogFilePath}/WriteSerializeMapLog.txt",
                except.ToString() + "\r\n"
            );
            return null;
        }
    }

    public async Awaitable<SavingDatum> LoadMapJSONFileAsync()
    {
        string full_path = SaveMapFilePath + "/" + fileName + ".json";
        if (!File.Exists(full_path))
        {
            await File.AppendAllTextAsync($"{LogFilePath}/LoadJSONMapLog.txt", "File Exist;");
            return null;
        }
        try
        {
            string json_str = File.ReadAllText(full_path);
            return JsonUtility.FromJson<SavingDatum>(json_str);
        }
        catch (Exception except)
        {
            await File.AppendAllTextAsync(
                $"{LogFilePath}/LoadJSONMapLog.txt",
                except.ToString() + "\r\n"
            );
            return null;
        }
    }
}
