﻿using System.Collections;
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

    [SerializeField] Image img_reason_;

    [SerializeField] Sprite reasons_truth_;
    [SerializeField] Sprite reasons_entertaiment_;
    [SerializeField] Sprite reasons_drama_;

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

    public float GetTruthBar() {
        return bar_truth_fill_amount;
    }

    public float GetEntertainmentBar() {
        return bar_entertainment_fill_amount;
    }
    public float GetDramaBar() {
        return bar_drama_fill_amount;
    }


    public void SetupUI() {
        main_text_.text = CardDataHelper.GetMainText();
        right_answer_text_.text = CardDataHelper.GetRightAnswerText();
        left_answer_text_.text = CardDataHelper.GetLeftAnswerText();

        bar_truth_.fillAmount = bar_truth_fill_amount;
        bar_entertaiment_.fillAmount = bar_entertainment_fill_amount;
        bar_drama_.fillAmount = bar_drama_fill_amount;
    }

    public void SetReason(bool dead_by_truth, bool dead_by_entertainment, bool dead_by_drama) {
        if (dead_by_truth) {
            img_reason_.sprite = reasons_truth_;
            return;
        }

        if (dead_by_entertainment)
        {
            img_reason_.sprite = reasons_entertaiment_;
            return;
        }

        if (dead_by_drama)
        {
            img_reason_.sprite = reasons_drama_;
            return;
        }
    }

}
