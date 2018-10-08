using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

/*
        参考：http://magnaga.com/unity/2016/04/25/unity-save2/
        //セーブの方法
        var saveTest = new PlayerSaveData();
        saveTest.SetItemIDs();
        saveTest.setStorageItemIDs();
        SaveData.SetClass<PlayerSaveData>("p1", saveTest);
        SaveData.Save();
*/
public class SaveData
{
    static SaveDataBase saveDB = null;
    static SaveDataBase SaveDB
    {
        get
        {
            if (saveDB == null)
            {
                var path = Application.persistentDataPath + "/";
                var fileName = Application.companyName + "." + Application.productName + ".savedata.json";
                saveDB = new SaveDataBase(path, fileName);
            }
            return saveDB;
        }
    }

    public static void SetList<T>(string key, List<T> list)
    {
        SaveDB.SetList<T>(key, list);
    }

    public static List<T> GetList<T>(string key, List<T> _default)
    {
        return SaveDB.GetList<T>(key, _default);

    }

    public static void SetClass<T>(string key, T obj) where T : class, new()
    {
        SaveDB.SetClass<T>(key, obj);
    }

    public static T GetClass<T>(string key, T _default) where T : class, new()
    {
        return SaveDB.GetClass<T>(key, _default);
    }

    public static void SetString(string key, string value)
    {
        SaveDB.SetString(key, value);
    }

    public static string GetString(string key, string _default = "")
    {
        return SaveDB.GetString(key, _default);
    }

    public static void SetInt(string key, int value)
    {
        SaveDB.SetInt(key, value);
    }

    public static int GetInt(string key, int _default = 0)
    {
        return SaveDB.GetInt(key, _default);
    }

    public static void Clear()
    {
        SaveDB.Clear();
    }

    public static void Remove(string key)
    {
        SaveDB.Remove(key);
    }

    public static bool ContainsKey(string key)
    {
        return SaveDB.ContainsKey(key);
    }

    public static List<string> Keys()
    {
        return SaveDB.Keys();
    }

    public static void Save()
    {
        SaveDB.Save();
    }

    [Serializable]
    class SaveDataBase
    {
        #region Field

        //保存先
        public string Path { get; set; }

        //ファイル名
        public string FileName { get; set; }

        Dictionary<string, string> saveDictionary;

        #endregion

        #region Constructor&Destructor

        public SaveDataBase(string _path, string _fileName)
        {
            Path = _path;
            FileName = _fileName;
            saveDictionary = new Dictionary<string, string>();
            Load();
        }

        ~SaveDataBase()
        {
            Save();
        }

        #endregion


        #region Method

        public void SetList<T>(string key, List<T> list)
        {
            keyCheck(key);
            var seriallizebleList = new Serialization<T>(list);
            saveDictionary[key] = JsonUtility.ToJson(seriallizebleList);
        }

        public List<T> GetList<T>(string key, List<T> _default)
        {
            keyCheck(key);
            if (!saveDictionary.ContainsKey(key)) { return _default; }
            var json = saveDictionary[key];
            var deserializeList = JsonUtility.FromJson<Serialization<T>>(json);
            return deserializeList.ToList();
        }

        public void SetClass<T>(string key, T obj) where T : class, new()
        {
            keyCheck(key);
            saveDictionary[key] = JsonUtility.ToJson(obj);
        }

        public T GetClass<T>(string key, T _default) where T : class, new()
        {
            keyCheck(key);
            if (!saveDictionary.ContainsKey(key)) { return _default; }
            var json = saveDictionary[key];
            return JsonUtility.FromJson<T>(json);
        }

        public void SetString(string key, string value)
        {
            keyCheck(key);
            saveDictionary[key] = value;
        }

        public string GetString(string key, string _default)
        {
            keyCheck(key);
            if (!saveDictionary.ContainsKey(key)) { return _default; }
            return saveDictionary[key];
        }

        public void SetInt(string key, int value)
        {
            keyCheck(key);
            saveDictionary[key] = value.ToString();
        }

        public int GetInt(string key, int _default)
        {
            keyCheck(key);
            if (!saveDictionary.ContainsKey(key)) { return _default; }
            int ret;
            if (!int.TryParse(saveDictionary[key], out ret)) { ret = 0; }
            return ret;
        }

        public void Clear()
        {
            saveDictionary.Clear();
        }

        public void Remove(string key)
        {
            keyCheck(key);
            if (saveDictionary.ContainsKey(key)) { saveDictionary.Remove(key); }
        }

        public bool ContainsKey(string key)
        {
            return saveDictionary.ContainsKey(key);
        }

        public List<string> Keys()
        {
            return saveDictionary.Keys.ToList<string>();
        }

        public void Save()
        {
            using (StreamWriter writer = new StreamWriter(Path + FileName, false, Encoding.GetEncoding("utf-8")))
            {
                var serialDict = new Serialization<string, string>(saveDictionary);
                serialDict.OnBeforeSerialize();
                var dictJsonString = JsonUtility.ToJson(serialDict);
                writer.WriteLine(dictJsonString);
            }
        }

        public void Load()
        {
            if (File.Exists(Path + FileName))
            {
                using (StreamReader sr = new StreamReader(Path + FileName, Encoding.GetEncoding("utf-8")))
                {
                    if (saveDictionary != null)
                    {
                        var stringDict = JsonUtility.FromJson<Serialization<string, string>>(sr.ReadToEnd());
                        stringDict.OnAfterDeSerialize();
                        saveDictionary = stringDict.ToDictionary();
                    }
                }
            }
            else { saveDictionary = new Dictionary<string, string>(); }
        }

        public string GetJsonString(string key)
        {
            keyCheck(key);
            if (saveDictionary.ContainsKey(key)) { return saveDictionary[key]; }
            else { return null; }
        }

        void keyCheck(string key)
        {
            if (string.IsNullOrEmpty(key)) { throw new ArgumentException("invalid key!!"); }
        }

        #endregion

    }

    [Serializable]
    class Serialization<T>
    {
        public List<T> Target;

        public List<T> ToList() { return Target; }

        public Serialization() { }
        public Serialization(List<T> target) { Target = target; }
    }

    [Serializable]
    class Serialization<TKey, TValue>
    {
        public List<TKey> Keys;
        public List<TValue> Values;
        Dictionary<TKey, TValue> target;

        public Dictionary<TKey, TValue> ToDictionary() { return target; }

        public Serialization() { }
        public Serialization(Dictionary<TKey, TValue> _target) { target = _target; }

        public void OnBeforeSerialize()
        {
            Keys = new List<TKey>(target.Keys);
            Values = new List<TValue>(target.Values);
        }

        public void OnAfterDeSerialize()
        {
            int count = Math.Min(Keys.Count, Values.Count);
            target = new Dictionary<TKey, TValue>(count);
            Enumerable.Range(0, count).ToList().ForEach(i => target.Add(Keys[i], Values[i]));
        }
    }

}

