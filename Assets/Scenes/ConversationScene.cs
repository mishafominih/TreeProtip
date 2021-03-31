using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConversationScene : MonoBehaviour
{
    public void Conversation(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
}
