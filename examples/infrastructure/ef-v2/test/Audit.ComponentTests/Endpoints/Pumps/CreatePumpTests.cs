using Microsoft.EntityFrameworkCore;

namespace Audit.ComponentTests.Endpoints.Pumps;

[TestFixture]
public class CreatePumpTests : ApiTestBase
{
    [Test]
    public async Task TestCreatePump()
    {
        await using var dbContext = WebApplicationSetUpFixture.CreateDbContext();
        var pumps = await dbContext.Pumps.ToListAsync();
    }
}