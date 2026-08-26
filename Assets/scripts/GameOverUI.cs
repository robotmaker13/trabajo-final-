using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverUI : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup blackScreen;
    public GameObject gameOverText;

    [Header("Tiempos")]
    public float delayBeforeFade = 1.2f;
    public float fadeDuration = 1.0f;
    public float delayBeforeText = 0.5f;

    private bool gameOverStarted = false;

    void Start()
    {
        if (blackScreen != null)
        {
            blackScreen.alpha = 0f;
            blackScreen.blocksRaycasts = false;
        }

        if (gameOverText != null)
            gameOverText.SetActive(false);
    }

    public void StartGameOver()
    {
        if (!gameOverStarted)
            StartCoroutine(GameOverSequence());
    }

    IEnumerator GameOverSequence()
    {
        gameOverStarted = true;

        yield return new WaitForSeconds(delayBeforeFade);

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            blackScreen.alpha =
                Mathf.Lerp(0f, 1f, timer / fadeDuration);

            yield return null;
        }

        blackScreen.alpha = 1f;

        yield return new WaitForSeconds(delayBeforeText);

        gameOverText.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}