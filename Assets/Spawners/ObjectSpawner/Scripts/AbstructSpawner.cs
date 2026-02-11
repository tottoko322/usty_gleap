using UnityEngine;

public abstract class AbstractSpawner : MonoBehaviour
{
    [Header("Common Settings")]
    [SerializeField] protected float spawnInterval = 2f;

    [Header("Life Time Settings")]
    [SerializeField] private float lifeTime = 10f; // ← インスペクターで指定（秒）

    protected float timer;
    private float lifeTimer;

    protected virtual void Update()
    {
        // ===== 生成タイマー =====
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            OnSpawnRequest();
        }

        // ===== 自己破壊タイマー =====
        lifeTimer += Time.deltaTime;

        if (lifeTimer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    // 子クラスに生成処理を委譲
    protected abstract void OnSpawnRequest();
}
