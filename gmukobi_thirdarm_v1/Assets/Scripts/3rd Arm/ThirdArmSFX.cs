using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdArmSFX : MonoBehaviour
{
    [SerializeField] private AudioCuePlayerAtMyPosition growAudioCuePlayer;
    [SerializeField] private AudioCuePlayerAtMyPosition shrinkAudioCuePlayer;

    public void PlayGrowAudioCue()
    {
        if(ThirdArmSettingsReader.Instance.settings.thirdArmBuildType == ThirdArmBuildType.TribecaFilm)
        {
            // don't play audio for the lab demo
            growAudioCuePlayer.PlayAtMyPosition();
        }
    }

    public void PlayShrinkAudioCue()
    {
        if(ThirdArmSettingsReader.Instance.settings.thirdArmBuildType == ThirdArmBuildType.TribecaFilm)
        {
            // don't play audio for the lab demo
            shrinkAudioCuePlayer.PlayAtMyPosition();
        }
    }
}
