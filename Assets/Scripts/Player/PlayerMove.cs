using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 moveInput;

    // “ü—Í’l‚ð“n‚·
    public void SetMoveInput(Vector2 input) => moveInput = input;

    // Update‚©‚çŒÄ‚Ô
    public void TickMove()
    {
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f);
        transform.position += movement * speed * Time.deltaTime;
    }
}
