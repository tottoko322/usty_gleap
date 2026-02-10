using UnityEngine;

public class OrbitCenterBehaviour : MonoBehaviour
{
    [Header("Orbit Settings")]
    [SerializeField] float radius = 2.5f;
    [SerializeField] int count = 3;

    [Header("Rotation")]
    [SerializeField] float rotateSpeed = 180f;

    [Header("Spawn")]
    [SerializeField] GameObject orbitChildPrefab;
    [SerializeField] GameObject centerObject;

    public void SetupCenter(Transform center)
    {
        centerObject = center != null ? center.gameObject : null;
        if (centerObject != null)
        {
            transform.position = centerObject.transform.position;
            transform.SetParent(centerObject.transform);
        }
    }

    void Start()
    {
        if (centerObject != null)
        {
            transform.position = centerObject.transform.position;
            transform.SetParent(centerObject.transform);
        }
        Spawn();
    }

    void Update()
    {
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    void Spawn()
    {
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = angleStep * i;
            float rad = angle * Mathf.Deg2Rad;

            Vector3 localPos = new Vector3(
                Mathf.Cos(rad) * radius,
                Mathf.Sin(rad) * radius,
                0f
            );

            GameObject child = Instantiate(
                orbitChildPrefab,
                transform
            );

            child.transform.localPosition = localPos;
        }
    }
}