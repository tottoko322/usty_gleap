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

    private bool isProcessing = false; // 判定中フラグ
    private bool isGameOver = false;   // 画面表示済みフラグ

    void Start()
    {
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
        Transform player = PlayerManager.Instance.CurrentPlayer;
        // PlayerManagerが存在しない場合は判定しない
        if (PlayerManager.Instance == null) return false;

        // 条件A：プレイヤーが存在しない（CurrentPlayerがnull）
        bool isPlayerNull = player == null;

        // 条件B：墓石のリストが空（数が0）
        bool isGraveEmpty = PlayerManager.Instance.GetPlayerGravesData().Count == 0;

        // 両方を満たしていれば true
        return isPlayerNull && isGraveEmpty;
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