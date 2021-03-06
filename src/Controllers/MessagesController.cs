﻿using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Linq;

namespace XamUBot
{
	/// <summary>
	/// This controller handles all incoming messages the bot can process.
	/// </summary>
	[BotAuthentication]
    [RoutePrefix("api/messages")]
    public class MessagesController : ApiController
    {
	    /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> HandleIncomingActivity([FromBody]Activity activity)
        {
            if(activity == null)
            {
                return BadRequest("No activity provided.");
            }

            // TODO: shows typing indicator even if the bot won't send an answer. 
            // The typing indicator should automagically appear if sending a reply takes longer.
            // show typing indicator - because bots type too :)
            //if (activity.Type == ActivityTypes.Message)
            //{
            //    var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            //    Activity isTypingReply = activity.CreateReply();
            //    isTypingReply.Type = ActivityTypes.Typing;
            //    await connector.Conversations.ReplyToActivityAsync(isTypingReply);
            //}

            //if(activity.Type == ActivityTypes.ConversationUpdate)
            //{
            //    IConversationUpdateActivity update = activity;
                
            //    // Remove bot from the members added
            //    update.MembersAdded = update.MembersAdded.Where(member => member.Id != update.Recipient.Id).ToList();

            //    if (update.MembersAdded.Count == 0)
            //    {
            //        return Ok();
            //    }
            //}


			if (activity.Type == ActivityTypes.Message
				|| activity.Type == ActivityTypes.Event)
			{
				// Setup the first dialog. Note: the delegate will only be executed if the dialog stack is empty! This means after the first request
				// or after a IDialogStack.Reset() call a new instance will be generated but not if we are popping a dialog of the stack and return to RootDialog.
				await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
			}

            return Ok();
        }
    }
}