using System.ComponentModel.DataAnnotations;

namespace StreamingService.Infrastructure.Options
{
    public sealed class MediaMtxOptions
    {
        public const string SectionName = "MediaMtx";

        /// <summary>
        /// Внутренний адрес control API MediaMTX (docker network),
        /// используется для create/delete path. Наружу не смотрит.
        /// </summary>
        [Required]
        public string ApiUrl { get; set; } = null!;

        /// <summary>
        /// Публичный адрес, по которому браузер преподавателя/студентов
        /// достучится до WHIP-паблишера и HLS-плеера.
        /// </summary>
        [Required]
        public string PublicUrl { get; set; } = null!;
    }
}
