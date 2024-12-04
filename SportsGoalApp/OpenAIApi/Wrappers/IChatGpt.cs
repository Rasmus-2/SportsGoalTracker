
namespace OpenAIApi.Wrappers
{
    public interface IChatGpt
    {
        Task<string> Ask(string question);
    }
}