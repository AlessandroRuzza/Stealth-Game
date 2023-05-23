using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayButton : MonoBehaviour
{
	public Sprite playSprite;
	public Sprite pauseSprite;
	public Button musicButton;

	void Awake()
	{
		musicButton.image.sprite = pauseSprite;
	}
    public void StopAndPlayMusic()
    {
		// If music is playing, pause it, otherwise unpause it
        if (MusicManager.self.audioSource.isPlaying)
        {
            MusicManager.self.audioSource.Pause();
			musicButton.image.sprite = playSprite;
        }
        else
        {
            MusicManager.self.audioSource.UnPause();
			musicButton.image.sprite = pauseSprite;
        }
    }
}
