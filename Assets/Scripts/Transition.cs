using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    public static Transition reference;
    Image transitionScreen;
    float alpha = 0;
    bool transitioning = false;
    bool startTransition = false;
    Action transitionEvent = () => { };

    float transitionSpeed = 0;
    float waitTime = 0;


    bool test2 = false;

    void Awake()
    {
        reference = this;
        transitionScreen = GetComponent<Image>();
        CoverScreen();
    }

    void Update()
    {
        transitionScreen.color = new Vector4(0, 0, 0, alpha);

        if (startTransition)
        {
            TransitionFade(transitionSpeed, waitTime);
        }
    }

    public void StartTransition(float transitionSpeed = 3f, float waitTime = 3f)
    {
        this.transitionSpeed = transitionSpeed;
        this.waitTime = waitTime;

        startTransition = true;
        transitioning = false;
    }

    public void AddFunctions(List<Action> functions)
    {
        foreach (Action a in functions) transitionEvent += a;
    }

    void CoverScreen()
    {
        transitionScreen.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
    }

    void FadeIn(float speed = 1f)
    {
        if (alpha < 1) alpha += 0.1f * (speed * Time.deltaTime);
    }

    void FadeOut(float speed = 1f)
    {
        if (alpha > 0) alpha -= 0.1f * (speed * Time.deltaTime);
    }

    public void TransitionFade(float speed = 3f, float waitTime = 3f)
    {
        if (!transitioning) FadeIn(speed);
        if (alpha >= 1 && !transitioning)
        {
            transitioning = true;
            waitTime += Time.time;
            transitionEvent();
        }
        if (Time.time > waitTime && transitioning)
        {
            FadeOut(speed);
        }
    }

    void TEST() => Debug.Log("TEST");

    void OnRectTransformDimensionsChange()
    {
        CoverScreen();
    }

}
