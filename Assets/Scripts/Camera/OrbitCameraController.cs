using UnityEngine;
using System.Collections;

public class OrbitCameraController : MonoBehaviour
{
    public Transform target;
    public float distance = 10.0f;
    public float zoomMin = 4.0f;
    public float zoomMax = 25.0f;
    public float rotationSpeed = 5.0f;
    public float zoomSpeed = 5.0f;

    private float _yaw = 0.0f;
    private float _pitch = 0.0f;

    void Start()
    {
        if (!target)
            return;
        var offset = (transform.position - target.position).normalized;
        _yaw = Mathf.Atan2(offset.x, offset.z) * Mathf.Rad2Deg;
        _pitch = Mathf.Asin(offset.y) * Mathf.Rad2Deg;
    }

    void LateUpdate()
    {
        if (!target) return;


        if (Input.GetMouseButton(0))
        {
            _yaw += Input.GetAxis("Mouse X") * rotationSpeed;
            _pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
            _pitch = Mathf.Clamp(_pitch, -89.99f, 89.99f);
        }
        
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            distance = Mathf.Clamp(distance - scroll * zoomSpeed, zoomMin, zoomMax);
        }
        
        float yawRad = Mathf.Deg2Rad * _yaw;
        float pitchRad = Mathf.Deg2Rad * _pitch;
        var direction = new Vector3(
            Mathf.Cos(pitchRad) * Mathf.Sin(yawRad),
            Mathf.Sin(pitchRad),
            Mathf.Cos(pitchRad) * Mathf.Cos(yawRad)
            );

        transform.position = target.position + direction * distance;
        transform.LookAt(target.position, Vector3.up);
    }
    
    public void FocusOnPoint(Vector3 worldPosition, float targetDistance = 1.1f, float duration = 1f)
    {
        StartCoroutine(SmoothFocus(worldPosition, targetDistance, duration));
    }

    IEnumerator SmoothFocus(Vector3 worldPosition, float targetDistance, float duration)
    {
        var dir = (worldPosition - target.position).normalized;
        
        float targetPitch = Mathf.Asin(dir.y) * Mathf.Rad2Deg;
        float targetYaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        float startYaw = _yaw;
        float startPitch = _pitch;
        float startDistance = distance;
        float deltaYaw = Mathf.DeltaAngle(startYaw, targetYaw);
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / duration);
            _yaw = startYaw + deltaYaw * t;
            _pitch = Mathf.Lerp(startPitch, targetPitch, t);
            distance = Mathf.Lerp(startDistance, targetDistance, t);
            yield return null;
        }
        _yaw = targetYaw;
        _pitch = targetPitch;
        distance = targetDistance;
    }
}
