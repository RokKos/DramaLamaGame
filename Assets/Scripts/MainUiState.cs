using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainUiState : MonoBehaviour
{

    [SerializeField] MainUiController main_menu_controller_;
    [SerializeField] CardDataHelper card_data_helper_;
    private enum Card { Left = -1, Right = 1 };

    private void Start()
    {
        card_data_helper_.ReloadCardData(false);
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

    }

    void EvaluateDeadConditions() {
        bool dead = main_menu_controller_.GetTruthBar() <= 0 || main_menu_controller_.GetEntertainmentBar() <= 0 || main_menu_controller_.GetDramaBar() <= 0;

        if (dead) {
            SceneManager.LoadScene("GameOver");
        }

    }
}
