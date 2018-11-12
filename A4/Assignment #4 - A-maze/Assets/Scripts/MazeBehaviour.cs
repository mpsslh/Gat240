using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/// <summary>
/// The maze behaviour controls the creation and access to the maze
/// Students must parse data from Xml
/// Students must then use this parsed data to generate the maze using prefabs
/// Functions are given for direction and to start the xml process
/// You are welcome to make any adjustments you need to make this work
/// </summary>
/// 
public class MazeBehaviour : MonoBehaviour
{
	[Header("The maze data to generate: switch to new mazes to test!")]
	public TextAsset mazeData;

	[Header("These properties should NOT change")]
	[Tooltip("The location to add the tiles")]
	public GameObject tileContainer;
	[Tooltip("The location to add objects like Waldorf")]
	public GameObject objectContainer;
	[Tooltip("The main camera used for the maze")]
	public Camera mazeCamera;

	[Tooltip("Prefab: open tile,use in level generation")]
	public GameObject open0;
	[Tooltip("Prefab: wall,use in level generation")]
	public GameObject wall1;
	// Use this for initialization
	void Start()
	{
		// Let's generate the maze!
		GenerateMaze();
	}

	/// <summary>
	/// Function to generate the maze called at the start of the game
	/// </summary>
	private void GenerateMaze()
	{
		XmlDocument xml = CreateXmlFromData();
		CreateTiles(xml);
		CreateWaldorf(xml);
		SetCamera(xml);
	}

	/// <summary>
	/// Creates the tiles from the xml data
	/// You will need to make a list of prefabs (0 = open, 1 = wall)
	/// The maze/tile xml has attributes "xdist" & "zdist" which controls the distance between tiles
	/// the maze/tile xml has a list of tiles: "#" seperates rows; "," sperates columns
	/// </summary>
	/// <param name="xml">The xml document that contains the data</param>
	private void CreateTiles(XmlDocument xml)
	{
		XmlElement maze = (XmlElement)xml.GetElementsByTagName("maze")[0];
		XmlElement tile = (XmlElement)xml.GetElementsByTagName("tile")[0];
		int xdist = int.Parse(tile.GetAttributeNode("xdist").InnerXml);
		int zdist = int.Parse(tile.GetAttributeNode("zdist").InnerXml);
		string tiles = maze.InnerXml;
		string[] rows = tiles.Split('#'); //splits strings into columns and stores them in a string array
		for (int i = 0; i < rows.Length; i++)
		{
			string[] columns = rows[i].Split(',');
			for (int j = 0; j < columns.Length; j++)
			{
				if (columns[j] == "0")
				{
					GameObject g = Instantiate(open0);
					g.GetComponent<Transform>().position = new Vector3(j * xdist, -wall1.GetComponent<Transform>().position.y, i * zdist);
					g.transform.parent = tileContainer.GetComponent<Transform>();
					g.name = j + "_" + i;
					g.GetComponent<TileBehaviour>().SetLocation(j,i);
				}
				else if (columns[j] == "1")
				{
					GameObject g = Instantiate(wall1);
					g.GetComponent<Transform>().position = new Vector3(j * xdist, 0, i * zdist);
					g.transform.parent = tileContainer.GetComponent<Transform>();
					g.name = j + "_" + i;
					g.GetComponent<TileBehaviour>().SetLocation(j,i);
				}
			}
		}
	}

	/// <summary>
	/// Creates a Waldorf character and places him within the maze
	/// You will need to have a Waldorf prefab
	/// The maze xml has attributes "spawn" which is in the format [xtile,ztile]
	/// </summary>
	/// <param name="xml">The xml document that contains the data</param>
	private void CreateWaldorf(XmlDocument xml)
	{
		//xml, generate and put at location
		XmlElement maze = (XmlElement)xml.GetElementsByTagName("maze")[0];
		string spawn = maze.GetAttributeNode("spawn").InnerXml;
		string[] spawn_pos = spawn.Split(',');
		GameObject wah = Instantiate(objectContainer);
		wah.GetComponent<WaldorfBehaviour>().SnapToTile(GameObject.Find(spawn_pos[0] +"_"+ spawn_pos[1]).GetComponent<TileBehaviour>());
	}

	/// <summary>
	/// Takes the maze camera and places it at an initial position and rotation
	/// You will need access to the camera in some way
	/// </summary>
	/// <param name="xml">The xml document that contains the data</param>
	private void SetCamera(XmlDocument xml)
	{
		XmlElement maze = (XmlElement)xml.GetElementsByTagName("maze")[0];
		string cam_pos = maze.GetAttributeNode("camPos").InnerXml;
		string[] p_tokens = cam_pos.Split(','); //splits strings into tokens and stores them in a string array, [0]:x [1]:y [2]:z
		mazeCamera.GetComponent<Transform>().position = new Vector3(int.Parse(p_tokens[0]), int.Parse(p_tokens[1]), int.Parse(p_tokens[2]));
		string cam_rot = maze.GetAttributeNode("camRot").InnerXml;
		string[] rot_tokens = cam_rot.Split(','); //splits strings into tokens and stores them in a string array, [0]:x [1]:y [2]:z
		mazeCamera.GetComponent<Transform>().rotation = Quaternion.Euler(int.Parse(rot_tokens[0]), int.Parse(rot_tokens[1]), int.Parse(rot_tokens[2]));
	}

	/// <summary>
	/// Creates a new xml document from the text asset attached to the behaviour
	/// </summary>
	/// <returns>The xml data</returns>
	private XmlDocument CreateXmlFromData()
	{
		XmlDocument xml = new XmlDocument();
		xml.LoadXml(mazeData.text);
		return xml;
	}
}
