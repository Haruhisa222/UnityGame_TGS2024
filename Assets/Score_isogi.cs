using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
public class RandomGenerator3
    {
        public int GenerateRandomNumber3(){
        System.Random rnd = new System.Random();
        return rnd.Next(0,11);
        }

    }

public class RandomGenerator4
    {
        public int GenerateRandomNumber4(){
        System.Random stk = new System.Random();
        return stk.Next(0,4);
        }
    }

public class Score_isogi : MonoBehaviour
{
    public Text scoreText;
    public Text kosuText;
    public Sprite[] Item;
    public Sprite[] Number;
    public GameObject imageObject;  
    private SpriteRenderer spriteRenderer;
    private string barcodeData = "";
    private float barcodeTimeout = 0.5f;
    private float lastInputTime;
    private bool RanSw = true;
    public int ResSw;
    private int a;
    private int b;
    private int count = 0;
    public GameObject targetImage; // 操作するImageコンポーネント
    public GameObject targetObject;
    private HitoSpown hitospown;
    public GameObject targetObject2;
    private BarcodeTimer_isogi barcodetimer;
    private Score score;
    public GameObject targetObject3;
    private bool timerActive = false;
    private float timeRemaining;
    private float countdownTime = 2f; // タイマーの制限時間
    string[] code = new string[12]{
      "957027284756",
      "1495759033949",
      "2985709284006",
      "4620810956753",
      "7438724708002",
      "7594037507487",
      "7857515928526",
      "9847517591871",
      "3794964875978",
      "4538469765773",
      "4827598990171",
      "6745487745924"
    };
    
    void Start()
    {
        targetImage.gameObject.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateRandomItem();
        //UpdateRandomKosu();
        Vector3 imageTop = imageObject.transform.position + new Vector3(0, imageObject.GetComponent<SpriteRenderer>().bounds.size.y / 1.4f, 0);
            spriteRenderer.transform.position = imageTop;
        Vector3 imageDow = imageObject.transform.position + new Vector3(0, imageObject.GetComponent<SpriteRenderer>().bounds.size.y / 1.9f, 0);
            kosuText.transform.position = imageDow;
    }

    public void UpdateScoreText(string newText)
    {
        if (kosuText != null)
        {
            kosuText.text = newText;
        }
    }

    void Update()
    {
        if (timerActive)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                Debug.Log("HUAAA");
                timeRemaining = 0;
                TimerEnded();
            } 
        }
        foreach (char c in Input.inputString)
        {
            if (c == '\n' || c == '\r')
            {
                if (Time.time - lastInputTime < barcodeTimeout && !timerActive)
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
            //if (ResSw == 0)
            //{
               // StartCoroutine(DelayedChangeItem());
            //}
        }
    }

    public void UpdateResSw(int newResSw)
    {
        Debug.Log("YAA");
        ResSw = newResSw;
        count = 0;
    }

    void ProcessBarcode(string data)
    {
        RanSw = false;

        if (data == code[a] && ResSw == 1)
        {
            targetImage.SetActive(false);

            if (count == 0)
            {
                /*spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = Number[y];*/
            }

            count = count + 1;

            if (count == (b + 1))
            {
                if (targetObject != null)
                {
                    hitospown = targetObject.GetComponent<HitoSpown>();
                    if (hitospown != null)
                    {
                        hitospown.UpdateSW2(1);
                    }
                }
                if (targetObject2 != null)
                {
                    barcodetimer = targetObject2.GetComponent<BarcodeTimer_isogi>();
                    if (barcodetimer != null)
                    {
                        barcodetimer.UpdatetimerActive(false);
                        barcodetimer.UpdateResetT(true);
                    }
                }
                if(targetObject3!=null)
              {
                score=targetObject3.GetComponent<Score>();
                count = 0;
                score.Updatescore();
                Debug.Log("tuuka");
                RanSw = true;
                Debug.Log(score);
                kosuText.text = "ありがとう";
                StartCoroutine(DelayedChangeItem());
                spriteRenderer.enabled=false;
                kosuText.gameObject.SetActive(false);
              }
            }
            else
            {
                //spriteRenderer.sprite = Number[y + 1 - count];
                Debug.Log("1hiku");
            }
        }
        else if (data == "8910320573042" && ResSw == 1 &&
                 imageObject.name == "nidai_hakobu_tenin_man")
        {
            if (targetObject != null)
            {
                hitospown = targetObject.GetComponent<HitoSpown>();
                if (hitospown != null)
                {
                    hitospown.UpdateSW2(1);
                }
            }
            if (targetObject2 != null)
            {
                barcodetimer = targetObject2.GetComponent<BarcodeTimer_isogi>();
                if (barcodetimer != null)
                {
                    barcodetimer.UpdatetimerActive(false);
                    barcodetimer.UpdateResetT(true);
                }
            }
            if(targetObject3!=null)
                {
                score=targetObject3.GetComponent<Score>();
                count = 0;
                score.Updatescore();
                RanSw = true;
                Debug.Log(score);
                kosuText.text = "ありがとう";
                StartCoroutine(DelayedChangeItem());
                spriteRenderer.enabled=false;
                kosuText.gameObject.SetActive(false);
                }
        }
    }

    void Yobidashi()
    {
        Debug.Log("コルーチン開始");
        timeRemaining=countdownTime;
        if (targetImage != null)
        {
            targetImage.SetActive(true);
            Debug.Log("ターゲットオブジェクトが表示されました。アクティブ状態: " + targetImage.activeSelf);

            timerActive=true;
        }
        else
        {
            Debug.LogError("Target Object is not assigned.");
        }
        Debug.Log("コルーチン終了");
    }
    void TimerEnded()
    {
        timerActive=false;
        targetImage.SetActive(false);
        Debug.Log("ターゲットオブジェクトが非表示になりました。アクティブ状態: " + targetImage.activeSelf);
    }


    void UpdateRandomItem()
    {
        RandomGenerator3 randomGenerator3 = new RandomGenerator3();
        a = randomGenerator3.GenerateRandomNumber3();
        spriteRenderer.sprite = Item[a];
        kosuText.text = "を" + 1 + "個お願いします";
    }

    void UpdateRandomKosu()
    {
        RandomGenerator4 randomGenerator4 = new RandomGenerator4();
        b = randomGenerator4.GenerateRandomNumber4();
        kosuText.text = "を" + (b + 1) + "個お願いします";
    }

    System.Collections.IEnumerator DelayedChangeItem()
    {
        if (RanSw == true)
        {
            yield return new WaitForSeconds(0.001f);
            UpdateRandomItem();
            //UpdateRandomKosu();
        }
    }
}



