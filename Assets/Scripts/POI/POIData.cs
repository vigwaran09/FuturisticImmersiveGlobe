using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class POI
{
    public string name;
    public float lat;
    public float lon;
    public string category;
    public string color;
    public List<POI> children;
    public List<float> chartData;
}


