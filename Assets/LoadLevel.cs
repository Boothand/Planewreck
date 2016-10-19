using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class ColorToPrefab
{
	public Color32 color;
	public GameObject prefab;
}

public class LoadLevel : MonoBehaviour
{
	[SerializeField]
	string levelName;

	[SerializeField]
	Texture2D levelImage;

	[SerializeField]
	ColorToPrefab[] colorToPrefab;

	public static string[] mapString() // use this to list all maps
	{

		//FileInfo[] fileInfo = System.IO.FileInfo
		//i dont know what im doing...

		string[] a = new string[3];
		return a;
		
	}

	void LoadMap()
	{
		Color32[] allPixels = levelImage.GetPixels32();
		int width = levelImage.width;
		int height = levelImage.height;

		for (int x = 0; x < width; x++)
		{
			for (int z = 0; z < height; z++)
			{
				SpawnTileAt(allPixels[(z * width) + x], x, z);
			}
		}

		StartCoroutine(CheckForDestroy());
	}

	void SpawnTileAt(Color32 c, int x, int z)
	{

		if (c.a <= 0) // Fully transparent, ignore air.
		{
			return;
		}

		// Find the right color in the image


		// NOTE: optimize this by using a dictionary lookup instead
		foreach (ColorToPrefab ctp in colorToPrefab)
		{
			if (ctp.color.Equals(c))
			{
				GameObject go = Instantiate(ctp.prefab, new Vector3(x-50f, 0, z-50f), Quaternion.identity);
				go.transform.localScale = new Vector3(1f, 5f, 1f);
				go.transform.SetParent(this.transform);
				return;
			}
		}
		Debug.LogError("No color to prefab found for: " + c.ToString());
	}
	
	
	void Start ()
	{
		LoadMap();
	}

	IEnumerator CheckForDestroy()
	{
		yield return new WaitForSeconds(5f);

		foreach(Transform t in transform)
		{
			if (t.transform.position.y < -1f)
			{
				Destroy(t);
			}
		}

		StartCoroutine(CheckForDestroy());
		print("End of CheckForDestroy()");
	}
	
}
