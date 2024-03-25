using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public GameObject maxhealthTextPrefab;
    public Canvas gameCanvas;
    // Start is called before the first frame update
    private void Awake()
    {
        //gameCanvas = GetComponent<Canvas>();
        gameCanvas = GameObject.Find("GameCanva").GetComponent<Canvas>();
        
    }
    private void OnEnable()
    {
        CharactorUIEvents.characterDamaged += (CharactorTookDamage);
        CharactorUIEvents.characterHealed += (CharactorHealed);
        CharactorUIEvents.characterMaxHealthChange += (CharactorMaxHealthChange);
        //CharactorUIEvents.controlTextEnable += (ControlTextEnable);
    }
    private void OnDisable()
    {
        CharactorUIEvents.characterDamaged -= (CharactorTookDamage);
        CharactorUIEvents.characterHealed -= (CharactorHealed);
        CharactorUIEvents.characterMaxHealthChange -= (CharactorMaxHealthChange);
        //CharactorUIEvents.controlTextEnable -= (ControlTextEnable);
    }
    public void ControlTextEnable(Transform position, string content)//”√≤ª…œ£ø
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(position.transform.position);
        TMP_Text tmpText = Instantiate(maxhealthTextPrefab, spawnPosition , Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = content;
    }
    // Update is called once per frame
    public void CharactorTookDamage(GameObject charactor,int damageReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(charactor.transform.position);
        TMP_Text tmpText = Instantiate(damageTextPrefab,spawnPosition,Quaternion.identity,
            gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text=damageReceived.ToString();
            
    }
    public void CharactorHealed(GameObject charactor, int healReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(charactor.transform.position);
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition,Quaternion.identity,gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = "+"+healReceived.ToString();
    }
    public void CharactorMaxHealthChange(GameObject charactor, int healthChanged)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(charactor.transform.position);
        TMP_Text tmpText = Instantiate(maxhealthTextPrefab, spawnPosition+new Vector3(0,100,0), Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = "maxhealth+"+healthChanged.ToString();
    }
//    public void WinGame()
//    {
//        Invoke("_WinGame", 1.5f);
//    }
//    public void _WinGame()
//    {
//        SceneManager.LoadScene("VictoryScene");
//    }
//    public void OnExitGame(InputAction.CallbackContext context)
//    {
//        if (context.started)
//        {
//#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
//            Debug.Log(this.name+":"+this.GetType()+":"+System.Reflection.MethodBase.GetCurrentMethod().Name);
//#endif
//#if(UNITY_EDITOR)
//            UnityEditor.EditorApplication.isPlaying = false;
//#elif (UNITY_STANDALONE)
//            Application.Quit();
//#elif (UNITY_WEGBL)
//            SceneManager.LoadScene("MainScene");
//#endif
//        }
//    }
}
