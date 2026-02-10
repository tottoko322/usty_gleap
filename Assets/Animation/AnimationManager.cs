using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class AnimationManager : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private bool isJumping = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 動いているかどうか
        bool isMoving = rb.linearVelocity.sqrMagnitude > 0.1f;

        // ★ 動き始めた瞬間だけ Trigger を撃つ
        if (isMoving && !isJumping)
        {
            anim.SetTrigger("Jump"); // ← Trigger名（スペースNG）
            isJumping = true;
        }

        // ★ 止まったらリセット
        if (!isMoving)
        {
            isJumping = false;
        }
    }
}
