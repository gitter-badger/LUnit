using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;
using LCore.Extensions;

namespace LCore.LUnit.Markdown
    {
    public abstract class MarkdownGenerator
        {
        public const string CSharpLanguage = "cs";

        protected abstract Assembly[] DocumentAssemblies { get; }

        #region Variables + 

        protected Dictionary<string, GitHubMarkdown> Markdown_Other { get; } = new Dictionary<string, GitHubMarkdown>();

        protected Dictionary<Assembly, GitHubMarkdown> Markdown_Assembly { get; } =
            new Dictionary<Assembly, GitHubMarkdown>();

        protected Dictionary<Type, GitHubMarkdown> Markdown_Type { get; } = new Dictionary<Type, GitHubMarkdown>();

        protected Dictionary<MemberInfo[], GitHubMarkdown> Markdown_Member { get; } =
            new Dictionary<MemberInfo[], GitHubMarkdown>();


        #endregion

        #region Generators + 
        // TODO Generate assembly markdown
        protected virtual GitHubMarkdown GenerateMarkdown(Assembly Assembly)
            {
            var MD = new GitHubMarkdown(this, this.MarkdownPath_Assembly(Assembly), Assembly.GetName().Name);

            MD.Header($"{Assembly.GetName().Name}", Size: 3);

            return MD;
            }

        // TODO Generate type markdown
        protected virtual GitHubMarkdown GenerateMarkdown(Type Type)
            {
            var MD = new GitHubMarkdown(this, this.MarkdownPath_Type(Type), Type.Name);

            MD.Header($"{Type.Name}", Size: 3);

            return MD;
            }

        protected virtual GitHubMarkdown GenerateMarkdown(MemberInfo[] MemberGroup)
            {
            var Member = MemberGroup.First();

            var MD = new GitHubMarkdown(this, this.MarkdownPath_Member(Member), Member.Name);
            MD.Header($"{Member.DeclaringType?.Name}", Size: 3);
            MD.Header(Member.Name);

            if (Member is MethodInfo)
                {
                var Method = (MethodInfo)Member;
                var Coverage = new MethodCoverage(Method);
                var Comments = Method.GetComments();

                string Static = Method.IsStatic
                    ? "Static "
                    : "Instance";

                string StaticLower = Method.IsStatic
                    ? " static"
                    : "";

                // TODO: Add return type link
                string ReturnType = Method.ReturnType == typeof(void)
                    ? "void"
                    : Method.ReturnType.FullyQualifiedName();


                // TODO: Add parameter type link
                string Parameters = Method.GetParameters().Convert(Param =>
                    $"{Param.ParameterType.GetGenericName()} {Param.Name}").Combine(", ");

                MD.Header($"{Static}Method", Size: 4);

                List<string> Badges = this.GetBadges(MD, Coverage, Comments);

                MD.Line("");
                MD.Line(Badges.JoinLines(" | "));
                MD.Line("");

                MD.Header($"public{StaticLower} {ReturnType} {Member.Name}({Parameters});", Size: 6);

                MD.Header("Summary", Size: 6);
                MD.Line(Comments?.Summary);

                if (Method.GetParameters().Length > 0)
                    {
                    MD.Header("Parameters", Size: 6);

                    var Table = new List<string[]>
                        {
                        new[] {"Parameter", "Optional", "Type", "Description"}
                        };

                    // TODO: Add parameter type link
                    MD.Line(MD.Highlight("__Add parameter type link__"));

                    Method.GetParameters().Each((ParamIndex, Param) =>
                        {
                            Table.Add(new[]
                                {
                            Param.Name,
                            Param.IsOptional
                                ? "Yes"
                                : "No",
                            Param.ParameterType.GetGenericName(),
                            Comments?.Parameters.GetAt(ParamIndex)?.Obj2
                            });
                        });

                    MD.Table(Table);
                    }

                MD.Header("Returns", Size: 4);

                // TODO: Add return type link
                MD.Line(MD.Highlight("__Add return type link__"));
                MD.Header(Method.ReturnType == typeof(void)
                    ? "void"
                    : Method.ReturnType.GetGenericName(), Size: 6);

                MD.Line(Comments?.Returns);

                if (Comments?.Examples.Length > 0)
                    {
                    MD.Header("Examples", Size: 4);
                    Comments.Examples.Each(Example => MD.Code(new[] { Example }));
                    }

                // TODO: Add source link
                MD.Line(MD.Highlight("source link"));
                // TODO: Add test coverage link
                MD.Line(MD.Highlight("coverage link"));
                // TODO: Add exception details
                MD.Line(MD.Highlight("exception comments"));
                // TODO: Add permission details
                MD.Line(MD.Highlight("permission comments"));
                // TODO: Add root link
                MD.Line(MD.Highlight("root link"));
                // TODO: Add footer [this markdown was auto-generated by LCore]
                MD.Line(MD.Highlight("footer"));
                }

            return MD;
            }

        // TODO Generate root markdown
        protected virtual GitHubMarkdown GenerateRootMarkdown()
            {
            var MD = new GitHubMarkdown(this, this.MarkdownPath_Root, this.MarkdownTitle_MainReadme);

            return MD;
            }

        // TODO Generate table of contents markdown
        protected virtual GitHubMarkdown GenerateTableOfContentsMarkdown()
            {
            var MD = new GitHubMarkdown(this, this.MarkdownPath_TableOfContents, this.MarkdownTitle_TableOfContents);

            this.Markdown_Other.Each(OtherMarkdown => { });
            this.Markdown_Assembly.Each(AssemblyMarkdown => { });
            this.Markdown_Type.Each(TypeMarkdown => { });
            this.Markdown_Member.Each(MemberMarkdown => { });

            return MD;
            }

        // TODO Generate summary markdown
        protected virtual GitHubMarkdown GenerateCoverageSummaryMarkdown()
            {
            var MD = new GitHubMarkdown(this, this.MarkdownPath_Root, this.MarkdownTitle_MainReadme);

            return MD;
            }

        protected virtual Dictionary<string, GitHubMarkdown> GetOtherDocuments()
            {
            return new Dictionary<string, GitHubMarkdown>();
            }

        protected virtual List<string> GetBadges([NotNull] GitHubMarkdown MD, [CanBeNull] MethodCoverage Coverage,
            [CanBeNull] Interfaces.ICodeComment Comments)
            {
            return new List<string>
                {
                MD.Badge("Documented", Comments != null ? "Yes" : "No",
                    Comments != null ? GitHubMarkdown.BadgeColor.BrightGreen : GitHubMarkdown.BadgeColor.Red),
                MD.Badge("Unit Tested", Coverage?.MemberTraitFound == true ? "Yes" : "No",
                    Coverage?.MemberTraitFound == true
                        ? GitHubMarkdown.BadgeColor.BrightGreen
                        : GitHubMarkdown.BadgeColor.Grey),
                MD.Badge("Attribute Tests", $"{Coverage?.AttributeCoverage ?? 0}",
                    (Coverage?.AttributeCoverage ?? 0) > 0u
                        ? GitHubMarkdown.BadgeColor.BrightGreen
                        : GitHubMarkdown.BadgeColor.Grey)
                // TODO: Count assertions by scanning test code '.should' 'assert'
                };
            }


        #endregion

        #region Options +


        public virtual string GeneratedMarkdownRoot => L.Ref.GetSolutionRootPath();

        protected virtual string MarkdownPath_RootFile => "README.md";
        public virtual string MarkdownPath_Root => $"{this.GeneratedMarkdownRoot}\\{this.MarkdownPath_RootFile}";

        protected virtual string MarkdownPath_TableOfContentsFile => "TableOfContents.md";

        public virtual string MarkdownPath_TableOfContents
            => $"{this.GeneratedMarkdownRoot}\\{this.MarkdownPath_TableOfContentsFile}";


        protected virtual string MarkdownPath_CoverageSummaryFile => "CoverageSummary.md";

        public virtual string MarkdownPath_CoverageSummary
            => $"{this.GeneratedMarkdownRoot}\\{this.MarkdownPath_CoverageSummaryFile}";


        protected virtual string MarkdownPath_Documentation => "docs";

        public virtual string MarkdownPath_Assembly(Assembly Assembly) =>
            $"{Assembly.GetRootPath()}\\{Assembly.GetName().Name.CleanFileName()}.md";

        public virtual string MarkdownPath_Type(Type Type) =>
            $"{Type.Assembly.GetRootPath()}\\{this.MarkdownPath_Documentation}\\" +
            $"{Type.Name.CleanFileName()}.md";

        public virtual string MarkdownPath_Member(MemberInfo Member) =>
            $"{Member.GetAssembly().GetRootPath()}\\{this.MarkdownPath_Documentation}\\" +
            $"{Member.DeclaringType?.Name.CleanFileName()}_{Member.Name.CleanFileName()}.md";


        protected virtual bool IncludeType(Type Type) =>
            !Type.HasAttribute<ExcludeFromCodeCoverageAttribute>(IncludeBaseClasses: true) &&
            !Type.HasAttribute<IExcludeFromMarkdownAttribute>();

        protected virtual bool IncludeMember(MemberInfo Member) =>
            !Member.HasAttribute<ExcludeFromCodeCoverageAttribute>(IncludeBaseClasses: true) &&
            !Member.HasAttribute<IExcludeFromMarkdownAttribute>() &&
            !(Member is MethodInfo && ((MethodInfo)Member).IsPropertyGetterOrSetter()) &&
            Member.IsDeclaredMember() &&
            !(Member is ConstructorInfo);

        protected virtual string MarkdownTitle_MainReadme => "Home";
        protected virtual string MarkdownTitle_TableOfContents => "Table of Contents";
        protected virtual string MarkdownTitle_CoverageSummary => "Coverage Summary";

        #endregion

        #region Private Methods +

        private void Load(Assembly Assembly)
            {
            this.Markdown_Assembly.Add(Assembly, this.GenerateMarkdown(Assembly));

            Assembly.GetExportedTypes().Select(this.IncludeType).Each(this.Load);
            }

        private void Load(Type Type)
            {
            this.Markdown_Type.Add(Type, this.GenerateMarkdown(Type));

            Dictionary<string, List<MemberInfo>> MemberNames = Type.GetMembers().Select(Member =>
            {
                return this.IncludeMember(Member);
            }).Group(Member => Member.Name);

            MemberNames.Values.Convert(EnumerableExt.Array).Each(this.Load);
            }

        private void Load(MemberInfo[] MemberGroup)
            {
            this.Markdown_Member.Add(MemberGroup, this.GenerateMarkdown(MemberGroup));
            }

        #endregion

        public void Generate(bool WriteToDisk = false)
            {
            // Generates all assembly, type, and member Markdown
            this.DocumentAssemblies.Each(this.Load);

            // Generate root markdown
            this.Markdown_Other.Add(this.MarkdownTitle_MainReadme, this.GenerateRootMarkdown());

            // Generate coverage summary markdown
            this.Markdown_Other.Add(this.MarkdownTitle_CoverageSummary, this.GenerateCoverageSummaryMarkdown());

            // Generate any custom markdown
            this.GetOtherDocuments().Each(Document => this.Markdown_Other.Add(Document.Key, Document.Value));

            // Lastly, generate table of contents
            this.Markdown_Other.Add(this.MarkdownTitle_TableOfContents, this.GenerateTableOfContentsMarkdown());

            List<GitHubMarkdown> AllMarkdown = this.GetAllMarkdown();

            if (WriteToDisk)
                {
                AllMarkdown.Each(MD =>
                {
                    string Path = MD.FilePath;

                    // just to be safe
                    if (Path.EndsWith(".md"))
                        {
                        Path.EnsurePathExists();

                        File.WriteAllLines(Path, MD.GetMarkdownLines().Array());
                        }
                });
                }
            }

        public List<GitHubMarkdown> GetAllMarkdown()
            {
            var AllMarkdown = new List<GitHubMarkdown>();

            AllMarkdown.AddRange(this.Markdown_Other.Values);
            AllMarkdown.AddRange(this.Markdown_Assembly.Values);
            AllMarkdown.AddRange(this.Markdown_Type.Values);
            AllMarkdown.AddRange(this.Markdown_Member.Values);

            return AllMarkdown;
            }
        }
    }