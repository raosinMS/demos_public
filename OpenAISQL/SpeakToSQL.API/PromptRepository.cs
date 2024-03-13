namespace SpeakToSQL.API
{
    public static class PromptRepository
    {
        /// <summary>
        /// Returns a prompt for summarizing a SQL script.
        /// </summary>
        public static string SummarizeSQLScript
        {
            get
            {
                // in SummarizeSQLScript.txt the following teqniques of prompt engineering are used:
                // 1. Instructive Prompting
                // 2. Few-Shot Learning with Examples 
                // 3. Specificity in Prompting
                using StreamReader reader = new StreamReader("Prompts/SummarizeSQL.txt");
                return reader.ReadToEnd();
            }
        }
    }
}
