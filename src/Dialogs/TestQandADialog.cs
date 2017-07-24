﻿using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using XamUApi;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using QnAMakerDialog;

namespace XamUBot.Dialogs
{
	[Serializable]
	[QnAMakerService("b2b027cd86564a9b9fbaa1b14ca5f86f", "89c441bd-9b04-429e-a862-f1ea4ffa48b7")]
	public class TestQandADialog : QnAMakerDialog<object>
	{
		public override async Task NoMatchHandler(IDialogContext context, string originalQueryText)
		{
			await context.PostAsync($"Sorry, I couldn't find an answer for '{originalQueryText}'.");
			context.Wait(MessageReceived);
		}

		[QnAMakerResponseHandler(50)]
		public async Task LowScoreHandler(IDialogContext context, string originalQueryText, QnAMakerResult result)
		{
			await context.PostAsync($"I found an answer that might help...{result.Answer}.");
			context.Wait(MessageReceived);
		}

		public override async Task DefaultMatchHandler(IDialogContext context, string originalQueryText, QnAMakerResult result)
		{
			await base.DefaultMatchHandler(context, originalQueryText, result);
			//await context.PostAsync($"I found an answer that might help...{result.Answer}.");
			//context.Done(true);
		}
	}
}