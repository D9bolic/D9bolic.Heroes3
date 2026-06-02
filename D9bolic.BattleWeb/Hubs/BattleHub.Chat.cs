using Microsoft.AspNetCore.SignalR;

namespace D9bolic.BattleWeb.Hubs;

public partial class BattleHub
{
    public const int MaxChatMessageLength = 500;

    public const string ReceiveHubMessageMethod = "ReceiveHubMessage";
    public const string ReceiveDirectMessageMethod = "ReceiveDirectMessage";

    public async Task SendHub(string text)
    {
        var (sender, payload) = BuildChatPayload(text);
        await Clients.All.SendAsync(ReceiveHubMessageMethod, payload);
    }

    public async Task SendDirect(string targetUserId, string text)
    {
        if (string.IsNullOrWhiteSpace(targetUserId))
        {
            throw new HubException("targetUserId is required.");
        }

        var (_, payload) = BuildChatPayload(text);

        await Clients.User(targetUserId).SendAsync(ReceiveDirectMessageMethod, payload);
    }

    private (string SenderId, ChatMessage Payload) BuildChatPayload(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new HubException("Message text cannot be empty.");
        }

        if (text.Length > MaxChatMessageLength)
        {
            throw new HubException($"Message exceeds {MaxChatMessageLength}-character limit.");
        }

        var senderId = Context.UserIdentifier
            ?? throw new HubException("Sender identity unavailable.");

        return (senderId, new ChatMessage(senderId, text, DateTimeOffset.UtcNow));
    }

    public sealed record ChatMessage(string SenderId, string Text, DateTimeOffset SentAt);
}
