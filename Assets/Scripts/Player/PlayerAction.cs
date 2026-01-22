using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    private HitBoxHolder hitBoxHolder;
    private List<GameObject> hitBoxList;
    private int currentHitBoxIndex = 0;
    private InputAction fireAction;
    void Awake()
    {
        hitBoxHolder = gameObject.GetComponent<HitBoxHolder>();
        hitBoxList = hitBoxHolder.GetHitBoxList();

        // 攻撃用 InputAction(Button)
        fireAction = new InputAction(
            name: "Fire",
            type: InputActionType.Button,
            binding: "<Mouse>/leftButton" // マウス左クリック
        );

        fireAction.performed += OnFire;

        fireAction.Enable();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        // クリックが押されたときだけ処理
        if (!context.performed) return;

        // マウス座標をスクリーン空間で取得
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        // スクリーン座標 → ワールド座標
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        // Z座標は2Dなら0に固定
        mouseWorldPos.z = 0f;

        // 自分（このオブジェクト）の位置
        Vector3 myPos = transform.position;

        // 自分→マウス方向のベクトル
        Vector3 direction = -(mouseWorldPos - myPos).normalized;

        GameObject clone = Instantiate(hitBoxList[currentHitBoxIndex], transform.position, Quaternion.identity);
        clone.transform.up = direction; // 2Dでは上方向をベクトルに合わせる
    }
}
