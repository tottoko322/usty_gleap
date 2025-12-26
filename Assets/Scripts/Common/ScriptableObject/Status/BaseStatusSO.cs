using UnityEngine;

[CreateAssetMenu(fileName = "BaseStatus", menuName = "Status/BaseStatus")]
public class BaseStatusSO : ScriptableObject
{
    [Header("Base Parameters")]

    [Min(1f)]
    [SerializeField] public float maxHP = 10f;

    [Min(0f)]
    [SerializeField] public float baseAttack = 1f;

    [Min(0f)]
    [SerializeField] public float baseSpeed = 1f;

    [Min(0f)]
    [SerializeField] public float baseDefense = 1f;

    // ===== Getter（読み取り専用）=====
    public float MaxHP => maxHP;
    public float BaseAttack => baseAttack;
    public float BaseSpeed => baseSpeed;
    public float BaseDefense => baseDefense;
}
