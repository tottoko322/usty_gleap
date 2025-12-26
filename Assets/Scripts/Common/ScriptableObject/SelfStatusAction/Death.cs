using UnityEngine;

public class Death : MonoBehaviour
{
    private StatusManager statusManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // 同じGameObjectに付いているStatusManagerを取得
        statusManager = GetComponent<StatusManager>();

        if (statusManager == null)
        {
            Debug.LogError("StatusManagerがアタッチされていません");
        }
    }

    // Update is called once per frame
    void Update()
    {
        KillMe();
    }

    void KillMe()
    {
        if (statusManager == null) return;

        // HPが0以下なら死亡
        if (statusManager.BaseStatus.CurrentHP <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
