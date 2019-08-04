using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainUiState : MonoBehaviour
{

    [SerializeField] MainUiController main_menu_controller_;
    [SerializeField] CardDataHelper card_data_helper_;
    [SerializeField] Animator character_animator_;
    //[SerializeField] Animator main_ui_;

    [SerializeField] Animator left_answer_animator_;
    [SerializeField] Animator right_answer_animator_;

    [SerializeField] AudioSource sfx_btn_click_;


    private enum Card { Left = -1, Right = 1 };

    private void Start()
    {
        card_data_helper_.ReloadCardData(false);
        CardDataHelper.SelectNewCard();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) {
            UnSelectButton();
        }   
    }

    public void SelectCard(int side) {
        float truth_meter_change = 0;
        float entertaiment_meter_change = 0;
        float drama_meter_change = 0;

        Card side_ = (Card)side;
        switch (side_) {
            case Card.Right: {
                    Debug.Log("Right");
                    truth_meter_change  = CardDataHelper.GetRightAnswerTruthMeterChange() / 10.0f;
                    entertaiment_meter_change = CardDataHelper.GetRightAnswerEntertainmentMeterChange() / 10.0f;
                    drama_meter_change  = CardDataHelper.GetRightAnswerDramaMeterChange() / 10.0f;

                    break;

            }

            case Card.Left: {
                    Debug.Log("Left");

                    truth_meter_change = CardDataHelper.GetLeftAnswerTruthMeterChange() / 10.0f;
                    entertaiment_meter_change = CardDataHelper.GetLeftAnswerEntertainmentMeterChange() / 10.0f;
                    drama_meter_change = CardDataHelper.GetLeftAnswerDramaMeterChange() / 10.0f;
                    
                    break;

            }



            default:
                Debug.Log("Error");
                break;
        }

        Debug.Log("truth " + truth_meter_change);

        main_menu_controller_.ChangeTruthFillAmount(truth_meter_change);
        main_menu_controller_.ChangeEntertainmetnFillAmount(entertaiment_meter_change);
        main_menu_controller_.ChangeDramaFillAmount(drama_meter_change);
        CardDataHelper.SelectNewCard();
        main_menu_controller_.SetupUI();
        EvaluateDeadConditions();
        StartCoroutine(TriggerAnimations());

        sfx_btn_click_.Play();

    }

    void EvaluateDeadConditions() {
        bool dead = main_menu_controller_.GetTruthBar() <= 0 || main_menu_controller_.GetEntertainmentBar() <= 0 || main_menu_controller_.GetDramaBar() <= 0;

        if (dead) {
            SceneManager.LoadScene("GameOver");
        }

    }

    private IEnumerator TriggerAnimations()
    {
        character_animator_.SetTrigger("ChangeCharacter");
        // main_ui_.SetTrigger("ChangeCharacter");
        left_answer_animator_.SetTrigger("FadeOut");
        right_answer_animator_.SetTrigger("FadeOut");

        yield return new WaitForSeconds(3f);

        left_answer_animator_.SetTrigger("FadeIn");

        yield return new WaitForSeconds(3f);

        right_answer_animator_.SetTrigger("FadeIn");

        yield return null;
    }

    private void UnSelectButton() {
        // This is hacky
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
}
