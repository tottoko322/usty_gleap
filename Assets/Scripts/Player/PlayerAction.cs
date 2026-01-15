using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private Knockback knockback;
    private StatusActionHolder statusActionHolder;
    private TargetStatusAction attackAction;
    private TargetStatusAction playerEffectAction;

    void Awake()
    {
        knockback = GetComponent<Knockback>();
        statusActionHolder = GetComponent<StatusActionHolder>();

        attackAction = statusActionHolder.GetTargetStatusActionFromIndex(0);
        playerEffectAction = statusActionHolder.GetTargetStatusActionFromIndex(1);
    }

    public void HandleCollision(GameObject otherGameObject)
    {
        knockback.DoKnockback(otherGameObject);
        attackAction.Execute(this.gameObject, otherGameObject);
        playerEffectAction.Execute(this.gameObject, otherGameObject);
    }
}
