using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class POILoader : MonoBehaviour
{
    public string jsonFileName = "poi_data.json";
    public List<POI> rootPOIs;

    void Awake()
    {
        LoadPOIData();
    }

    void LoadPOIData()
    {
        string path = Path.Combine(Application.streamingAssetsPath, jsonFileName);
        if (!File.Exists(path))
        {
            Debug.LogError("POI JSON not found: " + path);
            return;
        }
        string json = File.ReadAllText(path);
        rootPOIs = JsonHelper.FromJson<POI>(json);
    }
}

public static class JsonHelper
{
    public static List<T> FromJson<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return new List<T>(wrapper.array);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}
