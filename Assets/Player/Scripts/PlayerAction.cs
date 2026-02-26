using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    private WeaponCoreHolder weaponCoreHolder;
    private List<GameObject> weaponCoreList;
    private GraveHolder graveHolder;
    private List<GameObject> graves;
    private int currentWeaponCoreIndex = 0;
    private InputAction fireAction;
    private InputAction scrollAction;
    private InputAction spaceAction;
    private bool isDestroyed = false;
    void Start()
    {
        weaponCoreHolder = gameObject.GetComponent<WeaponCoreHolder>();
        weaponCoreList = weaponCoreHolder.GetWeaponCoreList();
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
    void OnDisable()
    {
        if (fireAction != null)
        {
            fireAction.performed -= OnFire;
            fireAction.Disable();
            fireAction.Dispose();
        }

        if (scrollAction != null)
        {
            scrollAction.performed -= OnScroll;
            scrollAction.Disable();
            scrollAction.Dispose();
        }

        if (spaceAction != null)
        {
            spaceAction.performed -= OnSpace;
            spaceAction.Disable();
            spaceAction.Dispose();
        }
    }


    public void OnFire(InputAction.CallbackContext context)
    {
        if (isDestroyed) return;
        // クリックが押されたときだけ処理
        if (!context.performed) return;
        // 武器がなければ処理しない
        if (weaponCoreList == null || weaponCoreList.Count == 0) return;

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

        GameObject clone = Instantiate(weaponCoreList[currentWeaponCoreIndex], transform.position, Quaternion.identity);
        clone.transform.SetParent(transform);
        clone.transform.up = direction; // 2Dでは上方向をベクトルに合わせる
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        if (isDestroyed) return;
        // 武器がなければ処理しない
        if (weaponCoreList == null || weaponCoreList.Count == 0) return;
        Vector2 scroll = context.ReadValue<Vector2>();

        if (scroll.y > 0)
        {
            currentWeaponCoreIndex++;
        }
        else if (scroll.y < 0)
        {
            currentWeaponCoreIndex--;
        }

        // 範囲制限 or ループ
        if (currentWeaponCoreIndex >= weaponCoreList.Count)
            currentWeaponCoreIndex = 0;
        if (currentWeaponCoreIndex < 0)
            currentWeaponCoreIndex = weaponCoreList.Count - 1;
    }

    public void OnSpace(InputAction.CallbackContext context)
    {
        Debug.Log($"graveHolder: {graveHolder}, graves: {graves}");
        if (isDestroyed) return;
        if (!context.performed) return;
        // graves と graveHolder が初期化されているか確認
        if (graves == null) return;
        if (graves.Count == 0)
        {
            DestroyMe();
            return;
        }

        // 自分の位置に生成
        Instantiate(graves[0], transform.position, Quaternion.identity);
        graveHolder.RemoveFirstGrave();

        // 自分を破壊
        DestroyMe();
    }

    public void DestroyMe()
    {
        if (isDestroyed) return;
        isDestroyed = true;
        StartCoroutine(DestroyNextFrame());
    }

    IEnumerator DestroyNextFrame()
    {
        yield return null; // 1フレーム待つ
        Destroy(gameObject);
    }
}
