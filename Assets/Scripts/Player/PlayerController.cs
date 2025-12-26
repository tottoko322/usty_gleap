using UnityEngine;
//追加
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, PlayerInputActions.IPlayerActions
{
  public float speed = 5f;

  //追加
  private Vector2 moveInput;
  private PlayerInputActions inputActions;
  //ノックバック用の機能クラス
  private Knockback knockback;
  private StatusActionHolder statusActionHolder;
  private TargetStatusAction attackAction;
  void Awake()
  {
    inputActions = new PlayerInputActions();
    inputActions.Player.AddCallbacks(this);
    //StatusActionの初期設定
    knockback = GetComponent<Knockback>();
    statusActionHolder = GetComponent<StatusActionHolder>();
    attackAction = statusActionHolder.GetTargetStatusActionFromIndex(0);
  }

  void OnEnable()
  {
    inputActions.Player.Enable();
  }

  void OnDisable()
  {
      inputActions.Player.Disable();
  }


  // PlayerInput から呼ばれる
  public void OnMove(InputAction.CallbackContext context)
  {
    moveInput = context.ReadValue<Vector2>();
    Debug.Log("MoveInput: " + moveInput);
  }

  void Update()
  {
    // //入力を取得(旧バージョン)
    // float moveX = Input.GetAxis("Horizontal"); // -1 ~ 1
    // float moveY = Input.GetAxis("Vertical");

    //移動量を計算
    Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f);

    //現在位置に移動を加える
    transform.position += movement * speed * Time.deltaTime;

  }
  void OnCollisionEnter2D(Collision2D other)
  {
    GameObject otherGameObject = other.gameObject;
    knockback.DoKnockback(otherGameObject);
    attackAction.Execute(this.gameObject,otherGameObject);
  }
}
