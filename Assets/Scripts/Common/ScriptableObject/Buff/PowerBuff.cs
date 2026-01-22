using UnityEngine;

[CreateAssetMenu(fileName = "PowerBuff", menuName = "Buff/PowerBuff")]
public class PowerBuff : Buff
{
    [Header("Buff Parameters")]
    [Min(0f)]
    [SerializeField] private float _duration;
    [Min(0f)]
    [SerializeField] private float _interval;
    [Min(0f)]
    [SerializeField] private float _staticInterval;
    [SerializeField] private BuffStatusParam _param;
    private BuffStatus _buffStatus;
    private int _objectId;
    public override float Duration { get => _duration; set => _duration = value;}
    public override float Interval { get => _interval; set => _interval = value;}
    public override float StaticInterval {get => _staticInterval; set => _staticInterval = value ;}
    public override BuffStatus BuffStatus { get => _buffStatus; set => _buffStatus =  value;}
    public override int ObjectId { get => _objectId; set => _objectId = value; }

    public override void EmbedBuff(BuffStatus temporaryBuffStatus)
    {
        return;
    }

    public override void Initialize(int objectId)
    {
        _objectId = objectId;
        _buffStatus = new BuffStatus(_param);
        Debug.Log(_buffStatus);
    }

}
