using UnityEngine;

public class AreaEffectTrigger : MonoBehaviour
{
    [Header("Effect Settings")]
    [SerializeField] private Effect effectToApply;
    [SerializeField] private string targetTag = "Enemy";
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // タグが一致しない場合は処理をスキップ
        if (!other.CompareTag(targetTag)) return;
        
        // StatusManagerを持つオブジェクトにエフェクトを適用
        StatusManager statusManager = other.GetComponent<StatusManager>();
        if (statusManager != null && effectToApply != null)
        {
            // エフェクトのコピーを作成して追加
            Effect effectCopy = Instantiate(effectToApply);
            effectCopy.Initialize(gameObject.GetInstanceID());
            statusManager.AddEffect(effectCopy);
            Debug.Log($"{other.name}にエフェクト{effectToApply.name}を適用しました");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // エリアから出た時の処理が必要な場合はここに記述
        Debug.Log($"{other.name}がエフェクトエリアから出ました");
    }
}
