using UnityEngine;

public class PlazmaOrbitBehaviour : MonoBehaviour
{
    [Header("Effect Settings")]
    [SerializeField] private Effect stunEffect;
    private StatusActionHolder _statusActionHolder;
    private TargetStatusAction _plazmaAction;

    void Awake()
    {
        _statusActionHolder = GetComponent<StatusActionHolder>();
        _plazmaAction = _statusActionHolder.GetTargetStatusActionFromIndex(0);
    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject enemy = collision.gameObject;
            _plazmaAction.Execute(this.gameObject, enemy);

            // Stun effect application
            ApplyStunEffect(enemy);
        }
    }

    private void ApplyStunEffect(GameObject enemy)
    {
        if (stunEffect != null)
        {
            // StatusManagerを取得（StatusActionHolderではなく）
            StatusManager statusManager = enemy.GetComponent<StatusManager>();
            if (statusManager != null)
            {
                // エフェクトのコピーを作成して適用
                Effect effectCopy = Instantiate(stunEffect);
                effectCopy.Initialize(gameObject.GetInstanceID());
                statusManager.AddEffect(effectCopy);
            
                Debug.Log($"{enemy.name}にStunEffectを適用しました");
            }
        }
    }
}