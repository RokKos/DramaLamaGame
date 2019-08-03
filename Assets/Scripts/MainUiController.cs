using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUiController : MonoBehaviour
{
    [SerializeField] Text main_text_;
    [SerializeField] Text right_answer_text_;
    [SerializeField] Text left_answer_text_;

    [SerializeField] Image bar_truth_;
    [SerializeField] Image bar_entertaiment_;
    [SerializeField] Image bar_drama_;

    private float bar_truth_fill_amount = 0.5f;
    private float bar_entertainment_fill_amount = 0.5f;
    private float bar_drama_fill_amount = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        SetupUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void  ChangeTruthFillAmount(float change) {
        bar_truth_fill_amount += change;
        bar_truth_fill_amount = Mathf.Min(1.0f, Mathf.Max(0.0f, bar_truth_fill_amount));
    }

    public void ChangeEntertainmetnFillAmount(float change)
    {
        bar_entertainment_fill_amount += change;
        bar_entertainment_fill_amount = Mathf.Min(1.0f, Mathf.Max(0.0f, bar_entertainment_fill_amount));
    }
    public void ChangeDramaFillAmount(float change)
    {
        bar_drama_fill_amount += change;
        bar_drama_fill_amount = Mathf.Min(1.0f, Mathf.Max(0.0f, bar_drama_fill_amount));
    }


    public void SetupUI() {
        main_text_.text = GoogleDocsUtils.GetMainText();
        right_answer_text_.text = GoogleDocsUtils.GetRightAnswerText();
        left_answer_text_.text = GoogleDocsUtils.GetLeftAnswerText();

        bar_truth_.fillAmount = bar_truth_fill_amount;
        bar_entertaiment_.fillAmount = bar_entertainment_fill_amount;
        bar_drama_.fillAmount = bar_drama_fill_amount;
    }

}
