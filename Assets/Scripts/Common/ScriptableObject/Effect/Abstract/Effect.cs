using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public abstract float Duration { get; set; }

    public abstract float Interval { get; set; }
    public abstract float StaticInterval{get;set;}
    public abstract int ObjectId {get; set; }

    //Duration関連
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

    //Interval関連
    public float GetInterval()
    {
        return Interval;
    }

    public void SetInterval(float interval)
    {
        Interval = interval;
    }
    public void ChangeInterval(float delta)
    {
        Interval += delta;
    }
    //StaticInterval関連
    public float GetStaticInterval()
    {
        return StaticInterval;
    }

    public abstract void Initialize(int objectId);
    //ダメージ計算などする用でoverrides
    public abstract void CustomEffect(StatusManager ownStatusManager);
}