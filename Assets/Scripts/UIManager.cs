using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("Menus"), SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject creditsMenu;
    [Header("Menu Prefabs"), SerializeField]
    private GameObject mainMenuPrefab;
    [SerializeField]
    private GameObject pausePrefab;
    [SerializeField]
    private GameObject optionsPrefab;
    [SerializeField]
    private GameObject creditsPrefab;
    [SerializeField]
    public static bool isPaused;
    [Header("Main menu Contents List"), SerializeField]
    private List<GameObject> mainMenuContents;

    [Header("Scene Variables"), SerializeField]
    private int mainMenuIndex = 0;
    [SerializeField]
    private int levelIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) // if instance is empty
        {
            instance = this; // store THIS instance of the class in the instance variable
            DontDestroyOnLoad(this.gameObject); //keep this instance of game manager when loading new scenes
        }
        else
        {
            Destroy(this.gameObject); // delete the new game manager attempting to store itself, there can only be one.
            Debug.Log("Warning: A second game manager was detected and destrtoyed"); // display message in the console to inform of its demise
        }
        //check for missing menu objects at start
        CheckMenuObjects();
    }

    // Update is called once per frame
    void Update()
    {
        //check for missing menu objects
        CheckMenuObjects();
        //check to see if we are not at our main menu
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(levelIndex)) 
        {
            //and if the player is pressing down the escape key
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                //if the pause menu is not active
                if (pauseMenu.activeSelf == false)
                {
                    //set time scale to zero
                    Time.timeScale = 0f;
                    //tell the game its paused
                    isPaused = true;
                    //activate it
                    pauseMenu.SetActive(true);
                }
                //otherwise
                else 
                {
                    //set time scale to 1
                    Time.timeScale = 1f;
                    //tell game its not paused
                    isPaused = false;
                    //deactivate it
                    pauseMenu.SetActive(false);
                }
            }
        }
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

    public void CheckMenuObjects() 
    {
        if (!mainMenu) 
        {
            mainMenu = Instantiate(mainMenuPrefab, Vector3.zero, Quaternion.identity, this.gameObject.transform);
            mainMenu.SetActive(false);
        }
        if (!pauseMenu)
        {
            pauseMenu = Instantiate(pausePrefab, Vector3.zero, Quaternion.identity, this.gameObject.transform);
            pauseMenu.SetActive(false);
        }
        if (!optionsMenu) 
        {
            optionsMenu = Instantiate(optionsPrefab, Vector3.zero, Quaternion.identity, this.gameObject.transform);
            optionsMenu.SetActive(false);
        }
        if (!creditsMenu) 
        {
            creditsMenu = Instantiate(creditsPrefab, Vector3.zero, Quaternion.identity, this.gameObject.transform);
            creditsMenu.SetActive(false);
        }
    }

    public void EnableMainMenu() 
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(mainMenuIndex))
        {
            mainMenu.SetActive(true); 
        }
    }

    public void LoadNextScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
