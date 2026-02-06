using UnityEngine;

public class BombCoreBehaviour : MonoBehaviour
{
    [Header("Bomb Settings")]
    [SerializeField] private GameObject bombCenterPrefab;
    [SerializeField] private float launchForce = 10f;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnDistance = 1.0f;

    private void Start()
    {
        SpawnAndLaunch();
    }

    private void SpawnAndLaunch()
    {
        if (bombCenterPrefab == null)
            return;

        // 向いている方向の逆（後ろ方向）
        Vector2 direction = -transform.up;

        // 指定距離だけ後方に生成
        Vector3 spawnPosition =
            transform.position + (Vector3)(direction * spawnDistance);

        GameObject bomb = Instantiate(
            bombCenterPrefab,
            spawnPosition,
            transform.rotation
        );

        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(direction * launchForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogWarning("BombCenterPrefab に Rigidbody2D がありません");
        }
    }
}
