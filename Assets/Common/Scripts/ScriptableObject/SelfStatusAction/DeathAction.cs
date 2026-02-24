using UnityEngine;

[CreateAssetMenu(fileName = "DeathAction", menuName = "SelfStatusAction/DeathAction")]
public class DeathAction : SelfStatusAction
{
    public override void Execute(GameObject user)
    {
        StatusManager userStatusManager = user.GetComponent<StatusManager>();
        float CurrentHP = userStatusManager.BaseStatus.CurrentHP;
        if(CurrentHP <= 0)
        {
            Destroy(user);
        }
    }
}
