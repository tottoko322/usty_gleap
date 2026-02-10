using UnityEngine;

public class BombCenterBehaviour : MonoBehaviour
{
    [Header("Explosion Settings")]
    [SerializeField] private GameObject projectilePrefab; // 発射するPrefab
    [SerializeField] private int spawnCount = 8;           // 個数
    [SerializeField] private float delay = 2.0f;           // 爆発までの秒数
    [SerializeField] private float spawnRadius = 0.5f;     // 生成半径（中心から少し離す用）

    private void Start()
    {
        Invoke(nameof(Explode), delay);
    }

    private void Explode()
    {
        if (projectilePrefab == null || spawnCount <= 0)
        {
            Destroy(gameObject);
            return;
        }

        float angleStep = 360f / spawnCount;

        for (int i = 0; i < spawnCount; i++)
        {
            float angle = angleStep * i;
            float rad = angle * Mathf.Deg2Rad;

            // 円周上の位置（2D想定 XY平面）
            Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f);

            Vector3 spawnPos = transform.position + dir * spawnRadius;

            // 外向きに回転
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, dir);

            Instantiate(projectilePrefab, spawnPos, rotation);
        }

        // 自分を破壊
        Destroy(gameObject);
    }
}
