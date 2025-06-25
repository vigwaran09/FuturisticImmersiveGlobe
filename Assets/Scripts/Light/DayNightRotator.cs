using UnityEngine;

public class DayNightRotator : MonoBehaviour
{
    public float rotationSpeed = 10f;

    void Update()
    {
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime, Space.World);
    }
}

