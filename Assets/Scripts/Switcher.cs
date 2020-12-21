using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Switcher : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Button Level1;
	[SerializeField] string Level1Name;
	[SerializeField] Button Level2;
	[SerializeField] string Level2Name;

#pragma warning restore 0649

	private void Awake()
	{
		Level1.onClick.AddListener(LoadLevel1);
		Level2.onClick.AddListener(LoadLevel2);
	}

	void LoadLevel1()
	{
		SceneManager.LoadScene(Level1Name);
	}

	void LoadLevel2()
	{
		SceneManager.LoadScene(Level2Name);
	}
}
