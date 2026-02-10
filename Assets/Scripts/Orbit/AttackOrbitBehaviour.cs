using UnityEngine;

public class AttackOrbitBehaviour : MonoBehaviour
{
    private StatusActionHolder _statusActionHolder;
    private TargetStatusAction _attackAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _statusActionHolder = GetComponent<StatusActionHolder>();
        _attackAction = _statusActionHolder.GetTargetStatusActionFromIndex(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject enemy = collision.gameObject;
            _attackAction.Execute(this.gameObject,enemy);
        }
    }
}
