using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Directors;
using Core.GlobalInfo;
using Core.UI.Rewards;
using PrimeTween;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour {

    [Header("Reward UI")]
    public CanvasGroup SwapMenuCanvas;
    public RewardScreen rewardScreen;
    public CanvasGroup GameHUDCanvas;
    public PlayerHUD playerHUD;
    public RewardGenerator DefaultRewardGenerator;
    public float FadeDuration = 1;



    [Header("Player References")]
    public AbilityManager Player;
    public PlayerController playerController;

    [Header("Game Management")]
    public EnemyDirector EnemyDirector;
    public EventChannel OnRoomTraversed;
    public LocationChannel OnSpawnPointRegistered;

    // State
    public int RoomsTraversed { get; private set; } = 0;
    private Tween RewardFadeTween;
    private Tween HUDFadeTween;

    private System.Random random = new();

    [ContextMenu("Reset Game")]
    private void ReloadGame() {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }

    private void OnEnable() {
        rewardScreen.OnChoicesFinished += OnRewardCompleted;
        EnemyDirector.OnEnemiesCleared += ShowSwapUI;
        OnRoomTraversed.Event += LoadNewRoom;
        OnSpawnPointRegistered.Event += MovePlayerToLocation;
    }

    private void OnDisable() {
        rewardScreen.OnChoicesFinished -= OnRewardCompleted;
        EnemyDirector.OnEnemiesCleared -= ShowSwapUI;
        OnRoomTraversed.Event -= LoadNewRoom;
        OnSpawnPointRegistered.Event -= MovePlayerToLocation;
    }

    private void MovePlayerToLocation(Vector3 point) {
        playerController.abilityManager.Teleport(point);
    }


    private void OnRewardCompleted() {
        HideSwapUI();
        // TODO: Spawn checkpoints
    }


    [ContextMenu("Show Reward Menu")]
    public void ShowSwapUI() {
        playerController.enabled = false;
        rewardScreen.LoadPlayerData(Player);
        SwapMenuCanvas.gameObject.SetActive(true);
        RewardFadeTween = Tween.Alpha(SwapMenuCanvas, 0, 1, FadeDuration);

        // TODO Set levels on rewards
        rewardScreen.remainingTries = 3;
        rewardScreen.RewardPanel.SetRewards(DefaultRewardGenerator.GetRandomActions(), DefaultRewardGenerator.GetRandomModifiers());


        HUDFadeTween = Tween.Alpha(GameHUDCanvas, 1, 0, FadeDuration);
    }

    [ContextMenu("Hide Reward Menu")]
    public void HideSwapUI() {
        playerController.enabled = true;
        RewardFadeTween = Tween.Alpha(SwapMenuCanvas, 1, 0, FadeDuration).OnComplete(() => SwapMenuCanvas.gameObject.SetActive(false));
        HUDFadeTween = Tween.Alpha(GameHUDCanvas, 0, 1, FadeDuration);
    }

    public void RequestSpawns() {
        int level = GameLevel.current.level;
        level++;
        GameLevel.current.SetLevel(level);
        EnemyDirector.SpawnEnemies(level); // TODO: derive credits        
    }

    public void LoadNewRoom(int value) {
        // TODO: Use this to select difficulty for rooms
        RequestSpawns();
    }
}
