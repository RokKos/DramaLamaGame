using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class GoogleDocsUtils : MonoBehaviour
{
    static List<List<string>> cards_data_;

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



    // Start is called before the first frame update
    private void Start()
    {
        ReloadCardData(false);
    }


    public void ReloadCardData(bool reload_from_internet = false) {

        if (reload_from_internet) {
            IEnumerator coroutine = DownloadCSVCoroutine("1MkNslveWDSwaXd78xtS0H-fetib_j4Fp23VaFLU_PeM");
            StartCoroutine(coroutine);
        } else {
            cards_data_ = ReadCSV("test");
        }
    }

    private void Update() {
        if (Input.GetKey(KeyCode.R)) {
            ReloadCardData(true);
        }
    }

    public static string GetMainText() {
        return cards_data_[curr_card_id_][kCardMainTextInd];
    }

    public static string GetLeftAnswerText() {
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
        System.Int32.TryParse(s, out change);
        return change;
    }

    public static void SelectNewCard()
    {
        curr_card_id_++;
        curr_card_id_ %= cards_data_.Count;
    }




    public static IEnumerator DownloadCSVCoroutine(string docId, bool saveAsset = true, string assetName = "test", string sheetId = null)
    {
        string url =
            "https://docs.google.com/spreadsheets/d/" + docId + "/export?format=csv";

        if (!string.IsNullOrEmpty(sheetId))
            url += "&gid=" + sheetId;

        WWWForm form = new WWWForm();
        WWW download = new WWW(url, form);

        yield return download;

        if (!string.IsNullOrEmpty(download.error))
        {
            Debug.Log("Error downloading: " + download.error);
        }
        else
        {

            if (saveAsset)
            {
                if (!string.IsNullOrEmpty(assetName)) {
                    string file = "Assets/Resources/" + assetName + ".csv";
                    File.WriteAllText(file, download.text);

                    var data = Resources.Load(assetName) as TextAsset;
                    cards_data_ =  ParseCSV(data.text);
                }
                else
                {
                    throw new System.Exception("assetName is null");
                }
            }
        }
    }


    public static readonly string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    public static readonly string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    public static readonly char[] TRIM_CHARS = { '\"' };

    public static List<List<string>> ReadCSV(string file)
    {
        var data = Resources.Load(file) as TextAsset;
        return ParseCSV(data.text);
    }

    public static List<List<string>> ParseCSV(string text)
    {
        text = CleanReturnInCsvTexts(text);

        var list = new List<List<string>>();
        var lines = Regex.Split(text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);

        bool jumpedFirst = false;

        foreach (var line in lines)
        {
            if (!jumpedFirst)
            {
                jumpedFirst = true;
                continue;
            }
            var values = Regex.Split(line, SPLIT_RE);

            var entry = new List<string>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                var value = values[j];
                value = DecodeSpecialCharsFromCSV(value);
                entry.Add(value);
            }
            list.Add(entry);
        }
        return list;
    }

    public static string DecodeSpecialCharsFromCSV(string value)
    {
        value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "").Replace("<br>", "\n").Replace("<c>", ",");
        return value;
    }

    public static string CleanReturnInCsvTexts(string text)
    {
        text = text.Replace("\"\"", "'");

        if (text.IndexOf("\"") > -1)
        {
            string clean = "";
            bool insideQuote = false;
            for (int j = 0; j < text.Length; j++)
            {
                if (!insideQuote && text[j] == '\"')
                {
                    insideQuote = true;
                }
                else if (insideQuote && text[j] == '\"')
                {
                    insideQuote = false;
                }
                else if (insideQuote)
                {
                    if (text[j] == '\n')
                        clean += "<br>";
                    else if (text[j] == ',')
                        clean += "<c>";
                    else
                        clean += text[j];
                }
                else
                {
                    clean += text[j];
                }
            }
            text = clean;
        }
        return text;
    }


}
