//==============================================
//
// 2016/06/25 坂本
// プレイヤーの操作処理
//
//==============================================
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{


	//=============================================================
	// 変数宣言
	//=============================================================
	/// <summary>
	/// 自身のアニメーター
	/// </summary>
	public Animator myAnimator;
	/// <summary>
	/// 自身のRigidbody
	/// </summary>
	[SerializeField] private Rigidbody2D m_Rigidbody2D;

	/// <summary>
	/// 最高速度
	/// </summary>
	[SerializeField] private float m_MaxSpeed = 10f;
	/// <summary>
	/// ジャンプ力
	/// </summary>
	[SerializeField] private float m_JumpForce = 400f;
	/// <summary>
	/// // しゃがみ移動時の速さの減る割合 1 = 100%
	/// </summary>
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  
	/// <summary>
	/// ジャンプ中に動かせるかどうか
	/// </summary>
	[SerializeField] private bool m_AirControl = false;                 
	/// <summary>
	/// 地面を判別する為のレイヤーマスク
	/// </summary>
	[SerializeField] private LayerMask m_WhatIsGround;                  
	/// <summary>
	/// 地面との当たり判定用のTransform
	/// </summary>
	[SerializeField] private Transform m_GroundCheck;
	/// <summary>
	/// 地面と当たり判定を行う半径
	/// </summary>
	const float k_GroundedRadius = .2f;
	/// <summary>
	/// 地面と接しているのかどうか
	/// </summary>
	private bool m_Grounded;
	/// <summary>
	/// 天井との当たり判定用のTransform
	/// </summary>
	[SerializeField] private Transform m_CeilingCheck;
	/// <summary>
	/// 天井との当たり判定を行う半径
	/// </summary>
	const float k_CeilingRadius = .01f;
	/// <summary>
	/// プレイヤーが左右どっちを向いているのか判定する
	/// </summary>
	private bool m_FacingRight = true;
	/// <summary>
	/// ジャンプするかどうか
	/// </summary>
	private bool m_Jump;

	//=============================================================
	// 関数宣言
	//=============================================================
	//--------------------------------------------
	/// <summary>
	/// 更新処理(毎フレーム)
	/// </summary>
	//--------------------------------------------
	void Update ()
	{
		// 歩きアニメーションを行う条件を満たしているのかどうか
		//bool isWalk = Input.GetAxisRaw("Horizontal")!=0.0f;

		//myAnimator.SetBool("isWalk",isWalk);
		if (!m_Jump)
		{
			// Read the jump input in Update so button presses aren't missed.
			m_Jump = Input.GetButtonDown("Jump");
		}
	}

	//--------------------------------------------
	/// <summary>
	/// 更新処理(ある一定秒毎)
	/// </summary>
	//--------------------------------------------
	private void FixedUpdate()
	{
		m_Grounded = false;

		//地面の設定した半径内の地面に設定してあるレイヤーのオブジェクトを全検索
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

		//検索して出てきたオブジェクトをチェック
		for (int i = 0; i < colliders.Length; i++)
		{
			//自身のオブジェクトではない時地面と接している
			if (colliders[i].gameObject != gameObject)
				m_Grounded = true;
		}

		//アニメーターに地面に付いているかどうかをセット
		myAnimator.SetBool("Ground", m_Grounded);

		// アニメーターに落ちるスピードをセット
		myAnimator.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

		bool crouch = Input.GetKey(KeyCode.LeftControl);
		float h = Input.GetAxis("Horizontal");
		// Pass all parameters to the character control script.
		Move(h, crouch, m_Jump);
		m_Jump = false;
	}

	//--------------------------------------------
	/// <summary>
	/// 移動処理
	/// </summary>
	//--------------------------------------------
	public void Move(float move, bool crouch, bool jump)
	{
		// しゃがみ時に起き上がっているかどうかの判定を行う
		if (!crouch && myAnimator.GetBool("Crouch"))
		{
			// 体部分のコライダーが地面と接していればしゃがんでいる
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		// 更新したしゃがみかどうかの情報をAnimatoにセットする
		myAnimator.SetBool("Crouch", crouch);

		//地面に立っているか空中でのアクションが許可されている場合のみ処理を行う
		if (m_Grounded || m_AirControl)
		{
			// Reduce the speed if crouching by the crouchSpeed multiplier
			move = (crouch ? move*m_CrouchSpeed : move);

			// The Speed animator parameter is set to the absolute value of the horizontal input.
			myAnimator.SetFloat("Speed", Mathf.Abs(move));

			// Move the character
			m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}

		// ジャンプ処理
		if (m_Grounded && jump && myAnimator.GetBool("Ground"))
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			myAnimator.SetBool("Ground", false);
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}

	//--------------------------------------------
	/// <summary>
	/// 左右反転処理
	/// </summary>
	//--------------------------------------------
	private void Flip()
	{
		// 左右反転させる
		m_FacingRight = !m_FacingRight;

		// スケールを反転させて左右反転させる
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
