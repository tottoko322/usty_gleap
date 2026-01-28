using UnityEngine;
using UnityEngine.UI; // 画像(Image)を操るために追加
using UnityEngine.SceneManagement;
using System.Collections; // コルーチンを使うために追加

public class SceneChanger : MonoBehaviour
{
    public string mainSceneName = "MainScene";
    public Image fadeImage;     // 作成したFadeImageをここに入れる
    public float fadeSpeed = 1.0f; // フェードにかかる秒数

    void Update()
    {
        // スペースキーで開始
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartSceneChange();
        }
    }

    // ボタンから呼び出す用
    public void StartSceneChange()
    {
        // コルーチン（非同期処理）を開始
        StartCoroutine(FadeAndLoad());
    }

    IEnumerator FadeAndLoad()
    {
        float timer = 0f;
        Color color = fadeImage.color;

        // アルファ値を0から1へ徐々に上げていく
        while (timer < fadeSpeed)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Clamp01(timer / fadeSpeed);
            fadeImage.color = color;
            yield return null; // 1フレーム待機
        }

        // 真っ暗になったらシーン切り替え
        SceneManager.LoadScene(mainSceneName);
    }
}