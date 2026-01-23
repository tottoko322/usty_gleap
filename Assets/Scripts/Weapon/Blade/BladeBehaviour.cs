using UnityEngine;

public class BladeBehaviour : MonoBehaviour
{
    private StatusActionHolder statusActionHolder;
    private TargetStatusAction attackAction;
    private Knockback knockback;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        statusActionHolder = GetComponent<StatusActionHolder>();
        attackAction = statusActionHolder.GetTargetStatusActionFromIndex(0);
        knockback = GetComponent<Knockback>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 当たったオブジェクトを検出
    private void OnTriggerEnter2D(Collider2D other)
    {
        knockback.DoKnockback(other.gameObject);
        attackAction.Execute(gameObject, other.gameObject);
    }

}
