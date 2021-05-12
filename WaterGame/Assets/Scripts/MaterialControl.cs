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
    public PlayerCollector playerCollector;
    private string currentColor;
    List<int> colors;
    Dictionary<string, bool> collectedFla;
    bool isTrue;
    List<bool> flavorBool;
    private void Start()
    {
        currentColor = "Water";
        colors = new List<int>();
        colors.Add(0);
        collectedFla = ProgressManager.Instance.CollectedFlavors;

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

    public void swapRight(string currentFlavor)
    {
        
        bool oneChosen = false;
        if (currentColor == "Water")
        {
            if (flavorBool[0] == true)
            {
                this.grapeClicked();
                oneChosen = true;
                return;
            }
            else
            {
                currentColor = "Grape";
            }
        }
        if (currentColor == "Grape" && !oneChosen)
        {
            if (flavorBool[2] == true)
            {
                this.lemonClicked();
                oneChosen = true;
                return;
            }
            else
            {
                currentColor = "Lemon";
            }
        }
        if (currentColor == "Lemon" && !oneChosen)
        {
            if (flavorBool[5] == true)
            {
                this.strawBerryClicked();
                oneChosen = true;
                return;
            }
            else
            {
                currentColor = "Strawberry";
            }
        }
        if (currentColor == "Strawberry" && !oneChosen)
        {
            if (flavorBool[3] == true)
            {
                this.orangeClicked();
                oneChosen = true;
                return;
            }
            else
            {
                currentColor = "Orange";
            }
        }
        if (currentColor == "Orange" && !oneChosen)
        {
            if (flavorBool[4] == true)
            {
                this.waterMelonClicked();
                oneChosen = true;
                return;
            }
            else
            {
                currentColor = "Watermelon";
            }
        }
        if (currentColor == "Watermelon" && !oneChosen)
        {
            if (flavorBool[1] == true)
            {
                cherryClicked();
                oneChosen = true;
                return;
            }
            else
            {
                currentColor = "Cherry";
            }
        }
        if (currentColor == "Cherry")
        {
            waterClicked();
            return;
        }
        print(currentColor);
    }
    public void swapLeft()
    {
        
        bool oneChosen = false;

        if (currentColor == "Water" && !oneChosen)
        {
            if (flavorBool[1] == true)
            {
                cherryClicked();
                oneChosen = true;
                return;
            }
            else
            {
                currentColor = "Cherry";
            }
        }
        if (currentColor == "Cherry" && !oneChosen)
        {
            if (flavorBool[4] == true)
            {
                waterMelonClicked();
                oneChosen = true;
                return;
            }
            else
            {
                currentColor = "Watermelon";
            }
        }
        if (currentColor == "Watermelon" && !oneChosen)
        {
            if (flavorBool[3] == true)
            {
                orangeClicked();
                oneChosen = true;
                return;
            }
            else
            {
                currentColor = "Orange";
            }
        }
        if (currentColor == "Orange" && !oneChosen)
        {
            if (flavorBool[5] == true)
            {
                strawBerryClicked();
                oneChosen = true;
                return;
            }
            else
            {
                currentColor = "Strawberry";
            }
        }
        if (currentColor == "Strawberry" && !oneChosen)
        {
            if (flavorBool[2] == true)
            {
                lemonClicked();
                oneChosen = true;
                return;
            }
            else
            {
                currentColor = "Lemon";
            }
        }
        if (currentColor == "Lemon" && !oneChosen)
        {
            if (flavorBool[0] == true)
            {
                grapeClicked();
                oneChosen = true;
                return;
            }
            else
            {
                currentColor = "Grape";
            }
        }
        
        print(currentColor);

    }


}
