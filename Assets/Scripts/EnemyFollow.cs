using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
  public Transform player; //playerのTransform
  public float speed = 4f; //敵の移動速度
  public float stopDistance = 1f;
  private bool isGameOver = false;

  void Update()
  {
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
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    //プレイヤーとぶつかったら
    if (collision.gameObject.CompareTag("Player"))
    {
      Debug.Log("Game Over!");
      isGameOver = true;

      //プレイヤーの動きを止める
      PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();
      if (playerScript != null)
      {
        playerScript.enabled = false;
      }

      // 敵の動きを止める
      this.enabled = false;
    }
  }
}
