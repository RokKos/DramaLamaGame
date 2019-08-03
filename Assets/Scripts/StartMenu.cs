using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    [SerializeField] CardDataHelper card_data_helper_;

    void Start()
    {
        card_data_helper_.ReloadCardData(true);
    }
    // Start is called before the first frame update
    public void GoToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
