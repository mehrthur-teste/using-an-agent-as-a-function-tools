using System.ComponentModel;
using System;
using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OpenAI;
using Azure;

[Description("Get the weather for a given location.")]
static string GetWeather([Description("The location to get the weather for.")] string location)
    => $"The weather in {location} is ";

string endpoint = "";
string apiKey = "";

AIAgent weatherAgent = new AzureOpenAIClient(
    new Uri(endpoint),
    new AzureKeyCredential(apiKey))
    .GetChatClient("gpt-4.1-mini")  // Replace with your actual model deployment name
    .CreateAIAgent(
        instructions: "You answer questions about the weather.",
        name: "WeatherAgent",
        description: "An agent that answers questions about the weather.",
        tools: [AIFunctionFactory.Create(GetWeather)]);


AIAgent agent = new AzureOpenAIClient(
    new Uri(endpoint),
    new AzureKeyCredential(apiKey))
     .GetChatClient("gpt-4.1-mini")
     .CreateAIAgent(instructions: "You are a helpful assistant which answer if the season is.", tools: [weatherAgent.AsAIFunction()]);


Console.WriteLine(await agent.RunAsync("What is the season in Amsterdam now according that you know the weather? basing the date today is 2024-06-15."));

