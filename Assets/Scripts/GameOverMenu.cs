using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI highScore;

    private void OnEnable() {
        highScore.text = $"High Score: {GameManager.Instance.HighScore}";
    }

    public void Restart()
    {
        gameObject.SetActive(false);
        
        GameManager.Instance.Enable();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
