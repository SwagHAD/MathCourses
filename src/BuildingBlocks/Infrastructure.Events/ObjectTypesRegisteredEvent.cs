using SharedKernel.Enums;
using SharedKernel.Models;

namespace Infrastructure.Events
{
    /// <summary>Микросервис сообщает AuthService, на какие свои сущности выдаются доступы. Публикуется при каждом старте.</summary>
    public sealed record ObjectTypesRegisteredEvent
    {
        public ServiceType ServiceType { get; init; }
        public ObjectTypeDescriptor[] ObjectTypes { get; init; } = [];
    }
}
