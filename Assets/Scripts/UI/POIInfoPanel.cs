using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class POIInfoPanel : MonoBehaviour
{
    [Header("UI Bindings")]
    public TextMeshProUGUI POI_Name;
    public TextMeshProUGUI Category;
    public TextMeshProUGUI Coordinates;
    public Transform Charts;
    public Button Close;

    [Header("Chart images being filed by JSON data")]
    public List<Image> chartBars;
    
    [Header("Teleport SFX")]
    public AudioSource teleportSFX;

    void Awake()
    {
        if (Close) Close.onClick.AddListener(() => gameObject.SetActive(false));
    }

    public void Show(POI poi)
    {
        if(teleportSFX)
            teleportSFX.Play();
        gameObject.SetActive(true);
        POI_Name.text = poi.name;
        Category.text = poi.category;
        Coordinates.text = poi.lat.ToString() + " , " + poi.lon.ToString();
        
        if (poi.chartData != null && chartBars != null && chartBars.Count > 0)
        {
            float max = 1f;
            if (poi.chartData.Count > 0) max = Mathf.Max(poi.chartData.ToArray());
            for (int i = 0; i < chartBars.Count; i++)
            {
                if (i < poi.chartData.Count)
                {
                    float norm = max > 0 ? poi.chartData[i] / max : 0;
                    chartBars[i].fillAmount = norm;
                    chartBars[i].color = Color.cyan;
                    chartBars[i].gameObject.SetActive(true);
                }
                else
                {
                    chartBars[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            foreach (var bar in chartBars) bar.gameObject.SetActive(false);
        }
    }
}
