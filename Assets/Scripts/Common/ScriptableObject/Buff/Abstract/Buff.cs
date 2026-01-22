using UnityEngine;

public abstract class Buff : ScriptableObject
{
    public abstract float Duration{get; set; }
    public abstract float Interval{get; set; }
    public abstract float StaticInterval {get;set;}
    public abstract int ObjectId {get; set; }
    public abstract BuffStatus  BuffStatus{get; set; }

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

    //ObjectId関連
    public int GetObjectId()
    {
        return ObjectId;
    }

    public BuffStatus GetBuffStatus()
    {
        return BuffStatus;
    }
    //Buffを付与するオブジェクトのIdを格納する。
    public abstract void Initialize(int objectId);
    public abstract void EmbedBuff(BuffStatus temporaryBuffStatus);
}