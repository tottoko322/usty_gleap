using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // コルーチンを使うために必要

public class GameOverManager : MonoBehaviour
{
    [Header("UI設定")]
    [SerializeField] private GameObject gameOverUI; 
    [SerializeField] private string titleSceneName = "TitleScene";
    
    [Header("演出設定")]
    [SerializeField] private float delaySeconds = 1.5f; // ゲームオーバー表示までの待ち時間

    private RemainGrave graveScript;
    private PlayerTracker playerTracker;

    private bool isProcessing = false; // 判定中フラグ
    private bool isGameOver = false;   // 画面表示済みフラグ

    void Start()
    {
        // 各マネージャーからスクリプトを取得
        GameObject gManager = GameObject.Find("GraveManager");
        if (gManager != null) graveScript = gManager.GetComponent<RemainGrave>();

        GameObject pManager = GameObject.Find("PlayerManager");
        if (pManager != null) playerTracker = pManager.GetComponent<PlayerTracker>();

        if (gameOverUI != null) gameOverUI.SetActive(false);
    }

    void Update()
    {
        // 1. すでに画面が表示されている場合：入力を受け付ける
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ReturnToTitle();
            }
            return;
        }

        // 2. まだ判定中でない場合：条件をチェック
        if (!isProcessing)
        {
            if (CheckConditions())
            {
                // 条件を満たしたら、待機処理（コルーチン）を開始
                StartCoroutine(DelayedGameOver());
            }
        }
    }

    private bool CheckConditions()
    {
        bool isGraveEmpty = graveScript != null && graveScript.RemainingGrave() == 0;
        bool isPlayerNull = playerTracker != null && playerTracker.targetPlayer == null;

        return isGraveEmpty && isPlayerNull;
    }

    // 間を置くための処理
    IEnumerator DelayedGameOver()
    {
        isProcessing = true; // 二重に実行されないようにロック

        // 指定した秒数だけ待つ
        yield return new WaitForSeconds(delaySeconds);

        // 待機後にUIを表示
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        isGameOver = true;
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene(titleSceneName);
    }
}