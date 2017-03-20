using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EightSceneManager : MonoBehaviour {

    public GameObject moleculeRotator;
    public GameObject atomsPanel;

    public void LoadScene(int level) {
        SceneManager.LoadScene(level);
    }

    public void Exit() {
        Application.Quit();
    }

    public void StageComplete() {
        StartCoroutine(Show360StyleAtom());
    }

    public IEnumerator Show360StyleAtom()
    {
        yield return new WaitForSeconds(4f);
        // Hide the parent canvas and show the rotator
        moleculeRotator.SetActive(true);
        atomsPanel.SetActive(false);
        StartCoroutine(ProceedToNextLevel());
    }

    public IEnumerator ProceedToNextLevel()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
