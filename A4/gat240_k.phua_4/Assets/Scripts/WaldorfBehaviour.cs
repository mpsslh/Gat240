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
				Think();
				//think will set state to stuck if surrounding tiles has weights of 0
				break;
			case Waldorf_States.Moving:
				UpdateMove();
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
	/// <summary> 
	/// Waldorf ponders about life,
	/// and also the next tile to move to
	/// </summary>
	private void ReachGoal()
	{
		Current_state = Waldorf_States.Falling;
		return;
	}

	/// <summary> 
	/// Waldorf ponders about life,
	/// and also the next tile to move to
	/// </summary>
	private IEnumerator Thinking()
	{
		yield return new WaitForSeconds(thinkTime);
		animate.SetTrigger("move");
		Current_state = Waldorf_States.Moving;
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
		int x = location.X;
		int z = location.Z;
		float heaviest_weight = 0.0f;
		TileBehaviour new_target = null;
		if (top = GameObject.Find(x + "_" + (z + 1)))
		{
			if (top.GetComponent<TileBehaviour>().MoveWeight >= heaviest_weight)
			{
				if (top.GetComponent<TileBehaviour>().MoveWeight == heaviest_weight)
				{
					if (Random.Range(0.0f, 1.0f) > 0.5f)
					{
						heaviest_weight = top.GetComponent<TileBehaviour>().MoveWeight;
						new_target = top.GetComponent<TileBehaviour>();  
					}
				}
				else
				{
					heaviest_weight = top.GetComponent<TileBehaviour>().MoveWeight;
					new_target = top.GetComponent<TileBehaviour>();
				}
			}
		}
		else //if there is no tiles at location means its a goal
		{
			ReachGoal();
			return;
		}
		if (left = GameObject.Find((x - 1) + "_" + z))
		{
			if (left.GetComponent<TileBehaviour>().MoveWeight >= heaviest_weight)
			{
				if (left.GetComponent<TileBehaviour>().MoveWeight == heaviest_weight)
				{
					if (Random.Range(0.0f, 1.0f) > 0.5f)
					{
						heaviest_weight = left.GetComponent<TileBehaviour>().MoveWeight;
						new_target = left.GetComponent<TileBehaviour>();
					}
				}
				else
				{
					heaviest_weight = left.GetComponent<TileBehaviour>().MoveWeight;
					new_target = left.GetComponent<TileBehaviour>();
				}
			}
		}
		else //if there is no tiles at location means its a goal
		{
			ReachGoal();
			return;
		}
		if (right = GameObject.Find((x + 1) + "_" + z))
		{
			if (right.GetComponent<TileBehaviour>().MoveWeight >= heaviest_weight)
			{
				if (right.GetComponent<TileBehaviour>().MoveWeight == heaviest_weight)
				{
					if (Random.Range(0.0f, 1.0f) > 0.5f)
					{
						heaviest_weight = right.GetComponent<TileBehaviour>().MoveWeight;
						new_target = right.GetComponent<TileBehaviour>();
					}
				}
				else
				{
					heaviest_weight = right.GetComponent<TileBehaviour>().MoveWeight;
					new_target = right.GetComponent<TileBehaviour>();
				}
			}
		}
		else
		{
			ReachGoal();
			return;
		}
		if (bot = GameObject.Find(x + "_" + (z - 1)))
		{
			if (bot.GetComponent<TileBehaviour>().MoveWeight >=  heaviest_weight)
			{
				if (bot.GetComponent<TileBehaviour>().MoveWeight == heaviest_weight)
				{
					if (Random.Range(0.0f, 1.0f) > 0.5f)
					{
						heaviest_weight = bot.GetComponent<TileBehaviour>().MoveWeight;
						new_target = bot.GetComponent<TileBehaviour>();
					}
				}
				else
				{
					heaviest_weight = bot.GetComponent<TileBehaviour>().MoveWeight;
					new_target = bot.GetComponent<TileBehaviour>();
				}
			}
		}
		else
		{
			ReachGoal();
			return;
		}

		//theres is a tile to move to
		if (heaviest_weight != 0.0f && new_target)
		{
			StartCoroutine(Thinking());
			location.TileSelected();
			MoveToTile(new_target);
		}
		else//no tiles to go
		{
			animate.SetTrigger("stuck");
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
		if (target == null)
		{
			animate.SetTrigger("think");
			Current_state = Waldorf_States.Thinking;
			return; // don't call if there is no target
		}
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
