namespace StreamingService.Contracts
{
    /// <summary>Маршруты StreamingService: контроллеры используют их в атрибутах, клиент — для URL.</summary>
    public static class StreamingRoutes
    {
        public const string Prefix = "api/streams";

        public const string Start = "{lessonId:int}/start";
        public const string Stop = "{lessonId:int}/stop";

        public static string StartUrl(int lessonId) => $"{Prefix}/{lessonId}/start";
        public static string StopUrl(int lessonId) => $"{Prefix}/{lessonId}/stop";
    }
}
