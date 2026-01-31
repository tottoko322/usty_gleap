using System.Collections;
using UnityEngine;

public class BaritoneEnemyBehaviour : MonoBehaviour
{
    public GameObject noteBulletCenter;
    public float shotInterval = 0.5f;
    public float effectInterval = 0.5f;
    public float fireDistance = 5f;
    public float effectRadius = 3f;
    public float offsetDistance = 2f;
    public  LayerMask leadEnemyLayer;
    public LayerMask targetLayer;
    private Transform player; //playerのTransform
    private float speed; //敵の移動速度
    public float stopDistance = 1f;
    private bool isGameOver = false;
    private StatusManager statusManager;
    private StatusActionHolder statusActionHolder;
    private SelfStatusAction deathAction;
    private Coroutine shotRoutine;
    private Coroutine effectRoutine;

    void Start()
    {
        statusManager = GetComponent<StatusManager>();
        statusActionHolder = GetComponent<StatusActionHolder>();
        deathAction = statusActionHolder.GetSelfStatusActionFromIndex(0);
        player = GameObject.FindWithTag("Player").transform;
        effectRoutine = StartCoroutine(ApplyEffectLoop());
    }

    void Update()
    {
        speed = statusManager.GetSpeed();
        if (player == null || isGameOver ){
        if(GameObject.FindWithTag("Player") == null) return;
        player = GameObject.FindWithTag("Player").transform;
        return;
        }

        //playerの方向を取得
        Vector3 direction = GetDirectionToLeadEnemy();

        float distance = Vector3.Distance(player.position, transform.position);

        if(distance > stopDistance){
            Debug.Log("バリトンのスピード" + speed);
        //方向に向かって移動
        transform.position += direction * speed * Time.deltaTime;
        }

        HandleShootingByDistance();
        LookAtPlayer();
        //死の追加
        deathAction.Execute(this.gameObject);
    }

    Vector2 GetDirectionToLeadEnemy()
    {
        // 範囲内のLeadEnemyを取得
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            1000f,
            leadEnemyLayer
        );

        if (hits.Length == 0)
            return Vector2.zero; // 見つからなかった

        // 一番近いLeadEnemyを選ぶ
        Transform nearest = null;
        float minDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = hit.transform;
            }
        }

        // === ここだけ追加 ===
        float offset = offsetDistance; // ずらしたい距離
        Vector2 targetPoint =
            (Vector2)nearest.position +
            (Vector2)nearest.up * offset;
        // =====================

        // 方向ベクトルを返す
        return (targetPoint - (Vector2)transform.position).normalized;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //プレイヤーとぶつかったら
        if (collision.gameObject.CompareTag("Player") && false)
        {
        Debug.Log("Game Over!");
        isGameOver = true;

        //プレイヤーの動きを止める
        if (collision.gameObject.TryGetComponent<PlayerController>(out var playerScript))
        {
            playerScript.enabled = false;
        }

        // 敵の動きを止める
        this.enabled = false;
        }
        float currentAttack = statusManager.GetAttackPower();
        Debug.Log("敵の現在の攻撃力: "+currentAttack);
    }

    private void LookAtPlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;

        // Z軸回転だけを使う（2D用）
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    private void HandleShootingByDistance()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= fireDistance)
        {
            if (shotRoutine == null)
            {
                Debug.Log("バリトンコルーチン開始");
                shotRoutine = StartCoroutine(ShotLoop());
            }
        }
        else
        {
            if (shotRoutine != null)
            {
                StopCoroutine(shotRoutine);
                shotRoutine = null;
            }
        }
    }
    private IEnumerator ShotLoop()
    {
        while (true)
        {
            ShotNoteBullet();
            yield return new WaitForSeconds(shotInterval);
        }
    }
    private void ShotNoteBullet()
    {
        float spawnOffset = 0.5f; // 自分の前に出す距離（調整用）

        // 自分の向いている方向の「少し前」の位置
        Vector3 spawnPos = transform.position + transform.right * spawnOffset;

        Instantiate(noteBulletCenter, spawnPos, transform.rotation);
        Debug.Log("バリトンが発射しました！");
    }

    private IEnumerator ApplyEffectLoop()
    {
        while (true)
        {
            ApplyEffectInRadius();
            yield return new WaitForSeconds(effectInterval);
        }
    }
    private void ApplyEffectInRadius()
    {
        // 半径内のすべてのコライダーを取得
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            effectRadius,
            targetLayer
        );

        foreach (var hit in hits)
        {
            Debug.Log($"[LeadEnemy] 検出: {hit.name} / Tag:{hit.tag} / Layer:{LayerMask.LayerToName(hit.gameObject.layer)}");
            // コンポーネント取得
            if (hit.GetComponentInParent<NoteBulletBehaviour>() is { } note)
            {
                note.SetIsBaritoneTrue();
            }
        }
    }
}
