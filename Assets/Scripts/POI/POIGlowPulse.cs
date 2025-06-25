using UnityEngine;

public class POIGlowPulse : MonoBehaviour
{
    private Material _mat;
    private float _pulseSpeed = 1f;
    private Color _baseEmission;

    void Start()
    {
        _mat = GetComponent<Renderer>().material;
        _baseEmission = _mat.GetColor("_EmissionColor");
    }

    void Update()
    {
        float pulse = (Mathf.Sin(Time.time * _pulseSpeed) + 1f) * 0.5f;
        _mat.SetColor("_EmissionColor", _baseEmission * (0.7f + 0.6f * pulse));
    }
}
