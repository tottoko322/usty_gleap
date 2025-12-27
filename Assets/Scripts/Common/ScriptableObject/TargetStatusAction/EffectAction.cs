using UnityEngine;

[CreateAssetMenu(fileName = "EffectAction", menuName = "TargetStatusAction/EffectAction")]
public class EffectAction : TargetStatusAction
{
    [Header("Effect Setting")]
    [SerializeField] private Effect[] effects;

    public override void Execute(GameObject user, GameObject target)
    {
        if (target.TryGetComponent<IHasStatusManager>(out var hasStatus))
        {
            foreach(Effect effect in effects)
            {
                Effect copiedEffect = Instantiate(effect);
                StatusManager otherStatusManager = hasStatus.Status;
                otherStatusManager.AddEffect(copiedEffect);
            }
        }
    }
}
