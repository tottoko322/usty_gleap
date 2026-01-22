using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
  private Transform player; //playerのTransform
  public float speed = 4f; //敵の移動速度
  public float stopDistance = 1f;
  private bool isGameOver = false;
  private StatusManager statusManager;
  private StatusActionHolder statusActionHolder;
  private GenerateGrave generateGrave;
  private SelfStatusAction deathAction;
  void Start()
  {
    statusManager = GetComponent<StatusManager>();
    generateGrave = GetComponent<GenerateGrave>();
    statusActionHolder = GetComponent<StatusActionHolder>();
    deathAction = statusActionHolder.GetSelfStatusActionFromIndex(0);
    player = GameObject.FindWithTag("Player").transform;
  }

  void Update()
  {
    speed = statusManager.GetSpeed(); 
    if (player == null || isGameOver){
      return;
    }

    //playerの方向を取得
    Vector3 direction = (player.position - transform.position).normalized;

    float distance = Vector3.Distance(player.position, transform.position);

    if(distance > stopDistance){
      //方向に向かって移動
      transform.position += direction * speed * Time.deltaTime;
    }

    //お墓の生成
    if (statusManager.BaseStatus.CurrentHP <= 0)
    {
      generateGrave.Generate(transform.position);
    }

    //死の追加
    deathAction.Execute(this.gameObject);
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
}
