using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public abstract float Duration { get; set; }

    public abstract float Interval { get; set; }

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

    //ダメージ計算などする用でoverrides
    public abstract void CustomEffect(StatusManager ownStatusManager);
}

//★以下をコピペして、<name>を効果に応じた名前にすると新しいEffectが作れる。

// using UnityEngine;

// [CreateAssetMenu(fileName = "<name>Effect", menuName = "Effects/<name>")]
// public class <name>Effect : Effect
// {
//     [Min(0f)]
//     [SerializeField] private float _duration;
//     [Min(0f)]
//     [SerializeField] private float _interval;
//     public override float Interval { get => _duration; set => _duration = value;}
//     public override float Duration { get => _interval; set => _interval = value;}

//     public override void CustomEffect(StatusManager ownStatusManager)
//     {
//     }
// }