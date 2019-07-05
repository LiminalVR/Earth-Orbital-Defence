using System.Collections;

using UnityEngine;

using Liminal.Core.Fader;
using Liminal.SDK.Core;

/// <summary>
/// GameTimer is used to control the length of the experience
/// </summary>
public class GameTimer
    : MonoBehaviour
{
    public float MaxGameLength;
    public float GetTime() 
        => _currentTime;

    private float _currentTime;

    // Start is called before the first frame update
    public void StartTimer()
    {
        StartCoroutine(CountdownCoro());
    }

   private IEnumerator CountdownCoro()
   {
       while (_currentTime < MaxGameLength)
       {
           _currentTime += Time.deltaTime;

           yield return new WaitForEndOfFrame();
       }

       ScreenFader.Instance.FadeToBlack(2);

       yield return ScreenFader.Instance.WaitUntilFadeComplete();

       ExperienceApp.End();
    }
}
