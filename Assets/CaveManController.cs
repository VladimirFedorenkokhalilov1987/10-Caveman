using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CaveManController : MonoBehaviour {
	[SerializeField]
	private GameObject GameOver;

	[SerializeField]
	private GameObject CAVE;

	[SerializeField]
	private SpriteRenderer startCave;

	[SerializeField]
	private SpriteRenderer caveColor;

	[SerializeField]
	private ParticleSystem particle;

	[SerializeField]
	private Sprite finishCaveman;

	[SerializeField]
	private SpriteRenderer[] startCaveman;

	[SerializeField]
	private Sprite caveStat1;

	[SerializeField]
	private Sprite caveStat2;

	[SerializeField]
	private Sprite caveStat3;

	private bool animPlayed= false;

	[SerializeField]
	private Animator[] cavemanPlay;

	private int hitIndex=0;
	private int clicked=0;

	[SerializeField]
	private Text cavemanBlack;

	[SerializeField]
	private Text cavemanRed;

	[SerializeField]
	private Text cavemanGreen;

	[SerializeField]
	private Text cavemanWhite;

	[SerializeField]
	private Text cavemanBlackNeedT;

	[SerializeField]
	private Text cavemanRedNeedT;

	[SerializeField]
	private Text cavemanGreenNeedT;

	[SerializeField]
	private Text cavemanWhiteNeedT;

	[SerializeField]
	private Text caveNumber;
	private int CaveNumber=0;

	[SerializeField]
	private int cavemanBlackCount=100;

	[SerializeField]
	private int cavemanRedCount=100;

	[SerializeField]
	private int cavemanGreenCount=100;

	[SerializeField]
	private int cavemanWhiteCount=100;

	[SerializeField]
	private int cavemanBlackCountAdd=0;

	[SerializeField]
	private int cavemanRedCountAdd=0;

	[SerializeField]
	private int cavemanGreenCountAdd=0;

	[SerializeField]
	private int cavemanWhiteCountAdd=0;

	[SerializeField]
	private int cavemanBlackNeed;

	[SerializeField]
	private int cavemanRedNeed;

	[SerializeField]
	private int cavemanGreenNeed;

	[SerializeField]
	private int cavemanWhiteNeed;

	[SerializeField, Range(0,4)]
	private int nextCaveColor;


	void Start () {

		CaveNextColor ();
		CavemansNeed ();
	}


	void Update () {
		
		RaycastHit2D hit;
		Ray2D ray=new Ray2D(transform.position,transform.forward);;
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y, 10.0f)), Vector2.zero);
			if (hit.collider.gameObject.tag == "GameOver") {
				SceneManager.LoadScene (0);
			}
			if (hit.collider.gameObject.tag == "Cave" && animPlayed == false) {
				
				hitIndex++;
				CavemansCountAdd ();
				if (cavemanBlackNeed != 0) {
					cavemanPlay [0].SetBool ("CavemanPlay", true);
				}
				if (cavemanRedNeed != 0) {
					cavemanPlay [1].SetBool ("CavemanPlay", true);
				}
				if (cavemanGreenNeed != 0) {
					cavemanPlay [2].SetBool ("CavemanPlay", true);
				}
				if (cavemanWhiteNeed != 0) {
					cavemanPlay [3].SetBool ("CavemanPlay", true);
				}
				CavemansRemover ();
				CavemansNeed ();

				animPlayed = true;
				clicked = 1;
			} 
		}

		if (Input.GetKeyUp (KeyCode.Mouse0)&& animPlayed == true&&!Input.GetKey (KeyCode.Mouse0)&&clicked==1) {
			for (int i = 0; i < cavemanPlay.Length; i++) {
				cavemanPlay[i].SetBool ("CavemanPlay", false);
			}

			startCaveman [0].sprite = finishCaveman;
			animPlayed = false;
			clicked=0;
		}

		if (hitIndex == 1) {
			startCave.sprite = caveStat1;
		}

		if (hitIndex == 2) {
			startCave.sprite = caveStat2;
		}

		if (hitIndex == 3) {
			hitIndex=0;
			startCave.sprite = caveStat3;
			CaveNextColor ();
			CaveNumber++;
			caveNumber.text = "Cave: " + CaveNumber.ToString ();
		}
	}

	private void CavemansCountAdd()
	{
		if (nextCaveColor == 0 && hitIndex == 3) {
			cavemanBlackCountAdd += 1;
			particle.Play ();
		}
		if (nextCaveColor == 1 && hitIndex == 3) {
			cavemanRedCountAdd += 1;
			particle.Play ();
		}
		if (nextCaveColor == 2 && hitIndex == 3) {
			cavemanGreenCountAdd += 1;
			particle.Play ();
		}
		if (nextCaveColor == 3 && hitIndex == 3) {
			cavemanWhiteCountAdd += 1;
			particle.Play ();
		}
	}

	private void CaveNextColor()
	{
		nextCaveColor = Random.Range (0, 4);
		if (nextCaveColor == 0) {
			caveColor.color = Color.black;
		}
		if (nextCaveColor == 1) {
			caveColor.color = Color.red;
		}
		if (nextCaveColor == 2) {
			caveColor.color = Color.green;
		}
		if (nextCaveColor == 3) {
			caveColor.color = Color.white;
		}
	}

	private void CavemansNeed()
	{
		cavemanBlackNeed = Random.Range (0, 5);
		cavemanRedNeed =Random.Range (0, 5);
		cavemanGreenNeed =Random.Range (0, 5);
		cavemanWhiteNeed =Random.Range (0, 5);

		cavemanBlackNeedT.text = cavemanBlackNeed.ToString();
		cavemanRedNeedT.text = cavemanRedNeed.ToString();
		cavemanGreenNeedT.text = cavemanGreenNeed.ToString();
		cavemanWhiteNeedT.text = cavemanWhiteNeed.ToString();
	}

	private void GameAnd(){
		GameOver.SetActive (true);
		CAVE.SetActive (false);
		print ("GAME OVER");
	}

	private void CavemansRemover(){
		if (cavemanBlackNeed == 0 && cavemanRedNeed == 0 && cavemanGreenNeed == 0 && cavemanWhiteNeed == 0) {
			CavemansNeed ();
		}
		////////////////Black
		if (cavemanBlackCount > 0 && cavemanBlackCount < cavemanBlackNeed ){
			if (cavemanBlackCountAdd>= cavemanBlackNeed - cavemanBlackCount) {
				cavemanBlackCount-= cavemanBlackNeed;
				cavemanBlack.text = cavemanBlackCount.ToString () + "/" + cavemanBlackCountAdd.ToString ();
			}
		}

		if (cavemanBlackCount == 0) {
			cavemanBlackCount -= cavemanBlackNeed;
			cavemanBlack.text = cavemanBlackCount.ToString () + "/" + cavemanBlackCountAdd.ToString ();
		}

		if (cavemanBlackCount < 0) {
			cavemanBlackCountAdd += cavemanBlackCount;
			cavemanBlackCount = 0;
			cavemanBlack.text = cavemanBlackCount.ToString () + "/" + cavemanBlackCountAdd.ToString ();
		}

		if (cavemanBlackNeed != 0 && cavemanBlackCount >= cavemanBlackNeed) {
			if (cavemanBlackCount > 0 ){
				cavemanBlackCount -= cavemanBlackNeed;
				cavemanBlack.text = cavemanBlackCount.ToString () + "/" + cavemanBlackCountAdd.ToString ();
			}
		}

		if (cavemanBlackNeed == 0 && cavemanBlackCount >= cavemanBlackNeed) {
			if (cavemanBlackCount > 0 ){
				cavemanBlack.text = cavemanBlackCount.ToString () + "/" + cavemanBlackCountAdd.ToString ();
			}
		}

		if (cavemanBlackCountAdd <= 0 && cavemanBlackCount<=0) {
			cavemanBlackCountAdd = 0;
			cavemanBlack.text = cavemanBlackCount.ToString () + "/" + cavemanBlackCountAdd.ToString ();
			GameAnd ();
		}
		////////////Red
		if (cavemanRedCount > 0 && cavemanRedCount < cavemanRedNeed ){
			if (cavemanRedCountAdd>= cavemanRedNeed - cavemanRedCount) {
				cavemanRedCount-= cavemanRedNeed;
				cavemanRed.text = cavemanRedCount.ToString () + "/" + cavemanRedCountAdd.ToString ();
			}
		}

		if (cavemanRedCount == 0) {
			cavemanRedCount -= cavemanRedNeed;
			cavemanRed.text = cavemanRedCount.ToString () + "/" + cavemanRedCountAdd.ToString ();
		}

		if (cavemanRedCount < 0) {
			cavemanRedCountAdd += cavemanRedCount;
			cavemanRedCount = 0;
			cavemanRed.text = cavemanRedCount.ToString () + "/" + cavemanRedCountAdd.ToString ();
		}

		if (cavemanRedNeed != 0 && cavemanRedCount >= cavemanRedNeed) {
			if (cavemanRedCount > 0 ){
				cavemanRedCount -= cavemanRedNeed;
				cavemanRed.text = cavemanRedCount.ToString () + "/" + cavemanRedCountAdd.ToString ();
			}
		}

		if (cavemanRedNeed == 0 && cavemanRedCount >= cavemanRedNeed) {
			if (cavemanRedCount > 0 ){
				cavemanRed.text = cavemanRedCount.ToString () + "/" + cavemanRedCountAdd.ToString ();
			}
		}

		if (cavemanRedCountAdd <= 0 && cavemanRedCount<=0) {
			cavemanRedCountAdd = 0;
			cavemanRed.text = cavemanRedCount.ToString () + "/" + cavemanRedCountAdd.ToString ();
			GameAnd ();
		}
		/////////////////Green
		if (cavemanGreenCount > 0 && cavemanGreenCount < cavemanGreenNeed ){
			if (cavemanGreenCountAdd>= cavemanGreenNeed - cavemanGreenCount) {
				cavemanGreenCount-= cavemanGreenNeed;
				cavemanGreen.text = cavemanGreenCount.ToString () + "/" + cavemanGreenCountAdd.ToString ();
			}
		}

		if (cavemanGreenCount == 0) {
			cavemanGreenCount -= cavemanGreenNeed;
			cavemanGreen.text = cavemanGreenCount.ToString () + "/" + cavemanGreenCountAdd.ToString ();
		}

		if (cavemanGreenCount < 0) {
			cavemanGreenCountAdd += cavemanGreenCount;
			cavemanGreenCount = 0;
			cavemanGreen.text = cavemanGreenCount.ToString () + "/" + cavemanGreenCountAdd.ToString ();
		}

		if (cavemanGreenNeed != 0 && cavemanGreenCount >= cavemanGreenNeed) {
			if (cavemanGreenCount > 0 ){
				cavemanGreenCount -= cavemanGreenNeed;
				cavemanGreen.text = cavemanGreenCount.ToString () + "/" + cavemanGreenCountAdd.ToString ();
			}
		}

		if (cavemanGreenNeed == 0 && cavemanGreenCount >= cavemanGreenNeed) {
			if (cavemanGreenCount > 0 ){
				cavemanGreen.text = cavemanGreenCount.ToString () + "/" + cavemanGreenCountAdd.ToString ();
			}
		}

		if (cavemanGreenCountAdd <= 0 && cavemanGreenCount<=0) {
			cavemanGreenCountAdd = 0;
			cavemanGreen.text = cavemanGreenCount.ToString () + "/" + cavemanGreenCountAdd.ToString ();
			GameAnd ();
		}
		//////////////White
		if (cavemanWhiteCount > 0 && cavemanWhiteCount < cavemanWhiteNeed ){
			if (cavemanWhiteCountAdd>= cavemanWhiteNeed- cavemanWhiteCount) {
				cavemanWhiteCount-= cavemanWhiteNeed;
				cavemanWhite.text = cavemanWhiteCount.ToString () + "/" + cavemanWhiteCountAdd.ToString ();
			}
		}

		if (cavemanWhiteCount == 0) {
			cavemanWhiteCount -= cavemanWhiteNeed;
			cavemanWhite.text = cavemanWhiteCount.ToString () + "/" + cavemanWhiteCountAdd.ToString ();
		}

		if (cavemanWhiteCount < 0) {
			cavemanWhiteCountAdd += cavemanWhiteCount;
			cavemanWhiteCount = 0;
			cavemanWhite.text = cavemanWhiteCount.ToString () + "/" + cavemanWhiteCountAdd.ToString ();
		}

		if (cavemanWhiteNeed != 0 && cavemanWhiteCount >= cavemanWhiteNeed) {
			if (cavemanWhiteCount > 0 ){
				cavemanWhiteCount -= cavemanWhiteNeed;
				cavemanWhite.text = cavemanWhiteCount.ToString () + "/" + cavemanWhiteCountAdd.ToString ();
			}
		}

		if (cavemanWhiteNeed == 0 && cavemanWhiteCount >= cavemanWhiteNeed) {
			if (cavemanWhiteCount > 0 ){
				cavemanWhite.text = cavemanWhiteCount.ToString () + "/" + cavemanWhiteCountAdd.ToString ();
			}
		}

		if (cavemanWhiteCountAdd <= 0 && cavemanWhiteCount<=0) {
			cavemanWhiteCountAdd = 0;
			cavemanWhite.text = cavemanWhiteCount.ToString () + "/" + cavemanWhiteCountAdd.ToString ();
			GameAnd ();
		}
	}

	public void ExitGame(){
		Application.Quit ();
	}
}