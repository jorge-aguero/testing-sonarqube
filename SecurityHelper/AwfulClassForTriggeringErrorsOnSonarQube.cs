using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityHelper
{
    internal class AWfulClassForTriggeringErrorsOnSonarQube
    {
        public void UnusedMethod()
        {

        }

        public bool AWFULMethodInsideAwfulClass(string param1, dynamic param2, dynamic param3, dynamic param4,
            dynamic PARAM5, dynamic P4ram6, dynamic p_a_r_a_m_7, dynamic param8)
        {
            var LANGUAGES = new List<string>()
            {
                "c#",
                "javascript",
                "java",
                "python",
                "go",
                "html" //XD
            };

            string sa = null;
            foreach (var language in LANGUAGES)
            {
                foreach (var param in param1)
                {
                    foreach (var character in param2.something)
                    {
                        foreach (var s in param3.another)
                        {
                            sa += character + param + s;
                        }
                    }
                }
            }

            foreach(var language in LANGUAGES)
            {
                foreach (var param in param1)
                {
                    foreach (var character in param2.something)
                    {
                        foreach (var s in param3.another)
                        {
                            sa += character + param + s;
                        }
                    }
                }
            }

            foreach(var language in LANGUAGES)
            {
                foreach (var param in param1)
                {
                    foreach (var character in param2.something)
                    {
                        foreach (var s in param3.another)
                        {
                            sa += character + param + s;
                        }
                    }
                }
            }

            return sa == null ? true : sa != null ? true : false;
        }
    }
}
