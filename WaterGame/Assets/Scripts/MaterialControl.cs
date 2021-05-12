using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MaterialControl : MonoBehaviour
{
    // Start is called before the first frame update

    public Renderer dropletRender;
    public Renderer iceRender;
    public Renderer cloudRender;
    private string currentColor = "Water";
    Dictionary<string, bool> collectedFla;
    List<bool> flavorBool;
    ProgressManager pm;
    private void Start()
    {
        pm = ProgressManager.Instance;
        collectedFla = pm.CollectedFlavors;

        flavorBool = new List<bool>();
        foreach (bool flavorValue in collectedFla.Values)
        {
            flavorBool.Add(flavorValue);
        }
    }
    public void cherryClicked()
    {
        dropletRender.material.mainTextureOffset = new Vector2(3.4f, 0.55f);
        iceRender.material.mainTextureOffset = new Vector2(0.25f, 1.54f);//
        cloudRender.material.mainTextureOffset = new Vector2(0.25f, 0.565f);//
        currentColor = "Cherry";
    }
    public void grapeClicked()
    {
        dropletRender.material.mainTextureOffset = new Vector2(0.0f, 0.2f);
        iceRender.material.mainTextureOffset = new Vector2(0.12f, 1.53f);//
        cloudRender.material.mainTextureOffset = new Vector2(0.34f, 0.565f);//
        currentColor = "Grape";
    }
    public void lemonClicked()
    {
        dropletRender.material.mainTextureOffset = new Vector2(1.28f, -2.8f);
        iceRender.material.mainTextureOffset = new Vector2(-0.13f, 1.18f);//
        cloudRender.material.mainTextureOffset = new Vector2(0.14f, 0.19f);//
        currentColor = "Lemon";
    }
    public void orangeClicked()
    {
        dropletRender.material.mainTextureOffset = new Vector2(0.24f, -2.45f);
        iceRender.material.mainTextureOffset = new Vector2(-0.13f, 1.54f);//
        cloudRender.material.mainTextureOffset = new Vector2(0.25f, 0.19f);//
        currentColor = "Orange";
    }
    public void waterMelonClicked()
    {
        dropletRender.material.mainTextureOffset = new Vector2(0.0f, 0.38f);
        iceRender.material.mainTextureOffset = new Vector2(0.51f, 1.54f);//
        cloudRender.material.mainTextureOffset = new Vector2(-0.15f, 0.565f);//
        currentColor = "Watermelon";
    }
    public void strawBerryClicked()
    {
        dropletRender.material.mainTextureOffset = new Vector2(0.85f, -2.45f);
        iceRender.material.mainTextureOffset = new Vector2(0.0f, 1.54f);//
        cloudRender.material.mainTextureOffset = new Vector2(-0.49f, 0.565f);//
        currentColor = "Strawberry";
    }
    public void waterClicked()
    {
        dropletRender.material.mainTextureOffset = new Vector2(0.0f, 0.0f);
        iceRender.material.mainTextureOffset = new Vector2(0.0f, 0.0f);
        cloudRender.material.mainTextureOffset = new Vector2(0.0f, 0.0f);
        currentColor = "Water";
    }

    public void swapRight()
    {

        switch (currentColor)
        {
            case "Water":
                if (flavorBool[0] == true)
                {
                    grapeClicked();
                }
                break;

            case "Grape":
                if (flavorBool[2] == true)
                {
                    lemonClicked();
                }
                break;

            case "Lemon":
                if (flavorBool[5] == true)
                {
                    strawBerryClicked();
                }
                break;

            case "Strawberry":
                if (flavorBool[3] == true)
                {
                    orangeClicked();
                }
                break;

            case "Orange":
                if (flavorBool[4] == true)
                {
                    waterMelonClicked();
                }
                break;

            case "Watermelon":
                if (flavorBool[1] == true)
                {
                    cherryClicked();
                }
                break;

            case "Cherry":
                waterClicked();
                break;
        }
        
    }
    public void swapLeft()
    {
        switch (currentColor)
        {
            case "Cherry":
                if (flavorBool[4] == true)
                {
                    waterMelonClicked();
                }
                break;

            case "Watermelon":
                if (flavorBool[3] == true)
                {
                    orangeClicked();
                }
                break;

            case "Orange":
                if (flavorBool[5] == true)
                {
                    strawBerryClicked();
                }
                break;

            case "Strawberry":
                if (flavorBool[2] == true)
                {
                    lemonClicked();
                }
                break;

            case "Lemon":
                if (flavorBool[0] == true)
                {
                    grapeClicked();
                }
                break;

            case "Grape":
                waterClicked();
                break;

            case "Water":
                if (flavorBool[1] == true)
                {
                    cherryClicked();
                }
                break;

        }



    }


}
