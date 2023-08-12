using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public void EndGameWithDelay()
    {
        // Call the coroutine to wait for 1 second before quitting the application.
        StartCoroutine(QuitGameAfterDelay(2f));
    }

    // Coroutine to wait for the specified delay before quitting the application.
    private IEnumerator QuitGameAfterDelay(float delay)
    {
        // Wait for the specified delay.
        yield return new WaitForSeconds(delay);

        // Quit the application after the delay.
        QuitGame();
    }

    // Function to quit the application.
    private void QuitGame()
    {
        // Call Application.Quit() to close the game.
        Application.Quit();
    }
}
