using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private  PlayerData playerData;
    public Camera mainCamera;
    public Animator PlayerAnimator;
    public Player PlayerScript;
    public GameObject PlayerUI;

    public GameObject PauseMenu;
    void Start()
    {
        if(!playerData.ChestOpened)
        {
            playerData.Actualweapontype=0;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.SetActive(!PauseMenu.activeSelf);
            Time.timeScale = PauseMenu.activeSelf? 0 : 1;
        }
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if(hit.collider != null)
            {
                if(hit.transform.tag == "Player")
                {
                    PlayerUI.SetActive(true);
                    PlayerScript.enabled = true;
                    PlayerAnimator.enabled = true;

                    mainCamera.transform.parent = PlayerScript.transform;
                    mainCamera.transform.localPosition = new Vector3(0, 0, -10);
                }
            }
        }
    }

    public void OnClickResume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

}
