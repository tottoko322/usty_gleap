using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  public Transform player; //playerの位置
  //public float smoothspeed = 5f; //カメラ追従の滑らかさ(カメラを滑らかに追従したい場合に必要)

  //playerの動きに遅れて追従させる場合
  /*
  void LateUpdate()
  {
    if (player == null){
      return;
    }

    //playerの位置を取得
    Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

    //スムーズにカメラを追従させる
    transform.position = Vector3.Lerp(transform.position, targetPosition, smoothspeed * Time.deltaTime);
  }
  */

  void Update()
  {
    if (player == null){
      return;
    }

    //playerにカメラを固定
    transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
  }
}