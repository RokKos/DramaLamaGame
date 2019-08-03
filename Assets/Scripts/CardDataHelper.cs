using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataHelper : MonoBehaviour
{

    public static List<List<string>> cards_data_;

    private static int curr_card_id_ = 0;
    private const int kCardMainTextInd = 0;
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
        return cards_data_[curr_card_id_][kCardLeftAnswerTextInd];
    }

    public static string GetRightAnswerText()
    {
        return cards_data_[curr_card_id_][kCardRightAnswerTextInd];
    }

    public static int GetRightAnswerTruthMeterChange()
    {
        return GetIntValue(cards_data_[curr_card_id_][kCardRightAnswerTruthInd]);
    }

    public static int GetRightAnswerDramaMeterChange()
    {
        return GetIntValue(cards_data_[curr_card_id_][kCardRightAnswerDramaInd]);
    }

    public static int GetRightAnswerEntertainmentMeterChange()
    {
        return GetIntValue(cards_data_[curr_card_id_][kCardRightAnswerEntertainmentInd]);
    }

    public static int GetLeftAnswerTruthMeterChange()
    {
        return GetIntValue(cards_data_[curr_card_id_][kCardLeftAnswerTruthInd]);
    }

    public static int GetLeftAnswerDramaMeterChange()
    {
        return GetIntValue(cards_data_[curr_card_id_][kCardLeftAnswerDramaInd]);
    }

    public static int GetLeftAnswerEntertainmentMeterChange()
    {
        return GetIntValue(cards_data_[curr_card_id_][kCardLeftAnswerEntertainmentInd]);
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
    }
}
