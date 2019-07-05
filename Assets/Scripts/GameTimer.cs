using System.Collections;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>
/// GameTimer is used to control the length of the experience and display how long until the game ends. It also tells <see cref="Fire"/> that users can fire/not fire.
/// </summary>
public class GameTimer
    : MonoBehaviour
{
    public float MaxGameLength;
    public float CurrentTime { get; private set; }
    public float CountdownTime => MaxGameLength - CurrentTime;
    public float AudioFadeSpeed;

    public Text TextPanel;
    public Fire FireController;

    public void OnValidate()
    {
        Assert.IsNotNull(TextPanel, "TextPanel must not be null!");
        Assert.IsNotNull(FireController, "FireController must not be null!");
    }

    // Start is called before the first frame update
    public void StartTimer()
    {
        StartCoroutine(TimerCoro());
    }

    private IEnumerator TimerCoro()
    {
        FireController.CanFire(true);

        while (CurrentTime < MaxGameLength)
        {
            CurrentTime += Time.deltaTime;

            var minutes = Mathf.Floor(CountdownTime / 60).ToString("00");
            var seconds = (CountdownTime % 60).ToString("00");

            TextPanel.text = $"{minutes}:{seconds}";

            yield return new WaitForEndOfFrame();
        }

        TextPanel.text = $"00:00";

        yield return EndExperience.EndExperienceCoro(AudioFadeSpeed);
    }
}
