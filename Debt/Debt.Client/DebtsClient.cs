using Shared.Json;

namespace Debt.Client;

public partial class DebtsClient
{
    static partial void UpdateJsonSerializerSettings(System.Text.Json.JsonSerializerOptions settings)
    {
        settings.Converters.Add(new DateTimeOffsetConverter());
    }
}
