using UnityEngine;

[CreateAssetMenu(fileName = "PoisonEffect", menuName = "Effect/PoisonEffect")]
public class PoisonEffect : Effect
{
    [Header("Effect Parameters")]
    [Min(0f)]
    [SerializeField] private float _duration;
    [Min(0f)]
    [SerializeField] private float _interval;
    [Min(0f)]
    [SerializeField] private float _staticInterval;
    [Min(0f)]
    [SerializeField] private float _damage;
    private int _objectId;
    public override float Duration { get => _duration; set => _duration = value;}
    public override float Interval { get => _interval; set => _interval = value;}
    public override float StaticInterval { get => _staticInterval; set => _staticInterval = value; }
    public override int ObjectId { get => _objectId ; set => _objectId = value; }

    public override void CustomEffect(StatusManager ownStatusManager)
    {
        // ダメージ処理
        ownStatusManager.ApplyDamage(_damage);
        Debug.Log($"毒が効いた！");
        return;
    }

    public override void Initialize(int objectId)
    {
        _objectId = objectId;
    }
}

/*
・アセットで、インターバルと全体の時間を決める
・対応するクラスでメソッドをオーバーライド
*/