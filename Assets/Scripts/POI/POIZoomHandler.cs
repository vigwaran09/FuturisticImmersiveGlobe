using UnityEngine;

public class POIZoomHandler : MonoBehaviour
{
    private POISpawner spawner;
    public POI myPOI;

    void Start()
    {
        spawner = GetComponentInParent<POISpawner>();
    }

    void OnMouseDown()
    {
        if (spawner)
            spawner.ZoomToMarker(transform.position);
        var infoPanel = spawner.infoPanel;
        if (infoPanel)
            infoPanel.Show(myPOI);
    }
}
