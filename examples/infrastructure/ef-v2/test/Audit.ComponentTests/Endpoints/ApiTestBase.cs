namespace Audit.ComponentTests.Endpoints;

public class ApiTestBase
{
    protected HttpClient _httpClient;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _httpClient = WebApplicationSetUpFixture.CreateHttpClient();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _httpClient.Dispose();
    }

    protected delegate Task<HttpResponseMessage> RequestDelegate(HttpClient httpClient);

    protected async Task<HttpResponseMessage> CallApi(RequestDelegate requestDelegate)
    {
        return await requestDelegate(_httpClient);
    }
}