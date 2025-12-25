using UnityEngine;

public abstract class Buff : ScriptableObject
{
    public abstract float Duration{get; set; }
    public abstract float Interval{get; set; }
    public abstract BuffStatus BuffStatus{get; set; }

    //取得関連
    public float GetDuration()
    {
        return Duration;
    }

    public void SetDuration(float duration)
    {
        Duration = duration;
    }

    public void ChangeDuration(float delta)
    {
        Duration += delta;
    }

    public BuffStatus GetBuffStatus()
    {
        return BuffStatus;
    }

    public abstract void Initialize();
    public abstract void EmbedBuff(StatusManager statusManager);
}

//★以下をコピペして、<name>を効果に応じた名前にすると新しいBuffが作れる。

// using UnityEngine;

// [CreateAssetMenu(fileName = "<name>Buff", menuName = "Buff/<name>Buff")]
// public class <name>Buff : Buff
// {
//     [Header("Buff Parameters")]
//     [Min(0f)]
//     [SerializeField] private float _duration;
//     [Min(0f)]
//     [SerializeField] private float _interval;
//     [SerializeField] private BuffStatus _buffStatus;
//     public override float Interval { get => _duration; set => _duration = value;}
//     public override float Duration { get => _interval; set => _interval = value;}
//     public override BuffStatus BuffStatus { get => _buffStatus; set => _buffStatus = value;}

//     public override void EmbedBuff(StatusManager statusManager)
//     {
//         return;
//     }

//     public override void Initialize()
//     {
//         _buffStatus = new BuffStatus(_buffStatusSO);
//     }

// }
