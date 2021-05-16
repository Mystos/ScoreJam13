using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public ScenesData sceneDataBase;
    public static SceneLoader Instance;
    private List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    bool worldHasLoaded = false;
    bool isPlayerLoaded = false;
    bool isLoadingCompleted = false;
    bool levelChange = false;
    public GameObject leaderBoard;

    private void Awake()
    {
        // Singleton of the GameManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
            Instance = gameObject.GetComponent<SceneLoader>();
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void Start()
    {
        // We load the main menu
        LoadMainMenu();
    }

    private void LateUpdate()
    {
        if (isPlayerLoaded && !isLoadingCompleted)
            Spawn();

    }

    public void LoadMainMenu()
    {
        leaderBoard.SetActive(false);

        // Check if the scene already loaded
        if (!SceneManager.GetSceneByName(sceneDataBase.mainMenuScenes.sceneName).IsValid())
        {
            SceneManager.LoadScene(sceneDataBase.mainMenuScenes.sceneName, LoadSceneMode.Additive);
        }

        // Unloading old level sequence

        // Unload Active level
        Scene activeScene = SceneManager.GetActiveScene();

        // Check if the scene already loaded
        if (activeScene.IsValid() && activeScene.name != sceneDataBase.systemScene.sceneName)
        {
            SceneManager.UnloadSceneAsync(activeScene);
        }

        // Unload Player
        // Check if the scene already loaded
        if (SceneManager.GetSceneByName(sceneDataBase.playerScene.sceneName).IsValid())
        {
            SceneManager.UnloadSceneAsync(sceneDataBase.playerScene.sceneName);
        }

        //// We unload the world scene and load the main menu
        //foreach (GameScene scene in sceneDataBase.worldScenes)
        //{
        //    // Check if the scene already loaded
        //    if (SceneManager.GetSceneByName(scene.sceneName).IsValid())
        //    {
        //        SceneManager.UnloadSceneAsync(scene.sceneName);
        //    }
        //}

        isLoadingCompleted = false;
        isPlayerLoaded = false;
        worldHasLoaded = false;
    }

    public void LoadLevel(string sceneToLoadName)
    {
        //loadingScreen.SetActive(true);
        //logoClip.Play();


        if (SceneManager.GetSceneByName(sceneToLoadName).IsValid())
        {
            levelChange = true;
            worldHasLoaded = true;
            LevelChanged();

            return;
        }

        levelChange = true;

        // Check if the scene already loaded
        if (!SceneManager.GetSceneByName(sceneToLoadName).IsValid())
        {
            // Add the scene to the async operation list
            scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneToLoadName, LoadSceneMode.Additive));
        }

        // Unloading old level sequence

        Scene activeScene = SceneManager.GetActiveScene();

        // Check if the scene already loaded
        if (activeScene.IsValid())
        {
            SceneManager.UnloadSceneAsync(activeScene);
        }

        StartCoroutine(LoadingScreen());
    }

    public void LoadWorld()
    {
        if (!SceneManager.GetSceneByName(sceneDataBase.playerScene.sceneName).IsValid())
        {
            SceneManager.LoadScene(sceneDataBase.playerScene.sceneName, LoadSceneMode.Additive);
            isPlayerLoaded = true;
        }

        // Check if the scene already loaded
        if (!SceneManager.GetSceneByName(sceneDataBase.mainLevel.sceneName).IsValid())
        {
            // Add the scene to the async operation list
            scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneDataBase.mainLevel.sceneName, LoadSceneMode.Additive));
        }

        StartCoroutine(LoadingScreen());

    }

    /// <summary>
    /// Load the world via to a specific level
    /// This method was made just for a debug build not for release
    /// </summary>
    /// <param name="sceneToLoad"></param>
    public void LoadWorld(GameScene sceneToLoad)
    {
        // Check if the scene already loaded
        if (!SceneManager.GetSceneByName(sceneToLoad.sceneName).IsValid())
        {
            // Add the scene to the async operation list
            scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneToLoad.sceneName, LoadSceneMode.Additive));
        }

        StartCoroutine(LoadingScreen());
    }

    public void LoadEndScene()
    {
        // Check if the scene already loaded
        if (!SceneManager.GetSceneByName(sceneDataBase.endScene.sceneName).IsValid())
        {
            SceneManager.LoadScene(sceneDataBase.endScene.sceneName, LoadSceneMode.Additive);
        }

        // Unloading old level sequence

        // Unload Active level
        Scene activeScene = SceneManager.GetActiveScene();

        // Check if the scene already loaded
        if (activeScene.IsValid())
        {
            SceneManager.UnloadSceneAsync(activeScene);
        }

        // Unload Player
        // Check if the scene already loaded
        if (SceneManager.GetSceneByName(sceneDataBase.playerScene.sceneName).IsValid())
        {
            SceneManager.UnloadSceneAsync(sceneDataBase.playerScene.sceneName);
        }
    }


    IEnumerator LoadingScreen()
    {
        //float totalProgress = 0f;

        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {

                yield return null;
            }
        }

        // Hide loading screen
        worldHasLoaded = true;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == sceneDataBase.mainMenuScenes.sceneName)
        {
            SceneManager.SetActiveScene(scene);

            // Unload EndScene
            // Check if the scene already loaded
            if(sceneDataBase.endScene != null)
            {
                if (SceneManager.GetSceneByName(sceneDataBase.endScene.sceneName).IsValid())
                {
                    SceneManager.UnloadSceneAsync(sceneDataBase.endScene.sceneName);
                }
            }


            return;
        }

        foreach (GameObject item in scene.GetRootGameObjects())
        {
            if (item.CompareTag("SpawnScript"))
            {
                SceneManager.SetActiveScene(scene);
                item.SetActive(true);

                // We unload the main menu scene

                // Check if the scene already loaded
                if (SceneManager.GetSceneByName(sceneDataBase.mainMenuScenes.sceneName).IsValid())
                {
                    SceneManager.UnloadSceneAsync(sceneDataBase.mainMenuScenes.sceneName);
                }
            }
        }

        if (levelChange)
        {
            levelChange = false;
            LevelChanged();
        }
        Debug.Log("OnSceneLoaded: " + scene.name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Spawn()
    {
        SpawnScript sc = FindObjectOfType<SpawnScript>();
        if (sc != null)
        {
            sc.Spawn();

            isLoadingCompleted = true;
        }
    }

    public void LevelChanged()
    {

        PlayerController charController = FindObjectOfType<PlayerController>();
        charController.enabled = false;
        SpawnPlayerPosition spawn = FindObjectOfType<SpawnPlayerPosition>();
        Scene activeScene = SceneManager.GetActiveScene();


        if (spawn != null)
        {
            charController.transform.position = spawn.transform.position;
        }
        else
        {
            Debug.LogError("SpawnPlayerPosition not found, character will be put at (0,0,0)");
            charController.transform.position = new Vector3(0, 0, 0);
        }
        charController.transform.eulerAngles = new Vector3(0, 0, 0);
        charController.enabled = true;

        SpawnScript spawnScripts = FindObjectOfType<SpawnScript>();
        if (spawnScripts != null)
        {
            spawnScripts.wakeUp?.Play();
        }
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        leaderBoard.SetActive(true);
        HighscoreTable highscore = FindObjectOfType<HighscoreTable>();
        ScoreManager score = FindObjectOfType<ScoreManager>();
        if(highscore != null && score != null)
        {
            highscore.AddHigscoreEntry(score.score, "YOU");
        }
    }

}