using SharedKernel.Enums;

namespace SharedKernel.AspNetCore.Options
{
    /// <summary>Какой микросервис запущен: от этого зависит префикс проверяемых доступов.</summary>
    public sealed class ServiceOptions
    {
        public ServiceType ServiceType { get; set; }
    }
}
