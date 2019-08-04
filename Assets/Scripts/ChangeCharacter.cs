using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCharacter : MonoBehaviour
{
    [SerializeField] Image charater_image_;

    [SerializeField] List<Sprite> charater_avatars_;  // Change this to images
    public void ChangeCharacterAndDisable() {
        charater_image_.sprite = charater_avatars_[Random.Range(0, charater_avatars_.Count)];
        charater_image_.enabled = false;
    }

    public void EnableSprite()
    {
        charater_image_.enabled = true;
    }
}
