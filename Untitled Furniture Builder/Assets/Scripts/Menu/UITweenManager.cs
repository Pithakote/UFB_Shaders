using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UITweenManager : MonoBehaviour
{
    // panels
    public RectTransform homeMenu, controlsMenu, settingsMenu, levelSelectMenu;
    // home panel
    public RectTransform playButton, controlsButton, settingsButton, quitButton;
    // settings panel
    public RectTransform cog1, cog2, slider, toggle1, toggle2, dropDown, settingsBackButton, settingsText;
    // level selection panel
    public RectTransform lvl1Button, lvl2Button, lvl3Button, lvl4Button, lvl5Button;
    // controls panel
    public RectTransform controlsText, controlsBackButton;
    void Start()
    {
        homeMenu.DOAnchorPos(Vector2.zero, 0.50f);
        playButton.DOAnchorPos(new Vector2(0, 61.3f), 0.50f);
        controlsButton.DOAnchorPos(new Vector2(0, -4.5f), 0.70f);
        settingsButton.DOAnchorPos(new Vector2(0, -69.9f), 0.90f);
        quitButton.DOAnchorPos(new Vector2(0, -136.8f), 1.10f);
    }
    // cog (-400, 86) to (-225, 86)
    // slider (400, 51.4) to (0, 51.4)

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenControls()
    {
        homeMenu.DOAnchorPos(new Vector2(-800, 0), 0.25f);
        playButton.DOAnchorPos(new Vector2(-168, 61.3f), 0f);
        controlsButton.DOAnchorPos(new Vector2(-168, -4.5f), 0f);
        settingsButton.DOAnchorPos(new Vector2(-168, -69.9f), 0f);
        quitButton.DOAnchorPos(new Vector2(-168, -136.8f), 0f);

        controlsMenu.DOAnchorPos(new Vector2(0, 0), 0.25f).SetDelay(0.15f);
        controlsText.DOAnchorPos(new Vector2(0, -45.7f), 0.50f);
    }
    public void CloseControls()
    {
        homeMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
        playButton.DOAnchorPos(new Vector2(0, 61.3f), 0.20f);
        controlsButton.DOAnchorPos(new Vector2(0, -4.5f), 0.40f);
        settingsButton.DOAnchorPos(new Vector2(0, -69.9f), 0.60f);
        quitButton.DOAnchorPos(new Vector2(0, -136.8f), 0.80f);


        controlsMenu.DOAnchorPos(new Vector2(415, 0), 0.25f);
        controlsText.DOAnchorPos(new Vector2(-2200f, -45.7f), 0.50f);

    }
    public void OpenSettings()
    {
        // hide home menu
        homeMenu.DOAnchorPos(new Vector2(-800, 0), 0.25f);
        playButton.DOAnchorPos(new Vector2(-168, 61.3f), 0.50f);
        controlsButton.DOAnchorPos(new Vector2(-168, -4.5f), 0.70f);
        settingsButton.DOAnchorPos(new Vector2(-168, -69.9f), 0.90f);
        quitButton.DOAnchorPos(new Vector2(-168, -136.8f), 1.10f);

        // open settings menu
        settingsMenu.DOAnchorPos(new Vector2(0, 0), 0.25f).SetDelay(0.15f);
        // move each part in 
        settingsText.DOAnchorPos(new Vector2(0, -45.7f), 0.50f);
        cog1.DOAnchorPos(new Vector2(27.4f, -52.8f), 0.80f);
        cog2.DOAnchorPos(new Vector2(61, -27.8f), 0.80f);       
        slider.DOAnchorPos(new Vector2(0, 51.4f), 0.80f);
        toggle1.DOAnchorPos(new Vector2(-175, -22.8f), 0.80f);
        toggle2.DOAnchorPos(new Vector2(-114.2f, -22.8f), 0.80f);
        dropDown.DOAnchorPos(new Vector2(129, -26.4f), 0.80f);
        settingsBackButton.DOAnchorPos(new Vector2(0, 30.4f), 0.80f);
    }
    public void CloseSettings()
    {
        homeMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
        playButton.DOAnchorPos(new Vector2(0, 61.3f), 0.50f);
        controlsButton.DOAnchorPos(new Vector2(0, -4.5f), 0.70f);
        settingsButton.DOAnchorPos(new Vector2(0, -69.9f), 0.90f);
        quitButton.DOAnchorPos(new Vector2(0, -136.8f), 1.10f);

        settingsMenu.DOAnchorPos(new Vector2(0, -415), 0.25f);
        // move each part back to it's original pos
        cog1.DOAnchorPos(new Vector2(-200, -52.8f), 0.50f);
        cog2.DOAnchorPos(new Vector2(-200, -27.8f), 0.60f);
        slider.DOAnchorPos(new Vector2(400, 51.4f), 0.70f);
        toggle1.DOAnchorPos(new Vector2(-400, -22.8f), 0.80f);
        toggle2.DOAnchorPos(new Vector2(-350, -22.8f), 0.90f);
        dropDown.DOAnchorPos(new Vector2(129, -200), 1.00f);
        settingsBackButton.DOAnchorPos(new Vector2(0, -400.0f), 0.80f);
        settingsText.DOAnchorPos(new Vector2(-750f, -45.7f), 0.0f);


    }

    public void OpenLevelSelect()
    {
        // hide home menu
        homeMenu.DOAnchorPos(new Vector2(-800, 0), 0.25f);
        playButton.DOAnchorPos(new Vector2(-168, 61.3f), 0f);
        controlsButton.DOAnchorPos(new Vector2(-168, -4.5f), 0f);
        settingsButton.DOAnchorPos(new Vector2(-168, -69.9f), 0f);
        quitButton.DOAnchorPos(new Vector2(-168, -136.8f), 0f);

        // open level select menu
        levelSelectMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
        // animate each button in, each with a longer duration than the last
        lvl1Button.DOAnchorPos(new Vector2(-157.5f, 19.1f), 0.50f);
        lvl2Button.DOAnchorPos(new Vector2(-77.5f, 19.1f), 0.60f);
        lvl3Button.DOAnchorPos(new Vector2(2.5f, 19.1f), 0.70f);
        lvl4Button.DOAnchorPos(new Vector2(82.5f, 19.1f), 0.80f);
        lvl5Button.DOAnchorPos(new Vector2(162.5f, 19.1f), 0.90f);
    }

    public void CloseLevelSelect()
    {
        // open the home menu
        homeMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
        playButton.DOAnchorPos(new Vector2(0, 61.3f), 0.50f);
        controlsButton.DOAnchorPos(new Vector2(0, -4.5f), 0.70f);
        settingsButton.DOAnchorPos(new Vector2(0, -69.9f), 0.90f);
        quitButton.DOAnchorPos(new Vector2(0, -136.8f), 1.10f);

        levelSelectMenu.DOAnchorPos(new Vector2(0, 415), 0.25f);
        // move each button back to it's original pos
        lvl1Button.DOAnchorPos(new Vector2(-157.5f, 112f), 0.50f);
        lvl2Button.DOAnchorPos(new Vector2(-77.5f, 112f), 0.60f);
        lvl3Button.DOAnchorPos(new Vector2(2.5f, 112f), 0.70f);
        lvl4Button.DOAnchorPos(new Vector2(82.5f, 112f), 0.80f);
        lvl5Button.DOAnchorPos(new Vector2(162.5f, 112f), 0.90f);
    }
}
