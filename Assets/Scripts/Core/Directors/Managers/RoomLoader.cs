using System;
using Core.Directors.Common;
using Core.UI.Loading;
using PrimeTween;
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

        private AsyncOperationHandle<SceneInstance> CurrentHandle;
        public event Action<RoomType> OnLoadCompleted = delegate { };
        public LoadingScreen loadingScreen;

        public void LoadLevel(RoomType room, bool skipFadeIn = false) {
            loadingScreen.ShowLoadingScreen(skipFadeIn);
            if (skipFadeIn) {
                LoadLevelInternal(room);
                return;
            }
            Tween.Delay(loadingScreen.FadeInTime, () => LoadLevelInternal(room));
        }


        private async void LoadLevelInternal(RoomType room) {
            var scene = room.GetRandomLevel();
            var newHandle = Addressables.LoadSceneAsync(scene, LoadSceneMode.Additive, activateOnLoad: true);

            loadingScreen.SetProgressFunction(() => newHandle.PercentComplete);

            await newHandle.Task;

            SceneManager.SetActiveScene(newHandle.Result.Scene);

            if (CurrentHandle.IsValid()) {
                var unloadHandle = Addressables.UnloadSceneAsync(CurrentHandle);
                await unloadHandle.Task;
            }
            CurrentHandle = newHandle;
            OnLoadCompleted?.Invoke(room);
            loadingScreen.SetLoadFinished();
        }
    }
}