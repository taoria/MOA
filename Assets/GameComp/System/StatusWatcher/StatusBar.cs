using UnityEngine;
using UnityEngine.UI;
public class StatusBar : MonoBehaviour {
    public float StatusBarAmount;
    public bool ShowName=true;
    public bool ShowValue = true;
    public string WatchName;
    public bool EnableButton = true;
    public bool Linear = false;
    private string ShowingName;
    public void Add() {
        var statusWindow = GameObject.Find("StatusWatcher");
        var watchStatus = statusWindow.GetComponent<StatusWatcher>().CharacterStatus;
        watchStatus.AddPoint(WatchName);
    }
    // Use this for initialization
    private void Start() {
        this.ShowingName = WatchName;
        if (EnableButton == false) {
            transform.Find("Button").gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
     void Update() {
        var statusWindow =  GameObject.Find("StatusWatcher");
        var watchStatus = statusWindow.GetComponent<StatusWatcher>().CharacterStatus;
        object watchStatusObject = null;
        if (WatchName != null) {
            watchStatusObject = watchStatus.GetType().GetProperty(WatchName).GetValue(watchStatus, null);
            if (Linear == false) {
                if (watchStatusObject is float)
                    GetComponent<Image>().fillAmount = UnityMath.Sigmoid(((float) watchStatusObject - 10)/10);
            }
    
        }

        var statusText = transform.Find("StatusText").gameObject.GetComponent<Text>();

         if (watchStatusObject != null) {
             float f = (float) watchStatusObject;
             int res = (int) f;
             statusText.text = (ShowName?ShowingName:"") + (ShowValue? (ShowName?":"+res:res.ToString()):"");
         }
    }
}