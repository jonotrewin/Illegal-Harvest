using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VeggieUI : MonoBehaviour
{
    [SerializeField]Image waterImg;
    [SerializeField]Image healthIMG;
    [SerializeField]Image nutritionIMG;
    [SerializeField]GameObject SpecialImage;


    TextMeshProUGUI waterPercText;
    TextMeshProUGUI growthPercText;
    TextMeshProUGUI healthPercText;

    VeggieStats veggieStats;
    Veggie veggie;

    int thresholdExceed = 101;
    int thresholdOrange = 25;
    int thresholdGreen = 75;
    int thresholdYellow = 50;
    int thresholdRed = 0;
    private float _flashRate = 1;
    bool flash;

    Vector3 _startingScale;
    Vector3 _offsetFromVeg;

    void Start()
    {
        veggie = transform.parent.GetComponentInChildren<Veggie>();
        veggieStats = transform.parent.GetComponentInChildren<VeggieStats>();
        _startingScale = this.transform.localScale;
        _offsetFromVeg = veggie.transform.position - this.transform.position;

        waterPercText = waterImg.GetComponentInChildren<TextMeshProUGUI>();
        growthPercText = nutritionIMG.GetComponentInChildren<TextMeshProUGUI>();
        healthPercText = healthIMG.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {


        UpdateText();

        if(SpecialImage != null)
        SpecialImage.SetActive(veggie.GetComponent<SpecialAbility>()._specialActive);

        gameObject.transform.rotation = Camera.main.transform.rotation;

        ChangeColor(healthIMG,veggieStats._currentHealth, new Color32(216,207,121,255));
        ChangeColor(nutritionIMG, veggieStats._growthPercentage, new Color32(0, 0, 255, 255));
        ChangeColor(waterImg, veggieStats._waterPercentage, new Color32(139, 112, 205, 255)); 

        if(veggie.CurrentState == veggie._harvestableState)
        {
            waterImg.gameObject.SetActive(false);
            //healthIMG.gameObject.SetActive(false);
            healthIMG.gameObject.transform.position = this.transform.position + (Vector3.up*1.1f);
            nutritionIMG.transform.position = this.transform.position;
            
            //nutritionIMG.transform.localScale *= 1.3f;

        }

        if(veggieStats._isHarvested)
        {
            this.transform.localScale = Vector3.zero;
        }
        else { 
            this.transform.localScale = _startingScale;
            this.transform.position = veggie.transform.position - _offsetFromVeg;
            
        }
        

    }

    private void UpdateText()
    {
        waterPercText.text = ((int)veggieStats._waterPercentage).ToString();
        healthPercText.text = ((int)veggieStats._currentHealth).ToString();
        growthPercText.text = ((int)veggieStats._growthPercentage).ToString();

    }

    private void ChangeColor(Image img, float stat, Color exceed100color)
    {
        if (stat > thresholdExceed)
        {
            img.color = exceed100color;
            Flash(img,true);
            return;
            
        }

        if (stat > thresholdGreen)
        { 
            img.color = new Color32(177, 225, 82, 255);
            Flash(img, false);
            return;
        }
       

        if (stat > thresholdYellow)
        {
            img.color = new Color32(236, 236, 70, 255); 
            Flash(img, false);
            return;
        }
        if (stat > thresholdOrange)
        {
            img.color = new Color32(226, 136, 34, 255);
            Flash(img, false);
            return;
        }

        if (stat > thresholdRed)
        { 
            img.color = new Color32(216, 46, 46, 255);
            Flash(img, true);
            return;
            
        }

       







    }

    void Flash(Image img, bool value)
    {
        

        Blink blink = img.GetComponent<Blink>();

        if (blink != null) 
        blink.enabled = value;
       
        
   
    }
}
