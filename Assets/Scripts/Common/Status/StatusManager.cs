using UnityEngine;

public class StatusManager : MonoBehaviour
{
    // ===== 初期値用 ScriptableObject =====
    [Header("Initial Status (ScriptableObject)")]
    [SerializeField] private BaseStatusSO baseStatusSO;
    [SerializeField] private BuffStatusSO buffStatusSO;

    // ===== 実行時ステータス =====
    private BaseStatus baseStatus;

    private BuffStatus buffStatus;

    void Awake()
    {
        baseStatus = new BaseStatus(baseStatusSO);
        buffStatus = new BuffStatus(buffStatusSO);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public BaseStatus BaseStatus => baseStatus;
    public BuffStatus BuffStatus => buffStatus;

    public void ApplyDamage(float damage)
    {
        float currentHP = baseStatus.CurrentHP;
        baseStatus.SetCurrentHP(currentHP - damage);
    }

    public float GetAttackPower()
    {
        float baseAttack = baseStatus.BaseAttack;
        float addAttack = buffStatus.AddAttack;
        float multipleAttack = buffStatus.MultipleAttack;

        return (baseAttack + addAttack)*multipleAttack;
    }
}

//★StatusManagerはStatusインスタンスの保持と、変更ルールに応じたメソッドを設定

//外部関数はStatusManagerを用いることでメソッドを呼び出すだけで簡単に値変更できる。