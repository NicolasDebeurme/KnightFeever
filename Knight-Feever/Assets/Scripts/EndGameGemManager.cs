using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameGemManager : MonoBehaviour
{
    public GameObject PressSpace;
    bool isTriggered = false;
    public GameObject EndGameUI;

    private void Update()
    {
        if (isTriggered && Input.GetKeyDown(KeyCode.Space))
        {
            EndGameUI.SetActive(true);
            Player.Instance.Win();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PressSpace.SetActive(true);
            isTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PressSpace.SetActive(false);
        isTriggered = false;
    }
}
