using UnityEngine;

[CreateAssetMenu(fileName = "PowerBuff", menuName = "Buff/PowerBuff")]
public class PowerBuff : Buff
{
    [Header("Buff Parameters")]
    [Min(0f)]
    [SerializeField] private float _duration;
    [Min(0f)]
    [SerializeField] private float _interval;
    [SerializeField] private BuffStatusSO _buffStatusSO;
    private BuffStatus _buffStatus;
    public override float Duration { get => _duration; set => _duration = value;}
    public override float Interval { get => _interval; set => _interval = value;}
    public override BuffStatus BuffStatus { get => _buffStatus; set => _buffStatus =  value;}

    public override void EmbedBuff(StatusManager statusManager)
    {
        return;
    }

    public override void Initialize()
    {
        Debug.Log(_buffStatusSO);
        _buffStatus = new BuffStatus(_buffStatusSO);
        Debug.Log(_buffStatus);
    }

}
