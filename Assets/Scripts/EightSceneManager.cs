using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EightSceneManager : MonoBehaviour {

    public void LoadScene(int level) {
        SceneManager.LoadScene(level);
    }

    public void Exit() {
        Application.Quit();
    }
}
