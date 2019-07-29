using System.Collections;
using UnityEngine;
using Liminal.Core.Fader;
using Liminal.SDK.Core;

/// <summary>
/// EndExperience is used by other monobehaviours to automatically end the experience.
/// </summary>
public static class EndExperience
{
    public static IEnumerator EndExperienceCoro(float audioFadeSpeed = 1f)
    {
        var elapsedTime = 0f;

        ScreenFader.Instance.FadeToBlack();

        while (AudioListener.volume > 0)
        {
            elapsedTime += Time.deltaTime;
            AudioListener.volume -= audioFadeSpeed * Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(Mathf.Clamp(2 - elapsedTime, 0f, 2f));

        ExperienceApp.End();
    }
}

