using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using FluentAssertions.Primitives;
using FluentAssertions.Types;
using LCore.Extensions;
using LCore.LDoc.Markdown;

namespace LCore.LUnit.Tests
    {
    /// <summary>
    /// Generates markdown for the LUnit project
    /// </summary>
    public class LUnitMarkdownGenerator : SolutionMarkdownGenerator_L
        {
        /// <summary>
        /// Override this member to specify the assemblies to generae documentation.
        /// </summary>
        public override Assembly[] DocumentAssemblies => new[] {Assembly.GetAssembly(typeof(LUnit))};

        public override void Home_Intro(GeneratedDocument MD)
            {
            }

        public override void HowToInstall(GeneratedDocument MD)
            {
            MD.Line($"Add {nameof(LCore.LUnit)} as a nuget package:");
            MD.Code(new[] {$"Install-Package {nameof(LCore.LUnit)}"});
            }

        public override List<ProjectInfo> Home_RelatedProjects
            => base.Home_RelatedProjects.Select(Project => Project.Name != nameof(LUnit));

        public override string RootUrl => LUnit.Urls.GitHubRepository_LUnit;

        /// <summary>
        /// Override this value to display a large image on top ofthe main document
        /// </summary>
        public override string BannerImage_Large(GeneratedDocument MD) =>
            MD.GetRelativePath($"{typeof(LUnit).GetAssembly().GetRootPath()}\\Content\\{nameof(LCore.LUnit)}-banner-large.png");

        /// <summary>
        /// Override this value to display a small banner image on top of sub-documents
        /// </summary>
        public override string BannerImage_Small(GeneratedDocument MD) =>
            MD.GetRelativePath($"{typeof(LUnit).GetAssembly().GetRootPath()}\\Content\\{nameof(LCore.LUnit)}-banner-small.png");

        /// <summary>
        /// Override this value to display a large image in the upper right corner of the main document
        /// </summary>
        public override string LogoImage_Large(GeneratedDocument MD) =>
            MD.GetRelativePath($"{typeof(LUnit).GetAssembly().GetRootPath()}\\Content\\{nameof(LCore.LUnit)}-logo-small.png");

        /// <summary>
        /// Override this value to display a small image in the upper right corner of sub-documents
        /// </summary>
        public override string LogoImage_Small(GeneratedDocument MD) =>
            MD.GetRelativePath($"{typeof(LUnit).GetAssembly().GetRootPath()}\\Content\\{nameof(LCore.LUnit)}-logo-small.png");

        public override Dictionary<Type, string> CustomTypeLinks => new Dictionary<Type, string>
            {
            [typeof(TypeAssertions)] = "https://github.com/dennisdoomen/fluentassertions/wiki#type-method-and-property-assertions",
            [typeof(AndConstraint<>)] = "https://github.com/dennisdoomen/fluentassertions/wiki#basic-assertions",
            [typeof(BooleanAssertions)] = "https://github.com/dennisdoomen/fluentassertions/wiki#booleans",
            [typeof(ObjectAssertions)] = "https://github.com/dennisdoomen/fluentassertions/wiki#basic-assertions",
            [typeof(ILUnitAttribute)] = "", // TODO once LCore is documented.
            [typeof(ITestResultAttribute)] = "" // TODO once LCore is documented.
            };
        }
    }