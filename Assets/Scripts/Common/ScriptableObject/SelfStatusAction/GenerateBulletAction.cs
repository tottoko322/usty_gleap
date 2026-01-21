using UnityEngine;

[CreateAssetMenu(fileName = "GenerateBulletAction", menuName = "SelfStatusAction/GenerateBulletAction")]
public class GenerateBulletAction : SelfStatusAction
{
    [SerializeField] private GameObject bulletPrefab;

    public override void Execute(GameObject user)
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet Prefab is not assigned!");
            return;
        }

        // Bullet を生成
        // プレハブには以下がアタッチされていることを前提：
        // - StatusManager
        // - StatusHolder (BaseStatusSO と BuffStatusSO が設定済み)
        // - StatusActionHolder (TargetStatusAction が設定済み)
        // - BulletBehaviour
        // - Rigidbody2D (Is Trigger: true)
        // - Collider2D
        Instantiate(
            bulletPrefab,
            user.transform.position,
            user.transform.rotation
        );
    }
}
