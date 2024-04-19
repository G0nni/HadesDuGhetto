using UnityEngine;

public class KonamiCode : MonoBehaviour
{
    private KeyCode[] konamiCode = { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.B, KeyCode.A };
    private int currentIndex = 0;
    public AudioClip musicClip;
    public AudioSource audioSource;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(konamiCode[currentIndex]))
        {
            Debug.Log("Touche pressée : " + konamiCode[currentIndex]);
            currentIndex++;

            if (currentIndex == konamiCode.Length)
            {
                Debug.Log("Code Konami correctement entré !");

                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }

                if (musicClip != null)
                {
                    audioSource.clip = musicClip;
                    audioSource.Play();
                }

                currentIndex = 0;
            }
            else
            {
                Debug.Log("Prochaine touche à presser : " + konamiCode[currentIndex]);
            }
        }
    }
}
