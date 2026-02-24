using UnityEngine;

public class AreaBuffTrigger : MonoBehaviour
{
    [Header("Buff Settings")]
    [SerializeField] private Buff buffToApply;
    [SerializeField] private string targetTag = "Player";
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // タグが一致しない場合は処理をスキップ
        if (!other.CompareTag(targetTag)) return;
        
        // StatusManagerを持つオブジェクトにバフを適用
        StatusManager statusManager = other.GetComponent<StatusManager>();
        if (statusManager != null && buffToApply != null)
        {
            // バフのコピーを作成して追加
            Buff buffCopy = Instantiate(buffToApply);
            buffCopy.Initialize(gameObject.GetInstanceID());
            statusManager.AddBuff(buffCopy);
            Debug.Log($"{other.name}にバフ{buffToApply.name}を適用しました");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // エリアから出た時の処理が必要な場合はここに記述
        Debug.Log($"{other.name}がバフエリアから出ました");
    }
}
