using Microsoft.Extensions.Localization;

namespace API_020922.Misc
{
    public class LocalizerHelper<T> where T : class
    {
        public void Localize(IStringLocalizer localizer,IList<T> input)
        {

            var props = input.GetType().GetProperties();

            foreach(var prop in props)
            {
                try
                {
                    prop.SetValue(input, localizer[prop?.GetValue(input)?.ToString()]);
                }
                catch { }
            }
        }
    }
}
