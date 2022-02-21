using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Activatable : MonoBehaviour
{
    public PlayerData playerData;
    public Sprite unactivated;
    public Sprite activated;

    public SpriteRenderer spriteRenderer;

    public GameObject activateButton;

    public Text Text;

    [HideInInspector] public bool isAlreadyActivated = false;


    [HideInInspector] public bool _isclicked = false;

    public string textToWrite = "Hello";
    private void Start()
    {
        if (isAlreadyActivated)
            spriteRenderer.sprite = activated;
        else
            spriteRenderer.sprite = unactivated;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAlreadyActivated)
        {
            Text.text = textToWrite;
            activateButton.SetActive(true);
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        activateButton.SetActive(false);
    }
    public void IsClicked()
    {
        _isclicked = true;

        

    }
}
