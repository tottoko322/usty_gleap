using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class StatusManager : MonoBehaviour,IHasStatusManager
{
    // ===== StatusHolderの参照 =====
    private StatusHolder statusHolder;
    // ===== 実行時ステータス =====
    private BaseStatus baseStatus;

    private BuffStatus buffStatus;

    private BuffStatus temporaryBuffStatus;
    private List<Buff> buffs;
    private List<Effect> effects;

    //インターフェースによるStatusManagerの取得
    public StatusManager Status => this;
    void Start()
    {
        statusHolder = GetComponent<StatusHolder>();
        //基本用
        baseStatus = statusHolder.GetBaseStatus;
        buffStatus = statusHolder.GetBuffStatus;
        //バッファー用
        temporaryBuffStatus = new BuffStatus(statusHolder.GetTemporaryBuffStatus);
        buffs = statusHolder.GetBuffs;
        effects = statusHolder.GetEffects;
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
        BuffStatus resultBuffStatus = new BuffStatus(buffStatus);
        temporaryBuffStatus = new BuffStatus(resultBuffStatus);
        // buffの数が0であればそもそも処理しない
        if(buffs.Count <= 0) return;
        foreach (Buff buff in buffs)
        {
            // buffのIntervalが0よりも大きければ処理を飛ばす。
            if(buff.GetInterval() > 0) continue;
            // 各バフの BuffStatus をコピーしてからマージ
            var buffStatusCopy = new BuffStatus(buff.GetBuffStatus());
            resultBuffStatus = resultBuffStatus.Merged(buffStatusCopy);
        }
        foreach(Buff buff in buffs)
        {
            // buffのIntervalが0よりも大きければ処理を飛ばす。
            if(buff.GetInterval() > 0) continue;
            buff.EmbedBuff(this);
            // 各バフのIntervalをリセットする。
            float buffStaticInterval = buff.GetStaticInterval();
            buff.SetInterval(buffStaticInterval);
        }
        foreach (Buff buff in buffs)
        {
            // DurationとIntervalの値を減らす。
            buff.ChangeDuration(-Time.deltaTime);
            buff.ChangeInterval(-Time.deltaTime);
        }
        buffs.RemoveAll(buff => buff.GetDuration() <= 0);
        temporaryBuffStatus = new BuffStatus(resultBuffStatus);
    }
    public void AddBuff(Buff buff)
    {
        // buffを渡したオブジェクトとassetの種類が同じであれば追加しない。
        bool alreadyExists = buffs.Any(b =>
            b.ObjectId == buff.ObjectId &&
            b.name == buff.name
        );

        if (alreadyExists) return;

        buffs.Add(buff);
    }

    //エフェクトの処理
    public void ApplyEffect()
    {
        // effectの数が0であればそもそも処理しない
        if (effects.Count == 0) return;
        foreach(Effect effect in effects)
        {
            // effectのIntervalが0よりも大きければ処理を飛ばす。
            if(effect.GetInterval() > 0) continue;
            // 各エフェクトの処理を行う。
            effect.CustomEffect(this);
            // 各エフェクトのIntervalをリセットする。
            float effectStaticInterval = effect.GetStaticInterval();
            effect.SetInterval(effectStaticInterval);
            Debug.Log("Duration: "+effect.GetDuration());
        }
        foreach(Effect effect in effects)
        {
            // DurationとIntervalの値を減らす。
            effect.ChangeDuration(-Time.deltaTime);
            effect.ChangeInterval(-Time.deltaTime);
            // Debug.Log("interval: "+effect.GetInterval());
            // Debug.Log("staticInterval : "+effect.GetStaticInterval());
        }
        effects.RemoveAll(effect => effect.GetDuration() <= 0);
    }
    public void AddEffect(Effect effect)
    {
        // effectを渡したオブジェクトとassetの種類が同じであれば追加しない。
        bool alreadyExists = effects.Any(e =>
            e.ObjectId == effect.ObjectId &&
            e.name == effect.name
        );

        if(alreadyExists) return;

        effects.Add(effect);
        Debug.Log("追加しました");
    }
    public void ApplyDamage(float damage)
    {
        float currentHP = baseStatus.CurrentHP;
        baseStatus.SetCurrentHP(currentHP - damage);
    }

    public void ApplyHeal(float amount)
    {
        float currentHP = baseStatus.CurrentHP;
        baseStatus.SetCurrentHP(currentHP + amount);
    }

    public float GetAttackPower()
    {
        float baseAttack = baseStatus.BaseAttack;
        float addAttack = temporaryBuffStatus.AddAttack;
        float multipleAttack = temporaryBuffStatus.MultipleAttack;

        Debug.Log("あなたの攻撃力は: "+(baseAttack + addAttack)*multipleAttack);
        return (baseAttack + addAttack)*multipleAttack;
    }

    public float GetSpeed()
    {
        float baseSpeed = baseStatus.BaseSpeed;
        float addSpeed = temporaryBuffStatus.AddSpeed;
        float multipleSpeed = temporaryBuffStatus.MultipleSpeed;
        Debug.Log("GetSpeedにおいての速度："+multipleSpeed);
        Debug.Log("あなたの速度は: "+(baseSpeed + addSpeed)*multipleSpeed);
        return (baseSpeed + addSpeed)*multipleSpeed;
    }
}

//★StatusManagerはStatusインスタンスの保持と、変更ルールに応じたメソッドを設定

//外部関数はStatusManagerを用いてメソッドを呼び出すだけで、簡単に値変更できるようにしてある。

//・Intervalの実装、付与したインスタンスのIDとBuffまたはEffect名が一致したら外すようにする。