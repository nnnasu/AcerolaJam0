using Core.Abilities;
using Core.UI.Rewards;
using UnityEngine;

public class AbilityRewardTest : MonoBehaviour {
    public RewardScreen rewardScreen;
    public AbilityManager player;

    private void Start() {
        rewardScreen.LoadPlayerData(player);
    }
}