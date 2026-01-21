using UnityEngine;

[CreateAssetMenu(fileName = "TemplateEffect", menuName = "Effect/TemplateEffect")]
public class TemplateEffect : Effect
{
    [Header("Effect Parameters")]
    [Min(0f)]
    [SerializeField] private float _duration;
    [Min(0f)]
    [SerializeField] private float _interval;
    [Min(0f)]
    [SerializeField] private float _staticInterval;
    private int _objectId;
    public override float Duration { get => _duration; set => _duration = value;}
    public override float Interval { get => _interval; set => _interval = value;}
    public override float StaticInterval { get => _staticInterval; set => _staticInterval = value;}
    public override int ObjectId { get => _objectId; set => _objectId = value; }
    public override void CustomEffect(StatusManager ownStatusManager)
    {
        //ここにStatusManagerでの処理を書く。
        return;
    }
    public override void Initialize(int objectId)
    {
        //これはこのままでOK、EffectActionを利用した際に使用したオブジェクトのIDを格納する。
        _objectId = objectId;
        
    }
}
