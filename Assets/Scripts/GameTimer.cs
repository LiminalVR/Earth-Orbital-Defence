using System;
using System.Collections;

using UnityEngine;

using Liminal.Core.Fader;
using Liminal.SDK.Core;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>
/// GameTimer is used to control the length of the experience and display how long until the game ends.
/// </summary>
public class GameTimer
    : MonoBehaviour
{
    public float MaxGameLength;
    public float CurrentTime { get; private set; }
    public float CountdownTime => MaxGameLength - CurrentTime;

    public Text TextPanel;

    public void OnValidate()
    {
        Assert.IsNotNull(TextPanel, "TextPanel must not be null!");
    }

    // Start is called before the first frame update
    public void StartTimer()
    {
        StartCoroutine(CountdownCoro());
    }

   private IEnumerator CountdownCoro()
   {
       while (CurrentTime < MaxGameLength)
       {
           CurrentTime += Time.deltaTime;

           var minutes = Mathf.Floor(CountdownTime / 60).ToString("00");
           var seconds = (CountdownTime % 60).ToString("00");

           TextPanel.text = $"{minutes}:{seconds}";

           yield return new WaitForEndOfFrame();
       }

       TextPanel.text = $"00:00";
       ScreenFader.Instance.FadeToBlack(2);

       yield return ScreenFader.Instance.WaitUntilFadeComplete();

       ExperienceApp.End();
    }
}
