using UnityEngine;

[CreateAssetMenu(fileName = "AttackAction", menuName = "TargetStatusAction/AttackAction")]
public class AttackAction : TargetStatusAction
{    public override void Execute(GameObject user,GameObject target)
    {
        StatusManager userStatusManager = user.GetComponent<StatusManager>();
        if (target.TryGetComponent<IHasStatusManager>(out var hasStatus))
        {
            StatusManager targetStatusManager = hasStatus.Status;
            float myAttackPower = userStatusManager.GetAttackPower();
            targetStatusManager.ApplyDamage(myAttackPower);
        }
    }
}
