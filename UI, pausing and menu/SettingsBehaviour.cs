using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public interface SettingsAffectable
{
	void ApplySettingsValues();
}

public class PlayerSettings
{
	public bool hasGun = true;
	public bool hasSword = true;

	public PlayerSettings(bool g, bool s)
	{
		hasGun = g;
		hasSword = s;
	}
}

public class SettingsBehaviour : MonoBehaviour
{
	public Slider LavaSlider;
	public Image lavaSliderImage;

	public Toggle toggle;

	public Dropdown mapsListDropdown;
	public Dropdown numberOfPlayersDrowpdown;

	public Slider NumberOfBulletsSlider;
	public Text NumberOFBulletsSliderText;

	public GameObject thirdPlayer;

	public Toggle[] gunsToggles;
	public Toggle[] swordsToggles;

	public Slider gunStrengthSlider;
	public Slider swordStrengthSlider;

	void Awake()
	{
		List<Dropdown.OptionData> tmpOptions = new List<Dropdown.OptionData>();
		foreach (var name in Globals.mapNames)
			tmpOptions.Add(new Dropdown.OptionData(name));
		tmpOptions = tmpOptions.OrderBy(o => o.text).ToList();
		mapsListDropdown.AddOptions(tmpOptions);

		int iterator = 0;
		foreach (var item in tmpOptions)
		{
			Globals.mapNames[iterator] = item.text;
			iterator += 1;
		}
	}

	void Start()
	{
		SliderLavaValueChnaged();
	}

	public void NumberOfPlayerChanged(Dropdown thisDropdown)
	{
		if (thisDropdown.value == 0)
		{
			thirdPlayer.SetActive(false);
		}
		else
		{
			thirdPlayer.SetActive(true);
		}
	}

	public void LavaToggle(Toggle toggle)
	{
		LavaSlider.gameObject.SetActive(toggle.isOn);
	}

	public void SliderGunStrengthChanged(Text textBox)
	{
		textBox.text = gunStrengthSlider.value.ToString();
	}

	public void SliderSwordStrengthChanged(Text textBox)
	{
		textBox.text = swordStrengthSlider.value.ToString();
	}

	public void SliderLavaValueChnaged()
	{
		float interpolationCoe = LavaSlider.value / (LavaSlider.maxValue / 2);

		if (interpolationCoe < 1)
			lavaSliderImage.color = Color.Lerp(Color.green, Color.yellow, interpolationCoe);
		else
			lavaSliderImage.color = Color.Lerp(Color.yellow, Color.red, interpolationCoe - 1);
	}

	public void SliderNumberOfBulletsValueChanged()
	{
		if (NumberOfBulletsSlider.value == NumberOfBulletsSlider.maxValue)
			NumberOFBulletsSliderText.text = "∞";
		else
			NumberOFBulletsSliderText.text = NumberOfBulletsSlider.value.ToString();
	}

	/// <summary>
	/// //////// dropDown on main screen second page for changing game type settings
	/// </summary>

	public Dropdown GameTyepDropDown;
	public GameObject requiredSettings;
	public GameObject customSettings;

	public void OnValueChnagedOfGameTypeDropDown()
	{
		customSettings.SetActive(GameTyepDropDown.value == 1);
	}
}
