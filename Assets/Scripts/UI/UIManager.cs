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
    private int mainMenuIndex = 1;
    [SerializeField]
    private int levelIndex = 2;

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
        //check to see if we are not at our main menu
        if (SceneManager.GetActiveScene().buildIndex == levelIndex)
        {
            //and if the player is pressing down the escape key
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //if the pause menu is not active
                if (UIManager.isPaused)
                {
                    Resume();
                }
                //otherwise
                else
                {
                    Pause();
                }
            }
        }

        //check for missing menu objects
        CheckMenuObjects();
    }

    public void OpenOptionsMenu() 
    {
        //if we are at the main menu scene
        if (SceneManager.GetActiveScene().buildIndex == mainMenuIndex)
        {
            //for each item in the main menu contents
            foreach (GameObject item in mainMenuContents)
            {
                //deactivate them
                item.SetActive(false);
            } 
        }
        //if the pause menu is active
        if (pauseMenu.activeSelf == true) 
        {
            //deactivate it
            pauseMenu.SetActive(false);
        }
        //set the options menu to active
        optionsMenu.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        //set the options menu to inactive
        optionsMenu.SetActive(false);
        //if we are at the main menu scene
        if (SceneManager.GetActiveScene().buildIndex == mainMenuIndex)
        {
            //for each item in the main menu contents
            foreach (GameObject item in mainMenuContents)
            {
                //set them active
                item.SetActive(true);
            } 
        }
        //if we are in our level scene
        if (SceneManager.GetActiveScene().buildIndex == levelIndex) 
        {
            //set the pause menu active again
            pauseMenu.SetActive(true);
        }
    }
    public void OpenCreditsMenu()
    {
        //if we are at the main menu scene
        if (SceneManager.GetActiveScene().buildIndex == mainMenuIndex)
        {
            //for each item in the main menu contents
            foreach (GameObject item in mainMenuContents)
            {
                //deactivate them
                item.SetActive(false);
            } 
        }
        //set the credits menu to active
        creditsMenu.SetActive(true);
    }

    public void CloseCreditsMenu()
    {
        //set the credits menu to inactive
        creditsMenu.SetActive(false);
        //if we are at the main menu scene
        if (SceneManager.GetActiveScene().buildIndex == mainMenuIndex)
        {
            //for each item in the main menu contents
            foreach (GameObject item in mainMenuContents)
            {
                //set them to active
                item.SetActive(true);
            } 
        }
    }

    private void CheckMenuObjects() 
    {
        if (!mainMenu) 
        {
            mainMenu = Instantiate(mainMenuPrefab, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
            mainMenu.SetActive(false);
        }
        if (!pauseMenu)
        {
            pauseMenu = Instantiate(pausePrefab, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
            pauseMenu.SetActive(false);
        }
        if (!optionsMenu) 
        {
            optionsMenu = Instantiate(optionsPrefab, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
            optionsMenu.SetActive(false);
        }
        if (!creditsMenu) 
        {
            creditsMenu = Instantiate(creditsPrefab, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
            creditsMenu.SetActive(false);
        }
    }

    public void EnableMainMenu() 
    {
        mainMenu.SetActive(true); 
    }

    public void LoadNextScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Pause()
    {
        //activate the pause menu
        pauseMenu.SetActive(true);
        //set time scale to zero
        Time.timeScale = 0f;
        //tell the game its paused
        isPaused = true;
    }

    public void Resume() 
    {
        //deactivate the pause menu
        pauseMenu.SetActive(false);
        //set time scale to 1
        Time.timeScale = 1f;
        //tell game its not paused
        isPaused = false;
    }

    public void ReturnToMain() 
    {
        //deactivate the pause menu
        pauseMenu.SetActive(false);
        //load the main mneu build index
        SceneManager.LoadScene(mainMenuIndex);
        //enable main menu
        EnableMainMenu();
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    public void SetMenusInactive() 
    {
        if (mainMenu.activeSelf == true)
        {
            mainMenu.SetActive(false); 
        }
        if (pauseMenu.activeSelf == true)
        {
            pauseMenu.SetActive(false); 
        }
        if (creditsMenu.activeSelf == true)
        {
            creditsMenu.SetActive(false); 
        }
    }
}
