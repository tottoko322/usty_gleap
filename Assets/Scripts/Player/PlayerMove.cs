using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private StatusManager statusManager;
    private float speed;

    private Vector2 moveInput;

    // ���͒l��n��
    public void SetMoveInput(Vector2 input) => moveInput = input;

    void Start()
    {
        statusManager = GetComponent<StatusManager>();
    }

    // Update����Ă�
    public void TickMove()
    {
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f);
        speed = statusManager.GetSpeed();
        transform.position += movement * speed * Time.deltaTime;
    }
}
