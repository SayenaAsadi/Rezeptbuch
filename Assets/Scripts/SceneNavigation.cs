// Steuert Szenenwechsel zwischen MainMenu und BarcodeScannerScene.

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadScanner()
    {
        SceneManager.LoadScene("BarcodeScannerScene");
    }
}