public class BuffStatus
{
    //属性
    public float AddAttack {get; private set;}
    public float MultipleAttack {get; private set;}
    public float AddSpeed {get; private set;}
    public float MultipleSpeed {get; private set;}

    //属性変更のためのメソッド
    public void SetAddAttack(float value)
    {
        AddAttack = value;
    }

    public void SetMultipleAttack(float value)
    {
        MultipleAttack = value;
    }

    public void SetAddSpeed(float value)
    {
        AddSpeed = value;
    }

    public void SetMultipleSpeed(float value)
    {
        MultipleSpeed = value;
    }

    //コンストラクタ
    public BuffStatus(BuffStatusSO buffStatusSO)
    {
        AddAttack = buffStatusSO.AddAttack;
        MultipleAttack = buffStatusSO.MultipleAttack;
        AddSpeed =buffStatusSO.AddSpeed;
        MultipleSpeed = buffStatusSO.MultipleSpeed;
    }
}
//★BuffStatusは値の管理と、値の再セットだけ行う

//コンストラクタをデフォルト値にすることで、インスタンス作成時に引数を不要にする。
//statusを追加するには属性、セットメソッド、コンストラクタの初期値の三つを追加すること