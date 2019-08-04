using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCharacter : MonoBehaviour
{
    [SerializeField] Image charater_image_;
    Sprite prev_character_ = null;

    [SerializeField] AudioSource sfx_swoosh_;
    [SerializeField] List<AudioClip> sfx_for_swosh_in_;
    [SerializeField] List<AudioClip> sfx_for_swosh_out_;
    AudioClip prev_sound_swosh_in_ = null;
    AudioClip prev_sound_swosh_out_ = null;


    [SerializeField] List<Sprite> charater_avatars_;

    private void Start()
    {
        prev_character_ = charater_avatars_[0];
    }

    public void ChangeCharacterAndDisable() {
        AssignRandomCharacter();
        PlaySwoshInSFX();
        charater_image_.enabled = false;
    }

    public void EnableSprite()
    {
        charater_image_.enabled = true;
        PlaySwoshOutSFX();
    }

    private void AssignRandomCharacter()
    {
        Sprite character = charater_avatars_[Random.Range(0, charater_avatars_.Count)];
        while (character == prev_character_)
        {
            character = charater_avatars_[Random.Range(0, charater_avatars_.Count)];
        }
        prev_character_ = character;
        charater_image_.sprite = character;
    }

    private void PlaySwoshInSFX()
    {
        AudioClip clip_to_play = sfx_for_swosh_in_[Random.Range(0, sfx_for_swosh_in_.Count)];
        while (clip_to_play == prev_sound_swosh_in_)
        {
            clip_to_play = sfx_for_swosh_in_[Random.Range(0, sfx_for_swosh_in_.Count)];
        }
        prev_sound_swosh_in_ = clip_to_play;
        sfx_swoosh_.PlayOneShot(clip_to_play);
    }

    private void PlaySwoshOutSFX()
    {
        AudioClip clip_to_play = sfx_for_swosh_out_[Random.Range(0, sfx_for_swosh_out_.Count)];
        while (clip_to_play == prev_sound_swosh_out_)
        {
            clip_to_play = sfx_for_swosh_out_[Random.Range(0, sfx_for_swosh_out_.Count)];
        }
        prev_sound_swosh_out_ = clip_to_play;
        sfx_swoosh_.PlayOneShot(clip_to_play);
    }
}
