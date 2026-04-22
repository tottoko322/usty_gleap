using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, PlayerInputActions.IPlayerActions
{
    private Vector2 moveInput;
    private PlayerInputActions inputActions;

    private PlayerMove playerMove;

    void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.AddCallbacks(this);

        // 分離したコンポーネントを取得
        playerMove = GetComponent<PlayerMove>();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        // Debug.Log("MoveInput: " + moveInput);

        // Move担当へ渡す
        if (playerMove != null) playerMove.SetMoveInput(moveInput);
    }

    void Update()
    {
        // 移動担当に処理を委譲
        if (playerMove != null) playerMove.TickMove();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // look処理
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        // attack処理
    }

    public void OnSelfDistruction(InputAction.CallbackContext context)
    {
        // self destruction処理
    }

    public void OnChangeWeapon(InputAction.CallbackContext context)
    {
        // weapon切り替え
    }

    public void OnChangeGrave(InputAction.CallbackContext context){}
}
