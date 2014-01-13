using UnityEngine;
using System.Collections;

public class MasterServerController : MonoBehaviour {
	
	private int connections = 32;
	private int port = 25002;
	private bool useNat = false;
	private bool dedicatedServer = false;
	private string ipAddress = "127.0.0.1";
	private string gameTypeName = "MMORPG";
	private string gameName = "A&D";
	private string comment = "Under Development";

	private HostData[] hostData;

	void Update(){
		if(Input.GetKeyDown(KeyCode.C)){
			InitializeServer();
		}
		if(Input.GetKeyDown(KeyCode.V)){
			PollHost();
		}
		if(Input.GetKeyDown(KeyCode.B)){
			Network.Connect(hostData[0]);
		}
	}

	private void InitializeServer(){
		Debug.Log("Initializing Server");
		useNat = !Network.HavePublicAddress();
		Debug.Log("Use Nat: " + useNat);

		MasterServer.dedicatedServer = dedicatedServer;
		Network.InitializeServer(connections, port, useNat);
		ipAddress = MasterServer.ipAddress;
		Debug.Log(ipAddress);
	}

	private void ConnectToServer(string guid){
		Network.Connect(guid);
	}

	private void OnConnectedToServer(){
		Debug.Log("CONNECTED TO SERVER");
	}

	private void PollHost(){
		Debug.Log("Polling Host");
		MasterServer.RequestHostList(gameTypeName);
	}

	private void DisplayHosts(){
		hostData = MasterServer.PollHostList();

//		
//comment -	A miscellaneous comment (can hold data).
//connectedPlayers -	Currently connected players.
//gameName -	The name of the game (like John Doe's Game).
//gameType	- The type of the game (like "MyUniqueGameType").
//guid	- The GUID of the host, needed when connecting with NAT punchthrough.
//ip	- Server IP address.
//passwordProtected -	Does the server require a password?
//playerLimit	- Maximum players limit.
//port	- Server port.
//useNat	- Does this server require NAT punchthrough?

		foreach(HostData host in hostData){
			string comment = host.comment;
			int connectedPlayers = host.connectedPlayers;
			string gameName = host.gameName;
			string gameType = host.gameType;
			string guid = host.guid;
			string[] ip = host.ip;
			bool passwordProtected = host.passwordProtected;
			int playerLimit = host.playerLimit;
			int port = host.port;
			bool useNat = host.useNat;

			Debug.Log("------------------");
			Debug.Log("Comment: " + comment);
			Debug.Log("Connected Players: " + connectedPlayers);
			Debug.Log ("Game Name: " + gameName);
			Debug.Log("Game Type: " + gameType);
			Debug.Log("GUID: " + guid);
			foreach(string ipAddress in ip)
				Debug.Log("ip: " + ipAddress);
			Debug.Log("Password Protected: " + passwordProtected);
			Debug.Log("Player Limit: " + playerLimit);
			Debug.Log("Port: " + port);
			Debug.Log("Use Nat: " + useNat);
			Debug.Log("------------------");
		}

	}

	void OnServerInitialized() {
		Debug.Log ("Server Initialized");
		MasterServer.RegisterHost(gameTypeName, gameName, comment);
	}

	void OnMasterServerEvent(MasterServerEvent msEvent) {
		Debug.Log("msEvent: " + msEvent.ToString());

		if(msEvent == MasterServerEvent.HostListReceived){
			Debug.Log("Host List Received");		
			DisplayHosts();
		}

		if(msEvent == MasterServerEvent.RegistrationFailedGameName)
			Debug.Log("Registration Failed Game Name");

		if(msEvent == MasterServerEvent.RegistrationFailedGameType)
			Debug.Log("Registration Failed Game Type");

		if(msEvent == MasterServerEvent.RegistrationFailedNoServer)
			Debug.Log("Registration Failed No Server");

		if (msEvent == MasterServerEvent.RegistrationSucceeded)
			Debug.Log("Registration Succeeded");
	}

	void OnFailedToConnectToMasterServer(NetworkConnectionError info) {
		Debug.Log("Could not connect to master server: " + info);
	}
}
