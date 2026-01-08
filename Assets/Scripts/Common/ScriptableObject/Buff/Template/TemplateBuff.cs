using UnityEngine;

[CreateAssetMenu(fileName = "TemplateBuff", menuName = "Buff/TemplateBuff")]
public class TemplateBuff : Buff
{
    [Header("Buff Parameters")]
    [Min(0f)]
    [SerializeField] private float _duration;
    [Min(0f)]
    [SerializeField] private float _interval;
    [Min(0f)]
    [SerializeField] private float _staticInterval;
    [SerializeField] private BuffStatusParam _param;
    private int _objectId;
    private BuffStatus _buffStatus;
    public override float Duration { get => _duration; set => _duration = value;}
    public override float Interval { get => _interval; set => _interval = value;}
    public override float StaticInterval { get => _staticInterval; set => _staticInterval = value ;}
    public override BuffStatus BuffStatus { get => _buffStatus; set => _buffStatus =  value;}
    public override int ObjectId {get => _objectId; set => _objectId = value;}

    //Statusの加算がされた後に処理されるので、強制的なBuffStatusの変更に用いる。
    public override void EmbedBuff(BuffStatus temporaryBuffStatus)
    {
        //ここに書き加える。temporaryBuffを直接操作する。
        return;
    }

    public override void Initialize(int objectId)
    {
        //これはこのままでOK
        _buffStatus = new BuffStatus(_param);
        _objectId = objectId;
    }
}
