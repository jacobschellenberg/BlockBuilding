using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BlockType{
	Dirt,
	Water
}

public class WorldBuilder : MonoBehaviour {

	public GameObject playerPrefab;
	public bool runInBackground = true;
	static public List<Block> blocks = new List<Block>();

	private bool playerSpawned = false;
	private int currentIndex = 0;
	private List<BlockTransport> blockTransports = new List<BlockTransport>();
	private float saveTime = 10;

	static public float TimeUntilSave{get;set;}

	void Awake(){
		Application.runInBackground = runInBackground;
		StartCoroutine("LoadWorld");
	}

	void Update(){
		TimeUntilSave -= Time.deltaTime;
		//Debug.Log("SAVE TIME: " + TimeUntilSave);

		if(TimeUntilSave <= 0){
			SaveWorld();
			TimeUntilSave = saveTime;
		}
	}

	void FixedUpdate(){
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			StartCoroutine("LoadWorld");
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			SaveWorld();
		}
	}
	
	void SaveWorld(){
		Debug.Log("SAVING WORLD");

		foreach(Block block in blocks){
			BlockTransport transport = new BlockTransport(){
				HitPoints = block.hitPoints,
				BlockType = block.blockType,
				X = block.transform.position.x,
				Y = block.transform.position.y,
				Z = block.transform.position.z,
				Owners = block.owners
			};

			blockTransports.Add(transport);
			
		}
	
		JSONSerializer js = new JSONSerializer();
		js.Serialize(blockTransports);
		blockTransports.Clear();

		Debug.Log("WORLD SAVED");
	}
	
	IEnumerator LoadWorld(){
		JSONSerializer serializer = new JSONSerializer();
		List<BlockTransport> transports = serializer.Deserialize();
		Debug.Log("TRANSPORTS: " + transports.Count);
		Debug.Log("BLOCKS: " + blocks.Count);
		currentIndex = 0;

//		foreach(BlockTransport transport in transports){
		while(currentIndex < transports.Count){
			BlockTransport transport = new BlockTransport();
			transport = transports[currentIndex];

			Vector3 position = new Vector3(transport.X, transport.Y, transport.Z);

			GameObject block = (GameObject)Instantiate((GameObject)Resources.Load("Block"), position , Quaternion.identity);

			blocks.Add(block.GetComponent<Block>());
			block.GetComponent<Block>().position = new Vector3(transport.X, transport.Y, transport.Z);
			block.GetComponent<Block>().hitPoints = transport.HitPoints;
			block.GetComponent<Block>().owners = transport.Owners;
			block.GetComponent<Block>().blockType = transport.BlockType;

			currentIndex++;
		}

		if(!playerSpawned){
			Instantiate(playerPrefab, new Vector3(0,10,0), Quaternion.identity);
			playerSpawned = true;
		}
		Debug.Log("BLOCKS: " + blocks.Count);

		yield return null;
	}
}
