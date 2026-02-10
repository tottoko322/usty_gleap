using UnityEngine;

public abstract class AbstractSpawner : MonoBehaviour
{
    [Header("Common Settings")]
    [SerializeField] protected float spawnInterval = 2f;

    protected float timer;

    protected virtual void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            OnSpawnRequest();
        }
    }

    // 子クラスに生成処理を委譲
    protected abstract void OnSpawnRequest();
}