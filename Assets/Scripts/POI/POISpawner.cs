using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POISpawner : MonoBehaviour
{
    [Header("Marker & Globe")]
    public GameObject markerPrefab;
    public float globeRadius = 5f;
    public OrbitCameraController orbitCameraController;

    [Header("Zoom Thresholds")]
    public float highZoomThreshold = 1.3f;
    public float lowZoomThreshold = 2.0f;

    [Header("Zoom Animation")]
    public float zoomDuration = 1f;
    public float zoomDistance = 1.8f;
    public float minZoomDistance = 1.1f;

    [Header("UI Bindings")]
    public POIInfoPanel infoPanel;

    private Transform _mainCamera;
    private POILoader _poiLoader;

    private List<GameObject> _continentMarkers = new();
    private List<GameObject> _countryMarkers = new();
    private List<GameObject> _cityMarkers = new();

    private Coroutine zoomCoroutine = null;

    void Start()
    {
        _mainCamera = Camera.main.transform;
        _poiLoader = FindObjectOfType<POILoader>();
        if (!_poiLoader) return;

        foreach (var continent in _poiLoader.rootPOIs)
        {
            var cont = SpawnPOI(continent);
            _continentMarkers.Add(cont);

            if (continent.children != null)
            {
                foreach (var country in continent.children)
                {
                    var count = SpawnPOI(country);
                    _countryMarkers.Add(count);

                    if (country.children != null)
                    {
                        foreach (var city in country.children)
                        {
                            var cityGO = SpawnPOI(city);
                            _cityMarkers.Add(cityGO);
                        }
                    }
                }
            }
        }
    }

    void Update()
    {
        UpdatePOIVisibility();
    }

    GameObject SpawnPOI(POI poi)
    {
        Vector3 pos = LatLonToPosition(poi.lat, poi.lon, globeRadius);
        GameObject marker = Instantiate(markerPrefab, pos, Quaternion.identity, this.transform);
        var zoomHandler = marker.GetComponent<POIZoomHandler>();
        if (zoomHandler) 
            zoomHandler.myPOI = poi;
        marker.name = poi.name;
        
        if (!string.IsNullOrEmpty(poi.color))
        {
            if (ColorUtility.TryParseHtmlString(poi.color, out var poiColor))
            {
                var renderer = marker.GetComponentInChildren<Renderer>();
                if (renderer != null && renderer.material.HasProperty("_EmissionColor"))
                    renderer.material.SetColor("_EmissionColor", poiColor);
            }
        }

        if (!marker.TryGetComponent<POIZoomHandler>(out _))
            marker.AddComponent<POIZoomHandler>();

        return marker;
    }

    Vector3 LatLonToPosition(float lat, float lon, float radius)
    {
        float phi = Mathf.Deg2Rad * (90f - lat);
        float theta = Mathf.Deg2Rad * (lon + 180f);

        float x = radius * Mathf.Sin(phi) * Mathf.Cos(theta);
        float y = radius * Mathf.Cos(phi);
        float z = radius * Mathf.Sin(phi) * Mathf.Sin(theta);

        return new Vector3(x, y, z);
    }

    void UpdatePOIVisibility()
    {
        float distance = Vector3.Distance(transform.position, _mainCamera.position);

        bool showContinents = distance >= lowZoomThreshold;
        bool showCountries = distance >= highZoomThreshold && distance < lowZoomThreshold;
        bool showCities = distance < highZoomThreshold;

        SetActiveForList(_continentMarkers, showContinents);
        SetActiveForList(_countryMarkers, showCountries);
        SetActiveForList(_cityMarkers, showCities);
    }

    void SetActiveForList(List<GameObject> list, bool state)
    {
        foreach (var obj in list)
        {
            if (obj && obj.activeSelf != state)
                obj.SetActive(state);
        }
    }


    IEnumerator RotateToMarker(Vector3 markerPosition, System.Action onComplete = null)
    {
        var startRot = transform.rotation;
        var targetDir = (markerPosition - transform.position).normalized;
        var forward = -_mainCamera.forward;
        var endRot = Quaternion.FromToRotation(targetDir, forward) * startRot;

        float elapsed = 0f;
        float duration = 0.6f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / duration);
            transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }

        transform.rotation = endRot;
        onComplete?.Invoke();
    }

    public void ZoomToMarker(Vector3 markerPosition)
    {
        orbitCameraController.FocusOnPoint(markerPosition, 1.1f, 1f);
    }

    IEnumerator SmoothZoomTo(Vector3 markerPosition)
    {
        float elapsed = 0f;
        var start = transform.position;
        var dir = (markerPosition - _mainCamera.position).normalized;
        var target = _mainCamera.position + dir * minZoomDistance;

        while (elapsed < zoomDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / zoomDuration);
            float easedT = EaseInOutQuad(t);
            transform.position = Vector3.Lerp(start, target, easedT);
            yield return null;
        }

        transform.position = target;
        zoomCoroutine = null;
    }

    float EaseInOutQuad(float t)
    {
        return t < 0.5f ? 2f * t * t : 1f - Mathf.Pow(-2f * t + 2f, 2f) / 2f;
    }
}
