using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    private HitBoxHolder hitBoxHolder;
    private List<GameObject> hitBoxList;
    private GraveHolder graveHolder;
    private List<GameObject> graves;
    private int currentHitBoxIndex = 0;
    private InputAction fireAction;
    private InputAction scrollAction;
    private InputAction spaceAction;
    private bool isDestroyed = false;
    void Awake()
    {
        hitBoxHolder = gameObject.GetComponent<HitBoxHolder>();
        hitBoxList = hitBoxHolder.GetHitBoxList();
        graveHolder = gameObject.GetComponent<GraveHolder>();
        graves = graveHolder.GetGraves();

        // 攻撃用 InputAction(Button)
        fireAction = new InputAction(
            name: "Fire",
            type: InputActionType.Button,
            binding: "<Mouse>/leftButton" // マウス左クリック
        );

        fireAction.performed += OnFire;
        fireAction.Enable();

        // スクロール用 InputAction(Value)
        scrollAction = new InputAction(
            name: "Scroll",
            type: InputActionType.Value,
            binding: "<Mouse>/scroll"
        );

        scrollAction.performed += OnScroll;
        scrollAction.Enable();

        // スペースキー用 InputAction(Button)
        spaceAction = new InputAction(
            name: "Space",
            type: InputActionType.Button,
            binding: "<Keyboard>/space"
        );

        spaceAction.performed += OnSpace;
        spaceAction.Enable();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (isDestroyed) return;
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
        clone.transform.SetParent(transform);
        clone.transform.up = direction; // 2Dでは上方向をベクトルに合わせる
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        if (isDestroyed) return;
        Vector2 scroll = context.ReadValue<Vector2>();

        if (scroll.y > 0)
        {
            currentHitBoxIndex++;
        }
        else if (scroll.y < 0)
        {
            currentHitBoxIndex--;
        }

        // 範囲制限 or ループ
        if (currentHitBoxIndex >= hitBoxList.Count)
            currentHitBoxIndex = 0;
        if (currentHitBoxIndex < 0)
            currentHitBoxIndex = hitBoxList.Count - 1;
    }

    public void OnSpace(InputAction.CallbackContext context)
    {
        if (isDestroyed) return;
        if (!context.performed) return;
        if (graves.Count == 0) {
            DestroyMe();
            return;
        };

        // 自分の位置に生成
        Instantiate(graves[0], transform.position, Quaternion.identity);

        // 自分を破壊
        DestroyMe();
    }

    public void DestroyMe()
    {
        if (isDestroyed) return;
        isDestroyed = true;
        Destroy(gameObject);
    }
}
