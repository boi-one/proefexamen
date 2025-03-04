using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Make sure the Transition script is applied to a canvas image
/// </summary>
public class Transition : MonoBehaviour
{
    public static Transition reference;
    Image transitionScreen;
    float alpha = 0;
    bool transitioning = false;
    bool startTransition = false;
    Vector3 transitionColor = new Vector3(0, 0, 0);
    Action transitionEvent = () => { };
    float transitionSpeed = 0;
    float waitTime = 0;

    void Awake()
    {
        reference = this;
        transitionScreen = GetComponent<Image>();
        CoverScreen();
    }

    void Update()
    {
        transitionScreen.color = new Vector4(transitionColor.x, transitionColor.y, transitionColor.z, alpha);

        if (startTransition)
        {
            TransitionFade(transitionSpeed, waitTime);
        }
    }

    /// <summary>
    /// Start a fade effect
    /// <param name="color"> the color of the fade (defaults to black), </param>
    /// <param name="transitionSpeed"> how fast the faded happens (higher is faster), </param>
    /// <param name="waitTime"> how much time it takes to fade out again </param>
    /// </summary>
    public void StartTransition(Vector3 color = default, float transitionSpeed = 3f, float waitTime = 3f)
    {
        this.transitionSpeed = transitionSpeed;
        this.waitTime = waitTime;
        this.transitionColor = color;

        startTransition = true;
        transitioning = false;
    }

    public void AddFunction(Action function)
    {
        void Temp()
        {
            transitionEvent -= Temp;

            function.Invoke();
        }

        transitionEvent += Temp;
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
}
