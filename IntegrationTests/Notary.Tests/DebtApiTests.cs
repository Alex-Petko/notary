using Debt.Client;
using Microsoft.AspNetCore.Http;
using Shared.Tests;

namespace Notary.Tests;

public sealed class DebtApiTests : IAsyncDisposable
{

    private readonly DebtApiFactory _factory;

    public DebtApiTests()
    {
        var container = new ContainerFactory().CreateContainer("1234");
        container.StartAsync().Wait();
        _factory = new(container.Hostname, container.GetMappedPublicPort(5432), "1234");
    }

    [Theory, CustomAutoData]
    public async Task Lend_NotJwt_Status401Unauthorized(string debtor, int sum, DateTimeOffset begin)
    {
        // Arrange
        var client = Sut(false);
        var command = new CreateDebtCommandBody
        {
            Login = debtor,
            Sum = sum,
            Begin = begin
        };

        // Act
        // Assert
        var exception = await Assert.ThrowsAsync<ApiException>(() => client.LendDebtCommandAsync(command));

        Assert.Equal(StatusCodes.Status401Unauthorized, exception.StatusCode);
    }

    [Theory, CustomAutoData]
    public async Task Lend_Jwt_Status401Unauthorized(string debtor, int sum)
    {
        // Arrange
        var client = Sut();
        var command = new CreateDebtCommandBody
        {
            Login = debtor,
            Sum = sum,
            Begin = DateTimeOffset.UtcNow
        };

        // Act
        var debtId = await client.LendDebtCommandAsync(command);
        var debt = await client.GetDebtQueryAsync(debtId.ToString());
        // Assert


    }

    public ValueTask DisposeAsync()
    {
        return _factory.DisposeAsync();
    }

    private DebtsClient Sut(bool hasJwt = true)
    {
        var httpClient = _factory.CreateClient();

        if (hasJwt)
        {
            var token = new AccessControl.Application.Token("TestUser");
            var jwt = token.Sign("1234567890123456", DateTime.UtcNow.AddMinutes(5));
            httpClient.DefaultRequestHeaders.Add("Cookie", $"JwtBearer={jwt}");
        }

        return new DebtsClient(httpClient);
    }
}