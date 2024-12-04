using ChatGPT.Net;

namespace OpenAIApi.Wrappers
{
    public class ChatGptWrapper : IChatGpt
    {
        private readonly ChatGpt _chatgpt;
        public ChatGptWrapper(ChatGpt chatgpt) 
        {
            _chatgpt = chatgpt;
        }

        public async Task<string> Ask(string question)
        {
            return await _chatgpt.Ask(question);
        }
    }
}
