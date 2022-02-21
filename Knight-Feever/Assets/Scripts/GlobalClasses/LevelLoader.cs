using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public Animator Transition;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {

            LoadNextLevel();
        }
    }

    public void LoadNextLevel() {
        if(SceneManager.GetActiveScene().buildIndex==1)
            StartCoroutine(LoadLevel(0));
        else
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int LevelIndex) {

        Transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(LevelIndex);
    }
}
