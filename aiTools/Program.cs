

using Microsoft.Extensions.AI;





//create a minimal api to call openai api to fetch mcqs with correct answer and map it into a model to return with input token and output token 





var app = WebApplication.Create(args);

app.MapGet("/getMCQs", async (string topic, int numberOfQuestions) =>
{
    IChatClient client =
        new OpenAI.Chat.ChatClient("gpt-4.1-nano", Environment.GetEnvironmentVariable("OPENAI_API_KEY"))
        .AsIChatClient();
    var messages = new List<ChatMessage>
    {
        new ChatMessage(ChatRole.System, "You are a helpful assistant that generates multiple choice questions (MCQs) with correct answers."),
        new ChatMessage(ChatRole.User, $"Generate {numberOfQuestions} MCQs on the topic: {topic}.")
    };
     var responses = await client.GetResponseAsync<Mcqs>(messages);
    
    return Results.Ok(responses);
}).WithName("GetMCQs");
//https://localhost:7291/getmcqs?topic=french revolution&numberOfQuestions=4




app.Run();
    public class Mcqs
    {
        public required  List<McCQ> McQ { get; set; } 
   
        
   
    }
public class McCQ
{
    public required string question { get; set; }
    public required List<string> options { get; set; }
    public  required string correctAnswer { get; set; }
    public  required  string explanation { get; set; }

}