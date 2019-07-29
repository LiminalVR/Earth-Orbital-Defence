using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>
/// ScoreMenu is used to display how well the player did in the experience.
/// </summary>
public class ScoreMenu 
    : MonoBehaviour
{
    public Text ShotsDisplay;
    public Text EnemiesDisplay;
    public Text AccuracyDisplay;

    public void OnValidate()
    {
        Assert.IsNotNull(EnemiesDisplay, "EnemiesDisplay should not be null.");
        Assert.IsNotNull(ShotsDisplay, "ShotsDisplay should not be null.");
        Assert.IsNotNull(AccuracyDisplay, "AccuracyDisplay should not be null.");
    }

    public IEnumerator DisplayScoreCoro(int shotsFired, int enemiesHit)
    {
        var shotsText = $"Shots Fired: {shotsFired}";
        var enemiesText = $"Enemies Destroyed: {enemiesHit}";
        var accuracy = ((float) enemiesHit / shotsFired) * 100;
        var accText = $"Accuracy: {accuracy:F2}%";

        yield return new WaitForSeconds(0.25f);
        StartCoroutine(WriteTextCoro(ShotsDisplay, shotsText));

        yield return new WaitForSeconds(0.25f);
        StartCoroutine(WriteTextCoro(EnemiesDisplay, enemiesText));

        yield return new WaitForSeconds(0.25f);

        yield return WriteTextCoro(AccuracyDisplay, accText);

        yield return new WaitForSeconds(5f);
    }

    private IEnumerator WriteTextCoro(Text display, string textToWrite, float timeToWrite = 1f)
    {
        var waitTime = timeToWrite / textToWrite.Length;

        foreach (var character in textToWrite)
        {
            display.text += character;

            if(character != ' ')
                yield return new WaitForSeconds(waitTime);
        }
    }
}
