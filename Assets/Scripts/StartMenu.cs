using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    [SerializeField] CardDataHelper card_data_helper_;
    [SerializeField] Animator start_animatio_;

    void Start()
    {
        card_data_helper_.ReloadCardData(true);
        StartCoroutine(ShowText());

    }
    // Start is called before the first frame update
    public void GoToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public IEnumerator ShowText() {
        yield return new WaitForSeconds(3.0f);
        start_animatio_.SetTrigger("Start");
    }
}
