using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    public static Transition reference;
    Image transitionScreen;
    float alpha = 0;
    bool transitioningBack = false;
    bool startTransition = false;
    Vector3 transitionColor = new(0, 0, 0);
    Action transitionEvent = () => { };
    float transitionSpeed = 0;
    float waitTime = 0;
    

    void Awake()
    {
        reference = this;
        transitionScreen = GetComponent<Image>();
        CoverScreen();
        DontDestroyOnLoad(transform.parent.gameObject);
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
    /// </summary>
    /// <param name="transitionSpeed"> how fast the faded happens </param>
    /// <param name="waitTime"> how much time it takes to fade out again </param>
    /// <param name="color"> the color of the fade, defaults to black </param>
    public void StartTransition() => StartTransition(3);
    public void StartTransition(float transitionSpeed = 3f, float waitTime = 3f, Vector3 color = default)
    {
        this.transitionSpeed = transitionSpeed;
        this.waitTime = waitTime;
        this.transitionColor = color;

        startTransition = true;
        transitioningBack = false;
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
        if (!transitioningBack) FadeIn(speed);
        if (alpha >= 1 && !transitioningBack)
        {
            transitioningBack = true;
            waitTime += Time.time;
            transitionEvent();
        }
        if (Time.time > waitTime && transitioningBack)
        {
            FadeOut(speed);
        }
    }
}
