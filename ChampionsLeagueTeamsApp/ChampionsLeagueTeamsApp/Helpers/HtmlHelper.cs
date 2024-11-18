using System.Text;

namespace ChampionsLeagueTeamsApp.Helpers
{
        public static class HtmlHelper
        {
            public static string EscapeHtml(string input)
            {
                if (string.IsNullOrEmpty(input)) return input;

                var builder = new StringBuilder(input.Length);

                foreach (var c in input)
                {
                    switch (c)
                    {
                        case '<': builder.Append("&lt;"); break;
                        case '>': builder.Append("&gt;"); break;
                        case '&': builder.Append("&amp;"); break;
                        case '"': builder.Append("&quot;"); break;
                        case '\'': builder.Append("&#39;"); break;
                        default: builder.Append(c); break;
                    }
                }

                return builder.ToString();
            }
        }
    
}
