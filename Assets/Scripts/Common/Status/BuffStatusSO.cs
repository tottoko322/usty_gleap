using UnityEngine;

[CreateAssetMenu(fileName = "BuffStatus", menuName = "Status/BuffStatus")]
public class BuffStatusSO : ScriptableObject
{
    [Header("Attack Buff")]

    [Min(0f)]
    [SerializeField] public float addAttack = 0f;

    [Min(1f)]
    [SerializeField] public float multipleAttack = 1f;

    [Header("Speed Buff")]

    [Min(0f)]
    [SerializeField] public float addSpeed = 0f;

    [Min(1f)]
    [SerializeField] public float multipleSpeed = 1f;

    // ===== Getter =====
    public float AddAttack => addAttack;
    public float MultipleAttack => multipleAttack;
    public float AddSpeed => addSpeed;
    public float MultipleSpeed => multipleSpeed;
}
