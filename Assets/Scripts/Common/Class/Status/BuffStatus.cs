public class BuffStatus
{
    private BuffStatusParam param;

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

    //buffStatusからのコンストラクタ
    public BuffStatus(BuffStatusSO buffStatusSO)
    {
        AddAttack = buffStatusSO.AddAttack;
        MultipleAttack = buffStatusSO.MultipleAttack;
        AddSpeed =buffStatusSO.AddSpeed;
        MultipleSpeed = buffStatusSO.MultipleSpeed;
    }
    //デフォルト値コンストラクタ
    public BuffStatus()
    {
        AddAttack = 0f;
        MultipleAttack = 1f;
        AddSpeed = 0f;
        MultipleSpeed = 1f;
    }

    //BuffStatusからのコンストラクタ
    public BuffStatus(BuffStatus buffStatus)
    {
        AddAttack = buffStatus.AddAttack;
        MultipleAttack = buffStatus.MultipleAttack;
        AddSpeed = buffStatus.AddSpeed;
        MultipleSpeed = buffStatus.MultipleSpeed;
    }

    public BuffStatus(BuffStatusParam param)
    {
        this.param = param;
    }

    public BuffStatus Merged(BuffStatus other)
    {
        return new BuffStatus
        {
            AddAttack = this.AddAttack + other.AddAttack,
            MultipleAttack = this.MultipleAttack * other.MultipleAttack,
            AddSpeed = this.AddSpeed + other.AddSpeed,
            MultipleSpeed = this.MultipleSpeed * other.MultipleSpeed
        };
    }
}
//★BuffStatusは値の管理と、値の再セットだけ行う

//コンストラクタをデフォルト値にすることで、インスタンス作成時に引数を不要にする。
//statusを追加するには属性、セットメソッド、コンストラクタの初期値の三つを追加すること

/*
「取得ステータスの概念」

base：基本ステータス値
add：標準バフによる加算
multiple：標準バフによる乗算
tempAdd.sum：一時的なバフの加算の和
tempMultiple.sum：一時的なバフの乗算の和

これらより、正味の値は以下のようになる。

(base + add + temAdd.sum)*(multiple + temMultiple.sum)

これらをAttack,Speed,Defense,...などのすべてのステータスに構造として適応する。
*/