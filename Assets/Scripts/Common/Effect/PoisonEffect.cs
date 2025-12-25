using UnityEngine;

[CreateAssetMenu(fileName = "PoisonEffect", menuName = "Effect/PoisonEffect")]
public class PoisonEffect : Effect
{
    [Header("Effect Parameters")]
    [Min(0f)]
    [SerializeField] private float _duration;
    [Min(0f)]
    [SerializeField] private float _interval;
    public override float Interval { get => _duration; set => _duration = value;}
    public override float Duration { get => _interval; set => _interval = value;}

    public override void CustomEffect(StatusManager ownStatusManager)
    {
        // ダメージ処理
        ownStatusManager.ApplyDamage(1f);
        Debug.Log($"毒が効いた！");
        return;
    }
}

/*
・アセットで、インターバルと全体の時間を決める
・対応するクラスでメソッドをオーバーライド
*/