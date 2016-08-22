using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using LCore.Extensions;

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

        protected override string HowToInstall_Text(GitHubMarkdown MD) => $"Add {nameof(LUnit)} as a nuget package:";
        protected override string HowToInstall_Code(GitHubMarkdown MD) => $"nuget install-package {nameof(LUnit)}";

        protected override string BannerImage_Large(GitHubMarkdown MD) =>
            MD.Image(MD.GetRelativePath($"{typeof(LUnit).GetAssembly().GetRootPath()}\\Content\\LCore-banner-large.png"));

        protected override string BannerImage_Small(GitHubMarkdown MD) =>
            MD.Image(MD.GetRelativePath($"{typeof(LUnit).GetAssembly().GetRootPath()}\\Content\\LCore-banner-small.png"));
        }
    }