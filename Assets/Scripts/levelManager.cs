using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour {

	public string levelToLoad;
	private Texture2D levelImage;
	private List<Color> levelData;
	private int levelWidth;
	private int levelHeight;

	public delegate void levelLoaded();
	public static event levelLoaded onLevelLoaded;

	public GameObject goalManager;

	enum objectTypes {floor, player, wall, crate, goal };

	//Used to pair color combinations to enum values
	private Dictionary<Vector3, objectTypes> objectDictionary = new Dictionary<Vector3, objectTypes>();

	//Used to pair enum values with loaded prefabs
	private Dictionary<objectTypes, GameObject> prefabDictionary = new Dictionary<objectTypes, GameObject>();

	private struct levelTile {
		GameObject prefab;
		int data;
	}

	void Awake() {
		objectDictionary.Add(new Vector3(255, 255, 255), objectTypes.floor);
		objectDictionary.Add(new Vector3(139, 147, 175), objectTypes.wall);
		objectDictionary.Add(new Vector3(245, 160, 151), objectTypes.player);
		objectDictionary.Add(new Vector3(160, 134, 98 ), objectTypes.crate);
		objectDictionary.Add(new Vector3(89 , 193, 53 ), objectTypes.goal);

		prefabDictionary.Add(objectTypes.wall, Resources.Load("Prefabs/Wall") as GameObject);
		prefabDictionary.Add(objectTypes.player, Resources.Load("Prefabs/Player") as GameObject);
		prefabDictionary.Add(objectTypes.crate, Resources.Load("Prefabs/Crate") as GameObject);
		prefabDictionary.Add(objectTypes.goal, Resources.Load("Prefabs/Goal") as GameObject);
	}

	void Start() {
		loadLevel(levelToLoad);
	}
	
	private void loadLevel(string levelName) {
		levelImage = Resources.Load("LevelData/" + levelName) as Texture2D;
		if (levelImage != null) 
		{ 
			levelWidth = levelImage.width;
			levelHeight = levelImage.height;
			levelData = new List<Color>(levelImage.GetPixels());

			int xx = 0;
			int yy = 0;

			var goalManagerPrefab = Resources.Load("Prefabs/GoalManager") as GameObject;
			goalManager = Instantiate(goalManagerPrefab);
			var managerComponent = goalManager.GetComponent<goalManager>();
			onLevelLoaded += managerComponent.discoverGoals;

			foreach (Color pixel in levelData) {
				Vector3 key = new Vector3((int)(pixel.r * 255), (int)(pixel.g * 255), (int)(pixel.b * 255));

				switch (objectDictionary[key]) {
					case objectTypes.floor:; break;
					case objectTypes.wall:   { Instantiate(prefabDictionary[objectTypes.wall]   , new Vector3(xx, yy), Quaternion.identity); }; break;
					case objectTypes.player: { Instantiate(prefabDictionary[objectTypes.player] , new Vector3(xx, yy), Quaternion.identity); }; break;
					case objectTypes.goal:   { Instantiate(prefabDictionary[objectTypes.goal]   , new Vector3(xx, yy), Quaternion.identity); }; break;
					case objectTypes.crate:  { Instantiate(prefabDictionary[objectTypes.crate]  , new Vector3(xx, yy), Quaternion.identity); }; break;
				}

				xx++;
				if (xx >= levelWidth) { yy++; xx = 0; }
			}

			onLevelLoaded();
			onLevelLoaded = null;

		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
