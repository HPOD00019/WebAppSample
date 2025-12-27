using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace ChessEngineService.SystemTests;

[TestFixture]
public class ChessEngineServiceSystemTests
{
    private WebApplicationFactory<Program> _factory = null!;
    private HttpClient _client = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
    }

    [Test]
    public async Task Root_Endpoint_Should_Return_Hello_World()
    {
        var response = await _client.GetAsync("/");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var content = await response.Content.ReadAsStringAsync();
        Assert.That(content, Is.EqualTo("Hello, World!"));
    }


    [Test]
    public async Task GetEvaluation_Endpoint_With_Valid_FEN_Should_Return_Evaluation()
    {
        var startFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        var url = $"/ChessEngine/GetEvaluation?fen={Uri.EscapeDataString(startFen)}&maxThinkTimeMs=1000";

        var response = await _client.GetAsync(url);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var content = await response.Content.ReadAsStringAsync();
        Assert.That(content, Is.Not.Null.And.Not.Empty);
        var json = JsonDocument.Parse(content);
        var root = json.RootElement;
        var isFen = root.TryGetProperty("fen", out var fenProperty);
        var isEval = root.TryGetProperty("evaluation", out var evalProperty);
        var eval = evalProperty.ToString().Split(' ')[0];
        var evaluationResult = Int32.Parse(eval);
        Assert.That(evaluationResult > 0 && evaluationResult < 300);


        Assert.That(isFen, Is.True);
        Assert.That(fenProperty.GetString(), Is.EqualTo(startFen));

        Assert.That(isEval, Is.True);
        var evaluation = evalProperty.GetString();

        Assert.That(evaluation, Is.Not.Null.And.Not.Empty);

        Assert.That(root.TryGetProperty("thinkTimeMs", out var timeProperty), Is.True);
        Assert.That(timeProperty.GetInt32(), Is.EqualTo(1000));
    }


    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _client?.Dispose();
        _factory?.Dispose();
    }
}