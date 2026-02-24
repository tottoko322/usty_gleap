using System;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float force = 5f;
    private Vector2 myPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoKnockback(GameObject oppositeObject)
    {
        //通知
        Debug.Log("ノックバック！");
        // 自分の座標（攻撃者）
        Vector2 myPosition = transform.position;

        // 相手の Rigidbody2D を取得
        Rigidbody2D oppositeRb = oppositeObject.GetComponent<Rigidbody2D>();
        if(oppositeRb == null) return; // Rigidbody2D がなければ処理しない

        // 相手の座標
        Vector2 oppositePos = oppositeRb.position;

        // ノックバック方向（攻撃者から反対方向）
        Vector2 knockbackDir = (oppositePos - myPosition).normalized;

        // 力を加える
        oppositeRb.AddForce(knockbackDir * force, ForceMode2D.Impulse);
    }

}

//RigidBody2Dを必要とする。
