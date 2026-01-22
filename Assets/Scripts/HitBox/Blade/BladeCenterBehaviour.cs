using UnityEngine;

public class BladeCenterBehaviour : MonoBehaviour
{
    [Header("Orbit Settings")]
    [SerializeField] float radius = 2.5f;
    [SerializeField] int count = 1;

    [Header("Rotation")]
    [SerializeField] float rotateSpeed = 180f;
    [SerializeField] float rotateRange = 90f;

    [Header("Spawn")]
    [SerializeField] GameObject orbitChildPrefab;
    private GameObject centerObject;
    private float rotatedAmount = 0f;

    void Start()
    {
        centerObject = gameObject;
        if (centerObject != null)
        {
            transform.position = centerObject.transform.position;
            transform.SetParent(centerObject.transform);
        }
        Spawn();
    }

    void Update()
    {
        // このフレームで回転する量
        float deltaRotation = rotateSpeed * Time.deltaTime;

        // 回転を加算
        transform.Rotate(0f, 0f, deltaRotation);
        rotatedAmount += Mathf.Abs(deltaRotation);

        // 回転範囲を超えたら自滅
        if (rotatedAmount >= rotateRange)
        {
            Destroy(gameObject);
        }
    }

    void Spawn()
    {
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            // 角度を半分ずらして回転開始
            float angle = -rotateRange / 2 + angleStep * i;
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
