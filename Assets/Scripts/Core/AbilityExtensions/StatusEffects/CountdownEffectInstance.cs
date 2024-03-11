
using PrimeTween;

public class CountdownEffectInstance : EffectInstance {

    Tween CountdownTween;

    public CountdownEffectInstance(StatusEffect def, int level) : base(def, level) {
    }


    public void StartCountdown() {
        CountdownTween = Tween.Delay(effectDefinition.duration.GetValueAtLevel(level), ApplyEffectToTarget);
    }

    public void CancelCountdown() {
        CountdownTween.Stop();
    }

    private void ApplyEffectToTarget() {
        // Kills the target instantly // TODO make this more generic??
        target.Kill();

    }


}