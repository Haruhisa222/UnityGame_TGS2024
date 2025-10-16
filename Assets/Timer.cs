
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

	//　トータル制限時間
	private float totalTime;
	//　制限時間（分）
	[SerializeField]
	private int minute;
	//　制限時間（秒）
	[SerializeField]
	private float seconds;
	//　前回Update時の秒数
	private float oldSeconds;
	[SerializeField]
    private Text timerText;

    public Text scoreText;
    private bool soundSw = true;
    public GameObject bgm;
    public GameObject gameoverbgm;
    private AudioSource audioSource = null;
	void Start () {
		totalTime = minute * 60 + seconds;
		oldSeconds = 0f;
		timerText = GetComponentInChildren<Text>();
        audioSource = GetComponent<AudioSource>();
    if (timerText == null) {
        Debug.LogError("Textコンポーネントが見つかりません");
    } else {
        Debug.Log("STAER");
    }
	}
    
	void Update () {
		//　制限時間が0秒以下なら何もしない
		if (totalTime <= 0f) {
			return;
		}
		//　一旦トータルの制限時間を計測；
		totalTime = minute * 60 + seconds;
		totalTime -= Time.deltaTime;

		//　再設定
		minute = (int) totalTime / 60;
		seconds = totalTime - minute * 60;
		//　タイマー表示用UIテキストに時間を表示する
		if((int)seconds != (int)oldSeconds) {
			timerText.text = minute.ToString("00") + ":" + ((int) seconds).ToString("00");
		}
        
		oldSeconds = seconds;
        if(totalTime <= 30f) {
            
			timerText.color = Color.red;
            if(soundSw)
            {
            lastTime();
            soundSw=false;
            }
		} 
        

        
		//　制限時間以下になったらコンソールに『制限時間終了』という文字列を表示する
		if(totalTime <= 0f) {
			Debug.Log("制限時間終了");
            SceneManager.LoadScene("end");
		} 
	}

    void lastTime()
        {
            audioSource = bgm.GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource = gameoverbgm.GetComponent<AudioSource>();
        audioSource.Play();
        Debug.Log("sound");
        }
}
