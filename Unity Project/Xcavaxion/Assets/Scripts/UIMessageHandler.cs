using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMessageHandler : MonoBehaviour {

	//This script is attached to the UIMessageHandler GameObject and works with all the messages that need to be displayed on screen, behind the scenes

	public Queue<ActionMessage> actionMessages; //starting with message 0

	public int messageCount; //number of messages for this session, used on the message as an identifier
	public int messageLimit; //max number of message to keep in the queue just so it doesn't bog down, maintain message number though

//	public bool newMessage; //whether or not a new message has been added to the queue


	// Use this for initialization
	void Start () {
		messageCount = 0;
//		newMessage = false;

		actionMessages = new Queue<ActionMessage> ();
	}
	
	// Update is called once per frame
	void Update () {



		if(actionMessages.Count == messageLimit){ //if we reach the message limit clear the messages out
			actionMessages.Clear ();
		}

//		newMessage = false;
	}

	public void AddMessage(ActionMessage toAdd){
		actionMessages.Enqueue (toAdd);
		messageCount++;
//		newMessage = true;
	}

	public void CreateMessage(string messageText, string composer){
		ActionMessage newMessage = new ActionMessage (messageCount, messageText, composer);
		AddMessage (newMessage);
	}

	public bool CheckForNewMessages(){
		bool newFound = false;
		foreach(ActionMessage message in actionMessages){
			if(message.unreadMessage){
				newFound = true;
			}
		}
		return newFound;
	}


}
