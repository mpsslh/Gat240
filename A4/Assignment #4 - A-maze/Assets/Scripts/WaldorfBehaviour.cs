using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Waldorf behaviour controls our hero as he attempts to escape from the maze
/// Students need to implement four states for Waldorf (Thinking, Moving, Falling, Stuck)
/// Students need to implement the decision logic for which tile to move to next
/// Functions are given to handle the movement between tiles
/// You are welcome to make any adjustments you need to get this working
/// </summary>
public class WaldorfBehaviour : MonoBehaviour
{
	enum Waldorf_States { Thinking, Moving, Falling, Stuck };
	public const float MIN_DIST = 0.05f; // the minimum distance before changing directions

	[Header("Allows for animations: Do NOT touch!")]
	public Animator animate;

	[Header("Controls the movement speed and thinking time, adjust as desired")]
	public float moveSpeed = 1.0f;
	public float thinkTime = 0.25f;

	public GameObject startingPosition;

	// the current tile location Waldorf belongs to
	// NOTE: this is null when Waldorf is moving as he doesn't belong to any tile
	private TileBehaviour location = null;

	// the tile location Waldorf is currently moving to
	// NOTE: this is null when Waldorf is thinking as he doesn't know his next target
	private TileBehaviour target = null;

	private float movementspeed = 5.0f;
	Waldorf_States Current_state;
	// Use this for initialization
	void Start()
	{
		Current_state = Waldorf_States.Thinking;
	}

	// Update is called once per frame
	void Update()
	{
		switch (Current_state)
		{
			case Waldorf_States.Thinking:
				animate.SetTrigger("think");
				Current_state = Waldorf_States.Moving;
				Think();
				//think will set state to stuck if surrounding tiles has weights of 0
				break;
			case Waldorf_States.Moving:
				animate.SetTrigger("move");
				UpdateMove();
				Current_state = Waldorf_States.Thinking;
				break;
			case Waldorf_States.Falling:
				animate.SetTrigger("fall");
				transform.position += transform.forward * Time.deltaTime * movementspeed;
				break;
			case Waldorf_States.Stuck:
				break;

			default: break;
		}
	}
	IEnumerator Wait()
	{
		yield return new WaitForSecondsRealtime(thinkTime);
	}

	private void ReachGoal(int x, int z)
	{
		Current_state = Waldorf_States.Falling;
		return;
	}

	/// <summary> 
	/// Waldorf ponders about life,
	/// and also the next tile to move to
	/// </summary>
	public void Think()
	{
		if (location == null)
			return;
		GameObject top = null, bot = null, left = null, right = null;
		//StartCoroutine(Wait());
		int x = location.X;
		int z = location.Z;
		float heaviest_weight = 0.0f;
		TileBehaviour new_target = null;
		Debug.Log("CURRENTLY" + location.name);
		if (top = GameObject.Find(x + "_" + (z + 1)))
		{
			if (top.GetComponent<TileBehaviour>().MoveWeight > heaviest_weight)
			{
				heaviest_weight = top.GetComponent<TileBehaviour>().MoveWeight;
				Debug.Log("top"+ top.GetComponent<TileBehaviour>().MoveWeight+"name"+ x + "_" + (z + 1));
				new_target = top.GetComponent<TileBehaviour>();
			}
		}
		else
		{
			ReachGoal(x, z + 1);
			return;
		}
		if (left = GameObject.Find((x - 1) + "_" + z))
		{
			Debug.Log("left" + top.GetComponent<TileBehaviour>().MoveWeight + "name"+(x - 1) + "_" + z);
			if (left.GetComponent<TileBehaviour>().MoveWeight > heaviest_weight)
			{
				heaviest_weight = left.GetComponent<TileBehaviour>().MoveWeight;
				new_target = left.GetComponent<TileBehaviour>();
			}
		}
		else
		{
			ReachGoal(x, z + 1);
			return;
		}
		if (right = GameObject.Find((x + 1) + "_" + z))
		{
			Debug.Log("right" + top.GetComponent<TileBehaviour>().MoveWeight + "name"+ (x + 1) + "_" + z);
			if (right.GetComponent<TileBehaviour>().MoveWeight > heaviest_weight)
			{
				heaviest_weight = right.GetComponent<TileBehaviour>().MoveWeight;
				new_target = right.GetComponent<TileBehaviour>();
			}
		}
		if (bot = GameObject.Find(x + "_" + (z - 1)))
		{
			Debug.Log("bot" + top.GetComponent<TileBehaviour>().MoveWeight + "name"+ x + "_" + (z - 1));
			if (bot.GetComponent<TileBehaviour>().MoveWeight > heaviest_weight)
			{
				heaviest_weight = bot.GetComponent<TileBehaviour>().MoveWeight;
				new_target = bot.GetComponent<TileBehaviour>();
			}
		}
		if (new_target)
		{
			Debug.Log("New target" + new_target.X + "_" + new_target.Z);
			location.TileSelected();
			MoveToTile(new_target);
		}
		else
		{
			Current_state = Waldorf_States.Stuck;
		}
	}

	/// <summary>
	/// Places Waldorf on a specific tile location
	/// Should be called when Waldorf is first initialiazed
	/// </summary>
	/// <param name="tile">The tile to attach Waldorf to</param>
	public void SnapToTile(TileBehaviour tile)
	{
		this.transform.position = new Vector3(tile.transform.position.x, 0, tile.transform.position.z);
		location = tile;
		target = null;
	}

	/// <summary>
	/// Assigns a target for Waldorf to move to and faces him in that direction
	/// Should be called when Waldorf finishes thinking
	/// </summary>
	/// <param name="tile">The tile to move Waldorf towards</param>
	public void MoveToTile(TileBehaviour tile)
	{
		location = null;
		target = tile;
		this.gameObject.transform.LookAt(new Vector3(tile.transform.position.x, 0.0f, tile.transform.position.z), Vector3.up);
	}

	/// <summary>
	/// Moves Waldorf in the direction of the targeted tile and stops him when he reaches it
	/// Should be called when Waldorf is in the "Move" state
	/// </summary>
	private void UpdateMove()
	{
		if (target == null) return; // don't call if there is no target

		// Direction to move in
		Vector3 dir = (target.gameObject.transform.position - this.gameObject.transform.position);

		// Don't move in the y direction
		dir.y = 0;

		// if direction is less than our min, let's move to the next target
		if (dir.magnitude <= MIN_DIST)
		{
			// set the current target's position, to avoid issues later
			this.gameObject.transform.position = new Vector3(target.gameObject.transform.position.x, 0.0f, target.gameObject.transform.position.z);

			// assign waldorf to the tile
			location = target;

			// stop movement by removing the target
			target = null;
		}

		// Move Waldorf in the direction by the movement speed
		this.gameObject.transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
	}
}
