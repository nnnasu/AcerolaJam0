using System.Collections;
using System.Collections.Generic;
using Core.AttributeSystem;
using Core.Utilities.Sounds;
using PrimeTween;
using UnityEngine;
using UnityEngine.Events;

public class TutorialGateDestroyer : MonoBehaviour, IDamageable {

    public List<Transform> transforms = new();
    public TweenSettings<Vector3> TweenToApply;
    public Collider col;
    public SoundGroup doorDropSounds;
    public SoundGroup doorHitSounds;

    public AudioSource audioSource;
    public UnityEvent OnGateDestroyed;

    public EntityType GetEntityType() {
        return EntityType.Enemy;
    }

    public void TakeDamage(float amount) {
        audioSource.PlayOneShot(doorHitSounds.GetRandomClip());
        var seq = Sequence.Create();
        foreach (var item in transforms) {
            seq.Group(Tween.LocalRotation(item, TweenToApply));
        }
        seq.OnComplete(DisableColliders);
    }

    private void DisableColliders() {
        audioSource.PlayOneShot(doorDropSounds.GetRandomClip(), 0.7f);
        col.enabled = false;
        OnGateDestroyed?.Invoke();
    }

}
