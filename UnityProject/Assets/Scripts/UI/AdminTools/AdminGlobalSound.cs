﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lets Admins play sounds
/// </summary>
public class AdminGlobalSound : MonoBehaviour
{
	[SerializeField]
	private GameObject buttonTemplate;
	private AdminGlobalSoundSearchBar SearchBar;
	public List<GameObject> soundButtons = new List<GameObject>();

	private void Awake()
	{
		SearchBar = GetComponentInChildren<AdminGlobalSoundSearchBar>();
		SoundList();
	}

	/// <summary>
	/// Generates buttons for the list
	/// </summary>
	public void SoundList()
	{
		if (SearchBar != null)
		{
			SearchBar.Resettext();
		}

		foreach (var pair in SoundManager.Instance.sounds)//sounds is a readonly so will never change hopefully
		{
			if (pair.Value.loop) return;

			GameObject button = Instantiate(buttonTemplate) as GameObject;//creates new button
			button.SetActive(true);
			button.GetComponent<AdminGlobalSoundButton>().SetAdminGlobalSoundButtonText(pair.Key);
			soundButtons.Add(button);

			button.transform.SetParent(buttonTemplate.transform.parent, false);
		}
	}

	public void PlaySound(string index)//send sound to sound manager
	{
		var players = FindObjectsOfType(typeof(PlayerScript));

		if (players == null) return;//If list of Players is empty dont run rest of code.
		
		foreach (PlayerScript player in players)
		{
			SoundManager.PlayNetworkedForPlayerAtPos(player.gameObject, player.gameObject.GetComponent<RegisterTile>().WorldPositionClient, index);
		}
	}
}
