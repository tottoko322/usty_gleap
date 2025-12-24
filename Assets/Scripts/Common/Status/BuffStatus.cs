public class BuffStatus
{
    //属性
    public float addAttack {get; private set;}
    public float multipleAttack {get; private set;}
    public float addSpeed {get; private set;}
    public float multipleSpeed {get; private set;}

    //属性変更のためのメソッド
    public void SetAddAttack(float value)
    {
        addAttack = value;
    }

    public void SetMultipleAttack(float value)
    {
        multipleAttack = value;
    }

    public void SetAddSpeed(float value)
    {
        addSpeed = value;
    }

    public void SetMultipleSpeed(float value)
    {
        multipleSpeed = value;
    }

    //コンストラクタ
    public BuffStatus(BuffStatusSO buffStatusSO)
    {
        addAttack = buffStatusSO.addAttack;
        multipleAttack = buffStatusSO.multipleAttack;
        addSpeed =buffStatusSO.addSpeed;
        multipleSpeed = buffStatusSO.multipleSpeed;
    }
}
//★BuffStatusは値の管理と、値の再セットだけ行う

//コンストラクタをデフォルト値にすることで、インスタンス作成時に引数を不要にする。
//statusを追加するには属性、セットメソッド、コンストラクタの初期値の三つを追加すること