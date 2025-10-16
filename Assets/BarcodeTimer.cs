using UnityEngine;
using UnityEngine.UI; // UIテキストを操作するため
using System.Collections;

public class BarcodeTimer : MonoBehaviour
{
    public GameObject barcodeObject; // バーコードゲームオブジェクト
    public GameObject sirowaku; // バーコードゲームオブジェクト
    public Text timerText; // 制限時間を表示するUIテキスト
    private float countdownTime = 25f; // タイマーの制限時間
    public int FinSw = 0;
    private bool timerActive = false;
    private bool ResetT = false;
    private float timeRemaining;
    public GameObject imageObject;  
    private string barcodeData = "";
    private float barcodeTimeout = 0.5f;
    private float lastInputTime;
    public Text kosuText;
    public GameObject targetObject;
    private Score score;
    public GameObject targetObject2;
    private HitoSpown hitospown;

    void Start()
    {
        // 初期化
        timerText.gameObject.SetActive(true); // 初期状態でテキストは非表示
        timerText.text="残り25秒";
        sirowaku.gameObject.SetActive(false);
        Vector3 imageButt = imageObject.transform.position + new Vector3(0, imageObject.GetComponent<SpriteRenderer>().bounds.size.y / 100f, 0);
        barcodeObject.transform.position = imageButt;
        Vector3 imageSS = imageObject.transform.position + new Vector3(0, imageObject.GetComponent<SpriteRenderer>().bounds.size.y / 1f, 0);
        timerText.transform.position = imageSS;
        Debug.Log(timeRemaining);
        Vector3 imageSU = imageObject.transform.position + new Vector3(0, imageObject.GetComponent<SpriteRenderer>().bounds.size.y / 1f, 0);
        sirowaku.transform.position = imageSU;
        Debug.Log(timeRemaining);
    }

    void Update()
    {
        if (timerActive)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 5f)
            {
                timerText.color = Color.red;
            } 
            if (timeRemaining <= 0)
            {
                Debug.Log("HUAAA");
                timeRemaining = 0;
                TimerEnded();
            } 

            // タイマーの表示更新
            timerText.text = "残り" + Mathf.Ceil(timeRemaining).ToString() + "秒";
        }

        foreach (char c in Input.inputString)
        {
            if (c == '\n' || c == '\r')
            {
                if (Time.time - lastInputTime < barcodeTimeout)
                {
                    ProcessBarcode(barcodeData);
                }
                barcodeData = "";
            }
            else
            {
                barcodeData += c;
                lastInputTime = Time.time;
            }
        }
        if (ResetT)
        {
            // タイマーをリセット
            timeRemaining = 0;
            TimerEnded();
            ResetT = false;
        }
    }

    void ProcessBarcode(string data)
    {
        if (data == "2845205811345")
        {   
            FinSw = 1;
            StartTimer();
        }
        if (data == "2374982427659")
        {
            FinSw = 2;
            StartTimer();
        }
        if (data == "5619740981828")
        {
            FinSw = 3;
            StartTimer();
        }
        if (data == "7897845124366")
        {

            FinSw = 4;
            StartTimer();
        }
    }

    public void UpdatetimerActive(bool newtimerActive)
    {
        Debug.Log("OK");
        timerActive = newtimerActive;
    }

    //public void UpdateCollisionSw(bool newCollisionSw)
    //{
      //  CollisionSw = newCollisionSw;
    //}

    public void UpdateResetT(bool newResetT)
    {
        Debug.Log("UpdateResetT called with value: " + newResetT);
        ResetT = newResetT;
    }

    // バーコードを読み取ったときに呼ばれるメソッド
    public void StartTimer()
    {
        if (barcodeObject.name == "jan_2845205811345" && FinSw == 1 ||
            barcodeObject.name == "jan_2374982427659" && FinSw == 2 ||
            barcodeObject.name == "jan_5619740981828" && FinSw == 3 ||
            barcodeObject.name == "7897845124366" && FinSw == 4
            )
        {
            timerActive = true;
            //timerText.gameObject.SetActive(true); // テキストを表示
            sirowaku.gameObject.SetActive(true);
            if (targetObject != null)
            {
                // スクリプトAのインスタンスを取得
                score = targetObject.GetComponent<Score>();

                if (score != null)
                {
                    if (barcodeObject.name == "jan_2845205811345" && FinSw == 1 ||
                        barcodeObject.name == "jan_2374982427659" && FinSw == 2 ||
                        barcodeObject.name == "jan_5619740981828" && FinSw == 3 ||
                        barcodeObject.name == "7897845124366" && FinSw == 4
                        )
                    {
                        score.UpdateResSw(1);
                    }  
                }
                else
                {
                    Debug.LogError("Score script is not attached to the target object.");
                }
            }
            else
            {
                Debug.LogError("Target object is not assigned.");
            }
        }
        barcodeObject.SetActive(false); // バーコードオブジェクトを非表示
        timeRemaining = countdownTime;
        Debug.Log(timeRemaining);
    }

    void TimerEnded()
    {
        Debug.Log("TimerEnded called");
        timerActive = false;
        timerText.text="残り25秒";
        //timerText.gameObject.SetActive(false); // テキストを非表示
        sirowaku.gameObject.SetActive(false);
        FinSw = 0;

        // すべてのバーコードオブジェクトを再表示
        foreach (var obj in FindObjectsOfType<BarcodeTimer>())
        {
            obj.barcodeObject.SetActive(true);
        }

        barcodeObject.SetActive(true); // 現在のバーコードオブジェクトも再表示

        if (targetObject != null)
        {
            // スクリプトAのインスタンスを取得
            score = targetObject.GetComponent<Score>();

            if (score != null)
            {
                score.UpdateResSw(0);
            }
            else
            {
                Debug.LogError("Score script is not attached to the target object.");
            }
        }
        else
        {
            Debug.LogError("Target object is not assigned.");
        }

        if (targetObject2 != null)
        {
            // スクリプトBのインスタンスを取得
            hitospown = targetObject2.GetComponent<HitoSpown>();

            if (hitospown != null)
            {
                hitospown.UpdateSW(1); 
            }
            else
            {
                Debug.LogError("HitoSpown script is not attached to targetObject2.");
            }
        }
        else
        {
            Debug.LogError("Target object is not assigned.");
        }
        Debug.Log("最終通過");
    }
}
