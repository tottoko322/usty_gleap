using UnityEngine;
using System.Collections.Generic;

public class StatusManager : MonoBehaviour
{
    // ===== 初期値用 ScriptableObject =====
    [Header("Initial Status (ScriptableObject)")]
    [SerializeField] private BaseStatusSO baseStatusSO;
    [SerializeField] private BuffStatusSO buffStatusSO;

    // ===== 実行時ステータス =====
    private BaseStatus baseStatus;

    private BuffStatus buffStatus;

    private BuffStatus temporaryBuffStatus;
    private List<Buff> buffs;
    private List<Effect> effects;

    void Awake()
    {
        //基本用
        baseStatus = new BaseStatus(baseStatusSO);
        buffStatus = new BuffStatus(buffStatusSO);
        //バッファー用
        temporaryBuffStatus = new BuffStatus(buffStatusSO);
        buffs = new List<Buff>() {};
        effects = new List<Effect> {};
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ApplyBuff();
        ApplyEffect();
    }

    public BaseStatus BaseStatus => baseStatus;
    public BuffStatus BuffStatus => buffStatus;
    public BuffStatus TemporaryBuffStatus => temporaryBuffStatus;

    //バフの処理
    public void ApplyBuff()
    {
        BuffStatus resultBuffStatus = new BuffStatus(buffStatusSO);
        Debug.Log(buffs.Count);
        if(buffs.Count <= 0) return;
        buffs.ForEach(buff => Debug.Log(buff.GetBuffStatus().AddAttack));
        foreach (var buff in buffs)
        {
            // 各バフの BuffStatus をコピーしてからマージ
            var buffStatusCopy = new BuffStatus(buff.GetBuffStatus());
            resultBuffStatus = resultBuffStatus.Merged(buffStatusCopy);
        }
        buffs.ForEach(buff => buff.EmbedBuff(this));
        buffs.ForEach(buff => buff.ChangeDuration(-Time.deltaTime));
        buffs.RemoveAll(buff => buff.GetDuration() <= 0);

        temporaryBuffStatus = resultBuffStatus;
        Debug.Log("バフ後の攻撃力は"+resultBuffStatus.AddAttack);
    }
    public void AddBuff(Buff buff)
    {
        buff.Initialize();
        buffs.Add(buff);
    }

    //エフェクトの処理
    public void ApplyEffect()
    {
        if (effects.Count == 0) return;
        effects.ForEach(effect => effect.CustomEffect(this));
        effects.ForEach(effect => effect.ChangeDuration(-Time.deltaTime));
        effects.RemoveAll(effect => effect.GetDuration() <= 0);
    }
    public void AddEffect(Effect effect)
    {
        effects.Add(effect);
    }
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