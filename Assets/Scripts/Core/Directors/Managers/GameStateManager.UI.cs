using Core.GlobalInfo;
using Core.UI.Rewards;
using Core.UI.Targeting;
using PrimeTween;
using UnityEngine;

namespace Core.Directors.Managers {
    public partial class GameStateManager : MonoBehaviour {
        [Header("Reward UI")]
        public CanvasGroup SwapMenuCanvas;
        public RewardScreen rewardScreen;
        public CanvasGroup GameHUDCanvas;
        public PlayerHUD playerHUD;
        public float FadeDuration = 1;
        private Tween RewardFadeTween;
        private Tween HUDFadeTween;

        public CursorManager CursorManager;

        [ContextMenu("Show Reward Menu")]
        public void ShowSwapUI() {
            LockPlayer();
            CursorManager?.SetMenuMode(true);
            rewardScreen.LoadPlayerData(Player);
            SwapMenuCanvas.gameObject.SetActive(true);
            RewardFadeTween = Tween.Alpha(SwapMenuCanvas, 0, 1, FadeDuration);

            // TODO Set levels on rewards
            rewardScreen.remainingTries = 3;
            var roomer = EnemyDirector.CurrentRoom;
            int currentLevel = GameLevel.current.level;

            rewardScreen.RewardPanel.SetRewards(roomer.GetActionRewards(currentLevel), roomer.GetModifierRewards(currentLevel));


            HUDFadeTween = Tween.Alpha(GameHUDCanvas, 1, 0, FadeDuration);
        }

        [ContextMenu("Hide Reward Menu")]
        public void HideSwapUI() {
            UnlockPlayer();
            CursorManager?.SetMenuMode(false);
            RewardFadeTween = Tween.Alpha(SwapMenuCanvas, 1, 0, FadeDuration).OnComplete(() => SwapMenuCanvas.gameObject.SetActive(false));
            HUDFadeTween = Tween.Alpha(GameHUDCanvas, 0, 1, FadeDuration);
        }

    }
}