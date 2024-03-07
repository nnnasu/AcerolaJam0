using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceSoundManager : MonoBehaviour {
    public AudioSource audioPlayer;

    public AudioClip OnHoverSound;
    public AudioClip OnHoverLeftSound;    
    public AudioClip OnClickedSound;

    public void OnHover() {
        //* not even going to bother fixing this
        // audioPlayer.PlayOneShot(OnHoverSound);
    }

    public void OnHoverLeft()  {
        // audioPlayer.PlayOneShot(OnHoverLeftSound);
    }

    public void OnClicked() {
        audioPlayer.PlayOneShot(OnClickedSound);
    }
}
