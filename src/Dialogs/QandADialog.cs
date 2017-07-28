﻿using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using QnAMakerDialog;

namespace XamUBot.Dialogs
{
	/// <summary>
	/// Queries Q&A. Returns a boolean to indicate if something was found.
	/// </summary>
	[Serializable]
	[QnAMakerService("732de060-547d-430b-ad75-a781ede77355", "0646d377dab54c62b894ba9427ab0e7f")]
	public class QandADialog : QnAMakerDialog<object>
	{
		/// <summary>
		/// Creates a new instance of the dialog
		/// </summary>
		/// <param name="returnImmediately">use TRUE to make the dialog return to the calling dialog without waiting for any other messages</param>
        public QandADialog(bool returnImmediately = false)
        {
			_returnImmediately = returnImmediately;
		}

		bool _returnImmediately;
		bool _foundResult;

		protected override void WaitOrExit(IDialogContext context)
		{
			if (_returnImmediately)
			{
				context.Done(_foundResult);
			}
			else
			{
				base.WaitOrExit(context);
			}
		}

		public override async Task NoMatchHandler(IDialogContext context, string originalQueryText)
		{
			_foundResult = false;
			await context.PostAsync($"Sorry, I couldn't find an answer for '{originalQueryText}'.");
		}

        [QnAMakerResponseHandler(100)]
        public async Task HiScoreHandler(IDialogContext context, string originalQueryText, QnAMakerResult result)
        {
			_foundResult = true;
            await context.PostAsync($"{result.Answer}.");
		}

        [QnAMakerResponseHandler(75)]
        public async Task PrettyHighScoreHandler(IDialogContext context, string originalQueryText, QnAMakerResult result)
        {
			_foundResult = true;
			await context.PostAsync($"This seems to be what you are after.");
            await context.PostAsync($"{result.Answer}.");
		}

        [QnAMakerResponseHandler(50)]
		public async Task LowScoreHandler(IDialogContext context, string originalQueryText, QnAMakerResult result)
		{
			_foundResult = true;

			await context.PostAsync($"I'm not exactly sure, but this might help.");
            await context.PostAsync($"{result.Answer}.");
		}
	}
}