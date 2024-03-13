using Azure;
using Azure.AI.OpenAI;

namespace SpeakToSQL.API.Services
{
    /// <summary>
    /// Client for interacting with the Azure OpenAI API.
    /// </summary>
    public class AOAIClient
    {
        private readonly string _apiKey;
        private readonly string _apiUrl;
        private readonly string _deploymentName;

        public AOAIClient(string apiKey, string apiUrl, string deploymentName)
        {
            _apiKey = apiKey;
            _apiUrl = apiUrl;
            _deploymentName = deploymentName;
        }

        public string SummarizeSQLScript()
        {
            OpenAIClient client = new OpenAIClient(new Uri(_apiUrl), new AzureKeyCredential(_apiKey));

            using StreamReader streamReader = new StreamReader("Scripts/DBScript.sql");
            string sqlScript = streamReader.ReadToEnd();

            string prompt = PromptRepository.SummarizeSQLScript + sqlScript;

            ChatCompletionsOptions completionsOptions = new()
            {
                DeploymentName = _deploymentName,
            };

            completionsOptions.Messages.Add(new ChatRequestUserMessage(prompt));

            Response<ChatCompletions> response = client.GetChatCompletions(completionsOptions);

            return response.Value.Choices[0].Message.Content;
        }

        public string GetSQLFromText(string prompt, string dbstructure)
        {
            using StreamReader streamReader = new StreamReader("Prompts/FewShotsLearning.txt");
            string samples = streamReader.ReadToEnd();
            
            using StreamReader streamReader2 = new StreamReader("Prompts/SqlGenerationPrompt.txt");
            string sqlGenerationPrompt = streamReader2.ReadToEnd();

            OpenAIClient client = new OpenAIClient(new Uri(_apiUrl), new AzureKeyCredential(_apiKey));

            ChatCompletionsOptions completionsOptions = new()
            {
                DeploymentName = _deploymentName,
            };

            prompt = $"Translate the following text into SQL: {prompt} based on the following description of the db structure: {dbstructure}. Example: {samples}\n";
            prompt += sqlGenerationPrompt;

            completionsOptions.Messages.Add(new ChatRequestUserMessage(prompt));

            Response<ChatCompletions> response = client.GetChatCompletions(completionsOptions);

            return response.Value.Choices[0].Message.Content;
        }
    }
}