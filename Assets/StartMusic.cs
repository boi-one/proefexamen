using UnityEngine;

public class StartMusic : MonoBehaviour
{
    public AudioSource music;
    
    void Awake()
    {
        music.Play();
    }
}
