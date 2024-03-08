using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;

public class SceneInitialization : MonoBehaviour {

    public Image fillImage;
    public TextMeshProUGUI textMeshProUGUI;

    public AssetReference GameplayScene;
    public AssetReference InitialLevel;
    public AssetReference StorageScene;

    private void Start() {
        LoadScenes();
    }

    private async void LoadScenes() {
        var storageHandle = Addressables.LoadSceneAsync(StorageScene, LoadSceneMode.Additive, activateOnLoad: true);
        await storageHandle.Task;

        var gameplayHandle = Addressables.LoadSceneAsync(GameplayScene, LoadSceneMode.Additive, activateOnLoad: true);
        

        var tutorialHandle = Addressables.LoadSceneAsync(InitialLevel, LoadSceneMode.Additive, activateOnLoad: true);
        await gameplayHandle.Task;
        SceneManager.SetActiveScene(gameplayHandle.Result.Scene);
        await tutorialHandle.Task;
        SceneManager.SetActiveScene(tutorialHandle.Result.Scene);



        // Addressables.Release(storageHandle);
        // Addressables.Release(gameplayHandle);
        // Addressables.Release(tutorialHandle);

        SceneManager.UnloadSceneAsync(this.gameObject.scene);
    }

}
