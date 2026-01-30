using UnityEngine;

public class PositionSpawner : AbstractSpawner, ISpawner
{
    [Header("Enemy Settings")]
    [SerializeField] private GameObject enemyPrefab;

    protected override void OnSpawnRequest()
    {
        // 内部呼び出しも外部呼び出しも同じ経路を通す
        Spawn();
    }

    public void Spawn()
    {
        Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);
    }
}
