using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Tile behaviour is used to handle the details for each individual tile
/// Each tile has a state which dictates how much weight to give for movement (0-1)
/// The tile will change material to reflect it's current state
/// NOTE: students do NOT need to make any changes to this class (but you may)
/// </summary>
public class TileBehaviour : MonoBehaviour
{
	/// <summary>
	/// The various states the tile can be in
	/// </summary>
	public enum State
	{
		Wall, // a blocking state
		Stage1, // never traversed
		Stage2, // traversed once
		Stage3, // traversed twice
		Block, // traversed three times so block this path
		Brittle,
		TotalStates // Do not reference this, it is here to give a total amount of defined states
	}

	// The starting state is used to make an open and wall tile
	[Header("Do not adjust these properties unless you want to make new tiles!")]
	public State startState = State.Wall;

	[Tooltip("The mesh so we can change material.")]
	public MeshRenderer theMesh;
	[Tooltip("A list of materials for each state.")]
	public List<Material> tileMaterial;
	[Tooltip("A list of movement weights to use for each state.")]
	public List<float> moveWeight;

	private State currentState; // the current state of the tile
	private int xPos; // the x position in the tile grid
	private int zPos; // the z position in the tile grid

	/// <summary>
	/// Getter/Setter for the tile state
	/// Set: assigns the state and adjusts the material
	/// </summary>
	public State TileState
	{
		get
		{
			return currentState;
		}
		set
		{
			currentState = value;
			theMesh.material = tileMaterial[(int)currentState];
		}
	}

	/// <summary>
	/// The getter for the move weight based on the current state
	/// </summary>
	public float MoveWeight
	{
		get
		{
			return moveWeight[(int)currentState];
		}
	}

	/// <summary>
	/// The getters for the X & Z grid position
	/// </summary>
	public int X { get { return xPos; } }
	public int Z { get { return zPos; } }

	/// <summary>
	/// Done when creating the tile to assign a position for this tile in the grid
	/// </summary>
	/// <param name="tileX">the x position in the grid</param>
	/// <param name="tileZ">the z position in the grid</param>
	public void SetLocation(int tileX, int tileZ)
	{
		xPos = tileX;
		zPos = tileZ;
	}


	/// <summary>
	/// Called when a tile has been moved to (whenever is most relevant)
	/// Increases the state of the tile by 1
	/// </summary>
	public void TileSelected()
	{
		if (TileState == State.Block || TileState == State.Wall) return;
		if (TileState == State.Brittle)
		{
			TileState = (State)((int)TileState - 1);
		}
		else
			TileState = (State)((int)TileState + 1);
	}

	void Start()
	{
		// Safety checks to make sure we don't break
		if (tileMaterial.Count != (int)State.TotalStates)
			Debug.Log("Not enough colors defined for the tile, this will crash!");
		if (moveWeight.Count != (int)State.TotalStates)
			Debug.Log("Not enough weights defined for the tile, this will crash!");

		TileState = startState; // set the correct start state
	}
}
