using System;
using Core.Directors.Common;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;


namespace Core.Directors.Managers {
    /// <summary>
    /// Class that handles the loading of scenes
    /// </summary>
    public class RoomLoader : MonoBehaviour {

        public RoomType tutorialRoom;
        private AsyncOperationHandle<SceneInstance> CurrentHandle;
        public event Action<RoomType> OnLoadCompleted = delegate { };
        public bool LoadTutorialOnStart = true;

        private void Start() {
            // Load the first level
            if (LoadTutorialOnStart) LoadLevel(tutorialRoom);
        }


        internal async void LoadLevel(RoomType room) {
            var scene = room.GetRandomLevel();
            var newHandle = Addressables.LoadSceneAsync(scene, LoadSceneMode.Additive, activateOnLoad: true);
            await newHandle.Task;
            SceneManager.SetActiveScene(newHandle.Result.Scene);
            if (CurrentHandle.IsValid()) {
                var unloadHandle = Addressables.UnloadSceneAsync(CurrentHandle);
                await unloadHandle.Task;
            }
            CurrentHandle = newHandle;
            OnLoadCompleted?.Invoke(room);
        }
    }
}