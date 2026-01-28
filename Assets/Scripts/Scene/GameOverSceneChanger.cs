using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("UI設定")]
    [SerializeField] private GameObject gameOverUI; // ゲームオーバー時に出すパネル

    private bool isGameOver = false;

    void Update()
    {
        // プレイヤーがいない、かつ、まだゲームオーバー処理をしていない場合
        if (GameObject.FindGameObjectWithTag("Player") == null && !isGameOver)
        {
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        isGameOver = true;
        
        // 画面を暗くし、ボタンを表示する
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // 必要に応じて、ここで自動復活スクリプトを止める
        // FindObjectOfType<PlayerSpawnerBehaviour>().enabled = false;
    }

    // ボタンに割り当てるリスタート処理
    public void RestartGame()
    {
        // 現在のシーンを再読み込み
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}