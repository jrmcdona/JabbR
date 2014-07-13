﻿using JabbR.Models;
using Microsoft.AspNet.SignalR;
using System;

namespace JabbR.Commands
{
    [Command("lmbtfy", "Lmbtfy_CommandInfo", "query", "user")]
    public class LmbtfyCommand : UserCommand
    {
        public override void Execute(CommandContext context, CallerContext callerContext, Models.ChatUser callingUser, string[] args)
        {
            if (String.IsNullOrEmpty(callerContext.RoomName))
            {
                throw new HubException(LanguageResources.InvokeFromRoomRequired);
            }

            if (args == null || args.Length < 1)
            {
                throw new HubException(LanguageResources.Lmbtfy_DataRequired);
            }

            ChatRoom callingRoom = context.Repository.GetRoomByName(callerContext.RoomName);

            string query = string.Join(" ", args);
            string queryEscaped = Uri.EscapeDataString(query);
            string url = String.Format("http://lmbtfy.com/?q={0}", queryEscaped);

            context.NotificationService.PostNotification(callingRoom, callingUser, url);
        }
    }
}