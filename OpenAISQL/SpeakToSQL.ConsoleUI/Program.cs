namespace SpeakToSQL.ConsoleUI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter your request: (type 'SQL' to see summary about DB structure, 'Exit' to quit the application)");

            HttpClient client = new()
            {
                BaseAddress = new Uri("https://localhost:7212")
            };

            Console.WriteLine("I am learning the structure of you db, give me a few moments, please...");
            HttpResponseMessage responseMessage = await client.GetAsync("SpeakToSQL/sqlsummary");
            if (!responseMessage.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to get SQL summary");
                return;
            }
            Console.WriteLine("I am ready. let's go!");

            string sqlSummary = await responseMessage.Content.ReadAsStringAsync();

            do
            {
                Console.Write("\nAsk me something about you DB -> ");
                string? prompt = Console.ReadLine();

                if (prompt == "SQL")
                {
                    Console.WriteLine($"DB Summary: {sqlSummary}");
                    continue;
                }

                if (prompt == "Exit")
                {
                    break;
                }

                if (string.IsNullOrEmpty(prompt))
                {
                    Console.WriteLine("Prompt cannot be empty");
                    continue;
                }

                var response = await client.GetAsync($"SpeakToSQL/texttosql?prompt={prompt}&dbstructure={sqlSummary}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to get SQL from text: {response.StatusCode} - {response.ReasonPhrase} ");
                    continue;
                }

                string generatedSQL = await response.Content.ReadAsStringAsync();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Generated SQL-> {generatedSQL}");
                Console.ResetColor();

                string result = await ExecuteSQL.GetResult(generatedSQL);

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Result:\n {result}");
                Console.ResetColor();
                Console.WriteLine();

            } while (true);
        }
    }
}