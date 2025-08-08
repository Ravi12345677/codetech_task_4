using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.OpenAI;

public class EchoBot : ActivityHandler
{
    private readonly OpenAIClient _openAiClient;

    public EchoBot(OpenAIClient openAiClient)
    {
        _openAiClient = openAiClient;
    }

    protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
    {
        var userMessage = turnContext.Activity.Text;

        // Call Azure OpenAI for AI-powered response
        var completion = await _openAiClient.GetChatCompletionsAsync(
            deploymentOrModelName: "gpt-35-turbo",
            new ChatCompletionsOptions
            {
                Messages =
                {
                    new ChatMessage(ChatRole.System, "You are an AI assistant."),
                    new ChatMessage(ChatRole.User, userMessage)
                }
            });

        var aiReply = completion.Value.Choices[0].Message.Content;
        await turnContext.SendActivityAsync(MessageFactory.Text(aiReply), cancellationToken);
    }
}
