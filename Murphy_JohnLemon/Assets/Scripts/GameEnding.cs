using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEnding : MonoBehaviour
{
    // instantiators
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;
    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI gameText;

    public float timeLeft = 60f;

    void Start()
    {
      SetCountText();
      SetGameText();
    }
    // writing triggers for when player is at exit
    void onTriggerEnter(Collider other)
    {
      if (other.gameObject == player)
      {
        m_IsPlayerAtExit = true;
      }
    }

    void SetCountText()
    {

      countText.text = "Time Left: " + timeLeft.ToString();
    }

    void SetGameText()
    {

      gameText.text = "Shoot and avoid enemies, find the key!";
    }

    // bool function updating IsPlayerCaught boolean
    public void CaughtPlayer ()
    {
      m_IsPlayerCaught = true;
    }


    void Update ()
    {
      // calling end level with proper arguments for if player wins
      if(m_IsPlayerAtExit)
      {
        EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
      }
      // calling end with proper arguments for if player dies
      else if (m_IsPlayerCaught)
      {
        EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
      }

      timeLeft -= Time.deltaTime;

      if (timeLeft <= 0)
      {
        EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
      }

      SetCountText();
      SetGameText();
    }

    void EndLevel (CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {

      // play audio and set boolean
      if(!m_HasAudioPlayed)
      {
        audioSource.Play();
        m_HasAudioPlayed = true;
      }

      // update timer
      m_Timer += Time.deltaTime;
      // set alpha
      imageCanvasGroup.alpha = m_Timer / fadeDuration;

      if(m_Timer > fadeDuration + displayImageDuration)
      {
        // reload if game ends
        if(doRestart)
        {
          SceneManager.LoadScene(0);
        }
        // quit application
        else
        {
          Application.Quit();
        }
      }
    }
}
