using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipController : MonoBehaviour
{
    [SerializeField] Sprite drama_time_;
    [SerializeField] Sprite text_invitation_;

    [SerializeField] GameObject main_image_;
    [SerializeField] GameObject btn_accept_;
    [SerializeField] GameObject img_invitation_;

    // Start is called before the first frame update
    public void ChangeSprite() {
        main_image_.SetActive(false);
        btn_accept_.SetActive(true);
        img_invitation_.SetActive(true);
    }
}
