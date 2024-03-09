using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;


namespace Core.Directors.Levels {
    public class LevelManager : MonoBehaviour {

        public AssetReference Tutorial;
        private AsyncOperationHandle<SceneInstance> CurrentHandle;
        public event Action OnLoadCompleted = delegate { };
        public bool LoadTutorialOnStart = true;

        private void Start() {
            // Load the first level
            if (LoadTutorialOnStart) LoadTutorial();
        }

        private async void LoadTutorial() {
            CurrentHandle = Addressables.LoadSceneAsync(Tutorial, LoadSceneMode.Additive, activateOnLoad: true);
            await CurrentHandle.Task;
            SceneManager.SetActiveScene(CurrentHandle.Result.Scene);
            OnLoadCompleted?.Invoke();
        }
    }
}