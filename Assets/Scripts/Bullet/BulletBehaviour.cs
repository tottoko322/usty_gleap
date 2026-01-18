using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private StatusActionHolder statusActionHolder;
    private StatusManager statusManager;
    private TargetStatusAction attackAction;
    private Rigidbody2D rb;

    void Awake()
    {
        statusActionHolder = GetComponent<StatusActionHolder>();
        statusManager = GetComponent<StatusManager>();
        rb = GetComponent<Rigidbody2D>();

        // AttackAction を取得（インデックス0を想定）
        attackAction = statusActionHolder.GetTargetStatusActionFromIndex(0);
    }

    void Update()
    {
        // ①毎フレームごとにTransform.rotationの方向に進む
        // StatusHolderからGetComponentして、BaseStatusからSpeedを取得
        float speed = statusManager.BaseStatus.BaseSpeed;
        
        // Transform.rotationの方向を取得
        Vector2 direction = transform.right; // rotationに応じた前方向
        
        // 速度を設定して移動
        rb.linearVelocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // ②Enemyのオブジェクトにぶつかったときに、ダメージを与えてから消滅
        if (collision.TryGetComponent<IHasStatusManager>(out var hasStatus))
        {
            // AttackActionを用いて攻撃
            attackAction.Execute(this.gameObject, collision.gameObject);
            
            // Bullet自身を破壊
            Destroy(this.gameObject);
        }
    }
}
