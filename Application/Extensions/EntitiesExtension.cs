namespace Application.Extensions
{
    public static class EntitiesExtension
    {
        public static void FillEntity<T,TDto>(this T targer, TDto sourse)
        {
            if (targer !=  null && sourse != null)
            {
                foreach (var prop in targer.GetType().GetProperties())
                    prop.SetValue(targer, sourse.GetValue(prop.Name));
            }
        }
        public static object? GetValue(this object? obj, string propName, object? defaultValue = null)
        {
            if(obj == null || string.IsNullOrEmpty(propName))
                return defaultValue;
            return GetValue(obj, propName.Split("."), defaultValue);
        }

        public static object? GetValue(this object? obj, string[] propChain, object? defaultValue = null)
        {
            if(obj == null || propChain.Length == 0)
                return defaultValue;
            foreach (var propName in propChain)
            {
                var type = obj.GetType();
                var prop = type.GetProperty(propName);
                if (prop != null && prop.CanRead)
                    obj = prop.GetValue(obj);
                else
                {
                    var fld = type.GetField(propName);
                    if(fld != null)
                        obj = fld.GetValue(obj);
                    else
                        return defaultValue;
                }
            }
            return obj;
        }
    }
}
