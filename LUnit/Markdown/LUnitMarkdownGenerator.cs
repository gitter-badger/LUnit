using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace LCore.LUnit.Markdown
    {
    public class LUnitMarkdownGenerator : MarkdownGenerator
        {
        protected override Assembly[] DocumentAssemblies => new[] { Assembly.GetAssembly(typeof(LUnit)) };
        }
    }