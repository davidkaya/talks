using Microsoft.SemanticKernel.ChatCompletion;

namespace AgenticDemo.Agents;

/// <summary>
/// Drafts and revises articles based on research notes and reviewer feedback.
/// </summary>
public class WriterAgent(IChatCompletionService chatService)
{
    private const string SystemPrompt = """
        You are a Writer Agent. Your job is to write clear, engaging, and well-structured
        short articles (300-500 words) based on research notes provided to you.
        Write in a professional but approachable tone suitable for a technical blog.
        Use headers, bullet points, and clear paragraphs.
        """;

    public async Task<string> WriteArticleAsync(string topic, string researchNotes)
    {
        var history = new ChatHistory(SystemPrompt);
        history.AddUserMessage(
            $"""
            Write a short article about "{topic}" based on these research notes:

            {researchNotes}
            """);

        var response = await chatService.GetChatMessageContentAsync(history);
        return response.Content ?? "No article generated.";
    }

    public async Task<string> ReviseArticleAsync(string originalDraft, string reviewerFeedback)
    {
        var history = new ChatHistory(SystemPrompt);
        history.AddUserMessage(
            $"""
            Revise the following article based on the reviewer's feedback.
            Keep what works, fix what doesn't, and incorporate the suggestions.

            ## Original Draft
            {originalDraft}

            ## Reviewer Feedback
            {reviewerFeedback}
            """);

        var response = await chatService.GetChatMessageContentAsync(history);
        return response.Content ?? "No revised article generated.";
    }
}
