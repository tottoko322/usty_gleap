using UnityEngine;

public class BuffItemBehaviour : MonoBehaviour
{
    [Header("Buff Setting")]
    [SerializeField] private Buff[] buffs;
    private StatusActionHolder statusActionHolder;
    private BuffAction buffAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        statusActionHolder = GetComponent<StatusActionHolder>();
        buffAction = statusActionHolder.GetTargetStatusActionFromIndex(0) as BuffAction;
        buffAction.SetBuffs(buffs);
    }

    // Update is called once per frame
    void Update()
    {}
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Playerタグのときだけ処理
        if (!other.CompareTag("Player")) return;

        Debug.Log("Playerに当たりました: " + other.name);

        // 例：ステータスアクションを適用
        if (buffAction == null) return;
        buffAction.Execute(gameObject, other.gameObject);
        Destroy(gameObject);
    }
}
