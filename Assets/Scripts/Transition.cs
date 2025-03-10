using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Make sure the Transition script is applied to a canvas image
/// </summary>

[RequireComponent(typeof(Image))]
public class Transition : SingletonMonobehaviour<Transition>
{
    public static Transition reference;
    Image transitionImage;
    CanvasGroup transitionScreen;
    float alpha = 0;
    bool transitioningBack = false;
    bool startTransition = false;
    Action transitionEvent = () => { };
    float transitionSpeed = 0;
    float waitTime = 0;
    
    public Sprite[] Images ;

    void Awake()
    {
        reference = this;
        transitionImage = GetComponent<Image>();
        transitionScreen = GetComponent<CanvasGroup>();
        DontDestroyOnLoad(transform.parent.gameObject);
            
    }

    void Update()
    {
        if(transitionScreen) transitionScreen.alpha = alpha;

        if (startTransition)
        {transitionScreen = GetComponent<CanvasGroup>();
            TransitionFade(transitionSpeed, waitTime);
        }
    }

    /// <summary>
    /// Start a fade effect
    /// <param name="color"> the color of the fade (defaults to black), </param>
    /// <param name="transitionSpeed"> how fast the faded happens (higher is faster), </param>
    /// <param name="waitTime"> how much time it takes to fade out again </param>
    /// </summary>
    public void StartTransition() => StartTransition(transitionSpeed: 10f, waitTime: 7);
    public void StartTransition(Vector3 color = default, float transitionSpeed = 3f, float waitTime = 3f, bool loading = true)
    {
        if (loading)
        {
            transitionImage.sprite = Images[0];
            StartCoroutine(LoadingScreen(0.5f));
        }
        else transitionImage.sprite = null;
        this.transitionSpeed = transitionSpeed;
        this.waitTime = waitTime;
        startTransition = true;
        transitioningBack = false;
    }

    IEnumerator LoadingScreen(float time)
    {
        for (int i = 0; i < Images.Length; i++)
        {
            transitionImage.sprite = Images[i];
            yield return new WaitForSeconds(time);
            i = i >= 3 ? -1 : i;
        }
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
