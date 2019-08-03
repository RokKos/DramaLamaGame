using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class CardDataHelper : MonoBehaviour
{

    public static List<List<string>> cards_data_;

    private static int curr_card_id_ = -1;
    private const int kCardMainTextInd = 0;
    private static int curr_right_anwer_ind = 0;
    private static int curr_left_anwer_ind = 0;


    private const int kCardRightAnswerTextInd = 1;
    private const int kCardRightAnswerTruthInd = 2;
    private const int kCardRightAnswerEntertainmentInd = 3;
    private const int kCardRightAnswerDramaInd = 4;
    private const int kCardLeftAnswerTextInd = 5;
    private const int kCardLeftAnswerTruthInd = 6;
    private const int kCardLeftAnswerEntertainmentInd = 7;
    private const int kCardLeftAnswerDramaInd = 8;


    public void ReloadCardData(bool reload_from_internet = false)
    {

        if (reload_from_internet)
        {
            IEnumerator coroutine = GoogleDocsUtils.DownloadCSVCoroutine("1MkNslveWDSwaXd78xtS0H-fetib_j4Fp23VaFLU_PeM");
            StartCoroutine(coroutine);
        }
        else
        {
            cards_data_ = GoogleDocsUtils.ReadCSV("test");
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            ReloadCardData(true);
        }
    }

    public static string GetMainText()
    {
        return cards_data_[curr_card_id_][kCardMainTextInd];
    }

    public static string GetLeftAnswerText()
    {
        return cards_data_[curr_card_id_][curr_left_anwer_ind];
    }

    public static string GetRightAnswerText()
    {
        return cards_data_[curr_card_id_][curr_right_anwer_ind];
    }

    public static int GetRightAnswerTruthMeterChange()
    {
        return GetIntValue(cards_data_[curr_card_id_][curr_right_anwer_ind + 1]);
    }

    public static int GetRightAnswerDramaMeterChange()
    {
        return GetIntValue(cards_data_[curr_card_id_][curr_right_anwer_ind + 2]);
    }

    public static int GetRightAnswerEntertainmentMeterChange()
    {
        return GetIntValue(cards_data_[curr_card_id_][curr_right_anwer_ind + 3]);
    }

    public static int GetLeftAnswerTruthMeterChange()
    {
        return GetIntValue(cards_data_[curr_card_id_][curr_left_anwer_ind + 1]);
    }

    public static int GetLeftAnswerDramaMeterChange()
    {
        return GetIntValue(cards_data_[curr_card_id_][curr_left_anwer_ind + 2]);
    }

    public static int GetLeftAnswerEntertainmentMeterChange()
    {
        return GetIntValue(cards_data_[curr_card_id_][curr_left_anwer_ind + 3]);
    }

    public static int GetIntValue(string s)
    {
        int change = 0;
        int sing = 1;
        Debug.Log(s);
        if (s.Substring(0, 1) == "−") {
            sing = -1;
            s = s.Substring(1);
        }
        
        System.Int32.TryParse(s, out change);
        return sing * change;
    }

    public static void SelectNewCard()
    {
        curr_card_id_++;
        curr_card_id_ %= cards_data_.Count;


        int len = cards_data_[curr_card_id_].Count;
        int possible_answers = (len - 1) / 4;

        Assert.IsFalse(possible_answers < 2);

        int first_answer_ind = Random.Range(0, possible_answers);
        int second_answer_ind = Random.Range(0, possible_answers);

        while (first_answer_ind == second_answer_ind) {
            second_answer_ind = Random.Range(0, possible_answers);
        }

        curr_left_anwer_ind = first_answer_ind * 4 + 1;
        curr_right_anwer_ind = second_answer_ind * 4 + 1;

    }
}
