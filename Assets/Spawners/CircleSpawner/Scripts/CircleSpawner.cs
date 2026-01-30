using UnityEngine;

public class CircleSpawner : AbstractSpawner, ISpawner
{
    [Header("Enemy Settings")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float radius = 5f;     // 生成する円の半径
    [SerializeField] private int spawnCount = 8;   // 生成する個数

    protected override void OnSpawnRequest()
    {
        // 内部呼び出しも外部呼び出しも同じ経路を通す
        Spawn();
    }

    public void Spawn()
    {
        if (enemyPrefab == null || spawnCount <= 0)
            return;

        float angleStep = 360f / spawnCount;

        for (int i = 0; i < spawnCount; i++)
        {
            float angle = angleStep * i;
            float rad = angle * Mathf.Deg2Rad;

            // 2D (XY平面)
            Vector3 offset = new Vector3(
                Mathf.Cos(rad),
                Mathf.Sin(rad),
                0f
            ) * radius;

            Vector3 spawnPos = transform.position + offset;

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }


#if UNITY_EDITOR
    // シーンビューで半径を可視化
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
#endif
}
