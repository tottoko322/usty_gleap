public class BaseStatus
{
    //属性
    public float currentHP {get; private set;}
    public float baseAttack {get; private set;}
    public float baseSpeed {get; private set;}

    public float baseDefense {get; private set;}

    //セットメソッド
    public void SetCurrentHP(float value)
    {
        currentHP = value;
    }

    public void SetBaseAttack(float value)
    {
        baseAttack = value;
    }

    public void SetBaseSpeed(float value)
    {
        baseSpeed = value;
    }

    public void SetBaseDefense(float value)
    {
        baseDefense = value;
    }

    //コンストラクタ
    public BaseStatus(BaseStatusSO baseStatusSO)
    {
        currentHP = baseStatusSO.maxHP;
        baseAttack = baseStatusSO.baseAttack;
        baseSpeed = baseStatusSO.baseSpeed;
        baseDefense = baseStatusSO.baseDefense;
    }
}

//コンストラクタをデフォルト値にすることで、インスタンス作成時に引数を不要にする。
//statusを追加するには属性、セットメソッド、コンストラクタの初期値の三つを追加すること