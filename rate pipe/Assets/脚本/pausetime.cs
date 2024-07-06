using System.Collections;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused = false;
    public float pauseDuration = 3f;
    public GameObject objectToActivate;
    public AudioClip soundEffect;
    private float pauseEndTime;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !isPaused)
        {
            PauseGame();
            StartCoroutine(ActivateObjectAndPlaySound());
        }

        if (isPaused && Time.realtimeSinceStartup >= pauseEndTime)
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        isPaused = true;
        pauseEndTime = Time.realtimeSinceStartup + pauseDuration;
    }

    void ResumeGame()
    {
        isPaused = false;
    }

    IEnumerator ActivateObjectAndPlaySound()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        if (soundEffect != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }

        yield return new WaitForSecondsRealtime(0.5f);

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
    }
}
