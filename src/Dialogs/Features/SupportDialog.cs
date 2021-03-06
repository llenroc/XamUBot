﻿using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace XamUBot.Dialogs
{
    /// <summary>
    /// Provides support.
    /// </summary>
    [Serializable]
    public class SupportDialog : BaseDialog
    {
        protected async override Task OnInitializeAsync(IDialogContext context)
        {
            await context.PostAsync(ResponseUtterances.GetResponse(ResponseUtterances.ReplyTypes.SupportWelcome));
            WaitForNextMessage(context);
        }

        protected async override Task OnHelpReceivedAsync(IDialogContext context, Activity activity)
        {
            await context.PostAsync(ResponseUtterances.GetResponse(ResponseUtterances.ReplyTypes.SupportHelp));
            WaitForNextMessage(context);
        }

        protected override Task OnMessageReceivedAsync(IDialogContext context, Activity activity)
        {
            WaitForNextMessage(context);
            return Task.CompletedTask;
        }
    }
}