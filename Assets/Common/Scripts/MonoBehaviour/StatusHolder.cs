using System.Collections.Generic;
using UnityEngine;

public class StatusHolder : MonoBehaviour
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //基本用
        baseStatus = new BaseStatus(baseStatusSO);
        buffStatus = new BuffStatus(buffStatusSO);
        //バッファー用
        temporaryBuffStatus = new BuffStatus(buffStatus);
        buffs = new List<Buff>() {};
        effects = new List<Effect> {};
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public BaseStatus GetBaseStatus => baseStatus;
    public BuffStatus GetBuffStatus => buffStatus;
    public BuffStatus GetTemporaryBuffStatus => temporaryBuffStatus;
    public List<Buff> GetBuffs => buffs;
    public List<Effect> GetEffects => effects;
}
