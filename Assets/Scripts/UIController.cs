using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

	private static UIController _instance;

	// https://dafont.com
	// find a font to use it on the textmeshpro controls => 10 puntos. done
	[SerializeField]
	TextMeshProUGUI scoreTextBox;

	[SerializeField]
	Transform livesContainer;

	bool hasLives = true;

	private void Awake()
	{


		_instance = this;

	}
	public void IncreaseScore(float points)
	{
		float score = float.Parse(scoreTextBox.text);
		score += points;
		scoreTextBox.text = score.ToString();
	}


	//IMPLEMENT SINGLETON
	public static UIController Instance { get { return _instance; } }

	public void DecreaseLives()
	{
		int maxLiveNumber = 0;
		Image[] liveImages = livesContainer.GetComponentsInChildren<Image>();
		Image maxLiveImage = null;

		foreach (Image image in liveImages)
		{
			if (image.name.StartsWith("Live-") && image.enabled)
			{
				int lifeNumber = int.Parse(image.name.Remove(0, 5));
				if (maxLiveNumber == 0 || lifeNumber > maxLiveNumber)
				{
					maxLiveNumber = lifeNumber;
					maxLiveImage = image;
				}
			}
		}
		if (maxLiveImage != null)
		{
			maxLiveImage.enabled = false;
		}

		//hasLives = maxLiveNumber > 0;
	}

	public bool HasLives()
	{
		return hasLives;
	}
}
