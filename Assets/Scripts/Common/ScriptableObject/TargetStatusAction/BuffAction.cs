using UnityEngine;

[CreateAssetMenu(fileName = "BuffAction", menuName = "TargetStatusAction/BuffAction")]

public class BuffAction : TargetStatusAction
{
    [Header("Buff Setting")]
    [SerializeField] private Buff[] buffs;

    public override void Execute(GameObject user, GameObject target)
    {
        if (target.TryGetComponent<IHasStatusManager>(out var hasStatus))
        {
            StatusManager otherStatusManager = hasStatus.Status;
            foreach(Buff buff in buffs)
            {
                Buff copiedBuff = Instantiate(buff);
                otherStatusManager.AddBuff(copiedBuff);
            }
        }
    }
}
