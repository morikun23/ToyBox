#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using UnityEngine.EventSystems;

public abstract class CaptureSendBase : MonoBehaviour {

    protected const string fileName = "unity_screenshot.png";
    private string _filePath = "";
    protected string filePath
    {
        get
        {
            if (string.IsNullOrEmpty(_filePath))
            {
#if UNITY_EDITOR
                _filePath = fileName;
#else         
                var directory = Application.persistentDataPath;
                _filePath = Path.Combine(directory, fileName);
                Directory.CreateDirectory(directory);
#endif

            }
            return _filePath;
        }
    }

    private bool isCapturing = false;
    protected string message = "";

    [SerializeField] protected Canvas root;
    [SerializeField] private InputField inputField;

    private static CaptureSendBase instance;

    protected virtual void Awake()
    {
        Init();
    }

    void Init()
    {
        // Make Singleton.
        if(instance != null && instance != this)
        {
            Debug.LogError("CaptureSendBase:" + gameObject.name + " destroyed");
        } else {
            instance = this;
        }
        
        // Check EventSystem count.
        if (FindObjectsOfType<EventSystem>().Length > 1)
            Destroy(GetComponentInChildren<EventSystem>().gameObject);
    }

	// Use this for initialization
	protected virtual void Start () {
        root.enabled = false;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        StartUpload();
        OpenCanvas();
	}

    void StartUpload()
    {
        if (isCapturing)
        {
            if (File.Exists(filePath))
            {
                isCapturing = false;
                StartCoroutine(UploadFile());
            }
        }
    }

    void OpenCanvas()
    {
        if (root.enabled) return;
#if UNITY_EDITOR
		if (Input.GetMouseButton(0) && Input.GetMouseButton(1)) {
			root.enabled = true;
		}
#endif

#if UNITY_ANDROID || UNITY_IPHONE
		if (Input.touchCount >= 3)
        {
            root.enabled = true;
        }
#else
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1)) {
			root.enabled = true;
		}
#endif
	}

	public void CloseCanvas()
    {
        root.enabled = false;
    }
    
    public void CaptureWithInputField()
    {
        string text = inputField ? inputField.text : "";
        Capture(text);
    }

    public void Capture(string s)
    {
        if (isCapturing) return;

        // delete prev capture
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        isCapturing = true;
        message = s;
        root.enabled = false;
        Application.CaptureScreenshot(fileName);
    }

    protected string GetEscapedText(string s)
    {
        return System.Uri.EscapeUriString(s);
    }
    
    protected virtual string GetUrl() { return ""; }

    protected virtual string GetUrl(string s) { return ""; }

    protected virtual IEnumerator UploadFile() { yield break; }
}
#endif