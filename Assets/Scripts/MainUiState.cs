using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUiState : MonoBehaviour
{

    [SerializeField] MainUiController main_menu_controller_;
    private enum Card { Left = -1, Right = 1 };

    public void SelectCard(int side) {
        float truth_meter_change = 0;
        float entertaiment_meter_change = 0;
        float drama_meter_change = 0;

        Card side_ = (Card)side;
        switch (side_) {
            case Card.Left: {
                    Debug.Log("Left");
                    truth_meter_change  = GoogleDocsUtils.GetRightAnswerTruthMeterChange() / 10.0f;
                    entertaiment_meter_change = GoogleDocsUtils.GetRightAnswerEntertainmentMeterChange() / 10.0f;
                    drama_meter_change  = GoogleDocsUtils.GetRightAnswerDramaMeterChange() / 10.0f;

                    break;

            }

            case Card.Right: {
                    Debug.Log("Right");

                    truth_meter_change = GoogleDocsUtils.GetLeftAnswerTruthMeterChange() / 10.0f;
                    entertaiment_meter_change = GoogleDocsUtils.GetLeftAnswerEntertainmentMeterChange() / 10.0f;
                    drama_meter_change = GoogleDocsUtils.GetLeftAnswerDramaMeterChange() / 10.0f;
                    
                    break;

            }



            default:
                Debug.Log("Error");
                break;
        }

        main_menu_controller_.ChangeTruthFillAmount(truth_meter_change);
        main_menu_controller_.ChangeEntertainmetnFillAmount(entertaiment_meter_change);
        main_menu_controller_.ChangeDramaFillAmount(drama_meter_change);
        GoogleDocsUtils.SelectNewCard();
        main_menu_controller_.SetupUI();
    }
}
