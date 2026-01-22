using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private Knockback knockback;
    private StatusActionHolder statusActionHolder;
    private TargetStatusAction attackAction;
    private TargetStatusAction playerEffectAction;
    private TargetStatusAction playerBuffAction;

    void Awake()
    {
        knockback = GetComponent<Knockback>();
        statusActionHolder = GetComponent<StatusActionHolder>();

        attackAction = statusActionHolder.GetTargetStatusActionFromIndex(0);
        playerEffectAction = statusActionHolder.GetTargetStatusActionFromIndex(1);
        playerBuffAction = statusActionHolder.GetTargetStatusActionFromIndex(2);
    }

    public void HandleCollision(GameObject otherGameObject)
    {
        knockback.DoKnockback(otherGameObject);
        attackAction.Execute(this.gameObject, otherGameObject);
        playerEffectAction.Execute(this.gameObject, otherGameObject);
        playerBuffAction.Execute(this.gameObject, otherGameObject);
    }
}
