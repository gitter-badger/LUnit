using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using LCore.Extensions;
using LCore.LDoc.Markdown;

namespace LCore.LUnit.Tests
    {
    /// <summary>
    /// Generates markdown for the LUnit project
    /// </summary>
    public class LUnitMarkdownGenerator : MarkdownGenerator
        {
        /// <summary>
        /// Override this member to specify the assemblies to generae documentation.
        /// </summary>
        public override Assembly[] DocumentAssemblies => new[] { Assembly.GetAssembly(typeof(LUnit)) };

        public override void Home_Intro(GitHubMarkdown MD)
            {
            }

        public override void HowToInstall(GitHubMarkdown MD)
            {
            MD.Line($"Add {nameof(LCore.LUnit)} as a nuget package:");
            MD.Code(new[] { $"Install-Package {nameof(LCore.LUnit)}" });
            }

        /// <summary>
        /// Override this value to display a large image on top ofthe main document
        /// </summary>
        public override string BannerImage_Large(GitHubMarkdown MD) =>
            MD.GetRelativePath($"{typeof(LUnit).GetAssembly().GetRootPath()}\\Content\\{nameof(LCore.LUnit)}-banner-large.png");

        /// <summary>
        /// Override this value to display a small banner image on top of sub-documents
        /// </summary>
        public override string BannerImage_Small(GitHubMarkdown MD) =>
            MD.GetRelativePath($"{typeof(LUnit).GetAssembly().GetRootPath()}\\Content\\{nameof(LCore.LUnit)}-banner-small.png");

        /// <summary>
        /// Override this value to display a large image in the upper right corner of the main document
        /// </summary>
        public override string LogoImage_Large(GitHubMarkdown MD) =>
            MD.GetRelativePath($"{typeof(LUnit).GetAssembly().GetRootPath()}\\Content\\{nameof(LCore.LUnit)}-logo-small.png");

        /// <summary>
        /// Override this value to display a small image in the upper right corner of sub-documents
        /// </summary>
        public override string LogoImage_Small(GitHubMarkdown MD) =>
            MD.GetRelativePath($"{typeof(LUnit).GetAssembly().GetRootPath()}\\Content\\{nameof(LCore.LUnit)}-logo-small.png");
        }
    }