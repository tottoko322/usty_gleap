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
            StatusManager otherStatusManager = hasStatus.Status;
            foreach(Effect effect in effects)
            {
                Effect copiedEffect = Instantiate(effect);
                copiedEffect.Initialize(user.GetInstanceID());
                otherStatusManager.AddEffect(copiedEffect);
            }
        }
    }

    public void SetEffects(Effect[] newEffects)
    {
        effects = newEffects;
    }
}
