using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearManager : MonoBehaviour
{
    [Header("参照設定")]
    [SerializeField] private Emitter emitter;
    [SerializeField] private GameObject clearUI;

    [Header("タイトルシーンの名前")]
    [SerializeField] private string titleSceneName = "TitleScene";

    private bool isCleared = false;

    void Start()
    {
        if (clearUI != null)
        {
            clearUI.SetActive(false);
        }

        if (emitter == null)
        {
            emitter = FindObjectOfType<Emitter>();
        }
    }

    void Update()
    {
        // まだクリアしていない場合：クリア判定を行う
        if (!isCleared)
        {
            if (emitter != null && emitter.allWaveFinish)
            {
                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    ExecuteGameClear();
                }
            }
        }
        else
        {
            // クリア済みの（画面が暗い）場合：スペースキー入力を監視
            // 前回の「Input System」の設定を「Both」にしていれば、この書き方で動きます
            if (Input.GetKeyDown(KeyCode.Space))
            {
                BackToTitle();
            }
        }
    }

    private void ExecuteGameClear()
    {
        isCleared = true;
        if (clearUI != null)
        {
            clearUI.SetActive(true);
        }
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene(titleSceneName);
    }
}