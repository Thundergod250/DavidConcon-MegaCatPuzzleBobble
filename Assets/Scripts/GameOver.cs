using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private SpriteRenderer darkScreen;
    [SerializeField] private float fadeSpeed = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Snap>() != null)
        {
            StartCoroutine(FadeInDarkScreen());
        }
    }

    private IEnumerator FadeInDarkScreen()
    {
        float alpha = 0.0f;
        while (alpha < 1.0f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            darkScreen.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        LoadNextScene();
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
