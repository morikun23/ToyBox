#if UNITY_EDITOR

using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class CaptureSend2Slack : CaptureSendBase {

    [Header(" --- Slack Settings --- ")]
    [SerializeField] private string slackApiToken = "";
    [SerializeField] private string channelName = "";
    
    private Dictionary<string, string> channels = new Dictionary<string, string>();

    [Serializable]
    private class SlackJsonData
    {
        public bool ok = false;
        public ChannelInfo[] channels = null;
    }
    [Serializable]
    private class ChannelInfo
    {
        public string id = "";
        public string name = "";
        public bool is_channe = false;
        public double created = 0;
        public string creator = "";
        public bool is_archived = false;
        public bool is_general = false;
        public bool is_member = false;
        public string[] members = null;
        public ChannelLabel topic = new ChannelLabel();
        public ChannelLabel purpose = new ChannelLabel();
        public double num_members = 0;
    }
    [Serializable]
    private class ChannelLabel
    {
        public string value = "";
        public string creator = "";
        public double last_set = 0;
    }

    protected override void Start()
    {
        base.Start();

        if(string.IsNullOrEmpty(slackApiToken))
        {
            Debug.LogError("Enter SlackAPI Token of your slack team.");
        }

        if(string.IsNullOrEmpty(channelName))
        {
            Debug.LogError("Enter channel name you want to post.");
        }

        StartCoroutine(GetChannelList());
    }

    private IEnumerator GetChannelList()
    {
        WWW www = new WWW(GetUrl("channels.list"));
        yield return www;
        channels.Clear();

        if(string.IsNullOrEmpty(www.error))
        {
            var json = JsonUtility.FromJson<SlackJsonData>(www.text);

            foreach(var cInfo in json.channels)
            {
                string cname = cInfo.name;
                string cid = cInfo.id;
                channels[cname] = cid;
            }
        }
    }

    private string GetChannelID(string name)
    {
        return channels[name];
    }

    protected override string GetUrl(string action)
    {
        return string.Format("https://slack.com/api/{0}?token={1}", action, slackApiToken);
    }

    protected override IEnumerator UploadFile()
    {
        while (channels.Count == 0) yield return new WaitForSeconds(0.5f);

        if (GetChannelID(channelName) == null)
        {
            Debug.LogError("Channel isn't exist.");
            yield break;
        }

        if(File.Exists(filePath))
        {
            root.enabled = true;
            byte[] data = File.ReadAllBytes(filePath);
            WWWForm form = new WWWForm();
            form.AddBinaryData("file", data, fileName, "image/png");
            WWW www = new WWW(GetUrl("files.upload") + string.Format("&channels={0}&initial_comment={1}", GetChannelID(channelName), GetEscapedText(message)), form);
            yield return www;
        }
    }
}
#endif