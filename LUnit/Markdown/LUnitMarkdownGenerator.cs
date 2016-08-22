using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace LCore.LUnit.Markdown
    {
    /// <summary>
    /// Generates markdown for the LUnit project
    /// </summary>
    public class LUnitMarkdownGenerator : MarkdownGenerator
        {
        /// <summary>
        /// Override this member to specify the assemblies to generae documentation.
        /// </summary>
        protected override Assembly[] DocumentAssemblies => new[] { Assembly.GetAssembly(typeof(LUnit)) };

        protected override string HowToInstall_Text => $"Add {nameof(LUnit)} as a nuget package:";
        protected override string HowToInstall_Code => $"nuget install-package {nameof(LUnit)}";
        }
    }