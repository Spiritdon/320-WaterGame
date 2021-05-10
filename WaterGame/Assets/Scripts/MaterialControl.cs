using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialControl : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Renderer dropletRender;
    public Renderer iceRender;
    public Renderer cloudRender;
    public void cherryClicked()
    {
        dropletRender.material.mainTextureOffset = new Vector2(3.4f, 0.55f);
        iceRender.material.mainTextureOffset = new Vector2(0.25f, 1.54f);//
        cloudRender.material.mainTextureOffset = new Vector2(0.25f, 0.565f);//
    }
    public void grapeClicked()
    {
        dropletRender.material.mainTextureOffset = new Vector2(0.0f, 0.2f);
        iceRender.material.mainTextureOffset = new Vector2(0.12f, 1.53f);//
        cloudRender.material.mainTextureOffset = new Vector2(0.34f, 0.565f);//
    }
    public void lemonClicked()
    {
        dropletRender.material.mainTextureOffset = new Vector2(1.28f, -2.8f);
        iceRender.material.mainTextureOffset = new Vector2(-0.13f, 1.18f);//
        cloudRender.material.mainTextureOffset = new Vector2(0.14f, 0.19f);//
    }
    public void orangeClicked()
    {
        dropletRender.material.mainTextureOffset = new Vector2(0.24f, -2.45f);
        iceRender.material.mainTextureOffset = new Vector2(-0.13f,1.54f);//
        cloudRender.material.mainTextureOffset = new Vector2(0.25f, 0.19f);//
    }
    public void waterMelonClicked()
    {
        dropletRender.material.mainTextureOffset = new Vector2(0.0f, 0.38f);
        iceRender.material.mainTextureOffset = new Vector2(0.51f, 1.54f);//
        cloudRender.material.mainTextureOffset = new Vector2(-0.15f, 0.565f);//
    }
    public void strawBerryClicked()
    {
        dropletRender.material.mainTextureOffset = new Vector2(0.85f, -2.45f);
        iceRender.material.mainTextureOffset = new Vector2(0.0f, 1.54f);//
        cloudRender.material.mainTextureOffset = new Vector2(-0.49f, 0.565f);//
    }
    public void waterClicked()
    {
        dropletRender.material.mainTextureOffset = new Vector2(0.0f,0.0f);
        iceRender.material.mainTextureOffset = new Vector2(0.0f, 0.0f);
        cloudRender.material.mainTextureOffset = new Vector2(0.0f, 0.0f);
    }
}
