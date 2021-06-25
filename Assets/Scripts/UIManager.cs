using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("Menus")]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject creditsMenu;
    [SerializeField]
    public static bool isPaused;
    [SerializeField]
    private List<GameObject> mainMenuContents;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenOptionsMenu() 
    { 
        foreach(GameObject item in mainMenuContents)
        {
            item.SetActive(false);
        }
        optionsMenu.SetActive(true); 
    }

    public void CloseOptionsMenu()
    {
        optionsMenu.SetActive(false);
        foreach (GameObject item in mainMenuContents)
        {
            item.SetActive(true);
        }
    }
    public void OpenCreditsMenu()
    {
        foreach (GameObject item in mainMenuContents)
        {
            item.SetActive(false);
        }
        creditsMenu.SetActive(true);
    }

    public void CloseCreditsMenu()
    {
        creditsMenu.SetActive(false);
        foreach (GameObject item in mainMenuContents)
        {
            item.SetActive(true);
        }
    }
}
