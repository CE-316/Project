using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAssignmentSoftware
{
    public static class TemplateHelper
    {
        /// <summary>
        /// Replaces every {{key}} in the template with map[key].
        /// </summary>
        public static string FillTemplate(string template, IDictionary<string, string> map)
        {
            foreach (var kv in map)
            {
                // "{{key}}" → kv.Value
                template = template.Replace("{{" + kv.Key + "}}", kv.Value);
            }
            return template;
        }
    }

}
