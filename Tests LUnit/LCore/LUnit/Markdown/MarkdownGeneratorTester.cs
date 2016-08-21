using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using LCore.Extensions;
using LCore.LUnit;
using LCore.LUnit.Markdown;
using Xunit;
using Xunit.Abstractions;

namespace LUnit_Tests.LCore.LUnit.Markdown
    {
    /*
    Covering class: LCore.LUnit.Markdown.MarkdownGenerator
    */
    public partial class MarkdownGeneratorTester : XUnitOutputTester, IDisposable
        {
        public MarkdownGeneratorTester([NotNull] ITestOutputHelper Output) : base(Output) { }

        public void Dispose() { }

        [Fact]
        [Trait(Traits.TargetMember, nameof(LCore) + "." + nameof(global::LCore.LUnit) + "." + nameof(global::LCore.LUnit.Markdown) + "." + nameof(MarkdownGenerator) + "." + nameof(MarkdownGenerator.Generate) + "(Boolean)")]
        public void GenerateMarkdown_LUnit()
            {
            var Gen = new LUnitMarkdownGenerator();

            Gen.Generate(WriteToDisk: true);

            List<GitHubMarkdown> Markdown = Gen.GetAllMarkdown();

            List<string> Paths = Markdown.Convert(MD => MD?.FilePath);

            Paths.Each(this._Output.WriteLine);
            }

        }
    }
