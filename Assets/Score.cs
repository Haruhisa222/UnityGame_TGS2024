using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
public class RandomGenerator1
    {
        public int GenerateRandomNumber1(){
        System.Random rnd = new System.Random();
        return rnd.Next(0,11);
        }

    }

public class RandomGenerator2
    {
        public int GenerateRandomNumber2(){
        System.Random stk = new System.Random();
        return stk.Next(0,4);
        }
    }

public class Score : MonoBehaviour
{
   public static int score = 0;
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
    private int x;
    private int y;
    private int count = 0;
    public GameObject targetImage; // 操作するImageコンポーネント
    public GameObject targetObject;
    private HitoSpown hitospown;
    public GameObject targetObject2;
    private BarcodeTimer barcodetimer;
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
        score = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateRandomItem();
        //UpdateRandomKosu();
        Vector3 imageTop = imageObject.transform.position + new Vector3(0, imageObject.GetComponent<SpriteRenderer>().bounds.size.y / 1.4f, 0);
            spriteRenderer.transform.position = imageTop;
        Vector3 imageDow = imageObject.transform.position + new Vector3(0, imageObject.GetComponent<SpriteRenderer>().bounds.size.y / 1.9f, 0);
            kosuText.transform.position = imageDow;
        UpdateScoreText();
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
    public void Updatescore()
    {
        Debug.Log("called UpdateScore");
        AddScore(2000);
    }

    void ProcessBarcode(string data)
    {
        RanSw = false;

        if (data == code[x] && ResSw == 1)
        {
            targetImage.SetActive(false);

            if (count == 0)
            {
                /*spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = Number[y];*/
            }

            count = count + 1;

            if (count == (y + 1))
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
                    barcodetimer = targetObject2.GetComponent<BarcodeTimer>();
                    if (barcodetimer != null)
                    {
                        barcodetimer.UpdatetimerActive(false);
                        barcodetimer.UpdateResetT(true);
                    }
                }
                count = 0;
                AddScore(1000);
                UpdateScoreText();
                RanSw = true;
                Debug.Log(score);
                kosuText.text = "ありがとう";
                StartCoroutine(DelayedChangeItem());
                spriteRenderer.enabled=false;
                kosuText.gameObject.SetActive(false);
                
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
                barcodetimer = targetObject2.GetComponent<BarcodeTimer>();
                if (barcodetimer != null)
                {
                    barcodetimer.UpdatetimerActive(false);
                    barcodetimer.UpdateResetT(true);
                }
            }
            count = 0;
            AddScore(500);
            UpdateScoreText();
            RanSw = true;
            Debug.Log(score);
            Yobidashi();
            kosuText.text = "ありがとう";
            StartCoroutine(DelayedChangeItem());
            spriteRenderer.enabled=false;
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

    void AddScore(int points)
    {
        score += points;
        count = 0;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = score + "点";
    }

    void UpdateRandomItem()
    {
        RandomGenerator1 randomGenerator1 = new RandomGenerator1();
        x = randomGenerator1.GenerateRandomNumber1();
        spriteRenderer.sprite = Item[x];
        kosuText.text = "を" + 1 + "個お願いします";
    }

    void UpdateRandomKosu()
    {
        RandomGenerator2 randomGenerator2 = new RandomGenerator2();
        y = randomGenerator2.GenerateRandomNumber2();
        //kosuText.text = "を" + (y + 1) + "個お願いします";
        
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


